﻿using Microsoft.Xna.Framework;
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
    public class Fireball : Entity
    {
        private float LifeTime { get; set; }
        private float Duration { get; set; }
        private float TickInterval { get; set; }
        private float DamagePerTick { get; set; }
        private float ElapsedTime { get; set; }

        public Fireball(float lifeTime,
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

            entityTexture = Art.FireBallTexture;

            entityName = "FireBall"; // name for player class
            entityType = EntityType.PowerUp;
            Position = new Vector2(Player.Instance.HitBox.X - 150, Player.Instance.HitBox.Y);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, 185, 150);
            //Size = HitBox.Size;
        }
        
        public override void Update(GameTime gameTime)
        {
            // This is a bit silly looking but it isn't completely unorthidox
            // It assumes the type of IComponent which DOES NOT have an Update method, but the IInputComponent, etc... does.
            ConfigureHitBox();
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
            target.AddComponent(new C_Burning_Status(Duration, TickInterval, DamagePerTick, source));
        }
        private void ConfigureHitBox()
        {
            // offsets the hitbox adjacent to the player's hitbox based on the direction the player is facing
            int offset = Player.Instance.HitBox.Center.X - Player.Instance.HitBox.X;

            if (Player.Instance.isLookingLeft)
            {
                offset = offset * -1 - HitBox.Width;
            }
            HitBox = new Rectangle(Player.Instance.HitBox.Center.X + offset, Player.Instance.HitBox.Y, HitBox.Width, HitBox.Height);
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
