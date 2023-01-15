using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    internal class Wall
    {
        public Rectangle Rect { get; private set; }
        public Texture2D Texture { get; private set; }

        public Wall(int x, int y, int width, int height, GraphicsDevice graphicsDevice)
        {
            Rect = new Rectangle(x, y, width, height);
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new Color[] { Color.White });
        }
    }
}
