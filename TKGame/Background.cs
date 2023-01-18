using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    internal class Background
    {
        // Variables to hold Background Image
        public Rectangle BackgroundRect { get; private set;}
        public Texture2D BackgroundTexture { get; private set; }

        public Background(int width, int height, GraphicsDevice graphicsDevice)
        {
            BackgroundRect = new Rectangle(0, 0, width, height);
            BackgroundTexture = new Texture2D(graphicsDevice, width, height);
        }
       
    }
}
