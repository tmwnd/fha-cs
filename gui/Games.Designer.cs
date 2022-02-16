namespace gui
{
    partial class Games
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
            this.GroupBoxGames = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox1.Location = new System.Drawing.Point(25, 15);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(250, 50);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Bitte wählen Sie das gewünschte Spiel aus der folgenden Liste aus";
            // 
            // GroupBoxGames
            // 
            this.GroupBoxGames.Location = new System.Drawing.Point(25, 75);
            this.GroupBoxGames.Name = "GroupBoxGames";
            this.GroupBoxGames.Size = new System.Drawing.Size(250, 300);
            this.GroupBoxGames.TabIndex = 1;
            this.GroupBoxGames.TabStop = false;
            this.GroupBoxGames.Text = "Games";
            // 
            // Games
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 461);
            this.Controls.Add(this.GroupBoxGames);
            this.Controls.Add(this.textBox1);
            this.Name = "Games";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private GroupBox GroupBoxGames;
    }
}