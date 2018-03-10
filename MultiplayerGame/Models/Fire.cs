namespace MultiplayerGame.Models
{
    //NOTE: akkor küldjük, ha a lövés sikeres volt. az ellenfél elszenvedi a lövést a constant osztály alapján

    public class Fire
    {
        public bool Shoted { get; set; }

        public Fire()
        {
            this.Shoted = true;
        }
    }
}