using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MultiplayerGame.Models;
using System.Drawing;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        private int myScore = 0;
        private int enemyScore = 0;

        public Game()
        {
            InitializeComponent();

            //ne lehessen átméretezni az ablakot
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            //player1
            if (Form1.playerData != null)
            {
                this.player1.Text = Form1.playerData.Name;
                this.pictureBox1.Image = Form1.GetCharacterImage(Form1.playerData.Character);
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = Form1.playerData.Life;
            }

            //player2
            if (Form1.otherPlayerData != null)
            {
                this.player2.Text = Form1.otherPlayerData.Name;
                this.pictureBox2.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                this.progressBar2.Maximum = 100;
                this.progressBar2.Value = Form1.otherPlayerData.Life;
            }

            label1.Text = "Eredmény: " + myScore;
            label2.Text = "Eredmény: " + enemyScore;

            player1.BringToFront();
            player2.BringToFront();

            Form1.SendData(Form1.playerData.Name + " belépett a játékba.");
        }

        public void UpdateGame()
        {
            if (Form1.playerData != null)
            {
                player1InGame.Image = Form1.GetCharacterImage(Form1.playerData.Character);
                player1InGame.Left = Form1.playerData.X;
                player1InGame.Top = Form1.playerData.Y;
                try
                {
                    this.progressBar1.Value = Form1.playerData.Life;
                }
                catch (Exception e)
                {
                    MessageBox.Show("vesztettél.", "mecs vége", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    enemyScore++;
                    label2.Text = "Eredmény: " + enemyScore;
                    RestartGame();
                }
            }

            //player2 adatainak a frissítése
            if (Form1.otherPlayerData != null)
            {
                player2InGame.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                player2InGame.Left = Form1.otherPlayerData.X;
                player2InGame.Top = Form1.otherPlayerData.Y;
                this.player2.Text = Form1.otherPlayerData.Name;
                this.pictureBox2.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                try
                {
                    this.progressBar2.Value = Form1.otherPlayerData.Life;
                }
                catch (Exception e)
                {
                    MessageBox.Show("nyertél.", "mecs vége", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    myScore++;
                    label1.Text = "Eredmény: " + myScore;
                    RestartGame();
                }
            }
        }
        
        private void RestartGame()
        {
            Form1.playerData = new PlayerData() {
                Character = Form1.playerData.Character,
                Name = Form1.playerData.Name
            };
            Form1.SendPlayerData(Form1.playerData);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                if (0 > Form1.playerData.Y)
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Top = Form1.playerData.Y - Constant.Step;
                    Form1.playerData.Y = Form1.playerData.Y - Constant.Step;
                    Form1.SendPlayerData(Form1.playerData);
                }

                Form1.SendPlayerData(Form1.playerData);
                UpdateGame();
            }
            else if (e.KeyCode == Keys.S)
            {
                if ((Constant.GameAreaSizeY - Constant.PlayerSizeInGame) <= (Form1.playerData.Y + Constant.PlayerSizeInGame))
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Top = Form1.playerData.Y + Constant.Step;
                    Form1.playerData.Y = Form1.playerData.Y + Constant.Step;
                }

                Form1.SendPlayerData(Form1.playerData);
                UpdateGame();
            }
            else if (e.KeyCode == Keys.A)
            {
                if (0 > Form1.playerData.X)
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Left = Form1.playerData.X - Constant.Step;
                    Form1.playerData.X = Form1.playerData.X - Constant.Step;
                }

                Form1.SendPlayerData(Form1.playerData);
                UpdateGame();
            }
            else if (e.KeyCode == Keys.D)
            {
                if ((Constant.GameAreaSizeX - Constant.PlayerSizeInGame) <= (Form1.playerData.X + Constant.PlayerSizeInGame))
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Left = Form1.playerData.X + Constant.Step;
                    Form1.playerData.X = Form1.playerData.X + Constant.Step;
                }

                Form1.SendPlayerData(Form1.playerData);
                UpdateGame();
            }
            else if (e.KeyCode == Keys.K)
            {
                //TODO: ezt még átgondolni
                Image img = player1InGame.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                player1InGame.Image = img;
            }
            else if (e.KeyCode == Keys.L)
            {
                //TODO: ezt még átgondolni
                Image img = player1InGame.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                player1InGame.Image = img;
            }
            else if (e.KeyCode == Keys.Space)
            {
                //TODO: ezt még átgondolni

                //fire!!!
            }
            else
            {
                Console.WriteLine("ismeretlen billentyű");
            }
        }
    }
}