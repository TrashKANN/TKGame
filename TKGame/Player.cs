using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;

namespace TKGame
{
    public class Player : Entity
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
                            instance = new Player(new Player_InputComponent(),
                                                  new Player_PhysicsComponent(),
                                                  new Player_GraphicsComponent());
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

            health = 100; //Sets player health
            originalHealth = health;
            needsHealth = true;
            weapon = new Sword();
            weapon.Activate();

            entityTexture = Art.PlayerTexture;
            MOVEMENT_SPEED = 500f;
            // Figure out how to not hard code for now
            // Starts at (1560, 450) at the middle on the floor level
            entityName = "player"; // name for player class
            Position = new Vector2(TKGame.ScreenWidth/2, TKGame.ScreenHeight - 111);
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }

        

        /// <summary>
        /// Updates each component the Player owns.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            input.Update(this);
            physics.Update(this, gameTime/*, world*/);
            graphics.Update(this);
            weapon.Update(this);
            graphics.Update(this);
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
