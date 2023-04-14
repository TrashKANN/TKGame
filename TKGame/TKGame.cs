using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Myra is a library that allows us to add GUI components.
// https://github.com/rds1983/Myra
using Myra;
using Myra.Graphics2D.UI;
using TKGame.Animations;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;
using TKGame.Weapons;

namespace TKGame
{
    public class TKGame : Game
    {
        #region Member Variables
        public static TKGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static int ScreenWidth { get { return (int)ScreenSize.X; } }
        public static int ScreenHeight { get { return (int)ScreenSize.Y; } }

        public static GameTime GameTime { get; private set; }

        //Declares public Vertical Stack Panel
        public VerticalStackPanel VSP { get; protected set; }

        // TODO: Move this to another class eventually
        private static readonly Color WALL_COLOR = new Color(0x9a, 0x9b, 0x9c, 0xFF);

        private static GraphicsDeviceManager graphics;
        private static SpriteBatch spriteBatch;

        public static GraphicsDeviceManager Graphics { get { return graphics; } private set { graphics = value; } }
        public static SpriteBatch SpriteBatch { get { return spriteBatch; } private set { spriteBatch = value; } }

        public Desktop desktop;

        //Declare Background Object
        private Background BackgroundImage;

        //Declare ScreenTransition Object
        private ScreenTransition transition;

        //Declare Triggers
        List<Trigger> triggers;

        
        // TODO: Refactor out of the main TKGame class
        public static bool paused = true;
        #endregion

        #region Components
        private LevelEditorComponent levelEditorComponent;
        public static LevelComponent levelComponent;
        #endregion Components

        public TKGame()
        {
            Instance = this;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true; // Time between frames is constant
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 240d); // Set target fps (240 for now)
            graphics = new GraphicsDeviceManager(this);
            VSP = new VerticalStackPanel();
            graphics.SynchronizeWithVerticalRetrace = false; // Disable v-sync
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            IsMouseVisible = true;
            levelEditorComponent = new World_LevelEditorComponent();
            levelComponent = new World_LevelComponent(new List<Level>());
        }
        protected override void Initialize()
        {
            // Let Myra know what our Game object is so we can use it
            MyraEnvironment.Game = this;
            desktop = new Desktop();

            //Create New Background Object w/variables for setting Rectangle and Texture
            BackgroundImage = new Background(ScreenWidth, ScreenHeight, graphics.GraphicsDevice);

            //Create New ScreenTransition Object
            transition = new ScreenTransition(graphics.GraphicsDevice);


            // Create Triggers
            // TODO: Create Functionality for Procedural Generation with Level Designer
            triggers = new List<Trigger>()
            {
                new Trigger(0,ScreenHeight - 240, 55, 195, "goPrevious"),
                new Trigger(ScreenWidth - 50, ScreenHeight - 240, 50, 195, "goNext"),
            };

            // Initialize debug information
            GameDebug.Initialize();
#if DEBUG
            // For now, just enable DebugMode when building a Debug version
            GameDebug.DebugMode = true;
#endif
            // Initialize main menu
            MainMenu.Initialize(desktop, this);

            //Initializing WeaponSystem
            WeaponSystem.Initialize();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LoadContent(Content);
            Music.LoadContent(Content, 0.15f);


            // Load Weapon System Content
            WeaponSystem.LoadContent(VSP);


            // Load debug content
            GameDebug.LoadContent(VSP);

            // Load main menu
            MainMenu.LoadContent();

            // Add and Load Default Stage
            levelComponent.AddLevel(new Level(new List<Stage>
            {   
                new Stage("defaultStage"), 
                new Stage("leftStage"), 
                new Stage("rightStage")
            }
            ));

            // Spawn a knight enemy
            EnemyFactory knightFactory = new KnightEnemyFactory();
            Enemy knight = knightFactory.CreateEnemy();
            EntityManager.Add(knight);

            // Spawn a potion item
            ItemFactory potionFactory = new PotionItemFactory();
            Item potion = potionFactory.CreateItem();
            EntityManager.Add(potion);
        }
        protected override async void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();

            // NEEDS TO BE MOVED INTO LEVEL.CS
            //if (triggers[0].checkLeftTrigger(Player.Instance))
            //{
            //    transition.Update(gameTime);
            //    paused = true;

            //    List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(triggers[0].leftStage, GraphicsDevice)).StageWalls;
            //    currentStage = new Stage(stageWalls);
            //    paused = false;
            //}

            //if (triggers[1].checkRightTrigger(Player.Instance))
            //{
            //    paused = true;
            //    transition.Update(gameTime);
            //    List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(triggers[1].rightStage, GraphicsDevice)).StageWalls;
            //    currentStage = new Stage(stageWalls);
            //    paused = false;
            //}

            // Add pause stuff here
            //Do if not paused
            if (!paused)
            {
                EntityManager.Update(gameTime);
            }


            // Exit the game if Escape is pressed
            if (Input.WasKeyPressed(Keys.Escape))
            {
                ExitGame();
            }

#if DEBUG
            // Toggle the debug UI's visibility once per key press
            // TODO: Probably move this somewhere else
            if (Input.WasKeyPressed(Keys.G))
            {
                GameDebug.ToggleVisibility();
            }

            // Toggle Editing mode for levels, pauses the game to hault entity movement.
            if (Input.WasKeyPressed(Keys.L))
            {
                LevelEditor.ToggleEditor();
                paused = !paused;
            }

#endif
            // Updates Weapon System
            WeaponSystem.Update();

            // Update debug information
            GameDebug.Update();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here

            // SpriteBatch sends your sprites in batches to the GPU. We can
            // Begin and End a couple hundred batches per frame. Sprites that
            // share the same shader are placed in the same batch.

            // Deferred rendering means things are drawn in the ordered they're called
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            //Draws the image into the Background
            spriteBatch.Draw(Art.BackgroundTexture, BackgroundImage.BackgroundRect, Color.White);


            // Draw each wall to the screen
            // Update level editor

            foreach (Wall wall in levelComponent.GetCurrentStage().StageWalls)
            {
                spriteBatch.Draw(wall.Texture, wall.HitBox, WALL_COLOR);
                if (GameDebug.DebugMode) 
                { 
                    GameDebug.DrawBoundingBox(wall.HitBox, Color.Lime, 5); 
                }
            }


            //Draw Triggers in gaps in the walls
            //TODO: Add Functionality for Level Designer
            foreach (Trigger trigger in triggers)
            {
                spriteBatch.Draw(trigger.Texture, trigger.HitBox, Color.White);
            }

            EntityManager.Draw(spriteBatch);

            if (GameDebug.DebugMode)
            {
                foreach (Entity entity in EntityManager.GetEntities())
                {
                    GameDebug.DrawBoundingBox(entity, Color.Blue, 5);
                }
            }

            // Draw the New Wall last so that the outline appears above all other images
            if (LevelEditor.EditMode)
            {
                levelEditorComponent.Update();
            }

            //Draws Loading Screen Offscreen until needed
            //spriteBatch.Draw(Art.LoadTexture, transition.rect, Color.White);
            transition.Draw(spriteBatch);

            spriteBatch.End();

            // Render UI elements from Myra
            desktop.Render();


            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }

        public void ExitGame()
        {
            LevelEditor.SaveStageDataToJSON(levelComponent.GetCurrentStage(), "auto_saved_stage_data");
            Exit();
        }

        public static void SwitchToGameplayMenu() 
        {
            Instance.desktop.Root = Instance.VSP;
        }
    }
}