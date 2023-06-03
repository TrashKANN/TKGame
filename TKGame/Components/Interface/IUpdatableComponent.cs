using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public interface IUpdatableComponent : IComponent
    {
        void Update(GameTime gameTime, Entity entity);
    }
}
