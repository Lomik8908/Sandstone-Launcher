namespace Sandstone_Launcher
{
    partial class SkinSelector
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
            this.SkinPreview = new System.Windows.Forms.PictureBox();
            this.account_box = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ElyCapePreview = new System.Windows.Forms.PictureBox();
            this.cape_box = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.from_file = new System.Windows.Forms.Button();
            this.from_username = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.SkinPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElyCapePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // SkinPreview
            // 
            this.SkinPreview.Location = new System.Drawing.Point(10, 35);
            this.SkinPreview.Name = "SkinPreview";
            this.SkinPreview.Size = new System.Drawing.Size(180, 228);
            this.SkinPreview.TabIndex = 0;
            this.SkinPreview.TabStop = false;
            this.SkinPreview.MouseEnter += new System.EventHandler(this.SkinPreview_MouseEnter);
            this.SkinPreview.MouseLeave += new System.EventHandler(this.SkinPreview_MouseLeave);
            // 
            // account_box
            // 
            this.account_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account_box.FormattingEnabled = true;
            this.account_box.Location = new System.Drawing.Point(10, 10);
            this.account_box.Name = "account_box";
            this.account_box.Size = new System.Drawing.Size(180, 21);
            this.account_box.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(10, 270);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(180, 20);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Slim";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ElyCapePreview
            // 
            this.ElyCapePreview.Location = new System.Drawing.Point(10, 320);
            this.ElyCapePreview.Name = "ElyCapePreview";
            this.ElyCapePreview.Size = new System.Drawing.Size(180, 120);
            this.ElyCapePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ElyCapePreview.TabIndex = 3;
            this.ElyCapePreview.TabStop = false;
            this.ElyCapePreview.MouseEnter += new System.EventHandler(this.ElyCapePreview_MouseEnter);
            this.ElyCapePreview.MouseLeave += new System.EventHandler(this.ElyCapePreview_MouseLeave);
            // 
            // cape_box
            // 
            this.cape_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cape_box.FormattingEnabled = true;
            this.cape_box.Location = new System.Drawing.Point(10, 295);
            this.cape_box.Name = "cape_box";
            this.cape_box.Size = new System.Drawing.Size(180, 21);
            this.cape_box.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(195, 35);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(400, 375);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(515, 415);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(80, 25);
            this.ok.TabIndex = 9;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(435, 415);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(80, 25);
            this.cancel.TabIndex = 8;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // from_file
            // 
            this.from_file.AutoEllipsis = true;
            this.from_file.Location = new System.Drawing.Point(295, 415);
            this.from_file.Name = "from_file";
            this.from_file.Size = new System.Drawing.Size(80, 25);
            this.from_file.TabIndex = 7;
            this.from_file.Text = "From File";
            this.from_file.UseVisualStyleBackColor = true;
            // 
            // from_username
            // 
            this.from_username.AutoEllipsis = true;
            this.from_username.Location = new System.Drawing.Point(195, 415);
            this.from_username.Name = "from_username";
            this.from_username.Size = new System.Drawing.Size(100, 25);
            this.from_username.TabIndex = 6;
            this.from_username.Text = "From Username";
            this.from_username.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(195, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 20);
            this.textBox1.TabIndex = 4;
            // 
            // SkinSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.from_username);
            this.Controls.Add(this.from_file);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cape_box);
            this.Controls.Add(this.ElyCapePreview);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.account_box);
            this.Controls.Add(this.SkinPreview);
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.Name = "SkinSelector";
            this.Text = "Skin Selector";
            ((System.ComponentModel.ISupportInitialize)(this.SkinPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElyCapePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox SkinPreview;
        private System.Windows.Forms.ComboBox account_box;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox ElyCapePreview;
        private System.Windows.Forms.ComboBox cape_box;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button from_file;
        private System.Windows.Forms.Button from_username;
        private System.Windows.Forms.TextBox textBox1;
    }
}