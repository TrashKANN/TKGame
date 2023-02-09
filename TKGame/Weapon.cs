using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    public abstract class Weapon
    {
        protected Texture2D weaponTexture;
        public Vector2 position, velocity;
        public SpriteEffects orientation;

        public Color color = Color.White;
        public int damageStat;

        public Vector2 size;

        public abstract int DamageEnemy(Weapon weapon);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(weaponTexture, position, null, color, 0, size / 2f, 1f, orientation, 0);
        }
    }
}
