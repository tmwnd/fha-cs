namespace fha_cs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GameField = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // GameField
            // 
            this.GameField.AutoSize = true;
            this.GameField.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GameField.Location = new System.Drawing.Point(15, 15);
            this.GameField.Name = "GameField";
            this.GameField.Size = new System.Drawing.Size(6, 5);
            this.GameField.TabIndex = 0;
            this.GameField.TabStop = false;
            this.GameField.Text = "Spielfeld";
            this.GameField.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GameField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox GameField;
    }
}