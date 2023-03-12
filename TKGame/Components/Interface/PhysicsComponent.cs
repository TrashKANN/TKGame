using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public interface PhysicsComponent
    {
        virtual public void PhysicsComponent() { }
        abstract internal void Update(ref Entity entity, GameTime gameTime);
    }
}
