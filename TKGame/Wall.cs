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

        /// <summary>
        /// Generates a (rectangular) wall that can be drawn to the screen. x/y represent position of
        /// top left corner.
        /// </summary>
        /// <param name="x">x-position of top left corner</param>
        /// <param name="y">y-position of top left corner</param>
        /// <param name="width">Wall width (x-axis)</param>
        /// <param name="height">Wall height (y-axis)</param>
        /// <param name="graphicsDevice">Graphics device used to create textures with</param>
        public Wall(int x, int y, int width, int height, GraphicsDevice graphicsDevice)
        {
            Rect = new Rectangle(x, y, width, height);
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new Color[] { Color.White });
        }

        public Wall(Rectangle rect, GraphicsDevice graphicsDevice)
        {
            Rect = rect;
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new Color[] { Color.White });
        }
    }
}
