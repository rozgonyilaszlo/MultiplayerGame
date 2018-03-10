using System;

namespace MultiplayerGame.Models
{
    public class PlayerData
    {
        public string Name { get; set; }
        
        public int Character { get; set; }

        public int Life { get; set; }

        public Coordinate PlayerCoordinate { get; set; }

        public Coordinate HintCoordinate { get; set; }
        
        public PlayerData()
        {
            this.Life = Constant.Life;
            this.Character = 1;

            Random random = new Random();
            this.PlayerCoordinate = new Coordinate(random.Next(0, (Constant.GameAreaSizeX - Constant.PlayerSizeInGame)),
                random.Next(100, (Constant.GameAreaSizeY - Constant.PlayerSizeInGame)));

            this.HintCoordinate = new Coordinate(this.PlayerCoordinate.X + Constant.HalfPlayerSizeInGame, this.PlayerCoordinate.Y + Constant.FireRange);
        }

        public override string ToString()
        {
            return String.Format("Player: [Name = {0}, Character = {1}, Life = {2}, PlayerCoordinate = {3}, HintCoordinate = {4}]", 
                this.Name, this.Character, this.Life, this.PlayerCoordinate.ToString(), this.HintCoordinate.ToString());
        }
    }
}