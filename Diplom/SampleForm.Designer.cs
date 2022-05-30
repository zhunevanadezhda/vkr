namespace Diplom
{
    partial class SampleForm
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
            this.reference_bt = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.form_bt = new System.Windows.Forms.Button();
            this.open_bt = new System.Windows.Forms.Button();
            this.add_bt = new System.Windows.Forms.Button();
            this.copy_bt = new System.Windows.Forms.Button();
            this.delete_bt = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rename_bt = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // reference_bt
            // 
            this.reference_bt.BackColor = System.Drawing.SystemColors.Info;
            this.reference_bt.Location = new System.Drawing.Point(12, 12);
            this.reference_bt.Name = "reference_bt";
            this.reference_bt.Size = new System.Drawing.Size(75, 23);
            this.reference_bt.TabIndex = 1;
            this.reference_bt.Text = "Справка";
            this.reference_bt.UseVisualStyleBackColor = false;
            this.reference_bt.Click += new System.EventHandler(this.reference_bt_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(244, 147);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // form_bt
            // 
            this.form_bt.BackColor = System.Drawing.Color.Bisque;
            this.form_bt.Location = new System.Drawing.Point(12, 194);
            this.form_bt.Name = "form_bt";
            this.form_bt.Size = new System.Drawing.Size(134, 23);
            this.form_bt.TabIndex = 3;
            this.form_bt.Text = "Сформировать отчет";
            this.form_bt.UseVisualStyleBackColor = false;
            this.form_bt.Click += new System.EventHandler(this.form_bt_Click);
            // 
            // open_bt
            // 
            this.open_bt.BackColor = System.Drawing.Color.Linen;
            this.open_bt.Enabled = false;
            this.open_bt.Location = new System.Drawing.Point(262, 70);
            this.open_bt.Name = "open_bt";
            this.open_bt.Size = new System.Drawing.Size(105, 23);
            this.open_bt.TabIndex = 4;
            this.open_bt.Text = "Открыть";
            this.open_bt.UseVisualStyleBackColor = false;
            this.open_bt.Click += new System.EventHandler(this.open_bt_Click);
            // 
            // add_bt
            // 
            this.add_bt.BackColor = System.Drawing.Color.Linen;
            this.add_bt.Location = new System.Drawing.Point(262, 41);
            this.add_bt.Name = "add_bt";
            this.add_bt.Size = new System.Drawing.Size(105, 23);
            this.add_bt.TabIndex = 5;
            this.add_bt.Text = "Добавить";
            this.add_bt.UseVisualStyleBackColor = false;
            this.add_bt.Click += new System.EventHandler(this.add_bt_Click);
            // 
            // copy_bt
            // 
            this.copy_bt.BackColor = System.Drawing.Color.Linen;
            this.copy_bt.Enabled = false;
            this.copy_bt.Location = new System.Drawing.Point(262, 99);
            this.copy_bt.Name = "copy_bt";
            this.copy_bt.Size = new System.Drawing.Size(105, 23);
            this.copy_bt.TabIndex = 6;
            this.copy_bt.Text = "Копировать";
            this.copy_bt.UseVisualStyleBackColor = false;
            this.copy_bt.Click += new System.EventHandler(this.copy_bt_Click);
            // 
            // delete_bt
            // 
            this.delete_bt.BackColor = System.Drawing.Color.DarkOrange;
            this.delete_bt.Enabled = false;
            this.delete_bt.Location = new System.Drawing.Point(262, 157);
            this.delete_bt.Name = "delete_bt";
            this.delete_bt.Size = new System.Drawing.Size(105, 23);
            this.delete_bt.TabIndex = 7;
            this.delete_bt.Text = "Удалить";
            this.delete_bt.UseVisualStyleBackColor = false;
            this.delete_bt.Click += new System.EventHandler(this.delete_bt_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rename_bt
            // 
            this.rename_bt.BackColor = System.Drawing.Color.Linen;
            this.rename_bt.Enabled = false;
            this.rename_bt.Location = new System.Drawing.Point(262, 128);
            this.rename_bt.Name = "rename_bt";
            this.rename_bt.Size = new System.Drawing.Size(105, 23);
            this.rename_bt.TabIndex = 8;
            this.rename_bt.Text = "Переименовать";
            this.rename_bt.UseVisualStyleBackColor = false;
            this.rename_bt.Click += new System.EventHandler(this.rename_bt_Click);
            // 
            // SampleForm
            // 
            this.AcceptButton = this.form_bt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(379, 229);
            this.Controls.Add(this.rename_bt);
            this.Controls.Add(this.delete_bt);
            this.Controls.Add(this.copy_bt);
            this.Controls.Add(this.add_bt);
            this.Controls.Add(this.open_bt);
            this.Controls.Add(this.form_bt);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.reference_bt);
            this.Name = "SampleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выберите шаблон";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button reference_bt;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button form_bt;
        private System.Windows.Forms.Button open_bt;
        private System.Windows.Forms.Button add_bt;
        private System.Windows.Forms.Button copy_bt;
        private System.Windows.Forms.Button delete_bt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button rename_bt;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}