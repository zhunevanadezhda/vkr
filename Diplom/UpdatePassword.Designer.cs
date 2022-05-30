namespace Diplom
{
    partial class UpdatePassword
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password2_tb = new System.Windows.Forms.TextBox();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.oldParole_tb = new System.Windows.Forms.TextBox();
            this.save_bt = new System.Windows.Forms.Button();
            this.cancel_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Повторите пароль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Новый пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Старый пароль";
            // 
            // password2_tb
            // 
            this.password2_tb.Location = new System.Drawing.Point(11, 98);
            this.password2_tb.Margin = new System.Windows.Forms.Padding(2);
            this.password2_tb.Name = "password2_tb";
            this.password2_tb.Size = new System.Drawing.Size(194, 20);
            this.password2_tb.TabIndex = 27;
            this.password2_tb.TextChanged += new System.EventHandler(this.password2_tb_TextChanged);
            // 
            // password_tb
            // 
            this.password_tb.Location = new System.Drawing.Point(11, 61);
            this.password_tb.Margin = new System.Windows.Forms.Padding(2);
            this.password_tb.Name = "password_tb";
            this.password_tb.Size = new System.Drawing.Size(194, 20);
            this.password_tb.TabIndex = 26;
            this.password_tb.TextChanged += new System.EventHandler(this.password_tb_TextChanged);
            // 
            // oldParole_tb
            // 
            this.oldParole_tb.Location = new System.Drawing.Point(11, 24);
            this.oldParole_tb.Margin = new System.Windows.Forms.Padding(2);
            this.oldParole_tb.Name = "oldParole_tb";
            this.oldParole_tb.Size = new System.Drawing.Size(194, 20);
            this.oldParole_tb.TabIndex = 25;
            this.oldParole_tb.TextChanged += new System.EventHandler(this.oldParole_tb_TextChanged);
            // 
            // save_bt
            // 
            this.save_bt.BackColor = System.Drawing.Color.Linen;
            this.save_bt.Enabled = false;
            this.save_bt.Location = new System.Drawing.Point(11, 135);
            this.save_bt.Name = "save_bt";
            this.save_bt.Size = new System.Drawing.Size(116, 34);
            this.save_bt.TabIndex = 31;
            this.save_bt.Text = "Сохранить изменения";
            this.save_bt.UseVisualStyleBackColor = false;
            this.save_bt.Click += new System.EventHandler(this.save_bt_Click);
            // 
            // cancel_bt
            // 
            this.cancel_bt.BackColor = System.Drawing.SystemColors.Control;
            this.cancel_bt.Location = new System.Drawing.Point(133, 135);
            this.cancel_bt.Name = "cancel_bt";
            this.cancel_bt.Size = new System.Drawing.Size(72, 34);
            this.cancel_bt.TabIndex = 32;
            this.cancel_bt.Text = "Отмена";
            this.cancel_bt.UseVisualStyleBackColor = false;
            this.cancel_bt.Click += new System.EventHandler(this.cancel_bt_Click);
            // 
            // UpdatePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(219, 184);
            this.Controls.Add(this.cancel_bt);
            this.Controls.Add(this.save_bt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password2_tb);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.oldParole_tb);
            this.Name = "UpdatePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password2_tb;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.TextBox oldParole_tb;
        private System.Windows.Forms.Button save_bt;
        private System.Windows.Forms.Button cancel_bt;
    }
}