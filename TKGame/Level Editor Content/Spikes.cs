using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using static System.Collections.Specialized.BitVector32;

namespace TKGame.Level_Editor_Content
{
    public class Spikes : IBlock
    {
        public Rectangle HitBox { get; set; }
        public Texture2D Texture { get; set; }

        public ComponentType Type => ComponentType.Spikes;
        public string Action { get; set; }


        public Spikes(int x, int y, int width, int height, string action = "None")
        {
            HitBox = new Rectangle(x, y, width, height);
            Texture = Art.SpikesTexture;
            Action = action;
        }
        public Spikes(Rectangle rect, string action = "None")
        {
            HitBox = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            Texture = Art.SpikesTexture;
            Action = action;
        }
    }
}
