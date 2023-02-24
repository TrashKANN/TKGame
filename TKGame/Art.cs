using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame
{
    static class Art
    {
        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D BackgroundTexture { get; private set; } 
        // "Enemy" will be the name of one such as "Goblin" since it has its own art
        public static Texture2D EnemyTexture { get; private set; }
        public static Texture2D ItemTexture {  get; private set; }

        /// <summary>
        /// Loads the Player Texture from Art/Player on the Player's texture.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            PlayerTexture = content.Load<Texture2D>(@"Art/Player"); // Will need to be modified when we create a better structure for our files.
            BackgroundTexture = content.Load<Texture2D>(@"Art/Cobble"); //Background image
            EnemyTexture = content.Load<Texture2D>(@"Art/DoomguyRightFacing"); // enemy as doomguy
            ItemTexture = content.Load<Texture2D>(@"Art/DiamondSwordLeftTilt"); // item as diamond sword 
        }
    }
}
