using System;

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

        public PlayerData()
        {
            this.Life = Constant.Life;
            this.Character = 1;
            this.Score = 0;
            this.GunAngle = Constant.StartAngle;

            Random random = new Random();
            this.PlayerCoordinate = new Coordinate(random.Next(0, (Constant.GameAreaSizeX - Constant.PlayerSizeInGame)),
                random.Next(100, (Constant.GameAreaSizeY - Constant.PlayerSizeInGame)));
        }

        public override string ToString()
        {
            return String.Format("Player: [Name = {0}, Character = {1}, Life = {2}, PlayerCoordinate = {3}, Score: {4}, GunAngle = {5}]", 
                this.Name, this.Character, this.Life, this.PlayerCoordinate.ToString(), this.Score, this.GunAngle);
        }
    }
}