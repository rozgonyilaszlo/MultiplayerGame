using System;

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
    }
}
