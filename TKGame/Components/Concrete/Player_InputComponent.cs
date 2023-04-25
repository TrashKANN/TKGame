using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    public class Player_InputComponent : InputComponent
    {
        private const int framesSinceJump = 0;
        private const int JUMP_FORCE = 10;


        void InputComponent.Update(Entity player)
        {
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

            if (Input.WasKeyPressed(Keys.Space))
            {
                ToggleJumping((Player)player);
                player.Velocity.Y = JUMP_FORCE;
                //player.Velocity.Y = JUMP_HEIGHT;
            }
        }

        void ToggleJumping(Player player)
        {
            player.isJumping= !player.isJumping;
        }
    }
}
