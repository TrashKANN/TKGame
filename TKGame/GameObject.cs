using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame
{
    public abstract class GameObject // Will become Entity. Need more refactoring first.
    {
        #region Components
        InputComponent input;
        PhysicsComponent physics;
        GraphicsComponent graphics;
        #endregion Components

        public string entityName; // to identify each entity by name
        public Vector2 Position, Velocity;
        public Rectangle HitBox;
        public Vector2 Size
        {
            get
            {
                return entityTexture == null ? Vector2.Zero : new Vector2(entityTexture.Width, entityTexture.Height);
            }
        }

        internal Texture2D entityTexture;
        public SpriteEffects Orientation; // Flip Horizontal/Vertical
        public Color color = Color.White;

        public float MOVEMENT_SPEED { get; internal set; }

        public bool IsExpired;


        GameObject(InputComponent input,
                   PhysicsComponent physics,
                   GraphicsComponent graphics)
        {
            this.input = input;
            this.physics = physics;
            this.graphics = graphics;
        }

        void Update(/*World world, */SpriteBatch spriteBatch)
        {
            //input.Update(this);
            //physics.Update(this, world);
            //graphics.Update(this, spriteBatch);
        }
    }
}
