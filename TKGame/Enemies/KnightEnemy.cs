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
using TKGame.Enemies.Goblin.Components;

namespace TKGame.Enemies
{
    public class KnightEnemy : Enemy
    {
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

            health = 10; //Base Health
            originalHealth = health; //used for displaying healthbar
            needsHealth = true;
            isEnemy = true;
            components = new Dictionary<ComponentType, List<IComponent>>
            {
                { ComponentType.Physics, new List<IComponent> { new C_KnightEnemy_Physics() } },
                { ComponentType.Graphics, new List<IComponent> { new C_KnightEnemy_Graphics() } }
            };
        }

        /// <summary>
        /// Update knight enemy components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime)
        {
            components[ComponentType.Physics].OfType<IPhysicsComponent>().First().Update(this, gameTime);

            // Update status effects that can update
            var statusEffects = this.GetStatusEffects();
            foreach (IStatusComponent statusEffect in statusEffects)
            {
                statusEffect.Update(gameTime, this);
            }

            components[ComponentType.Graphics].OfType<IGraphicsComponent>().First().Update(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
