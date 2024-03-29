﻿using Microsoft.Xna.Framework;
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
    // TODO: enemies have chilled status removed and cannot move for 1.5 seconds
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
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastTick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime >= Duration)
            {
                entity.RemoveComponent(this);
                return;
            }

            if (TimeSinceLastTick >= TickInterval)
            {
                //IHealthComponent healthComponent = EntityManager.GetComponent<IHealthComponent>(SourceEntity);
                //healthComponent.TakeDamage(DamagePerTick);
                entity.health -= DamagePerTick;
                TimeSinceLastTick = 0f;
            }
        }
        public Texture2D GetEffectTexture()
        {
            // TODO: add the texture to the project
            //return Art.FrozenTexture;
            throw new NotImplementedException();
        }

    }
}
