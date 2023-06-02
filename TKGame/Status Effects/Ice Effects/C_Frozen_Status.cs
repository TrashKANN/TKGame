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
    public class C_Frozen_Status : IStatusComponent
    {
        public ComponentType Type => ComponentType.Frozen;
        public StatusType StatusType => StatusType.Frozen;
        public float Duration { get; set; }
        public float TickInterval { get; set; }
        public float DamagePerTick { get; set; }
        public Entity SourceEntity { get; set; }
        public float ElapsedTime { get; set; }
        public float TimeSinceLastTick { get; set; }

        public C_Frozen_Status(float duration, float tickInterval, float damagePerTick, Entity sourceEntity)
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
            
        }
        public Texture2D GetEffectTexture()
        {
            return Art.FrozenTexture;
        }

    }
}
