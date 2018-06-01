using System;

namespace ROB.XrmToolBoxPlugins.SecurityRoleMerge
{
    public class SecurityRolePrivileges
    {
        public Guid RolePrivilegeId { get; set; }
        public Guid PrivilegeId { get; set; }
        public Guid RoleId { get; set; }
        public int PrivilegeDepthMask { get; set; }
        public string Name { get; set; }
    }
}