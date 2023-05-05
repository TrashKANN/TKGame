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

namespace TKGame.Players.Components
{
    public class C_Player_Input : IInputComponent
    {
        private const int JUMP_FORCE = -800;

        ComponentType IComponent.Type => ComponentType.Level;

        void IInputComponent.Update(Entity entity)
        {
            Player player = (Player)entity;

            if (Input.KeyboardState.IsKeyDown(Keys.A))
            {
                player.Velocity.X = -player.MOVEMENT_SPEED;
                player.isLookingLeft = true;
            }

            else if (Input.KeyboardState.IsKeyDown(Keys.D))
            {
                player.Velocity.X = player.MOVEMENT_SPEED;
                player.isLookingLeft = false;
            }

            //else if (Input.KeyboardState.IsKeyDown(Keys.W))
            //player.Velocity.Y = -WALK_ACCELERATION;

            //else if (Input.KeyboardState.IsKeyDown(Keys.S))
            //player.Velocity.Y = WALK_ACCELERATION;
            else
                player.Velocity.X = 0;

            // update crouched bool based on key input
            if (Input.KeyboardState.IsKeyDown(Keys.S))
                player.isCrouched = true;
            else
                player.isCrouched = false;

            if (Input.WasKeyPressed(Keys.Space) && player.IsOnGround)
            {
                player.Velocity.Y = JUMP_FORCE;
                player.IsOnGround = false;
            }

            player.FramesSinceJump = player.IsOnGround ? 0 : player.FramesSinceJump + 1;
        }
    }
}
