namespace cs_games
{
    partial class Spiel
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
            this.gameField = new System.Windows.Forms.GroupBox();
            this.ButtonStartGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameField
            // 
            this.gameField.Location = new System.Drawing.Point(15, 15);
            this.gameField.Name = "gameField";
            this.gameField.Size = new System.Drawing.Size(550, 350);
            this.gameField.TabIndex = 0;
            this.gameField.TabStop = false;
            this.gameField.Text = "Spielfeld";
            this.gameField.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ButtonStartGame
            // 
            this.ButtonStartGame.Location = new System.Drawing.Point(583, 40);
            this.ButtonStartGame.Name = "ButtonStartGame";
            this.ButtonStartGame.Size = new System.Drawing.Size(75, 23);
            this.ButtonStartGame.TabIndex = 1;
            this.ButtonStartGame.Text = "Starte Spiel";
            this.ButtonStartGame.UseVisualStyleBackColor = true;
            this.ButtonStartGame.Click += new System.EventHandler(this.ButtonStartGameClick);
            // 
            // Spiel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ButtonStartGame);
            this.Controls.Add(this.gameField);
            this.Name = "Spiel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gameField;
        private Button ButtonStartGame;
    }
}