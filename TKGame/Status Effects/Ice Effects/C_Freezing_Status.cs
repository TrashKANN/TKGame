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
    public class C_Freezing_Status : IStatusComponent
    {
        public StatusType StatusType => throw new NotImplementedException();

        public float Duration => throw new NotImplementedException();

        public float TickInterval => throw new NotImplementedException();

        public float DamagePerTick => throw new NotImplementedException();

        public Entity SourceEntity => throw new NotImplementedException();

        public float ElapsedTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float TimeSinceLastTick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ComponentType Type => throw new NotImplementedException();

        public Texture2D GetEffectTexture()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime, Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
