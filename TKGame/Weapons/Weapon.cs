using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Content.Weapons
{
    public abstract class Weapon
    {
        private static readonly int WALK_ACCELERATION = 1;
        private static readonly int JUMP_HEIGHT = -1;

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

        public void Update(GameTime gameTime)
        {
            if (Input.KeyboardState.IsKeyDown(Keys.A))
                this.velocity.X = -WALK_ACCELERATION;
            else if (Input.KeyboardState.IsKeyDown(Keys.D))
                this.velocity.X = WALK_ACCELERATION;
            else if (Input.KeyboardState.IsKeyDown(Keys.W))
                this.velocity.Y = -WALK_ACCELERATION;
            else if (Input.KeyboardState.IsKeyDown(Keys.S))
                this.velocity.Y = WALK_ACCELERATION;
        }
    }
}
