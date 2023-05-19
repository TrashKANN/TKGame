using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TKGame.BackEnd
{
    static class Art
    {
        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D PlayerRightCrouch { get; private set; }
        public static Texture2D PlayerSwordTexture { get; private set; }
        public static Texture2D PlayerSpearTexture { get; private set; }
        public static Texture2D PlayerAxeTexture { get; private set; }
        public static Texture2D SwordTexture { get; private set; }
        public static Texture2D RevSwordTexture { get; private set; }
        public static Texture2D SpearTexture { get; private set; }
        public static Texture2D RevSpearTexture { get; private set; }
        public static Texture2D AxeTexture {get; private set; }
        public static Texture2D RevAxeTexture { get; private set; }
        public static Texture2D BlankTexture { get; private set; }
        //Background Textures
        public static Texture2D BackgroundTexture1 { get; private set; }
        public static Texture2D BackgroundTexture2 { get; private set; }
        public static Texture2D BackgroundTexture3 { get; private set; }

        // "Enemy" will be the name of one such as "Goblin" since it has its own art
        public static Texture2D KnightEnemyTexture { get; private set; }
        public static Texture2D GoblinEnemyTexture { get; private set; }
        public static Texture2D PotionItemTexture {  get; private set; }
        public static Texture2D TransitionTexture { get; private set; }
        public static Texture2D WeaponTexture { get; private set; }
        public static Texture2D LoadTexture { get; private set; }

        #region Status Effects
        public static Texture2D BurningTexture { get; private set; }
        public static Texture2D ScorchedTexture { get; private set; }
        public static Texture2D SunBurstTexture { get; private set; }
        public static Texture2D FireBallTexture { get; private set; }
        public static Texture2D ChilledTexture { get; private set; }
        public static Texture2D FrozenTexture { get; private set; }
        public static Texture2D ShockedTexture { get; private set; }
        #endregion
        /// <summary>
        /// Loads the Player Texture from Art/Player on the Player's texture.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {

            PlayerTexture = content.Load<Texture2D>(@"Art/Player");                         // Will need to be modified when we create a better structure for our files.
            PlayerRightCrouch = content.Load<Texture2D>(@"Art/PlayerCrouchingRightFacing"); // player crouching facing right
            PlayerSwordTexture = content.Load<Texture2D>(@"Art/Player_Sword");              // player holding sword
            WeaponTexture = content.Load<Texture2D>(@"Art/Weapons/IronSword");              // current weapon image
            SwordTexture = content.Load<Texture2D>(@"Art/Weapons/IronSword");                // Sword Weapon images
            RevSwordTexture = content.Load<Texture2D>(@"Art/Weapons/IronSwordRev");
            SpearTexture = content.Load<Texture2D>(@"Art/Weapons/Spear");                       // Spear Weapon images
            RevSpearTexture = content.Load<Texture2D>(@"Art/Weapons/SpearRev"); 
            AxeTexture = content.Load<Texture2D>(@"Art/Weapons/Axe");                        // Axe Weapon Images
            RevAxeTexture = content.Load<Texture2D>(@"Art/Weapons/AxeRev");
            BlankTexture = content.Load<Texture2D>(@"Art/Weapons/blank");                    //Blank Weapon Image
            BackgroundTexture1 = content.Load<Texture2D>(@"Art/Cobble");                     // Background images
            BackgroundTexture2 = content.Load<Texture2D>(@"Art/Ruins");
            BackgroundTexture3 = content.Load<Texture2D>(@"Art/Dungeon");
            KnightEnemyTexture = content.Load<Texture2D>(@"Art/KnightLeftFacing");          // knight enemy
            PotionItemTexture = content.Load<Texture2D>(@"Art/Potion");                     // potion item
            LoadTexture = content.Load<Texture2D>(@"Art/Screens/LoadSpriteSheet");          
            GoblinEnemyTexture = content.Load<Texture2D>(@"Art/GoblinLeftFacing");          // goblin enemy
            TransitionTexture = content.Load<Texture2D>(@"Art/Screens/LoadSpriteSheet");
            BurningTexture = content.Load<Texture2D>(@"Art/BurningSprite");
            ScorchedTexture = content.Load<Texture2D>(@"Art/ScorchedSprite");
            SunBurstTexture = content.Load<Texture2D>(@"Art/SunBurstSprite");
            FireBallTexture = content.Load<Texture2D>(@"Art/FireBallSprite");

            //ChilledTexture = content.Load<Texture2D>(@"Art/ChilledSprite");
            //FrozenTexture = content.Load<Texture2D>(@"Art/FrozenSprite");
            //ShockedTexture = content.Load<Texture2D>(@"Art/ShockedSprite");
        }

        public static Texture2D CombineTextures(Texture2D texture1, Texture2D texture2)
        {
            int width = Math.Max(texture1.Width, texture2.Width);
            int height = Math.Max(texture1.Height, texture2.Height);

            float scaleX = (float)width / texture2.Width;
            float scaleY = (float)height / texture2.Height;
            float scale = Math.Min(scaleX, scaleY);

            int scaledWidth = (int)(texture2.Width * scale);
            int scaledHeight = (int)(texture2.Height * scale);

            Color[] pixels1 = new Color[texture1.Width * texture1.Height];
            texture1.GetData(pixels1);

            Color[] pixels2 = new Color[texture2.Width * texture2.Height];
            texture2.GetData(pixels2);

            Texture2D scaledTexture2 = new Texture2D(texture2.GraphicsDevice, scaledWidth, scaledHeight);
            Color[] scaledPixels2 = new Color[scaledWidth * scaledHeight];

            for (int y = 0; y < scaledHeight; y++)
            {
                for (int x = 0; x < scaledWidth; x++)
                {
                    int index = y * scaledWidth + x;

                    int originalX = (int)(x / scale);
                    int originalY = (int)(y / scale);
                    int originalIndex = originalY * texture2.Width + originalX;

                    scaledPixels2[index] = (originalX < texture2.Width && originalY < texture2.Height) ? pixels2[originalIndex] : Color.Transparent;
                }
            }

            scaledTexture2.SetData(scaledPixels2);

            Color[] resultPixels = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    Color color1 = (x < texture1.Width && y < texture1.Height) ? pixels1[y * texture1.Width + x] : Color.Transparent;
                    Color color2 = (x < scaledWidth && y < scaledHeight) ? scaledPixels2[y * scaledWidth + x] : Color.Transparent;

                    resultPixels[index] = Color.Lerp(color1, color2, color2.A / 255f);
                }
            }

            Texture2D resultTexture = new Texture2D(texture1.GraphicsDevice, width, height);
            resultTexture.SetData(resultPixels);

            return resultTexture;
        }

    }
}
