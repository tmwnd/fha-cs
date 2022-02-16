namespace cs_games
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
            this.groupBoxGames = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // groupBoxGames
            // 
            this.groupBoxGames.Location = new System.Drawing.Point(25, 75);
            this.groupBoxGames.Name = "groupBoxGames";
            this.groupBoxGames.Size = new System.Drawing.Size(250, 300);
            this.groupBoxGames.TabIndex = 1;
            this.groupBoxGames.TabStop = false;
            this.groupBoxGames.Text = "Games";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 50);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bitte wählen Sie das gewünschte Spiel aus\r\nder folgenden Liste aus:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Games
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxGames);
            this.Name = "Games";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox groupBoxGames;
        private Label label1;
    }
}