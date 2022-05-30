namespace Diplom
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add_bt = new System.Windows.Forms.Button();
            this.delete_bt = new System.Windows.Forms.Button();
            this.update_bt = new System.Windows.Forms.Button();
            this.updateUser_bt = new System.Windows.Forms.Button();
            this.updatePassword_bt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column6,
            this.Column4,
            this.Column5,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(696, 426);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContent);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Тип недвижимости";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Column3.HeaderText = "Заказчик";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 5;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Телефон заказчика";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Column4.HeaderText = "Адрес оцениваемого объекта";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 5;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Готовность отчета";
            this.Column5.Name = "Column5";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Создатель отчета";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            // 
            // add_bt
            // 
            this.add_bt.BackColor = System.Drawing.Color.Linen;
            this.add_bt.Location = new System.Drawing.Point(736, 12);
            this.add_bt.Name = "add_bt";
            this.add_bt.Size = new System.Drawing.Size(121, 41);
            this.add_bt.TabIndex = 4;
            this.add_bt.Text = "Создать новый отчет";
            this.add_bt.UseVisualStyleBackColor = false;
            this.add_bt.Click += new System.EventHandler(this.add_bt_Click);
            // 
            // delete_bt
            // 
            this.delete_bt.BackColor = System.Drawing.Color.DarkOrange;
            this.delete_bt.Enabled = false;
            this.delete_bt.Location = new System.Drawing.Point(736, 106);
            this.delete_bt.Name = "delete_bt";
            this.delete_bt.Size = new System.Drawing.Size(121, 41);
            this.delete_bt.TabIndex = 5;
            this.delete_bt.Text = "Удалить отчет";
            this.delete_bt.UseVisualStyleBackColor = false;
            this.delete_bt.Click += new System.EventHandler(this.delete_bt_Click);
            // 
            // update_bt
            // 
            this.update_bt.BackColor = System.Drawing.Color.Linen;
            this.update_bt.Enabled = false;
            this.update_bt.Location = new System.Drawing.Point(736, 59);
            this.update_bt.Name = "update_bt";
            this.update_bt.Size = new System.Drawing.Size(121, 41);
            this.update_bt.TabIndex = 6;
            this.update_bt.Text = "Изменить отчет";
            this.update_bt.UseVisualStyleBackColor = false;
            this.update_bt.Click += new System.EventHandler(this.update_bt_Click);
            // 
            // updateUser_bt
            // 
            this.updateUser_bt.BackColor = System.Drawing.Color.Linen;
            this.updateUser_bt.Location = new System.Drawing.Point(736, 329);
            this.updateUser_bt.Name = "updateUser_bt";
            this.updateUser_bt.Size = new System.Drawing.Size(121, 62);
            this.updateUser_bt.TabIndex = 7;
            this.updateUser_bt.Text = "Изменить информацию о пользователе";
            this.updateUser_bt.UseVisualStyleBackColor = false;
            this.updateUser_bt.Click += new System.EventHandler(this.updateUser_bt_Click);
            // 
            // updatePassword_bt
            // 
            this.updatePassword_bt.BackColor = System.Drawing.Color.Linen;
            this.updatePassword_bt.Location = new System.Drawing.Point(736, 397);
            this.updatePassword_bt.Name = "updatePassword_bt";
            this.updatePassword_bt.Size = new System.Drawing.Size(121, 41);
            this.updatePassword_bt.TabIndex = 8;
            this.updatePassword_bt.Text = "Изменить  пароль";
            this.updatePassword_bt.UseVisualStyleBackColor = false;
            this.updatePassword_bt.Click += new System.EventHandler(this.updatePassword_bt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(883, 450);
            this.Controls.Add(this.updatePassword_bt);
            this.Controls.Add(this.updateUser_bt);
            this.Controls.Add(this.update_bt);
            this.Controls.Add(this.delete_bt);
            this.Controls.Add(this.add_bt);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список отчетов";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button add_bt;
        private System.Windows.Forms.Button delete_bt;
        private System.Windows.Forms.Button update_bt;
        private System.Windows.Forms.Button updateUser_bt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button updatePassword_bt;
    }
}

