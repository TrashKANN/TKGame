using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public interface GraphicsComponent
    {
        virtual public void GraphicsComponent() { }
        abstract internal void Update(Entity entity, SpriteBatch spriteBatch);
    }
}
