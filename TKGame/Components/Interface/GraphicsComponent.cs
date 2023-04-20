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
        public void GraphicsComponent() { }
        public void Update(Entity entity);
    }
}
