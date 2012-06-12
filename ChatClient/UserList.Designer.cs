namespace ChatClient
{
    partial class UserListWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvUserList = new System.Windows.Forms.ListView();
            this.Names = new System.Windows.Forms.ColumnHeader();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvUserList
            // 
            this.lvUserList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Names});
            this.lvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUserList.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvUserList.FullRowSelect = true;
            this.lvUserList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvUserList.Location = new System.Drawing.Point(3, 53);
            this.lvUserList.MultiSelect = false;
            this.lvUserList.Name = "lvUserList";
            this.lvUserList.Size = new System.Drawing.Size(269, 334);
            this.lvUserList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUserList.TabIndex = 0;
            this.lvUserList.TileSize = new System.Drawing.Size(260, 34);
            this.lvUserList.UseCompatibleStateImageBehavior = false;
            this.lvUserList.View = System.Windows.Forms.View.Tile;
            this.lvUserList.ItemActivate += new System.EventHandler(this.lvUserList_ItemActivate);
            this.lvUserList.SelectedIndexChanged += new System.EventHandler(this.lvUserList_SelectedIndexChanged);
            // 
            // Names
            // 
            this.Names.Width = 270;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 2000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lvUserList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(275, 390);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 44);
            this.panel1.TabIndex = 1;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(10, 10);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(61, 22);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "lblUser";
            // 
            // UserListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 390);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserListWindow";
            this.Text = "Users";
            this.Load += new System.EventHandler(this.UserList_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserListWindow_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvUserList;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ColumnHeader Names;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUser;

    }
}