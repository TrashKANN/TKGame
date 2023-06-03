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
    public class SunBurst : Entity
    {
        private float LifeTime { get; set; }
        private float Duration { get; set; }
        private float TickInterval { get; set; }
        private float DamagePerTick { get; set; }
        private float ElapsedTime { get; set; }

        public SunBurst(float lifeTime,
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

            entityTexture = Art.SunBurstTexture;

            entityName = "SunBurst"; // name for player class
            entityType = EntityType.PowerUp;
            Position = new Vector2(TKGame.ScreenWidth/2, TKGame.ScreenHeight - entityTexture.Height/2);
            HitBox = new Rectangle(0, 0, TKGame.ScreenWidth, TKGame.ScreenHeight*2);
            //Size = HitBox.Size;
        }
        public override void Update(GameTime gameTime)
        {
            // This is a bit silly looking but it isn't completely unorthidox
            // It assumes the type of IComponent which DOES NOT have an Update method, but the IInputComponent, etc... does.
            components[ComponentType.Physics].OfType<IPhysicsComponent>().First().Update(this, gameTime);

            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime >= LifeTime)
            {
                this.IsExpired = true;
                return;
            }

            List<Entity> entities = EntityManager.GetEntities();

            foreach (Entity e in entities)
            {
                if (e != this && HitBox.Intersects(e.HitBox) && e != Player.Instance)
                {
                    OnHit(this, e);
                }
            }

            components[ComponentType.Graphics].OfType<IGraphicsComponent>().First().Update(this);
        }

        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Scorched_Status(Duration, TickInterval, DamagePerTick, source));
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
