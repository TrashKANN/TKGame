using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.PowerUps.RelatedEntities
{
    public class C_FireBall_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            entity.Position.X = entity.HitBox.X + entity.HitBox.Width/2;
            entity.Position.Y = entity.HitBox.Y + entity.HitBox.Height/2;
        }
    }
}
