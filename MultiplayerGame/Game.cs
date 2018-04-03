using System;
using System.Windows.Forms;
using MultiplayerGame.Models;
using System.Drawing;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        public PlayerData Me;
        public PlayerData Enemy;
        private Form1 parent;
        private bool isPlayerDataDisplayed;
        private int myPoint = 0;
        private int enemyPoint = 0;

        public Game(Form1 parent, PlayerData me)
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.parent = parent;
            this.Me = me;

            timer1.Interval = Convert.ToInt32((1.0 / Constant.FrameRate) * 1000);
            timer1.Start();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            if (Me != null)
            {
                player1.Text = Me.Name;
                pictureBox1.Image = parent.GetCharacterImage(Me.Character);
                progressBar1.Value = Me.Life;
            }

            if (Enemy != null)
            {
                player2.Text = Enemy.Name;
                pictureBox2.Image = parent.GetCharacterImage(Enemy.Character);
                progressBar2.Value = Enemy.Life;
            }

            player1.BringToFront();
            player2.BringToFront();
        }

        //Draw the game.
        protected override void OnPaint(PaintEventArgs e)
        {
            //update data bar
            if (!isPlayerDataDisplayed)
            {
                if (Me != null)
                {
                    player1.Text = Me.Name;
                    pictureBox1.Image = parent.GetCharacterImage(Me.Character);
                    progressBar1.Value = Me.Life;
                }
                if (Enemy != null)
                {
                    player2.Text = Enemy.Name;
                    pictureBox2.Image = parent.GetCharacterImage(Enemy.Character);
                    progressBar2.Value = Enemy.Life;

                    isPlayerDataDisplayed = true;
                }
            }
            else
            {
                try
                {
                    if (Me.Life > 0)
                    {
                        progressBar1.Value = Me.Life;
                    }
                    else
                    {
                        enemyPoint++;
                        enemyScore.Text = enemyPoint.ToString();
                        RestartGame();
                    }

                    if (Enemy.Life > 0)
                    {
                        progressBar2.Value = Enemy.Life;
                    }
                    else
                    {
                        myPoint++;
                        myScore.Text = myPoint.ToString();
                        RestartGame();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Hiba keletkezett: {0}", exception.Message);
                }
            }

            //update graphics
            Graphics g = e.Graphics;

            Bitmap me = new Bitmap(parent.GetCharacterImage(Me.Character));

            Rectangle meCompression = new Rectangle(Me.PlayerCoordinate.X, Me.PlayerCoordinate.Y,
               Constant.PlayerSize, Constant.PlayerSize);

            g.DrawImage(me, 10, 10);
            g.DrawImage(me, meCompression);

            Point gun = Me.GetTheGunPoint();
            g.DrawLine(new Pen(Color.Red, 1), Me.PlayerCoordinate.X + Constant.HalfPlayerSizeInGame,
                Me.PlayerCoordinate.Y + Constant.HalfPlayerSizeInGame, gun.X, gun.Y);

            //draw bullets
            if (Me.Bullets.Count > 0)
            {
                try
                {
                    foreach (var bullet in Me.Bullets)
                    {
                        if (bullet.IsValid)
                        {
                            g.DrawImage(Properties.Resources.bullet, bullet.GetPoint());
                            bullet.IncreaseDistance();
                        }
                        else
                        {
                            Me.Bullets.Remove(bullet);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            if (Enemy != null)
            {
                Bitmap enemy = new Bitmap(parent.GetCharacterImage(Enemy.Character));

                Rectangle enemyCompression = new Rectangle(Enemy.PlayerCoordinate.X, Enemy.PlayerCoordinate.Y,
                   Constant.PlayerSize, Constant.PlayerSize);

                g.DrawImage(enemy, 10, 10);
                g.DrawImage(enemy, enemyCompression);

                if (Enemy.Bullets.Count > 0)
                {
                    foreach (var bullet in Enemy.Bullets)
                    {
                        if (bullet.IsValid)
                        {
                            Point bulletPoint = bullet.GetPoint();

                            //draw bullets
                            g.DrawImage(Properties.Resources.bullet, bulletPoint);
                            bullet.IncreaseDistance();

                            //check that the bullet is close
                            if (Me.PlayerCoordinate.IsNear((Coordinate)bulletPoint))
                            {
                                Me.Life -= Constant.DamageFromGun;
                                bullet.InValidate();
                            }
                        }
                    }
                }


            }

            parent.SendData(Me);
        }

        /// <summary>
        /// Restart the game.
        /// </summary>
        private void RestartGame()
        {
            timer1.Start();

            Me = new PlayerData()
            {
                Character = Me.Character,
                Name = Me.Name
            };

            parent.SendData(Me);
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
                Me.GunAngle += Constant.RotateDegree;
            }
            else if (e.KeyCode == Keys.L)
            {
                Me.GunAngle -= Constant.RotateDegree;
            }
            else if (e.KeyCode == Keys.Space)
            {
                Shot();
            }
        }

        //Move the player.
        private void PlayerMoved(PlayerMove playerMove)
        {
            if (playerMove == PlayerMove.UP)
            {
                if (0 > Me.PlayerCoordinate.Y)
                {
                    Me.Life -= Constant.DamageFromTheWall;
                }
                else
                {
                    Me.PlayerCoordinate.Y -= Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.DOWN)
            {
                if ((Constant.GameAreaSizeY - Constant.PlayerSize) <= (Me.PlayerCoordinate.Y + Constant.PlayerSize))
                {
                    Me.Life -= Constant.DamageFromTheWall;
                }
                else
                {
                    Me.PlayerCoordinate.Y += Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.LEFT)
            {
                if (0 > Me.PlayerCoordinate.X)
                {
                    Me.Life -= Constant.DamageFromTheWall;
                }
                else
                {
                    Me.PlayerCoordinate.X -= Constant.Step;
                }
            }
            else if (playerMove == PlayerMove.RIGHT)
            {
                if ((Constant.GameAreaSizeX - Constant.PlayerSize) <= (Me.PlayerCoordinate.X + Constant.PlayerSize))
                {
                    Me.Life -= Constant.DamageFromTheWall;
                }
                else
                {
                    Me.PlayerCoordinate.X += Constant.Step;
                }
            }
        }

        /// <summary>
        /// Shot with the gun.
        /// </summary>
        private void Shot()
        {
            Me.Bullets.Add(new Bullet()
            {
                StartPoint = Me.PlayerCoordinate,
                Angle = Me.GunAngle
            });
        }

        //Invalidate the form to redraw.
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}