namespace cs_games
{
    partial class FormGame
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
            this.buttonNeu = new System.Windows.Forms.Button();
            this.ButtonStartGame = new System.Windows.Forms.Button();
            this.labelPlayerNow = new System.Windows.Forms.Label();
            this.spieler1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.spieler2 = new System.Windows.Forms.Label();
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
            // 
            // buttonNeu
            // 
            this.buttonNeu.Enabled = false;
            this.buttonNeu.Location = new System.Drawing.Point(15, 375);
            this.buttonNeu.Name = "buttonNeu";
            this.buttonNeu.Size = new System.Drawing.Size(50, 25);
            this.buttonNeu.TabIndex = 6;
            this.buttonNeu.Text = "Neu";
            this.buttonNeu.UseVisualStyleBackColor = true;
            this.buttonNeu.Click += new System.EventHandler(this.NeuClick);
            // 
            // ButtonStartGame
            // 
            this.ButtonStartGame.Location = new System.Drawing.Point(600, 25);
            this.ButtonStartGame.Name = "ButtonStartGame";
            this.ButtonStartGame.Size = new System.Drawing.Size(150, 75);
            this.ButtonStartGame.TabIndex = 1;
            this.ButtonStartGame.Text = "Starte Spiel";
            this.ButtonStartGame.UseVisualStyleBackColor = true;
            this.ButtonStartGame.Click += new System.EventHandler(this.ButtonStartGameClick);
            // 
            // labelPlayerNow
            // 
            this.labelPlayerNow.AutoSize = true;
            this.labelPlayerNow.Location = new System.Drawing.Point(600, 125);
            this.labelPlayerNow.Name = "labelPlayerNow";
            this.labelPlayerNow.Size = new System.Drawing.Size(50, 15);
            this.labelPlayerNow.TabIndex = 2;
            this.labelPlayerNow.Text = "Aktuell: ";
            // 
            // spieler1
            // 
            this.spieler1.AutoSize = true;
            this.spieler1.Location = new System.Drawing.Point(600, 160);
            this.spieler1.Name = "spieler1";
            this.spieler1.Size = new System.Drawing.Size(48, 15);
            this.spieler1.TabIndex = 3;
            this.spieler1.Text = "Spieler1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(670, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // spieler2
            // 
            this.spieler2.AutoSize = true;
            this.spieler2.Location = new System.Drawing.Point(685, 160);
            this.spieler2.Name = "spieler2";
            this.spieler2.Size = new System.Drawing.Size(48, 15);
            this.spieler2.TabIndex = 5;
            this.spieler2.Text = "Spieler2";
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonNeu);
            this.Controls.Add(this.spieler2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spieler1);
            this.Controls.Add(this.labelPlayerNow);
            this.Controls.Add(this.ButtonStartGame);
            this.Controls.Add(this.gameField);
            this.Name = "FormGame";
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox gameField;
        private Button ButtonStartGame;
        private Label labelPlayerNow;
        private Label spieler1;
        private Label label1;
        private Label spieler2;
        private Button buttonNeu;
    }
}