﻿using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public interface IPhysicsComponent : IComponent
    {
        public void PhysicsComponent() { }
        public void Update(Entity entity, GameTime gameTime);
    }
}
