using System;
using System.Windows.Forms;
using MultiplayerGame.Models;
using System.Drawing;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        private int myScore = 0;
        private int enemyScore = 0;
        private Graphics graphics;
        private Pen pen;
        private int angle = 90;
        
        public Game()
        {
            InitializeComponent();

            //ne lehessen átméretezni az ablakot
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            pen = new Pen(Color.FromKnownColor(KnownColor.Red));
            graphics = this.CreateGraphics();
            
            if (Form1.playerData != null)
            {
                this.player1.Text = Form1.playerData.Name;
                this.pictureBox1.Image = Form1.GetCharacterImage(Form1.playerData.Character);
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = Form1.playerData.Life;
            }
            
            if (Form1.otherPlayerData != null)
            {
                this.player2.Text = Form1.otherPlayerData.Name;
                this.pictureBox2.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                this.progressBar2.Maximum = 100;
                this.progressBar2.Value = Form1.otherPlayerData.Life;
            }

            ReDrawGun();

            label1.Text = "Eredmény: " + myScore;
            label2.Text = "Eredmény: " + enemyScore;

            player1.BringToFront();
            player2.BringToFront();
        }
        
        public void UpdateGame()
        {
            if (Form1.playerData != null)
            {
                player1InGame.Image = Form1.GetCharacterImage(Form1.playerData.Character);
                player1InGame.Left = Form1.playerData.X;
                player1InGame.Top = Form1.playerData.Y;
                if (Form1.playerData.Life > 0)
                {
                    this.progressBar1.Value = Form1.playerData.Life;
                }
                else
                {
                    MessageBox.Show("Vesztettél.", "Mérközés vége.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    enemyScore++;
                    label2.Text = "Eredmény: " + enemyScore;
                    RestartGame();
                }
            }
            
            if (Form1.otherPlayerData != null)
            {
                player2InGame.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                player2InGame.Left = Form1.otherPlayerData.X;
                player2InGame.Top = Form1.otherPlayerData.Y;
                this.player2.Text = Form1.otherPlayerData.Name;
                this.pictureBox2.Image = Form1.GetCharacterImage(Form1.otherPlayerData.Character);
                if (Form1.otherPlayerData.Life > 0)
                {
                    this.progressBar2.Value = Form1.otherPlayerData.Life;
                }
                else
                {
                    MessageBox.Show("Nyertél.", "Mérközés vége.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    myScore++;
                    label1.Text = "Eredmény: " + myScore;
                    RestartGame();
                }
            }
        }
        
        private void RestartGame()
        {
            this.angle = 90;

            Form1.playerData = new PlayerData() {
                Character = Form1.playerData.Character,
                Name = Form1.playerData.Name
            };

            Form1.SendData(Form1.playerData);
        }

        //TODO: kiszervezni
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                PlayerMoved(PlayerMove.UP);
            }
            else if (e.KeyCode == Keys.S)
            {
                PlayerMoved(PlayerMove.DOWN);
            }
            else if (e.KeyCode == Keys.A)
            {
                PlayerMoved(PlayerMove.LEFT);
            }
            else if (e.KeyCode == Keys.D)
            {
                PlayerMoved(PlayerMove.RIGHT);
            }
            else if (e.KeyCode == Keys.K)
            {
                RotateTheGun(Constant.RotateGrad, Rotate.LEFT);
            }
            else if (e.KeyCode == Keys.L)
            {
                RotateTheGun(Constant.RotateGrad, Rotate.RIGHT);
            }
            else if (e.KeyCode == Keys.Space)
            {
                Fire();
            }

            Form1.SendData(Form1.playerData);
            ReDrawGun();
            UpdateGame();
        }

        private void PlayerMoved(PlayerMove playerMove)
        {
            if (playerMove == PlayerMove.UP)
            {
                if (0 > Form1.playerData.Y)
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Top = Form1.playerData.Y - Constant.Step;
                    Form1.playerData.Y = Form1.playerData.Y - Constant.Step;
                    Form1.playerData.HintY = Form1.playerData.HintY - Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.DOWN)
            {
                if ((Constant.GameAreaSizeY - Constant.PlayerSizeInGame) <= (Form1.playerData.Y + Constant.PlayerSizeInGame))
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Top = Form1.playerData.Y + Constant.Step;
                    Form1.playerData.Y = Form1.playerData.Y + Constant.Step;
                    Form1.playerData.HintY = Form1.playerData.HintY + Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.LEFT)
            {
                if (0 > Form1.playerData.X)
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Left = Form1.playerData.X - Constant.Step;
                    Form1.playerData.X = Form1.playerData.X - Constant.Step;
                    Form1.playerData.HintX = Form1.playerData.HintX - Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.RIGHT)
            {
                if ((Constant.GameAreaSizeX - Constant.PlayerSizeInGame) <= (Form1.playerData.X + Constant.PlayerSizeInGame))
                {
                    Form1.playerData.Life = Form1.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Left = Form1.playerData.X + Constant.Step;
                    Form1.playerData.X = Form1.playerData.X + Constant.Step;
                    Form1.playerData.HintX = Form1.playerData.HintX + Constant.Step;
                }
            }
        }

        private void ReDrawGun()
        {
            graphics.Clear(this.BackColor);

            if (Form1.playerData != null)
            {
                graphics.DrawLine(pen, Form1.playerData.X + (Constant.PlayerSizeInGame / 2), Form1.playerData.Y + (Constant.PlayerSizeInGame / 2), Form1.playerData.HintX, Form1.playerData.HintY);
            }
            else if (Form1.otherPlayerData != null)
            {
                graphics.DrawLine(pen, Form1.otherPlayerData.X - (Constant.PlayerSizeInGame / 2), Form1.otherPlayerData.Y - (Constant.PlayerSizeInGame / 2), Form1.otherPlayerData.HintX, Form1.otherPlayerData.HintY);
            }
        }

        private void RotateTheGun(int angle, Rotate rotate)
        {
            if (rotate == Rotate.LEFT)
            {
                this.angle -= angle;
            }
            else if (rotate == Rotate.RIGHT)
            {
                this.angle += angle;
            }

            int x = Convert.ToInt32(Math.Round(Math.Cos(((Math.PI / 180) * this.angle)) * Constant.FireRange));
            int y = Convert.ToInt32(Math.Round(Math.Sin(((Math.PI / 180) * this.angle)) * Constant.FireRange));

            Form1.playerData.HintX = (Form1.playerData.X + (Constant.PlayerSizeInGame / 2)) + x;
            Form1.playerData.HintY = (Form1.playerData.Y + (Constant.PlayerSizeInGame / 2)) + y;
        }

        private void Fire()
        {
            //ez a négyzet okozza a sebzést
            var rect = new Rectangle(new Point(Form1.playerData.HintX, Form1.playerData.HintY), new Size(Constant.HalfPlayerSizeInGame, Constant.HalfPlayerSizeInGame));
            graphics.DrawRectangle(pen, rect);
            
            if (player2InGame.Bounds.IntersectsWith(rect))
            {
                Form1.SendData(new Fire());
            }
        }
    }
}