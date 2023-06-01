using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Status_Effects
{
    public class C_Shocked_Status : IStatusComponent
    {
        public ComponentType Type => ComponentType.Shocked;
        public StatusType StatusType => StatusType.Shocked;
        public float Duration { get; set; }
        public float TickInterval { get; set; }
        public float DamagePerTick { get; set; }
        public Entity SourceEntity { get; set; }
        public float ElapsedTime { get; set; }
        public float TimeSinceLastTick { get; set; }

        public C_Shocked_Status(float duration, float tickInterval, float damagePerTick, Entity sourceEntity)
        {
            Duration = duration;
            TickInterval = tickInterval;
            DamagePerTick = damagePerTick;
            SourceEntity = sourceEntity;

            ElapsedTime = 0f;
            TimeSinceLastTick = 0f;
        }
        // for now this just decrements affected entity health
        public void Update(GameTime gameTime, Entity entity)
        { 
            //ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //TimeSinceLastTick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (ElapsedTime >= Duration)
            //{
            //    entity.RemoveComponent(this);
            //    return;
            //}

            //if (TimeSinceLastTick >= TickInterval)
            //{
            //    entity.health -= DamagePerTick;
            //    TimeSinceLastTick = 0f;
            //}
        }
        public Texture2D GetEffectTexture()
        {
            return Art.ShockedTexture;
        }
    }
}
