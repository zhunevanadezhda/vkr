namespace Diplom
{
    partial class AddOwner
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
            this.dateogrnCom_mtb = new System.Windows.Forms.MaskedTextBox();
            this.telephoneCom_mtb = new System.Windows.Forms.MaskedTextBox();
            this.telephoneCom_lb = new System.Windows.Forms.Label();
            this.emailCom_lb = new System.Windows.Forms.Label();
            this.emailCom_tb = new System.Windows.Forms.TextBox();
            this.formCom_lb = new System.Windows.Forms.Label();
            this.formCom_tb = new System.Windows.Forms.TextBox();
            this.dateogrnCom_lb = new System.Windows.Forms.Label();
            this.ogrnCom_lb = new System.Windows.Forms.Label();
            this.ogrnCom_tb = new System.Windows.Forms.TextBox();
            this.adresCom_lb = new System.Windows.Forms.Label();
            this.adresCom_tb = new System.Windows.Forms.TextBox();
            this.pasportWhere_lb = new System.Windows.Forms.Label();
            this.pasportWhere_tb = new System.Windows.Forms.TextBox();
            this.nameCom_lb = new System.Windows.Forms.Label();
            this.nameCom_tb = new System.Windows.Forms.TextBox();
            this.pasportDate_lb = new System.Windows.Forms.Label();
            this.pasportNumber_lb = new System.Windows.Forms.Label();
            this.pasportNumber_tb = new System.Windows.Forms.TextBox();
            this.patronym_lb = new System.Windows.Forms.Label();
            this.name_lb = new System.Windows.Forms.Label();
            this.surname_lb = new System.Windows.Forms.Label();
            this.patronym_tb = new System.Windows.Forms.TextBox();
            this.name_tb = new System.Windows.Forms.TextBox();
            this.surname_tb = new System.Windows.Forms.TextBox();
            this.ur_rb = new System.Windows.Forms.RadioButton();
            this.fiz_rb = new System.Windows.Forms.RadioButton();
            this.pasportDate_mtb = new System.Windows.Forms.MaskedTextBox();
            this.save_bt = new System.Windows.Forms.Button();
            this.cancel_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateogrnCom_mtb
            // 
            this.dateogrnCom_mtb.Location = new System.Drawing.Point(8, 160);
            this.dateogrnCom_mtb.Mask = "00/00/0000";
            this.dateogrnCom_mtb.Name = "dateogrnCom_mtb";
            this.dateogrnCom_mtb.Size = new System.Drawing.Size(232, 20);
            this.dateogrnCom_mtb.TabIndex = 120;
            this.dateogrnCom_mtb.ValidatingType = typeof(System.DateTime);
            this.dateogrnCom_mtb.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.OwnerChanged);
            // 
            // telephoneCom_mtb
            // 
            this.telephoneCom_mtb.Location = new System.Drawing.Point(8, 302);
            this.telephoneCom_mtb.Mask = "+7(999) 000-0000";
            this.telephoneCom_mtb.Name = "telephoneCom_mtb";
            this.telephoneCom_mtb.Size = new System.Drawing.Size(231, 20);
            this.telephoneCom_mtb.TabIndex = 117;
            this.telephoneCom_mtb.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.telephoneCom_mtb.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.OwnerChanged);
            // 
            // telephoneCom_lb
            // 
            this.telephoneCom_lb.AutoSize = true;
            this.telephoneCom_lb.Location = new System.Drawing.Point(9, 286);
            this.telephoneCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.telephoneCom_lb.Name = "telephoneCom_lb";
            this.telephoneCom_lb.Size = new System.Drawing.Size(105, 13);
            this.telephoneCom_lb.TabIndex = 115;
            this.telephoneCom_lb.Text = "Телефон компании";
            // 
            // emailCom_lb
            // 
            this.emailCom_lb.AutoSize = true;
            this.emailCom_lb.Location = new System.Drawing.Point(9, 325);
            this.emailCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.emailCom_lb.Name = "emailCom_lb";
            this.emailCom_lb.Size = new System.Drawing.Size(85, 13);
            this.emailCom_lb.TabIndex = 114;
            this.emailCom_lb.Text = "Email компании";
            // 
            // emailCom_tb
            // 
            this.emailCom_tb.Location = new System.Drawing.Point(8, 340);
            this.emailCom_tb.Margin = new System.Windows.Forms.Padding(2);
            this.emailCom_tb.Name = "emailCom_tb";
            this.emailCom_tb.Size = new System.Drawing.Size(231, 20);
            this.emailCom_tb.TabIndex = 103;
            this.emailCom_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // formCom_lb
            // 
            this.formCom_lb.AutoSize = true;
            this.formCom_lb.Location = new System.Drawing.Point(9, 71);
            this.formCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.formCom_lb.Name = "formCom_lb";
            this.formCom_lb.Size = new System.Drawing.Size(180, 13);
            this.formCom_lb.TabIndex = 113;
            this.formCom_lb.Text = "Организационно-правовая форма";
            // 
            // formCom_tb
            // 
            this.formCom_tb.Location = new System.Drawing.Point(8, 86);
            this.formCom_tb.Margin = new System.Windows.Forms.Padding(2);
            this.formCom_tb.Name = "formCom_tb";
            this.formCom_tb.Size = new System.Drawing.Size(232, 20);
            this.formCom_tb.TabIndex = 100;
            this.formCom_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // dateogrnCom_lb
            // 
            this.dateogrnCom_lb.AutoSize = true;
            this.dateogrnCom_lb.Location = new System.Drawing.Point(9, 145);
            this.dateogrnCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dateogrnCom_lb.Name = "dateogrnCom_lb";
            this.dateogrnCom_lb.Size = new System.Drawing.Size(128, 13);
            this.dateogrnCom_lb.TabIndex = 112;
            this.dateogrnCom_lb.Text = "Дата присвоения ОГРН";
            // 
            // ogrnCom_lb
            // 
            this.ogrnCom_lb.AutoSize = true;
            this.ogrnCom_lb.Location = new System.Drawing.Point(9, 108);
            this.ogrnCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ogrnCom_lb.Name = "ogrnCom_lb";
            this.ogrnCom_lb.Size = new System.Drawing.Size(73, 13);
            this.ogrnCom_lb.TabIndex = 111;
            this.ogrnCom_lb.Text = "Номер ОГРН";
            // 
            // ogrnCom_tb
            // 
            this.ogrnCom_tb.Location = new System.Drawing.Point(8, 123);
            this.ogrnCom_tb.Margin = new System.Windows.Forms.Padding(2);
            this.ogrnCom_tb.Name = "ogrnCom_tb";
            this.ogrnCom_tb.Size = new System.Drawing.Size(231, 20);
            this.ogrnCom_tb.TabIndex = 101;
            this.ogrnCom_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // adresCom_lb
            // 
            this.adresCom_lb.AutoSize = true;
            this.adresCom_lb.Location = new System.Drawing.Point(9, 183);
            this.adresCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.adresCom_lb.Name = "adresCom_lb";
            this.adresCom_lb.Size = new System.Drawing.Size(148, 13);
            this.adresCom_lb.TabIndex = 110;
            this.adresCom_lb.Text = "Местоположение компании";
            // 
            // adresCom_tb
            // 
            this.adresCom_tb.Location = new System.Drawing.Point(8, 196);
            this.adresCom_tb.Margin = new System.Windows.Forms.Padding(2);
            this.adresCom_tb.Multiline = true;
            this.adresCom_tb.Name = "adresCom_tb";
            this.adresCom_tb.Size = new System.Drawing.Size(231, 88);
            this.adresCom_tb.TabIndex = 102;
            this.adresCom_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // pasportWhere_lb
            // 
            this.pasportWhere_lb.AutoSize = true;
            this.pasportWhere_lb.Location = new System.Drawing.Point(9, 219);
            this.pasportWhere_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pasportWhere_lb.Name = "pasportWhere_lb";
            this.pasportWhere_lb.Size = new System.Drawing.Size(63, 13);
            this.pasportWhere_lb.TabIndex = 109;
            this.pasportWhere_lb.Text = "Кем выдан";
            this.pasportWhere_lb.Visible = false;
            // 
            // pasportWhere_tb
            // 
            this.pasportWhere_tb.Location = new System.Drawing.Point(8, 234);
            this.pasportWhere_tb.Margin = new System.Windows.Forms.Padding(2);
            this.pasportWhere_tb.Multiline = true;
            this.pasportWhere_tb.Name = "pasportWhere_tb";
            this.pasportWhere_tb.Size = new System.Drawing.Size(231, 83);
            this.pasportWhere_tb.TabIndex = 105;
            this.pasportWhere_tb.Visible = false;
            this.pasportWhere_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // nameCom_lb
            // 
            this.nameCom_lb.AutoSize = true;
            this.nameCom_lb.Location = new System.Drawing.Point(9, 34);
            this.nameCom_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameCom_lb.Name = "nameCom_lb";
            this.nameCom_lb.Size = new System.Drawing.Size(136, 13);
            this.nameCom_lb.TabIndex = 108;
            this.nameCom_lb.Text = "Наименование компании";
            // 
            // nameCom_tb
            // 
            this.nameCom_tb.Location = new System.Drawing.Point(8, 49);
            this.nameCom_tb.Margin = new System.Windows.Forms.Padding(2);
            this.nameCom_tb.Name = "nameCom_tb";
            this.nameCom_tb.Size = new System.Drawing.Size(232, 20);
            this.nameCom_tb.TabIndex = 99;
            this.nameCom_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // pasportDate_lb
            // 
            this.pasportDate_lb.AutoSize = true;
            this.pasportDate_lb.Location = new System.Drawing.Point(9, 182);
            this.pasportDate_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pasportDate_lb.Name = "pasportDate_lb";
            this.pasportDate_lb.Size = new System.Drawing.Size(123, 13);
            this.pasportDate_lb.TabIndex = 107;
            this.pasportDate_lb.Text = "Дата выдачи паспорта";
            this.pasportDate_lb.Visible = false;
            // 
            // pasportNumber_lb
            // 
            this.pasportNumber_lb.AutoSize = true;
            this.pasportNumber_lb.Location = new System.Drawing.Point(9, 145);
            this.pasportNumber_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pasportNumber_lb.Name = "pasportNumber_lb";
            this.pasportNumber_lb.Size = new System.Drawing.Size(132, 13);
            this.pasportNumber_lb.TabIndex = 106;
            this.pasportNumber_lb.Text = "Серия и номер паспорта";
            this.pasportNumber_lb.Visible = false;
            // 
            // pasportNumber_tb
            // 
            this.pasportNumber_tb.Location = new System.Drawing.Point(8, 160);
            this.pasportNumber_tb.Margin = new System.Windows.Forms.Padding(2);
            this.pasportNumber_tb.Name = "pasportNumber_tb";
            this.pasportNumber_tb.Size = new System.Drawing.Size(231, 20);
            this.pasportNumber_tb.TabIndex = 104;
            this.pasportNumber_tb.Visible = false;
            this.pasportNumber_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // patronym_lb
            // 
            this.patronym_lb.AutoSize = true;
            this.patronym_lb.Location = new System.Drawing.Point(9, 108);
            this.patronym_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patronym_lb.Name = "patronym_lb";
            this.patronym_lb.Size = new System.Drawing.Size(54, 13);
            this.patronym_lb.TabIndex = 97;
            this.patronym_lb.Text = "Отчество";
            // 
            // name_lb
            // 
            this.name_lb.AutoSize = true;
            this.name_lb.Location = new System.Drawing.Point(9, 71);
            this.name_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.name_lb.Name = "name_lb";
            this.name_lb.Size = new System.Drawing.Size(33, 13);
            this.name_lb.TabIndex = 96;
            this.name_lb.Text = "Имя*";
            // 
            // surname_lb
            // 
            this.surname_lb.AutoSize = true;
            this.surname_lb.Location = new System.Drawing.Point(9, 34);
            this.surname_lb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.surname_lb.Name = "surname_lb";
            this.surname_lb.Size = new System.Drawing.Size(60, 13);
            this.surname_lb.TabIndex = 95;
            this.surname_lb.Text = "Фамилия*";
            // 
            // patronym_tb
            // 
            this.patronym_tb.Location = new System.Drawing.Point(8, 123);
            this.patronym_tb.Margin = new System.Windows.Forms.Padding(2);
            this.patronym_tb.Name = "patronym_tb";
            this.patronym_tb.Size = new System.Drawing.Size(231, 20);
            this.patronym_tb.TabIndex = 94;
            this.patronym_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // name_tb
            // 
            this.name_tb.Location = new System.Drawing.Point(8, 86);
            this.name_tb.Margin = new System.Windows.Forms.Padding(2);
            this.name_tb.Name = "name_tb";
            this.name_tb.Size = new System.Drawing.Size(231, 20);
            this.name_tb.TabIndex = 93;
            this.name_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // surname_tb
            // 
            this.surname_tb.Location = new System.Drawing.Point(8, 49);
            this.surname_tb.Margin = new System.Windows.Forms.Padding(2);
            this.surname_tb.Name = "surname_tb";
            this.surname_tb.Size = new System.Drawing.Size(231, 20);
            this.surname_tb.TabIndex = 92;
            this.surname_tb.TextChanged += new System.EventHandler(this.OwnerChanged);
            // 
            // ur_rb
            // 
            this.ur_rb.AutoSize = true;
            this.ur_rb.Location = new System.Drawing.Point(131, 12);
            this.ur_rb.Name = "ur_rb";
            this.ur_rb.Size = new System.Drawing.Size(119, 17);
            this.ur_rb.TabIndex = 91;
            this.ur_rb.Text = "юридическое лицо";
            this.ur_rb.UseVisualStyleBackColor = true;
            this.ur_rb.CheckedChanged += new System.EventHandler(this.ur_rb_CheckedChanged);
            // 
            // fiz_rb
            // 
            this.fiz_rb.AutoSize = true;
            this.fiz_rb.Checked = true;
            this.fiz_rb.Location = new System.Drawing.Point(12, 12);
            this.fiz_rb.Name = "fiz_rb";
            this.fiz_rb.Size = new System.Drawing.Size(113, 17);
            this.fiz_rb.TabIndex = 90;
            this.fiz_rb.TabStop = true;
            this.fiz_rb.Text = "физическое лицо";
            this.fiz_rb.UseVisualStyleBackColor = true;
            this.fiz_rb.CheckedChanged += new System.EventHandler(this.fiz_rb_CheckedChanged);
            // 
            // pasportDate_mtb
            // 
            this.pasportDate_mtb.Location = new System.Drawing.Point(8, 196);
            this.pasportDate_mtb.Mask = "00/00/0000";
            this.pasportDate_mtb.Name = "pasportDate_mtb";
            this.pasportDate_mtb.Size = new System.Drawing.Size(231, 20);
            this.pasportDate_mtb.TabIndex = 118;
            this.pasportDate_mtb.ValidatingType = typeof(System.DateTime);
            this.pasportDate_mtb.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.OwnerChanged);
            // 
            // save_bt
            // 
            this.save_bt.BackColor = System.Drawing.Color.Linen;
            this.save_bt.Location = new System.Drawing.Point(8, 366);
            this.save_bt.Name = "save_bt";
            this.save_bt.Size = new System.Drawing.Size(102, 32);
            this.save_bt.TabIndex = 121;
            this.save_bt.Text = "Сохранить";
            this.save_bt.UseVisualStyleBackColor = false;
            this.save_bt.Click += new System.EventHandler(this.save_bt_Click);
            // 
            // cancel_bt
            // 
            this.cancel_bt.BackColor = System.Drawing.SystemColors.Control;
            this.cancel_bt.Location = new System.Drawing.Point(148, 365);
            this.cancel_bt.Name = "cancel_bt";
            this.cancel_bt.Size = new System.Drawing.Size(91, 32);
            this.cancel_bt.TabIndex = 122;
            this.cancel_bt.Text = "Отмена";
            this.cancel_bt.UseVisualStyleBackColor = false;
            this.cancel_bt.Click += new System.EventHandler(this.cancel_bt_Click);
            // 
            // AddOwner
            // 
            this.AcceptButton = this.save_bt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(251, 410);
            this.Controls.Add(this.cancel_bt);
            this.Controls.Add(this.save_bt);
            this.Controls.Add(this.dateogrnCom_mtb);
            this.Controls.Add(this.telephoneCom_mtb);
            this.Controls.Add(this.telephoneCom_lb);
            this.Controls.Add(this.emailCom_lb);
            this.Controls.Add(this.emailCom_tb);
            this.Controls.Add(this.formCom_lb);
            this.Controls.Add(this.formCom_tb);
            this.Controls.Add(this.dateogrnCom_lb);
            this.Controls.Add(this.ogrnCom_lb);
            this.Controls.Add(this.ogrnCom_tb);
            this.Controls.Add(this.adresCom_lb);
            this.Controls.Add(this.adresCom_tb);
            this.Controls.Add(this.pasportWhere_lb);
            this.Controls.Add(this.pasportWhere_tb);
            this.Controls.Add(this.nameCom_lb);
            this.Controls.Add(this.nameCom_tb);
            this.Controls.Add(this.pasportDate_lb);
            this.Controls.Add(this.pasportNumber_lb);
            this.Controls.Add(this.pasportNumber_tb);
            this.Controls.Add(this.patronym_lb);
            this.Controls.Add(this.name_lb);
            this.Controls.Add(this.surname_lb);
            this.Controls.Add(this.patronym_tb);
            this.Controls.Add(this.name_tb);
            this.Controls.Add(this.surname_tb);
            this.Controls.Add(this.ur_rb);
            this.Controls.Add(this.fiz_rb);
            this.Controls.Add(this.pasportDate_mtb);
            this.Name = "AddOwner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Владелец";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox dateogrnCom_mtb;
        private System.Windows.Forms.MaskedTextBox telephoneCom_mtb;
        private System.Windows.Forms.Label telephoneCom_lb;
        private System.Windows.Forms.Label emailCom_lb;
        private System.Windows.Forms.TextBox emailCom_tb;
        private System.Windows.Forms.Label formCom_lb;
        private System.Windows.Forms.TextBox formCom_tb;
        private System.Windows.Forms.Label dateogrnCom_lb;
        private System.Windows.Forms.Label ogrnCom_lb;
        private System.Windows.Forms.TextBox ogrnCom_tb;
        private System.Windows.Forms.Label adresCom_lb;
        private System.Windows.Forms.TextBox adresCom_tb;
        private System.Windows.Forms.Label pasportWhere_lb;
        private System.Windows.Forms.TextBox pasportWhere_tb;
        private System.Windows.Forms.Label nameCom_lb;
        private System.Windows.Forms.TextBox nameCom_tb;
        private System.Windows.Forms.Label pasportDate_lb;
        private System.Windows.Forms.Label pasportNumber_lb;
        private System.Windows.Forms.TextBox pasportNumber_tb;
        private System.Windows.Forms.Label patronym_lb;
        private System.Windows.Forms.Label name_lb;
        private System.Windows.Forms.Label surname_lb;
        private System.Windows.Forms.TextBox patronym_tb;
        private System.Windows.Forms.TextBox name_tb;
        private System.Windows.Forms.TextBox surname_tb;
        private System.Windows.Forms.RadioButton ur_rb;
        private System.Windows.Forms.RadioButton fiz_rb;
        private System.Windows.Forms.MaskedTextBox pasportDate_mtb;
        private System.Windows.Forms.Button save_bt;
        private System.Windows.Forms.Button cancel_bt;
    }
}