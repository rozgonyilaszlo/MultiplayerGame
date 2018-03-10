using System;
using System.Windows.Forms;
using MultiplayerGame.Models;
using System.Drawing;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        private Form1 parent;

        private int myScore = 0;
        private int enemyScore = 0;
        private Graphics graphics;
        private Pen pen;
        private int angle = 90;
        
        public Game(Form1 parent)
        {
            InitializeComponent();

            this.parent = parent;

            //ne lehessen átméretezni az ablakot
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            pen = new Pen(Color.FromKnownColor(KnownColor.Red));
            graphics = this.CreateGraphics();
            
            if (parent.playerData != null)
            {
                this.player1.Text = parent.playerData.Name;
                this.pictureBox1.Image = parent.GetCharacterImage(parent.playerData.Character);
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = parent.playerData.Life;
            }
            
            if (parent.otherPlayerData != null)
            {
                this.player2.Text = parent.otherPlayerData.Name;
                this.pictureBox2.Image = parent.GetCharacterImage(parent.otherPlayerData.Character);
                this.progressBar2.Maximum = 100;
                this.progressBar2.Value = parent.otherPlayerData.Life;
            }

            ReDrawGun();

            label1.Text = "Eredmény: " + myScore;
            label2.Text = "Eredmény: " + enemyScore;

            player1.BringToFront();
            player2.BringToFront();
        }
        
        public void UpdateGame()
        {
            if (parent.playerData != null)
            {
                player1InGame.Image = parent.GetCharacterImage(parent.playerData.Character);
                player1InGame.Left = parent.playerData.PlayerCoordinate.X;
                player1InGame.Top = parent.playerData.PlayerCoordinate.Y;
                if (parent.playerData.Life > 0)
                {
                    this.progressBar1.Value = parent.playerData.Life;
                }
                else
                {
                    MessageBox.Show("Vesztettél.", "Mérközés vége.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    enemyScore++;
                    label2.Text = "Eredmény: " + enemyScore;
                    RestartGame();
                }
            }
            
            if (parent.otherPlayerData != null)
            {
                player2InGame.Image = parent.GetCharacterImage(parent.otherPlayerData.Character);
                player2InGame.Left = parent.otherPlayerData.PlayerCoordinate.X;
                player2InGame.Top = parent.otherPlayerData.PlayerCoordinate.Y;
                this.player2.Text = parent.otherPlayerData.Name;
                this.pictureBox2.Image = parent.GetCharacterImage(parent.otherPlayerData.Character);
                if (parent.otherPlayerData.Life > 0)
                {
                    this.progressBar2.Value = parent.otherPlayerData.Life;
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

            parent.playerData = new PlayerData() {
                Character = parent.playerData.Character,
                Name = parent.playerData.Name
            };

            parent.SendData(parent.playerData);
        }
        
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

            parent.SendData(parent.playerData);
            ReDrawGun();
            UpdateGame();
        }

        private void PlayerMoved(PlayerMove playerMove)
        {
            if (playerMove == PlayerMove.UP)
            {
                if (0 > parent.playerData.PlayerCoordinate.Y)
                {
                    parent.playerData.Life = parent.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Top = parent.playerData.PlayerCoordinate.Y - Constant.Step;
                    parent.playerData.PlayerCoordinate.Y = parent.playerData.PlayerCoordinate.Y - Constant.Step;
                    parent.playerData.HintCoordinate.Y = parent.playerData.HintCoordinate.Y - Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.DOWN)
            {
                if ((Constant.GameAreaSizeY - Constant.PlayerSizeInGame) <= (parent.playerData.PlayerCoordinate.Y + Constant.PlayerSizeInGame))
                {
                    parent.playerData.Life = parent.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Top = parent.playerData.PlayerCoordinate.Y + Constant.Step;
                    parent.playerData.PlayerCoordinate.Y = parent.playerData.PlayerCoordinate.Y + Constant.Step;
                    parent.playerData.HintCoordinate.Y = parent.playerData.HintCoordinate.Y + Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.LEFT)
            {
                if (0 > parent.playerData.PlayerCoordinate.X)
                {
                    parent.playerData.Life = parent.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Left = parent.playerData.PlayerCoordinate.X - Constant.Step;
                    parent.playerData.PlayerCoordinate.X = parent.playerData.PlayerCoordinate.X - Constant.Step;
                    parent.playerData.HintCoordinate.X = parent.playerData.HintCoordinate.X - Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.RIGHT)
            {
                if ((Constant.GameAreaSizeX - Constant.PlayerSizeInGame) <= (parent.playerData.PlayerCoordinate.X + Constant.PlayerSizeInGame))
                {
                    parent.playerData.Life = parent.playerData.Life - Constant.DamageFromTheWall;
                }
                else
                {
                    player1InGame.Left = parent.playerData.PlayerCoordinate.X + Constant.Step;
                    parent.playerData.PlayerCoordinate.X = parent.playerData.PlayerCoordinate.X + Constant.Step;
                    parent.playerData.HintCoordinate.X = parent.playerData.HintCoordinate.X + Constant.Step;
                }
            }
        }

        private void ReDrawGun()
        {
            graphics.Clear(this.BackColor);

            if (parent.playerData != null)
            {
                graphics.DrawLine(pen, parent.playerData.PlayerCoordinate.X + (Constant.PlayerSizeInGame / 2), parent.playerData.PlayerCoordinate.Y + (Constant.PlayerSizeInGame / 2), parent.playerData.HintCoordinate.X, parent.playerData.HintCoordinate.Y);
            }
            else if (parent.otherPlayerData != null)
            {
                graphics.DrawLine(pen, parent.otherPlayerData.PlayerCoordinate.X - (Constant.PlayerSizeInGame / 2), parent.otherPlayerData.PlayerCoordinate.Y - (Constant.PlayerSizeInGame / 2), parent.otherPlayerData.HintCoordinate.X, parent.otherPlayerData.HintCoordinate.Y);
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

            parent.playerData.HintCoordinate.X = (parent.playerData.PlayerCoordinate.X + (Constant.PlayerSizeInGame / 2)) + x;
            parent.playerData.HintCoordinate.Y = (parent.playerData.PlayerCoordinate.Y + (Constant.PlayerSizeInGame / 2)) + y;
        }

        private void Fire()
        {
            //ez a négyzet okozza a sebzést
            var rect = new Rectangle(new Point(parent.playerData.HintCoordinate.X, parent.playerData.HintCoordinate.Y), new Size(Constant.HalfPlayerSizeInGame, Constant.HalfPlayerSizeInGame));
            graphics.DrawRectangle(pen, rect);
            
            if (player2InGame.Bounds.IntersectsWith(rect))
            {
                parent.SendData(new Fire());
            }
        }
    }
}