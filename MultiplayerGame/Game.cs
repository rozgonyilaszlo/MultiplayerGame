using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MultiplayerGame.Models;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();

            //player1
            this.player1.Text = Form1.playerData.Name;
            this.pictureBox1.Image = Form1.GetCharacterImage(Form1.playerData.Character);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = Form1.playerData.Life;

            //player2
            if (Form1.otherPlayerData != null)
            {
                this.player2.Text = Form1.otherPlayerData.Name;
                this.pictureBox2.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.progressBar2.Maximum = 100;
                this.progressBar2.Value = Form1.otherPlayerData.Life;
            }

            Form1.SendData(Form1.playerData.Name + " belépett a játékba.");
        }
    }
}
