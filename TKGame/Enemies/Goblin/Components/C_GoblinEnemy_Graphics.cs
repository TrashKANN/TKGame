using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;
using TKGame.Players;


namespace TKGame.Enemies.Goblin.Components
{
    class C_GoblinEnemy_Graphics : IGraphicsComponent
    {
        public ComponentType Type => ComponentType.Graphics;

        void IGraphicsComponent.Update(Entity entity/*, SpriteBatch spriteBatch*/)
        {
            Player player = Player.Instance;

            if (player != null)
            {
                Vector2 playerPosition = player.Position;

                // logic assumes enemy starts by facing left
                if (entity.Position.X > playerPosition.X)
                {
                    entity.Orientation = SpriteEffects.None;
                }
                if (entity.Position.X < playerPosition.X)
                {
                    entity.Orientation = SpriteEffects.FlipHorizontally;
                }
            }
        }
    }
}

