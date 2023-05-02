using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.PowerUps.RelatedEntities
{
    public class C_SunBurst_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            entity.HitBox = new Rectangle(
                                0,
                                0,
                                (int)entity.HitBox.Width,
                                (int)entity.HitBox.Height);
        }
    }
}
