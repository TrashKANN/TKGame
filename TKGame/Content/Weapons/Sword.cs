using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Sword()
        {
            weaponTexture = Art.WeaponTexture;
            position = new Vector2(0, 0);
            
        }

        public void Update(GameTime gameTime, Vector2 position, Vector2 velocity)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(weaponTexture, position, null, color, 0, size / 4f, 1f, orientation, 0);
        }


        public override int DamageEnemy(Weapon weapon)
        {
            throw new NotImplementedException();
        }
    }
}
