namespace Sandstone_Launcher
{
    partial class JavaList
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
            this.list = new System.Windows.Forms.ListBox();
            this.select = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.BackColor = System.Drawing.Color.DimGray;
            this.list.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list.DisplayMember = "Name";
            this.list.ForeColor = System.Drawing.Color.White;
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(5, 5);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(245, 119);
            this.list.TabIndex = 0;
            // 
            // select
            // 
            this.select.BackColor = System.Drawing.Color.DimGray;
            this.select.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select.Location = new System.Drawing.Point(130, 130);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(120, 25);
            this.select.TabIndex = 1;
            this.select.Text = "Select";
            this.select.UseVisualStyleBackColor = false;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.DimGray;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Location = new System.Drawing.Point(5, 130);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(120, 25);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = false;
            // 
            // JavaList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(255, 160);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.select);
            this.Controls.Add(this.list);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JavaList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Java List";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button cancel;
        public System.Windows.Forms.ListBox list;
    }
}