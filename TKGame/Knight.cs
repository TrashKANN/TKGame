using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    // concrete enemy type Knight
    public class Knight : IEnemies
    {
        // IEnemies method to get this enemy type
        public void GetEnemy()
        {
            
        }

        private Knight() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
