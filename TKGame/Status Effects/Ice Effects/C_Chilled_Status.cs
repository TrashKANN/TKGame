using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Enemies;

namespace TKGame.Status_Effects
{
    public class C_Chilled_Status : IStatusComponent
    {
        public ComponentType Type => ComponentType.Chilled;
        public StatusType StatusType => StatusType.Chilled;
        public float Duration { get; set; }
        public float TickInterval { get; set; }
        public float DamagePerTick { get; set; }
        public Entity SourceEntity { get; set; }
        public float ElapsedTime { get; set; }
        public float TimeSinceLastTick { get; set; }

        public C_Chilled_Status(float duration, float tickInterval, float damagePerTick, Entity sourceEntity)
        {
            Duration = duration;
            TickInterval = tickInterval;
            DamagePerTick = damagePerTick;
            SourceEntity = sourceEntity;

            ElapsedTime = 0f;
            TimeSinceLastTick = 0f;
        }
        public void Update(GameTime gameTime, Entity entity)
        {
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastTick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime >= Duration)
            {
                entity.RemoveComponent(this);
                return;
            }
            else
            {
                // hacky way to simulate entity losing movement speed
                // only works for x plane movements and can be janky sometimes
                if (entity.Orientation == SpriteEffects.FlipHorizontally)
                {
                    entity.Position.X = entity.Position.X - 0.01f;
                }
                if (entity.Orientation == SpriteEffects.None)
                {
                    entity.Position.X = entity.Position.X + 0.01f;
                }
            }
        }
        public Texture2D GetEffectTexture()
        {
            return Art.ChilledTexture;
        }

    }
}