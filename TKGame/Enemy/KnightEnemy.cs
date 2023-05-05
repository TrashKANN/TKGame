using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.BackEnd;

namespace TKGame
{
    public class KnightEnemy : Enemy
    {
        private PhysicsComponent knightEnemyPhysics = new KnightEnemy_PhysicsComponent();
        private GraphicsComponent knightEnemyGraphics = new Enemy_GraphicsComponent();

        /// <summary>
        /// knight enemy components
        /// </summary>
        public KnightEnemy() 
        {
            entityTexture = Art.KnightEnemyTexture; 
            Position = new Vector2(300, 800); // hard coded spawn position at the moment
            velocity = new Vector2((float)1.5, 1);
            HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
            health = 20; //Double health for armor
            originalHealth = health; //used for displaying healthbar
            needsHealth = true;
        }

        /// <summary>
        /// Update knight enemy components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime)
        {
            knightEnemyPhysics.Update(this, gameTime/*, world*/);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            knightEnemyGraphics.Update(this/*, spriteBatch*/);
        }
    }
}
