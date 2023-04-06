using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Content.Weapons
{
    public abstract class Weapon
    {
        protected Texture2D weaponTexture;
        public Vector2 position, velocity;
        public Rectangle weaponRect;
        public SpriteEffects orientation;

        public Color color = Color.White;
        public int damageStat;

        public Vector2 size;
        /// <summary>
        /// Functionm to Damage Enemy
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public abstract int DamageEnemy(Weapon weapon);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
