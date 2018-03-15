using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiplayerGame.Models
{
    public class Bullet
    {
        public Coordinate StartPoint { get; set; }

        public int Distance { get; set; }

        public int Angle { get; set; }

        public bool IsValid { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Bullet()
        {
            this.Distance = 0;
            this.IsValid = true;
        }

        public override string ToString()
        {
            return String.Format("Bullet with StartPoint: {0}, Range: {1}, Angle: {2}, IsValid: {3}", this.StartPoint, this.Distance, this.Angle, this.IsValid);
        }

        /// <summary>
        /// Increase the bullet distance from the start point.
        /// </summary>
        public void IncreaseDistance()
        {
            this.Distance += Constant.IncreaseDistance;

            this.CheckValidity();
        }

        /// <summary>
        /// Invalidate this bullet.
        /// </summary>
        public void InValidate()
        {
            this.IsValid = false;
        }

        /// <summary>
        /// Get the bullet position from the start point and distance.
        /// </summary>
        /// <returns>Returns with the momentary position of the bullet.</returns>
        public Point GetPoint()
        {
            int x = Convert.ToInt32(Math.Round(Math.Cos(((Math.PI / 180) * this.Angle)) * this.Distance));
            int y = Convert.ToInt32(Math.Round(Math.Sin(((Math.PI / 180) * this.Angle)) * this.Distance));

            return new Point((this.StartPoint.X + Constant.HalfPlayerSizeInGame) + x, (this.StartPoint.Y + Constant.HalfPlayerSizeInGame) + y);
        }
        
        /// <summary>
        /// Check the bullet is in the screen.
        /// </summary>
        public void CheckValidity()
        {
            Screen[] screens = Screen.AllScreens;

            foreach (Screen screen in screens)
            {
                if (screen.WorkingArea.Contains(this.GetPoint()))
                {
                    return;
                }
            }

            this.InValidate();
        }
    }
}
