using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    // concrete enemy type Goblin
    public class Goblin : IEnemies
    {
        // IEnemies method to get this enemy type
        public void GetEnemy()
        {
            
        }

        private Goblin() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
