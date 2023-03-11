﻿using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TKGame.Level_Editor_Content;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;

namespace TKGame
{
    class Player : Entity
    {
        private static Player instance;
        private static object syncRoot = new object();

        #region Components
        InputComponent input;
        PhysicsComponent physics;
        GraphicsComponent graphics;
        #endregion Components

        internal bool isJumping = false;

        public static Player Instance
        {
            get
            {
                // Creates the player if it doesn't already exist
                // Uses thread locking to guarantee safety.
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Player(new PlayerInputComponent(),
                                                  new PlayerPhysicsComponent(),
                                                  new PlayerGraphicsComponent());
                    }

                return instance;
            }
        }

        /// <summary>
        /// Player components.
        /// </summary>
        private Player(InputComponent input_, PhysicsComponent physics_, GraphicsComponent graphics_)
        {
            input = input_;
            physics = physics_;
            graphics = graphics_;
            entityTexture = Art.PlayerTexture;
            MOVEMENT_SPEED = 500f;
            // Figure out how to not hard code for now
            // Starts at (1560, 450) at the middle on the floor level
            Position = new Vector2(1600/2, 900 - 40);
            entityName = "player"; // name for player class
            Position = new Vector2(1600/2, 900 - 70);
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }

        

        /// <summary>
        /// Updates each component the Player owns.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            input.Update(this);
            physics.Update(this, gameTime/*, world*/);
            graphics.Update(this, spriteBatch);
        }

        /// <summary>
        /// Draws each Player Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
