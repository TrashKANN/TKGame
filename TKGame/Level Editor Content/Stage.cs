using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace TKGame.Level_Editor_Content
{
    internal class Stage
    {
        private static readonly int screenWidth = 1600;
        private static readonly int screenHeight = 900;
        public List<Wall> walls { get; set; }
        public Stage(GraphicsDevice graphics) 
        { 
            this.walls = new List<Wall>()
            {
                new Wall(0, screenHeight - 40, screenWidth, 50, graphics),       // Floor 
                new Wall(0, 0, screenWidth, 50, graphics),                       // Ceiling
                new Wall(0, 0, 50, screenHeight - 250, graphics),                // Left wall
                new Wall(screenWidth - 50, 0, 50, screenHeight - 250, graphics), // Right wall
                new Wall(screenWidth / 2, screenHeight / 2, 250, 250, graphics)  // Extra wall to test collision on
            };
        }

        public Stage(List<Wall> walls, GraphicsDevice graphics) 
        {
            this.walls = walls;
        }
    }
}
