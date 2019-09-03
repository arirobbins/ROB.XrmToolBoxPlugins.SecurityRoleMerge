using System;
using System.Collections;

namespace ROB.XrmToolBoxPlugins.SecurityRoleMerge.Tool
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_getRoles = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox_securityRoleName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox_existingRoles = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton_merge = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.roleList = new System.Windows.Forms.CheckedListBox();
            this.linkLabel_showInstructions = new System.Windows.Forms.LinkLabel();
            this.textBox_instructions = new System.Windows.Forms.TextBox();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.toolStripButton_getRoles,
            this.toolStripSeparator1,
            this.toolStripTextBox_securityRoleName,
            this.toolStripComboBox_existingRoles,
            this.toolStripButton_merge});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(794, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(56, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_getRoles
            // 
            this.toolStripButton_getRoles.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_getRoles.Image")));
            this.toolStripButton_getRoles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_getRoles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_getRoles.Name = "toolStripButton_getRoles";
            this.toolStripButton_getRoles.RightToLeftAutoMirrorImage = true;
            this.toolStripButton_getRoles.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton_getRoles.Text = "Get Roles";
            this.toolStripButton_getRoles.ToolTipText = "Get Roles";
            this.toolStripButton_getRoles.Click += new System.EventHandler(this.toolStripButton_getRoles_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox_securityRoleName
            // 
            this.toolStripTextBox_securityRoleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox_securityRoleName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_securityRoleName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripTextBox_securityRoleName.Name = "toolStripTextBox_securityRoleName";
            this.toolStripTextBox_securityRoleName.Size = new System.Drawing.Size(200, 25);
            this.toolStripTextBox_securityRoleName.Text = "Enter New Security Role Name Here";
            this.toolStripTextBox_securityRoleName.Enter += new System.EventHandler(this.toolStripTextBox_securityRoleName_Enter);
            this.toolStripTextBox_securityRoleName.Leave += new System.EventHandler(this.toolStripTextBox_securityRoleName_Leave);
            // 
            // toolStripComboBox_existingRoles
            // 
            this.toolStripComboBox_existingRoles.Name = "toolStripComboBox_existingRoles";
            this.toolStripComboBox_existingRoles.Size = new System.Drawing.Size(200, 25);
            this.toolStripComboBox_existingRoles.Text = "Or Choose An Existing Role";
            this.toolStripComboBox_existingRoles.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_existingRoles_SelectedIndexChanged);
            // 
            // toolStripButton_merge
            // 
            this.toolStripButton_merge.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_merge.Image")));
            this.toolStripButton_merge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_merge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_merge.Name = "toolStripButton_merge";
            this.toolStripButton_merge.Size = new System.Drawing.Size(61, 22);
            this.toolStripButton_merge.Text = "Merge";
            this.toolStripButton_merge.Click += new System.EventHandler(this.toolStripButton_merge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select Roles to Merge";
            // 
            // roleList
            // 
            this.roleList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.roleList.CheckOnClick = true;
            this.roleList.DisplayMember = "Name";
            this.roleList.FormattingEnabled = true;
            this.roleList.Location = new System.Drawing.Point(6, 56);
            this.roleList.Name = "roleList";
            this.roleList.Size = new System.Drawing.Size(350, 409);
            this.roleList.TabIndex = 12;
            this.roleList.ValueMember = "Value";
            // 
            // linkLabel_showInstructions
            // 
            this.linkLabel_showInstructions.AutoSize = true;
            this.linkLabel_showInstructions.Location = new System.Drawing.Point(265, 34);
            this.linkLabel_showInstructions.Name = "linkLabel_showInstructions";
            this.linkLabel_showInstructions.Size = new System.Drawing.Size(91, 13);
            this.linkLabel_showInstructions.TabIndex = 14;
            this.linkLabel_showInstructions.TabStop = true;
            this.linkLabel_showInstructions.Text = "Show Instructions";
            this.linkLabel_showInstructions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_showInstructions_LinkClicked);
            // 
            // textBox_instructions
            // 
            this.textBox_instructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_instructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_instructions.Location = new System.Drawing.Point(362, 56);
            this.textBox_instructions.Multiline = true;
            this.textBox_instructions.Name = "textBox_instructions";
            this.textBox_instructions.ReadOnly = true;
            this.textBox_instructions.Size = new System.Drawing.Size(415, 409);
            this.textBox_instructions.TabIndex = 15;
            this.textBox_instructions.Text = resources.GetString("textBox_instructions.Text");
            this.textBox_instructions.Visible = false;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox_instructions);
            this.Controls.Add(this.linkLabel_showInstructions);
            this.Controls.Add(this.roleList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(794, 475);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox roleList;
        private System.Windows.Forms.ToolStripButton toolStripButton_getRoles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_merge;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_securityRoleName;
        private System.Windows.Forms.LinkLabel linkLabel_showInstructions;
        private System.Windows.Forms.TextBox textBox_instructions;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_existingRoles;
    }
}
