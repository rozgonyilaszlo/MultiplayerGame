namespace MultiplayerGame.Models
{
    public class Constant
    {
        /// <summary>
        /// Player's life.
        /// </summary>
        public static int Life = 100;

        /// <summary>
        /// Horizontal game area size.
        /// </summary>
        public static int GameAreaSizeX = 700;

        /// <summary>
        /// Vertical game area size.
        /// </summary>
        public static int GameAreaSizeY = 450;

        /// <summary>
        /// Player steps in pixel.
        /// </summary>
        public static int Step = 4;

        /// <summary>
        /// Damage from the wall.
        /// </summary>
        public static int DamageFromTheWall = 5;

        /// <summary>
        /// Damage from the gun.
        /// </summary>
        public static int DamageFromGun = 10;

        /// <summary>
        /// Player size in pixel.
        /// </summary>
        public static int PlayerSize = 32;

        /// <summary>
        /// Half player size in pixel.
        /// </summary>
        public static int HalfPlayerSizeInGame = PlayerSize / 2;

        /// <summary>
        /// Fire range.
        /// </summary>
        public static int FireRange = 25;

        /// <summary>
        /// Rotate degree.
        /// </summary>
        public static int RotateDegree = 10;

        /// <summary>
        /// Game frame rate.
        /// </summary>
        public static int FrameRate = 30;

        /// <summary>
        /// Gun start angle.
        /// </summary>
        public static int StartAngle = 90;

        /// <summary>
        /// Raise bullet range.
        /// </summary>
        public static int RaiseRange = 10;
    }
}