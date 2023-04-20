﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.BackEnd
{
    static class Art
    {
        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D PlayerSwordTexture { get; private set; }
        public static Texture2D WeaponTexture { get; private set; }
        public static Texture2D BackgroundTexture { get; private set; }

        // "Enemy" will be the name of one such as "Goblin" since it has its own art
        public static Texture2D KnightEnemyTexture { get; private set; }
        public static Texture2D GoblinEnemyTexture { get; private set; }
        public static Texture2D PotionItemTexture {  get; private set; }
        public static Texture2D TransitionTexture { get; private set; }

        /// <summary>
        /// Loads the Player Texture from Art/Player on the Player's texture.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            PlayerTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Player"); // Will need to be modified when we create a better structure for our files.
            PlayerSwordTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Player_Sword");
            WeaponTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Weapons/IronSword"); //Sword Weapon Image    
            BackgroundTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Cobble"); //Background image
            KnightEnemyTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/KnightLeftFacing"); // knight enemy
            PotionItemTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Potion"); // potion
            LoadTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Screens/LoadSpriteSheet");
            GoblinEnemyTexture = content.Load<Texture2D>(@"Art/GoblinLeftFacing"); // goblin enemy
            TransitionTexture = content.Load<Texture2D>(@"Art/Screens/LoadSpriteSheet");

        }
    }
}
