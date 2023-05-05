using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.Players;
using TKGame.Status_Effects;

namespace TKGame.PowerUps.RelatedEntities
{
    public class FireStep : Entity
    {
        // Uses passed values instead of constants
        //private readonly float BURNING_DURATION = 8f;
        //private readonly float BURNING_TICK_INTERVAL = 0.5f;
        //private readonly float BURNING_DAMAGE_PER_TICK = 1.5f;
        
        private float LifeTime { get; set; }
        private float Duration { get; set; }
        private float TickInterval { get; set; }
        private float DamagePerTick { get; set; }
        private float ElapsedTime { get; set; }
        private float TimeSinceLastTick { get; set; }

        public FireStep(float lifeTime,
                        float duration, 
                        float tickInterval, 
                        float damagePerTick, 
                        IPhysicsComponent physics_,
                        IGraphicsComponent graphics_)
        {
            components = new Dictionary<ComponentType, List<IComponent>>
            {
                { ComponentType.Physics,        new List<IComponent> { physics_ } },
                { ComponentType.Graphics,       new List<IComponent> { graphics_ } },
            };

            LifeTime = lifeTime;
            Duration = duration;
            TickInterval = tickInterval;
            DamagePerTick = damagePerTick;

            entityTexture = Art.BurningTexture;

            entityName = "FireStep"; // name for player class
            entityType = EntityType.PowerUp;
            Position = new Vector2();
            HitBox = new Rectangle();
        }
        public override void Update(GameTime gameTime)
        {
            // This is a bit silly looking but it isn't completely unorthidox
            // It assumes the type of IComponent which DOES NOT have an Update method, but the IInputComponent, etc... does.
            components[ComponentType.Physics].OfType<IPhysicsComponent>().First().Update(this, gameTime);

            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastTick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime >= LifeTime)
            {
                this.IsExpired = true;
                return;
            }

            if (TimeSinceLastTick >= TickInterval)
            {

                List<Entity> entities = EntityManager.GetEntities();

                foreach (Entity e in entities)
                {
                    if (e != this && HitBox.Intersects(e.HitBox) && e != Player.Instance)
                    {
                        OnHit(this, e);
                    }
                }
                TimeSinceLastTick = 0f;
            }


            components[ComponentType.Graphics].OfType<IGraphicsComponent>().First().Update(this);
        }

        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Burning_Status(Duration, TickInterval, DamagePerTick, source));
        }

        /// <summary>
        /// Draws each Player Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
