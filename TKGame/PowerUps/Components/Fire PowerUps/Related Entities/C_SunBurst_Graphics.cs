using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.PowerUps.RelatedEntities
{
    public class C_SunBurst_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        void IGraphicsComponent.Update(Entity entity)
        {
            //
        }
    }
}
