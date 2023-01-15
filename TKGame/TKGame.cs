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
                new Wall(0, screenHeight - 40, screenWidth, 50, graphics.GraphicsDevice),
                new Wall(0, 0, screenWidth, 50, graphics.GraphicsDevice),
                new Wall(0, 0, 50, screenHeight - 250, graphics.GraphicsDevice),
                new Wall(screenWidth - 50, 0, 50, screenHeight - 250, graphics.GraphicsDevice)
            };

            base.Initialize();
        }

        protected override void LoadContent() 
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) 
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (Wall wall in walls)
            {
                spriteBatch.Draw(wall.Texture, wall.Rect, Color.Beige);
            }

            spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }
}