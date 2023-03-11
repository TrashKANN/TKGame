using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TKGame.Components.Concrete
{
    public class InputComponent
    {
        private static readonly int WALK_ACCELERATION = 1;
        internal void Update(Player player)
        {
            if (Input.KeyboardState.IsKeyDown(Keys.A))
                player.Velocity.X = -WALK_ACCELERATION;

            else if (Input.KeyboardState.IsKeyDown(Keys.D))
                player.Velocity.X = WALK_ACCELERATION;

            else if (Input.KeyboardState.IsKeyDown(Keys.W))
                player.Velocity.Y = -WALK_ACCELERATION;

            else if (Input.KeyboardState.IsKeyDown(Keys.S))
                player.Velocity.Y = WALK_ACCELERATION;
            else
                player.Velocity = Vector2.Zero;
        }
    }
}
