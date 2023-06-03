using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.PowerUps.RelatedEntities
{
    public class C_FireStep_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            entity.HitBox = new Rectangle(((int)entity.Position.X - ((int)entity.Size.X / 2)),
                                            ((int)entity.Position.Y - (int)entity.Size.Y / 2),
                                            (int)entity.Size.X,
                                            (int)entity.Size.Y);
        }
    }
}
