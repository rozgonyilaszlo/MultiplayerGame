using System;
using System.Drawing;

namespace MultiplayerGame.Models
{
    public class Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            return String.Format("(X: {0}, Y: {1})", this.X, this.Y);
        }

        public static explicit operator Coordinate(Point v)
        {
            return new Coordinate(v.X, v.Y);
        }

        public static explicit operator Point(Coordinate v)
        {
            return new Point(v.X, v.Y);
        }

        internal bool IsNear(Coordinate coordinate)
        {
            int distance = (int)Math.Sqrt(Math.Pow(Math.Abs(this.X - coordinate.X), 2) + Math.Pow(Math.Abs(this.Y - coordinate.Y), 2));

            if (distance > 10)
            {
                return false;
            }

            return true;
        }
    }
}
