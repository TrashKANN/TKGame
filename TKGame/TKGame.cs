using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TKGame
{
    public class TKGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Character class
        Character player;

        // TODO: Refactor out of the main TKGame class
        List<Wall> walls;
        int screenWidth, screenHeight;

        public TKGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            // TODO: Remove magic numbers
            // We'll eventually probably want to generate walls based off level data (from a file).
            walls = new List<Wall>()
            {
                new Wall(0, screenHeight - 40, screenWidth, 50, graphics.GraphicsDevice),       // Floor 
                new Wall(0, 0, screenWidth, 50, graphics.GraphicsDevice),                       // Ceiling
                new Wall(0, 0, 50, screenHeight - 250, graphics.GraphicsDevice),                // Left wall
                new Wall(screenWidth - 50, 0, 50, screenHeight - 250, graphics.GraphicsDevice), // Right wall
                new Wall(screenWidth / 2, screenHeight / 2, 250, 250, graphics.GraphicsDevice)  // Extra wall to test collision on
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Load character graphic with actual texture
            player = new Character(Content.Load<Texture2D>("Texture"),
                new Vector2(50, 50)); 

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Exit the game if Escape is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update player state 
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // SpriteBatch sends your sprites in batches to the GPU. We can
            // Begin and End a couple hundred batches per frame. Sprites that
            // share the same shader are placed in the same batch.
            spriteBatch.Begin();

            // Draw each wall to the screen
            foreach (Wall wall in walls)
            {
                spriteBatch.Draw(wall.Texture, wall.Rect, Color.Beige);
            }

            // Draw the player
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}