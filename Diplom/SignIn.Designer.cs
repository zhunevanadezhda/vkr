namespace Diplom
{
    partial class SignIn
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.login_tb = new System.Windows.Forms.TextBox();
            this.signUp_bt = new System.Windows.Forms.Button();
            this.signIn_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Email";
            // 
            // password_tb
            // 
            this.password_tb.Location = new System.Drawing.Point(21, 70);
            this.password_tb.Margin = new System.Windows.Forms.Padding(2);
            this.password_tb.Name = "password_tb";
            this.password_tb.Size = new System.Drawing.Size(172, 20);
            this.password_tb.TabIndex = 1;
            this.password_tb.TextChanged += new System.EventHandler(this.password_tb_TextChanged);
            // 
            // login_tb
            // 
            this.login_tb.Location = new System.Drawing.Point(21, 31);
            this.login_tb.Margin = new System.Windows.Forms.Padding(2);
            this.login_tb.Name = "login_tb";
            this.login_tb.Size = new System.Drawing.Size(172, 20);
            this.login_tb.TabIndex = 0;
            this.login_tb.TextChanged += new System.EventHandler(this.login_tb_TextChanged);
            // 
            // signUp_bt
            // 
            this.signUp_bt.BackColor = System.Drawing.Color.Bisque;
            this.signUp_bt.Location = new System.Drawing.Point(37, 145);
            this.signUp_bt.Margin = new System.Windows.Forms.Padding(2);
            this.signUp_bt.Name = "signUp_bt";
            this.signUp_bt.Size = new System.Drawing.Size(136, 27);
            this.signUp_bt.TabIndex = 9;
            this.signUp_bt.Text = "Зарегистрироваться";
            this.signUp_bt.UseVisualStyleBackColor = false;
            this.signUp_bt.Click += new System.EventHandler(this.signUp_bt_Click);
            // 
            // signIn_bt
            // 
            this.signIn_bt.BackColor = System.Drawing.Color.Linen;
            this.signIn_bt.Location = new System.Drawing.Point(37, 105);
            this.signIn_bt.Margin = new System.Windows.Forms.Padding(2);
            this.signIn_bt.Name = "signIn_bt";
            this.signIn_bt.Size = new System.Drawing.Size(136, 29);
            this.signIn_bt.TabIndex = 2;
            this.signIn_bt.Text = "Войти";
            this.signIn_bt.UseVisualStyleBackColor = false;
            this.signIn_bt.Click += new System.EventHandler(this.signIn_bt_Click);
            // 
            // SignIn
            // 
            this.AcceptButton = this.signIn_bt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(213, 191);
            this.Controls.Add(this.signUp_bt);
            this.Controls.Add(this.signIn_bt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.login_tb);
            this.Name = "SignIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.TextBox login_tb;
        private System.Windows.Forms.Button signUp_bt;
        private System.Windows.Forms.Button signIn_bt;
    }
}