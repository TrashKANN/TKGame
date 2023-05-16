using Microsoft.Xna.Framework;
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

// graphics component class for ice item
namespace TKGame.Items.Ice.Components
{
    class C_IceItem_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        void IGraphicsComponent.Update(Entity entity/*, SpriteBatch spriteBatch*/)
        {

        }
    }
}
