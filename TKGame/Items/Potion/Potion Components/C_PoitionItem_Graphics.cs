﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.World;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;

namespace TKGame.Items.Potion.Components
{
    class C_PoitionItem_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        void IGraphicsComponent.Update(Entity entity/*, SpriteBatch spriteBatch*/)
        {

        }
    }
}
