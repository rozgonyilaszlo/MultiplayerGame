using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGame.Models
{
    public class PlayerData
    {
        public string Name { get; set; }

        public int UserId {
            get {
                return String.IsNullOrWhiteSpace(Name) ? 0 : this.Name.GetHashCode();
            }
        }

        public int Character { get; set; }

        public int Life { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public PlayerData()
        {
            this.Life = Constant.Life;

            //véletlenszerűen elhelyezi a pályán
            //700 * 450-es a form
            Random random = new Random();
            this.X = random.Next(0, Constant.GameAreaSizeX);
            this.Y = random.Next(0, Constant.GameAreaSizeY);
        }
    }
}
