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
                return this.Name.GetHashCode();
            }
        }

        public int Character { get; set; }
    }
}
