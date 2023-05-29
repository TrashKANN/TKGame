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
        private bool isChilled; // added for applying 

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
            if (!isChilled)
            {
                // apply 80% reduction in speed for affected entity
                entity.Velocity.X *= 0.2f;
                isChilled = true;
            }
            
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastTick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime >= 10)
            {
                entity.RemoveComponent(this);
                isChilled = false;
                return;
            }

            if (TimeSinceLastTick >= TickInterval)
            {
                //IHealthComponent healthComponent = EntityManager.GetComponent<IHealthComponent>(SourceEntity);
                //healthComponent.TakeDamage(DamagePerTick);
                TimeSinceLastTick = 0f;
            }
        }
        public Texture2D GetEffectTexture()
        {
            return Art.ChilledTexture;
        }

    }
}