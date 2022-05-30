namespace Diplom
{
    partial class RenameElementForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OK_bt = new System.Windows.Forms.Button();
            this.cancel_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(211, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите новое наименование элемента";
            // 
            // OK_bt
            // 
            this.OK_bt.BackColor = System.Drawing.Color.Linen;
            this.OK_bt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK_bt.Location = new System.Drawing.Point(29, 66);
            this.OK_bt.Name = "OK_bt";
            this.OK_bt.Size = new System.Drawing.Size(75, 23);
            this.OK_bt.TabIndex = 2;
            this.OK_bt.Text = "ОК";
            this.OK_bt.UseVisualStyleBackColor = false;
            this.OK_bt.Click += new System.EventHandler(this.OK_bt_Click);
            // 
            // cancel_bt
            // 
            this.cancel_bt.BackColor = System.Drawing.SystemColors.Control;
            this.cancel_bt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_bt.Location = new System.Drawing.Point(129, 66);
            this.cancel_bt.Name = "cancel_bt";
            this.cancel_bt.Size = new System.Drawing.Size(75, 23);
            this.cancel_bt.TabIndex = 3;
            this.cancel_bt.Text = "Отмена";
            this.cancel_bt.UseVisualStyleBackColor = false;
            this.cancel_bt.Click += new System.EventHandler(this.cancel_bt_Click);
            // 
            // RenameElementForm
            // 
            this.AcceptButton = this.OK_bt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(236, 102);
            this.Controls.Add(this.cancel_bt);
            this.Controls.Add(this.OK_bt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "RenameElementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новый наименование элемента сравнения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OK_bt;
        private System.Windows.Forms.Button cancel_bt;
    }
}