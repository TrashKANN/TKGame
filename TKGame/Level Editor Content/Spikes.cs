using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Level_Editor_Content
{
    public class Spikes : ICollideComponent
    {
        public Rectangle HitBox { get; set; }
        public Texture2D Texture { get; private set; }

        public ComponentType Type => ComponentType.Spikes;

        public Spikes(int x, int y, int width, int height)
        {
            HitBox = new Rectangle(x, y, width, height);
            Texture = Art.SpikesTexture;
        }
        public Spikes(Rectangle rect)
        {
            HitBox = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            Texture = Art.SpikesTexture;
        }
    }
}
