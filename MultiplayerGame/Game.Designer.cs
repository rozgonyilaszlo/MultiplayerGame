namespace MultiplayerGame
{
    partial class Game
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
            this.player1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.player2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.player1InGame = new System.Windows.Forms.PictureBox();
            this.player2InGame = new System.Windows.Forms.PictureBox();
            this.player1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.player2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1InGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2InGame)).BeginInit();
            this.SuspendLayout();
            // 
            // player1
            // 
            this.player1.Controls.Add(this.label1);
            this.player1.Controls.Add(this.progressBar1);
            this.player1.Controls.Add(this.pictureBox1);
            this.player1.Location = new System.Drawing.Point(13, 13);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(211, 59);
            this.player1.TabIndex = 0;
            this.player1.TabStop = false;
            this.player1.Text = "player1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(58, 29);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(141, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(7, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // player2
            // 
            this.player2.Controls.Add(this.label2);
            this.player2.Controls.Add(this.progressBar2);
            this.player2.Controls.Add(this.pictureBox2);
            this.player2.Location = new System.Drawing.Point(467, 13);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(211, 59);
            this.player2.TabIndex = 1;
            this.player2.TabStop = false;
            this.player2.Text = "player2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(58, 29);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(141, 23);
            this.progressBar2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(7, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // player1InGame
            // 
            this.player1InGame.Location = new System.Drawing.Point(102, 165);
            this.player1InGame.Name = "player1InGame";
            this.player1InGame.Size = new System.Drawing.Size(32, 32);
            this.player1InGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.player1InGame.TabIndex = 2;
            this.player1InGame.TabStop = false;
            // 
            // player2InGame
            // 
            this.player2InGame.Location = new System.Drawing.Point(426, 187);
            this.player2InGame.Name = "player2InGame";
            this.player2InGame.Size = new System.Drawing.Size(32, 32);
            this.player2InGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.player2InGame.TabIndex = 3;
            this.player2InGame.TabStop = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.player2InGame);
            this.Controls.Add(this.player1InGame);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.player1);
            this.Name = "Game";
            this.Text = "Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.player1.ResumeLayout(false);
            this.player1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.player2.ResumeLayout(false);
            this.player2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1InGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2InGame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox player1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox player2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox player1InGame;
        private System.Windows.Forms.PictureBox player2InGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}