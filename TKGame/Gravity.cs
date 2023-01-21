using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    /*internal*/
    class Gravity
    {
        Texture2D texture;
        Vector2 pos;
        Vector2 vel;
        bool jumped;

        // Gravity constructor
        public Gravity(Texture2D newTexture, Vector2 newPos)
        {
            texture = newTexture;
            pos = newPos;
            jumped = true;
        }

        public void Update(GameTime gameTime)
        {
            pos += vel; // put at end?

            // No double jumps due to hasJumped boolean
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && jumped == false)
            {
                // Height and speed when jumping
                pos.Y -= 10f;
                pos.Y = -5f;
                jumped = true;
            }

            // has jumped (gravity takes effect)
            if (jumped == true)
            {
                // multiplier for gravity
                //float i = 1;
                // gravity formula  
                vel.Y += 0.15f/** i*/;
            }

            // set hasJumped to false when touches ground
            if (pos.X + texture.Height >= 450)
                jumped = false;

            // gravity stops when is touching floor
            //if (jumped == false)
            //    vel.Y = 0f;
        }
    }
}

