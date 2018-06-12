using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace ROB.XrmToolBoxPlugins.SecurityRoleMerge
{

    class Methods
    {
        private readonly ILogger _logger;

        public Methods(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(string action, string reason)
        {
            _logger.LogInfo($"{action}: {reason}");
        }
        public static EntityCollection GetRootBusinessUnit(IOrganizationService svc)
        {
            var buQuery = new QueryExpression("businessunit");
            buQuery.ColumnSet = new ColumnSet("businessunitid", "parentbusinessunitid");
            var filter = new FilterExpression();
            filter.AddCondition("parentbusinessunitid", ConditionOperator.Null);
            buQuery.Criteria = filter;

            return svc.RetrieveMultiple(buQuery);
        }

        public static EntityCollection GetRoles(IOrganizationService svc)
        {
            var roleQuery = new QueryExpression("role");
            roleQuery.ColumnSet = new ColumnSet("name", "businessunitid", "roleid");
            var filter = new FilterExpression();
            var rootBU = GetRootBusinessUnit(svc).Entities;
            filter.AddCondition("businessunitid", ConditionOperator.Equal, rootBU[0].Id);
            roleQuery.AddOrder("name", OrderType.Ascending);
            roleQuery.Criteria = filter;

            return svc.RetrieveMultiple(roleQuery);
        }

        public static List<SecurityRolePrivileges> GetRolePrivileges(IOrganizationService svc, Guid roleid)
        {
            var query = new QueryExpression("roleprivileges");
            query.ColumnSet = new ColumnSet("roleprivilegeid", "privilegeid", "roleid", "privilegedepthmask");
            query.LinkEntities.Add(new LinkEntity("roleprivileges", "privilege", "privilegeid", "privilegeid", JoinOperator.Inner));
            query.LinkEntities[0].Columns.AddColumn("name");
            query.LinkEntities[0].EntityAlias = "priv";
            var filter = new FilterExpression();
            filter.AddCondition("roleid", ConditionOperator.Equal, roleid);
            query.Criteria = filter;

            DataCollection<Entity> rolePrivileges = svc.RetrieveMultiple(query).Entities;

            var listOfPrivs = new List<SecurityRolePrivileges>();

            foreach (var priv in rolePrivileges)
            {
                var securityRolePrivileges = new SecurityRolePrivileges();
                securityRolePrivileges.RoleId = priv.GetAttributeValue<Guid>("roleid");
                securityRolePrivileges.PrivilegeId = priv.GetAttributeValue<Guid>("privilegeid");
                securityRolePrivileges.RolePrivilegeId = priv.GetAttributeValue<Guid>("roleprivilegeid");
                securityRolePrivileges.PrivilegeDepthMask = priv.GetAttributeValue<int>("privilegedepthmask");
                securityRolePrivileges.Name = ((AliasedValue)priv["priv.name"]).Value.ToString();

                listOfPrivs.Add(securityRolePrivileges);
            }

            return listOfPrivs;
        }
        public static EntityCollection GetPrivileges(IOrganizationService svc)
        {
            var privilegeQuery = new QueryExpression("privilege");
            privilegeQuery.ColumnSet = new ColumnSet("name", "privilegeid", "accessright", "canbeentityreference");
            //privilegeQuery.ColumnSet = new ColumnSet(true);
            var order = new OrderExpression("accessright", OrderType.Ascending);
            var filter = new FilterExpression();
            filter.AddCondition("canbeentityreference", ConditionOperator.Equal, true);
            privilegeQuery.Orders.Add(order);
            privilegeQuery.Criteria = filter;

            return svc.RetrieveMultiple(privilegeQuery);
        }

        public static Enum GetPrivDepthMask(int privilege)
        {
            switch (privilege)
            {
                case 8://Global - Organization
                    return PrivilegeDepth.Global;
                case 4://Deep - Parent: Child Business Units
                    return PrivilegeDepth.Deep;
                case 2://Local - Business Unit
                    return PrivilegeDepth.Local;
                case 1://Basic - User
                    return PrivilegeDepth.Basic;
                default:
                    return null;
            }
        }
        public static int SetPrivDepthMask(PrivilegeDepth privilege)
        {
            switch (privilege)
            {
                case PrivilegeDepth.Global://Global - Organization
                    return 8;
                case PrivilegeDepth.Deep://Deep - Parent: Child Business Units
                    return 4;
                case PrivilegeDepth.Local://Local - Business Unit
                    return 2;
                case PrivilegeDepth.Basic://Basic - User
                    return 1;
                default:
                    return 0;
            }
        }

        public static Guid CreateRole(IOrganizationService svc, string name)
        {
            var roles = GetRolesForMerge(svc);

            var isMatch = roles.Find(x => x.Name.ToLower() == name.ToLower());

            if (isMatch == null)
            {
                var roleEntity = new Entity("role");
                roleEntity.Attributes["name"] = name;
                var rootBU = GetRootBusinessUnit(svc).Entities;
                roleEntity.Attributes["businessunitid"] = new EntityReference("businessunit", rootBU[0].Id);
                Guid roleid = svc.Create(roleEntity); //need to understand this better...
                ColumnSet attributes = new ColumnSet(new string[] { "name", "roleid" });
                roleEntity = svc.Retrieve(roleEntity.LogicalName, roleid, attributes);
                return roleEntity.Id;
            }

            MessageBox.Show("You must provide a unique name for your security role!");
            return Guid.Empty;
        }

        public static void CreateRolePrivileges(IOrganizationService svc, Guid roleid, Guid privilegeid, int privilegeDepthMask)
        {

            var rolePrivilege = new RolePrivilege(privilegeDepthMask, privilegeid);
            RolePrivilege[] rolePrivileges = { rolePrivilege };
            AddPrivilegesRoleRequest addPrivs = new AddPrivilegesRoleRequest();
            addPrivs.Privileges = rolePrivileges;
            addPrivs.RoleId = roleid;
            svc.Execute(addPrivs);

        }

        public static void CreateRolePrivilegesInBulk(IOrganizationService svc, Guid roleid, List<RolePrivilege> rolePrivileges)
        {
            AddPrivilegesRoleRequest addPrivs = new AddPrivilegesRoleRequest();
            addPrivs.Privileges = rolePrivileges.ToArray();
            addPrivs.RoleId = roleid;
            svc.Execute(addPrivs);
        }

        public static List<RoleOptions> GetRolesForMerge(IOrganizationService svc)
        {
            var roles = GetRoles(svc).Entities;
            var availableRoles = new List<RoleOptions>();

            foreach (var role in roles)
            {
                var newRoleOption = new RoleOptions(availableRoles.Count() + 1, role.Attributes["name"].ToString(), role.Id);
                availableRoles.Add(newRoleOption);
            }

            return availableRoles;
        }

        public static void Merge(IOrganizationService svc, Guid role, List<RoleOptions> selectedRoles)
        {
            List<RolePrivilege> rolePrivileges = new List<RolePrivilege>();

                for (int i = 0; i < selectedRoles.Count; i++)
                {
                    var selectedRole = GetRolePrivileges(svc, selectedRoles[i].ID);

                    foreach (var priv in selectedRole)
                    {
                        var match = rolePrivileges.Find(x => x.PrivilegeId == priv.PrivilegeId);

                        if (match != null)
                        {
                            if (priv.PrivilegeDepthMask > SetPrivDepthMask(match.Depth))
                            {
                                rolePrivileges[rolePrivileges.IndexOf(match)] = new RolePrivilege(Convert.ToInt32(GetPrivDepthMask(priv.PrivilegeDepthMask)), priv.PrivilegeId);
                                //logger.Log("Create", $"since {priv.Name} ({priv.PrivilegeDepthMask}) in {selectedRoles[i].Name} > {match.PrivilegeId} ({Methods.SetPrivDepthMask(match.Depth)}) in new Role.");
                            }
                            else
                            {
                                //logger.Log("Skip", $"since {selectedRoles[i].Name} {priv.Name} ({priv.PrivilegeDepthMask}) < OR == {match.PrivilegeId} ({Methods.SetPrivDepthMask(match.Depth)})");
                            }
                        }
                        else
                        {
                            rolePrivileges.Add(new RolePrivilege(Convert.ToInt32(GetPrivDepthMask(priv.PrivilegeDepthMask)), priv.PrivilegeId));
                            //logger.Log("Create", $"since {priv.Name} did not exist in new role.");
                        }
                    }
                }

                CreateRolePrivilegesInBulk(svc, role, rolePrivileges);
        }

        public static void DeleteRole(IOrganizationService svc, List<RoleOptions> rolesForDelete)
        {
            foreach (var role in rolesForDelete)
            {
                var target = new EntityReference("role", role.ID);
                var deleteRequest = new DeleteRequest();
                deleteRequest.Target = target;
                svc.Execute(deleteRequest);
            }
        }
    }
}
