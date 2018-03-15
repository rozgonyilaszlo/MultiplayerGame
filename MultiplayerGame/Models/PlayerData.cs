using System;
using System.Collections.Generic;
using System.Drawing;

namespace MultiplayerGame.Models
{
    public class PlayerData
    {
        public string Name { get; set; }
        
        public int Character { get; set; }

        public int Life { get; set; }

        public Coordinate PlayerCoordinate { get; set; }
        
        public int Score { get; set; }

        public int GunAngle { get; set; }

        public List<Bullet> Bullets { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerData()
        {
            this.Life = Constant.Life;
            this.Character = 1;
            this.Score = 0;
            this.GunAngle = Constant.StartAngle;
            this.Bullets = new List<Bullet>();

            Random random = new Random();
            this.PlayerCoordinate = new Coordinate(random.Next(0, (Constant.GameAreaSizeX - Constant.PlayerSize)),
                random.Next(100, (Constant.GameAreaSizeY - Constant.PlayerSize)));
        }
        
        public override string ToString()
        {
            return String.Format("Player: [Name = {0}, Character = {1}, Life = {2}, PlayerCoordinate = {3}, Score: {4}, GunAngle = {5}, Bullets count = {6}]", 
                this.Name, this.Character, this.Life, this.PlayerCoordinate.ToString(), this.Score, this.GunAngle, this.Bullets.Count);
        }

        /// <summary>
        /// Get the gun point.
        /// </summary>
        /// <returns>Returns with the point where the gun points.</returns>
        public Point GetTheGunPoint()
        {
            int x = Convert.ToInt32(Math.Round(Math.Cos(((Math.PI / 180) * this.GunAngle)) * Constant.FireRange));
            int y = Convert.ToInt32(Math.Round(Math.Sin(((Math.PI / 180) * this.GunAngle)) * Constant.FireRange));

            return new Point((this.PlayerCoordinate.X + Constant.HalfPlayerSizeInGame) + x, (this.PlayerCoordinate.Y + Constant.HalfPlayerSizeInGame) + y);
        }
    }
}