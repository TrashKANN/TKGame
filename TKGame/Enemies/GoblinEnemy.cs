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
using TKGame.Status_Effects;
using TKGame.Enemies.Goblin.Components;

namespace TKGame.Enemies
{
    public class GoblinEnemy : Enemy
    {
        /// <summary>
        /// goblin enemy components
        /// </summary>
        public GoblinEnemy()
        {
            entityTexture = Art.GoblinEnemyTexture;
            entityType = EntityType.Enemy;
            Position = new Vector2(200, 800); // hard coded spawn position at the moment
            velocity = new Vector2((float)1.5, 1);
            HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
            
            health = 80; //Base Health
            originalHealth = health; //used for displaying healthbar
            needsHealth = true;
            isEnemy = true;
            components = new Dictionary<ComponentType, List<IComponent>>
            {
                { ComponentType.Physics, new List<IComponent> { new C_GoblinEnemy_Physics() } },
                { ComponentType.Graphics, new List<IComponent> { new C_GoblinEnemy_Graphics() } }
            };
        }

        /// <summary>
        /// Update goblin enemy components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime)
        {
            //goblinEnemyPhysics.Update(this, gameTime);
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
            //goblinEnemyGraphics.Update(this/*, spriteBatch*/);
            base.Draw(spriteBatch);
        }
    }
}
