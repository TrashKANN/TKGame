using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.BackEnd
{
    static class Art
    {
        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D PlayerSwordTexture { get; private set; }
        public static Texture2D PlayerSpearTexture { get; private set; }
        public static Texture2D PlayerAxeTexture { get; private set; }
        public static Texture2D SwordTexture { get; private set; }
        public static Texture2D SpearTexture { get; private set; }
        public static Texture2D AxeTexture {get; private set; }
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
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 4306399 (Added New Weapons, removed code weapons from TKGame and moved them to player, fixed addresses)
            PlayerTexture = content.Load<Texture2D>(@"Art/Player"); // Will need to be modified when we create a better structure for our files.
            PlayerSwordTexture = content.Load<Texture2D>(@"Art/Player_Sword");
            PlayerSpearTexture = content.Load<Texture2D>(@"Art/Player_Spear");
            PlayerAxeTexture = content.Load<Texture2D>(@"Art/Player_Axe");
            SwordTexture = content.Load<Texture2D>(@"Art/Weapons/IronSword"); //Sword Weapon Image    
            SpearTexture = content.Load<Texture2D>(@"Art/Weapons/Spear"); //Spear Weapon Image    
            AxeTexture = content.Load<Texture2D>(@"Art/Weapons/Axe"); //Axe Weapon Image    
            BackgroundTexture = content.Load<Texture2D>(@"Art/Cobble"); //Background image
            KnightEnemyTexture = content.Load<Texture2D>(@"Art/KnightLeftFacing"); // knight enemy
            PotionItemTexture = content.Load<Texture2D>(@"Art/Potion"); // potion
            LoadTexture = content.Load<Texture2D>(@"Art/Screens/LoadSpriteSheet");
<<<<<<< HEAD
=======
            PlayerTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Player"); // Will need to be modified when we create a better structure for our files.
            PlayerSwordTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Player_Sword");
            WeaponTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Weapons/IronSword"); //Sword Weapon Image    
            BackgroundTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Cobble"); //Background image
            KnightEnemyTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/KnightLeftFacing"); // knight enemy
            PotionItemTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Potion"); // potion
            LoadTexture = content.Load<Texture2D>(@"C:/Users/Greywater/Documents/Git/TKGame/TKGame/Content/bin/DesktopGL/Art/Screens/LoadSpriteSheet");
            GoblinEnemyTexture = content.Load<Texture2D>(@"Art/GoblinLeftFacing"); // goblin enemy
            TransitionTexture = content.Load<Texture2D>(@"Art/Screens/LoadSpriteSheet");

>>>>>>> 95acb0dfc3575b29374f3fbff7e8f8493a6446c9
=======
>>>>>>> 4306399 (Added New Weapons, removed code weapons from TKGame and moved them to player, fixed addresses)
        }
    }
}
