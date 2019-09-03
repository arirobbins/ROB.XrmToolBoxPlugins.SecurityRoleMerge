using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;

namespace ROB.XrmToolBoxPlugins.SecurityRoleMerge.Tool
{
    public partial class MyPluginControl : PluginControlBase
    {
        public string SecurityRoleTextBoxDefaultText = "Enter New Security Role Name Here";
        private Settings mySettings;
        public List<RoleOptions> AvailableRoles { get; set; }
        public Guid Role { get; set; }

        public MyPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));
            
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            //adds a default value to the existing role drop down
            toolStripComboBox_existingRoles.Items.Add(new ComboboxItem() { Text = "Or Choose An Existing Role", Value = 0 });
        }

        

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void GetRoles()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting roles",
                Work = (worker, args) =>
                {
                    args.Result = Methods.GetRolesForMerge(Service);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as List<RoleOptions>;
                    if (result != null)
                    {
                        foreach (var role in result)
                        {
                            roleList.Items.Add(role);
                            toolStripComboBox_existingRoles.Items.Add(new ComboboxItem() { Text = role.Name, Value = role.ID });
                        }
                    }
                }
            });
        }

        private void CreateRole()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating Role",
                Work = (worker, args) =>
                {
                    args.Result = Methods.CreateRole(Service, toolStripTextBox_securityRoleName.Text);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //if no errors, here's where we should merge
                    var result = args.Result;
                    if ((Guid)result != Guid.Empty)
                    {
                        Role = (Guid)result;
                        ExecuteMethod(MergeRoles);
                    }
                    
                }
            });
        }

        private void MergeRoles()
        {
            var results = roleList.CheckedItems;
            var selectedRoles = new List<RoleOptions>();
            foreach (var result in results)
            {
                selectedRoles.Add(new RoleOptions(results.IndexOf(result) + 1, ((RoleOptions)result).Name, ((RoleOptions)result).ID));
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Merging Roles",
                Work = (worker, args) =>
                {
                    Methods.Merge(Service, Role, selectedRoles);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    MessageBox.Show("Merge Complete!");
                    toolStripTextBox_securityRoleName.Text = SecurityRoleTextBoxDefaultText;
                    roleList.Items.Clear();
                    ExecuteMethod(GetRoles);
                }
            });
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            roleList.Items.Clear();
            toolStripTextBox_securityRoleName.Text = SecurityRoleTextBoxDefaultText;

            base.UpdateConnection(newService, detail, actionName, parameter);
            
            //mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
        }

        private void toolStripTextBox_securityRoleName_Enter(object sender, EventArgs e)
        {
            if (toolStripTextBox_securityRoleName.Text == SecurityRoleTextBoxDefaultText)
            {
                toolStripTextBox_securityRoleName.Clear();
            }            
        }

        private void toolStripTextBox_securityRoleName_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(toolStripTextBox_securityRoleName.Text))
            {
                toolStripTextBox_securityRoleName.Text = SecurityRoleTextBoxDefaultText;
                toolStripComboBox_existingRoles.Enabled = true;
            }
            else {
                toolStripComboBox_existingRoles.SelectedIndex = 0;
                toolStripComboBox_existingRoles.Enabled = false;                
            }
        }

        private void toolStripButton_getRoles_Click(object sender, EventArgs e)
        {
            roleList.Items.Clear();
            ExecuteMethod(GetRoles);
        }

        private void toolStripButton_merge_Click(object sender, EventArgs e)
        {
            if ((String.IsNullOrEmpty(toolStripTextBox_securityRoleName.Text) || toolStripTextBox_securityRoleName.Text == SecurityRoleTextBoxDefaultText) && toolStripComboBox_existingRoles.SelectedIndex == 0)
            {
                MessageBox.Show("You must enter a name for your new security role or chose and existing one!");
            }
            else
            {
                if (roleList.CheckedItems.Count > 0)
                {
                    if (Role != Guid.Empty)
                    {
                        ExecuteMethod(MergeRoles);
                    }
                    else
                    {
                        ExecuteMethod(CreateRole);
                    }
                }
                else
                {
                    MessageBox.Show("You have not selected any roles!");
                }
            }
        }

        private void linkLabel_showInstructions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox_instructions.Visible = !textBox_instructions.Visible;
            linkLabel_showInstructions.Text = textBox_instructions.Visible ? "Hide Instructions" : "Show Instructions";
        }

        private void toolStripComboBox_existingRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox_existingRoles.SelectedIndex == 0)
            {
                toolStripTextBox_securityRoleName.Enabled = true;
                Role = Guid.Empty;
            }
            else {
                toolStripTextBox_securityRoleName.Enabled = false;
                Role = new Guid((toolStripComboBox_existingRoles.SelectedItem as ComboboxItem).Value.ToString());
            }
        }
    }
}