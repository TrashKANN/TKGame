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
using TKGame.PowerUps;
using TKGame.Status_Effects;
using TKGame.Enemies.Knight.Components;

namespace TKGame.Enemies
{
    public class KnightEnemy : Enemy
    {
        private IPhysicsComponent knightEnemyPhysics = new C_Knight_Physics();
        private IGraphicsComponent knightEnemyGraphics = new C_Enemy_Graphics();

        /// <summary>
        /// knight enemy components
        /// </summary>
        public KnightEnemy() 
        {
            entityTexture = Art.KnightEnemyTexture;
            entityType = EntityType.Enemy;
            Position = new Vector2(300, 800); // hard coded spawn position at the moment
            velocity = new Vector2((float)1.5, 1);
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
            components = new Dictionary<ComponentType, IComponent>
            {

            };
        }

        /// <summary>
        /// Update knight enemy components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime)
        {
            knightEnemyPhysics.Update(this, gameTime/*, world*/);

            // Update status effects that can update
            var statusEffects = this.components.Values.OfType<IStatusComponent>();
            foreach (IStatusComponent statusEffect in statusEffects)
            {
                statusEffect.Update(gameTime, this);
            }

            knightEnemyGraphics.Update(this/*, spriteBatch*/);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
