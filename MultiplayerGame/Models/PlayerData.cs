using System;

namespace MultiplayerGame.Models
{
    public class PlayerData
    {
        public string Name { get; set; }
        
        public int Character { get; set; }

        public int Life { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int HintX { get; set; }

        public int HintY { get; set; }

        public PlayerData()
        {
            this.Life = Constant.Life;
            
            Random random = new Random();
            this.X = random.Next(0, (Constant.GameAreaSizeX - Constant.PlayerSizeInGame));
            this.Y = random.Next(100, (Constant.GameAreaSizeY - Constant.PlayerSizeInGame));
            this.HintY = this.Y + Constant.FireRange;
            this.HintX = (this.X + (Constant.PlayerSizeInGame / 2));
        }
    }
}