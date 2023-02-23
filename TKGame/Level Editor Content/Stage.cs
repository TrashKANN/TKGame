using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

namespace TKGame.Level_Editor_Content
{
    public class Stage : IEnumerable<Stage>
    {
        // Mainly just used for the default dev stage
        private static readonly int screenWidth = 1600;
        private static readonly int screenHeight = 900;
        internal List<Wall> walls { get; set; }

        public Stage(GraphicsDevice graphics) 
        {
            this.walls = new List<Wall>() { };
        }

        internal Stage(List<Wall> walls, GraphicsDevice graphics) 
        {
            this.walls = walls;
        }

        public IEnumerator<Stage> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
