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
    /*internal*/ class Character
    {
        Texture2D texture;
        Vector2 pos;
        Vector2 vel;
        bool jumped;

        // Character constructor
        public Character(Texture2D newTexture, Vector2 newPos)
        {
            texture = newTexture;
            pos = newPos;
            jumped = true;
        }

        // Player character movement, gravity, jumping 
        public void Update(GameTime gameTime)
        {
            pos += vel;

            // Move to right
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                vel.X = 3f;
            // Move to left
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                vel.X = -3f;
            // No key pressed so don't move
            else
                vel.X = 0f;

            // No double jumps due to hasJumped boolean
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && jumped == false)
            {
                // Height and speed when jumping
                pos.Y -= 10f;
                pos.Y = -5f;  
                jumped = true;
            }

            // character has jumped (gravity takes effect)
            if (jumped == true)
            {
                // multiplier for gravity
                //float i = 1;
                // gravity formula  
                vel.Y += 0.15f/** i*/;
            }

            // set hasJumped to false when character touches ground
            if (pos.X + texture.Height >= 450)
                jumped = false;

            // gravity stops when character is touching floor
            if (jumped == false)
                vel.Y = 0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.Red);
        }
    }
}
