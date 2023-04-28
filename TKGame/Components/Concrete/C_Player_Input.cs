using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    public class C_Player_Input : InputComponent
    {
        private static readonly int WALK_ACCELERATION = 1;
        private static readonly int JUMP_HEIGHT = -1;
        private static int framesSinceJump = 0;

        void InputComponent.Update(Entity player)
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

            if (Input.WasKeyPressed(Keys.Space))
            {
                ToggleJumping((Player)player);
                //player.Velocity.Y = JUMP_HEIGHT;
            }
        }

        void ToggleJumping(Player player)
        {
            player.isJumping= !player.isJumping;
        }
    }
}
