using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;

namespace TKGame.Content.Weapons
{
    class Sword : Weapon
    {
        static Sword instance;
        private static object syncRoot = new object();
        /// <summary>
        /// Locks single sword instance
        /// </summary>
        public static Sword Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Sword();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Private Constructor for Sword
        /// </summary>
        public Sword()
        {
    
            weaponTexture = Art.WeaponTexture;
            position = new Vector2(815, 730);
            weaponRect = new Rectangle(815, 730, 85, 85);
            damageStat = 1;
        }

        public void Update(GameTime gameTime, Vector2 position, Vector2 velocity)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            weaponTexture = Art.WeaponTexture;
            //spriteBatch.Draw(weaponTexture, position, weaponRect, color, 0, position, 0.2f, orientation, 0);
            spriteBatch.Draw(weaponTexture, weaponRect, color);
        }


        public override int DamageEnemy(Weapon weapon)
        {
            throw new NotImplementedException();
        }
    }
}
