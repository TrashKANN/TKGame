using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TKGame.Level_Editor_Content
{
    internal class Map
    {
        private static List<Stage> stages { get; set; }

        public Map(Stage devStage)
        {
            stages = new List<Stage>(devStage);
        }
    }
}
