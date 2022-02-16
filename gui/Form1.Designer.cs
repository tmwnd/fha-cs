namespace fha_cs
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
            this.GameField = new System.Windows.Forms.GroupBox();
            this.DynamicGameField = new System.Windows.Forms.DataGridView();
            this.GameField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DynamicGameField)).BeginInit();
            this.SuspendLayout();
            // 
            // GameField
            // 
            this.GameField.Controls.Add(this.DynamicGameField);
            this.GameField.Location = new System.Drawing.Point(15, 15);
            this.GameField.Name = "GameField";
            this.GameField.Size = new System.Drawing.Size(550, 350);
            this.GameField.TabIndex = 0;
            this.GameField.TabStop = false;
            this.GameField.Text = "Spielfeld";
            this.GameField.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // DynamicGameField
            // 
            this.DynamicGameField.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DynamicGameField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DynamicGameField.Location = new System.Drawing.Point(25, 25);
            this.DynamicGameField.Name = "DynamicGameField";
            this.DynamicGameField.RowTemplate.Height = 25;
            this.DynamicGameField.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DynamicGameField.Size = new System.Drawing.Size(500, 300);
            this.DynamicGameField.TabIndex = 1;
            // 
            // Spiel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GameField);
            this.Name = "Spiel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GameField.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DynamicGameField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox GameField;
        private DataGridView DynamicGameField;
    }
}