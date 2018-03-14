using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiplayerGame.Models
{
    public class Bullet
    {
        public Coordinate StartPoint { get; set; }

        public int Range { get; set; }

        public int Angle { get; set; }

        public bool IsValid { get; set; }

        public Bullet()
        {
            this.Range = 0;
            this.IsValid = true;
        }

        public override string ToString()
        {
            return String.Format("Bullet with StartPoint: {0}, Range: {1}, Angle: {2}, IsValid: {3}", this.StartPoint, this.Range, this.Angle, this.IsValid);
        }

        public void RaiseRange()
        {
            this.Range += Constant.RaiseRange;

            this.CheckValidity();
        }

        public void InValidate()
        {
            this.IsValid = false;
        }

        public Point GetPoint()
        {
            int x = Convert.ToInt32(Math.Round(Math.Cos(((Math.PI / 180) * this.Angle)) * this.Range));
            int y = Convert.ToInt32(Math.Round(Math.Sin(((Math.PI / 180) * this.Angle)) * this.Range));

            return new Point((this.StartPoint.X + Constant.HalfPlayerSizeInGame) + x, (this.StartPoint.Y + Constant.HalfPlayerSizeInGame) + y);
        }

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
