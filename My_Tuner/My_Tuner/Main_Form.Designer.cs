namespace My_Tuner
{
    partial class Main_Form
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
        private void Initialize_Component()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.exit_Button = new System.Windows.Forms.Button();
            this.start_Button = new System.Windows.Forms.Button();
            this.stop_Button = new System.Windows.Forms.Button();
            this.chastota_TextBox = new System.Windows.Forms.TextBox();
            this.nota_chastTextBox = new System.Windows.Forms.TextBox();
            this.nota_TextBox = new System.Windows.Forms.TextBox();
            this.shkala_Chastoty1 = new My_Tuner.Shkala_Chastoty(this.components);
            this.SuspendLayout();
            // 
            // exit_Button
            // 
            this.exit_Button.BackColor = System.Drawing.Color.Teal;
            this.exit_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit_Button.Location = new System.Drawing.Point(575, 193);
            this.exit_Button.Name = "exit_Button";
            this.exit_Button.Size = new System.Drawing.Size(102, 33);
            this.exit_Button.TabIndex = 5;
            this.exit_Button.Text = "Выход";
            this.exit_Button.UseVisualStyleBackColor = false;
            this.exit_Button.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // start_Button
            // 
            this.start_Button.BackColor = System.Drawing.Color.Teal;
            this.start_Button.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.start_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start_Button.ForeColor = System.Drawing.Color.Black;
            this.start_Button.Location = new System.Drawing.Point(12, 123);
            this.start_Button.Name = "start_Button";
            this.start_Button.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.start_Button.Size = new System.Drawing.Size(92, 44);
            this.start_Button.TabIndex = 1;
            this.start_Button.Text = "Старт";
            this.start_Button.UseVisualStyleBackColor = false;
            this.start_Button.Click += new System.EventHandler(this.start_Button_Click);
            // 
            // stop_Button
            // 
            this.stop_Button.BackColor = System.Drawing.Color.Teal;
            this.stop_Button.Enabled = false;
            this.stop_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stop_Button.Location = new System.Drawing.Point(368, 126);
            this.stop_Button.Name = "stop_Button";
            this.stop_Button.Size = new System.Drawing.Size(79, 44);
            this.stop_Button.TabIndex = 2;
            this.stop_Button.Text = "Стоп";
            this.stop_Button.UseVisualStyleBackColor = false;
            this.stop_Button.Click += new System.EventHandler(this.stop_Button_Click);
            // 
            // chastota_TextBox
            // 
            this.chastota_TextBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chastota_TextBox.Font = new System.Drawing.Font("SketchFlow Print", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chastota_TextBox.ForeColor = System.Drawing.Color.Purple;
            this.chastota_TextBox.Location = new System.Drawing.Point(128, 126);
            this.chastota_TextBox.Name = "chastota_TextBox";
            this.chastota_TextBox.ReadOnly = true;
            this.chastota_TextBox.Size = new System.Drawing.Size(160, 41);
            this.chastota_TextBox.TabIndex = 3;
            this.chastota_TextBox.TabStop = false;
            this.chastota_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nota_chastTextBox
            // 
            this.nota_chastTextBox.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nota_chastTextBox.Location = new System.Drawing.Point(575, 123);
            this.nota_chastTextBox.Name = "nota_chastTextBox";
            this.nota_chastTextBox.ReadOnly = true;
            this.nota_chastTextBox.Size = new System.Drawing.Size(102, 46);
            this.nota_chastTextBox.TabIndex = 3;
            this.nota_chastTextBox.TabStop = false;
            this.nota_chastTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nota_TextBox
            // 
            this.nota_TextBox.Font = new System.Drawing.Font("Script MT Bold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nota_TextBox.ForeColor = System.Drawing.Color.Red;
            this.nota_TextBox.Location = new System.Drawing.Point(465, 123);
            this.nota_TextBox.Name = "nota_TextBox";
            this.nota_TextBox.ReadOnly = true;
            this.nota_TextBox.Size = new System.Drawing.Size(89, 46);
            this.nota_TextBox.TabIndex = 8;
            // 
            // shkala_Chastoty1
            // 
            this.shkala_Chastoty1.BackColor = System.Drawing.Color.MidnightBlue;
            this.shkala_Chastoty1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shkala_Chastoty1.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shkala_Chastoty1.ForeColor = System.Drawing.Color.Blue;
            this.shkala_Chastoty1.Location = new System.Drawing.Point(12, 12);
            this.shkala_Chastoty1.Name = "shkala_Chastoty1";
            this.shkala_Chastoty1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.shkala_Chastoty1.Size = new System.Drawing.Size(683, 91);
            this.shkala_Chastoty1.TabIndex = 0;
            this.shkala_Chastoty1.Load += new System.EventHandler(this.shkala_Chastoty1_Load);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(702, 240);
            this.Controls.Add(this.nota_TextBox);
            this.Controls.Add(this.shkala_Chastoty1);
            this.Controls.Add(this.nota_chastTextBox);
            this.Controls.Add(this.chastota_TextBox);
            this.Controls.Add(this.stop_Button);
            this.Controls.Add(this.start_Button);
            this.Controls.Add(this.exit_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.Text = "My_Tuner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exit_Button;
        private System.Windows.Forms.Button start_Button;
        private System.Windows.Forms.TextBox chastota_TextBox;
        private Shkala_Chastoty shkala_Chastoty1;
        private System.Windows.Forms.TextBox nota_chastTextBox;
        private System.Windows.Forms.TextBox nota_TextBox;
        private System.Windows.Forms.Button stop_Button;
    }
}

