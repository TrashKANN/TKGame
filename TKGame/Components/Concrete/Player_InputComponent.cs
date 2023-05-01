using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    public class Player_InputComponent : InputComponent
    {
        private const int JUMP_FORCE = -1100;

        void InputComponent.Update(Entity entity)
        {
            Player player = entity as Player;

            if (Input.KeyboardState.IsKeyDown(Keys.A))
                player.Velocity.X = -player.MOVEMENT_SPEED;

            else if (Input.KeyboardState.IsKeyDown(Keys.D))
                player.Velocity.X = player.MOVEMENT_SPEED;

            //else if (Input.KeyboardState.IsKeyDown(Keys.W))
            //player.Velocity.Y = -WALK_ACCELERATION;

            //else if (Input.KeyboardState.IsKeyDown(Keys.S))
            //player.Velocity.Y = WALK_ACCELERATION;
            else
                player.Velocity.X = 0;

            // update crouched bool based on key input
            if (Input.KeyboardState.IsKeyDown(Keys.S))
            {
                player.isCrouched = true;
            }
            else
            {
                player.isCrouched = false;
            }

            if (Input.WasKeyPressed(Keys.Space) && player.IsOnGround)
            {
                player.Velocity.Y = JUMP_FORCE;
                player.IsOnGround = false;
            }

            player.FramesSinceJump = player.IsOnGround ? 0 : player.FramesSinceJump + 1;
        }
    }
}
