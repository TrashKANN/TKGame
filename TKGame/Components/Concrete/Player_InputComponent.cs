﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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

            // crouching
            // check if C key was pressed to toggle crouch
            if (Input.WasKeyPressed(Keys.C))
            {
                // if not already crouching
                if (player.isCrouched == false)
                {
                    // swap player png to crouching
                    
                    // set isCrouched to true
                    player.isCrouched = true;
                }
                // otherwise if player is already crouched
                else
                {
                    // swap player png to original

                    // set isCoruched to false
                    player.isCrouched = false;
                }
            }

            if (Input.WasKeyPressed(Keys.Space) && player.IsOnGround)
            {
                player.Velocity.Y = JUMP_FORCE;
                player.IsOnGround = false;
            }

            player.FramesSinceJump = player.IsOnGround ? 0 : player.FramesSinceJump + 1;
            Debug.WriteLine(player.FramesSinceJump);
        }
    }
}
