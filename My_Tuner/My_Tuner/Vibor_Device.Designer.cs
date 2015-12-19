namespace My_Tuner
{
    partial class Vibor_Device
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vibor_Device));
            this.Ustroistva = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ok_Button = new System.Windows.Forms.Button();
            this.cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Ustroistva
            // 
            this.Ustroistva.BackColor = System.Drawing.Color.Teal;
            this.Ustroistva.FormattingEnabled = true;
            this.Ustroistva.Location = new System.Drawing.Point(12, 38);
            this.Ustroistva.Name = "Ustroistva";
            this.Ustroistva.Size = new System.Drawing.Size(336, 121);
            this.Ustroistva.TabIndex = 1;
            this.Ustroistva.DoubleClick += new System.EventHandler(this.deviceNamesListBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Доступные устройства:";
            // 
            // ok_Button
            // 
            this.ok_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok_Button.Location = new System.Drawing.Point(181, 168);
            this.ok_Button.Name = "ok_Button";
            this.ok_Button.Size = new System.Drawing.Size(75, 23);
            this.ok_Button.TabIndex = 2;
            this.ok_Button.Text = "Выбрать";
            this.ok_Button.UseVisualStyleBackColor = true;
            this.ok_Button.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancel_Button
            // 
            this.cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_Button.Location = new System.Drawing.Point(273, 168);
            this.cancel_Button.Name = "cancel_Button";
            this.cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.cancel_Button.TabIndex = 3;
            this.cancel_Button.Text = "Закрыть";
            this.cancel_Button.UseVisualStyleBackColor = true;
            // 
            // Vibor_Device
            // 
            this.AcceptButton = this.ok_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.CancelButton = this.cancel_Button;
            this.ClientSize = new System.Drawing.Size(371, 198);
            this.Controls.Add(this.cancel_Button);
            this.Controls.Add(this.ok_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ustroistva);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Vibor_Device";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор аудиоустройства";
            this.Load += new System.EventHandler(this.Vibor_Device_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Ustroistva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ok_Button;
        private System.Windows.Forms.Button cancel_Button;
    }
}