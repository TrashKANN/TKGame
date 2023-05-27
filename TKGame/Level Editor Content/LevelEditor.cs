using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MonoGame includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

// JSON includes
using System.IO;
using System.Text.Json;
using Color = Microsoft.Xna.Framework.Color;
using TKGame.BackEnd;
using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using TKGame.Players;
using TKGame.Enemies;
using System.Collections;
using TKGame.Items;

namespace TKGame.Level_Editor_Content
{
    public class BlockData
    {
        public string type { get; set; }
        public int X { get; set;}
        public int Y { get; set;}
        public int width { get; set;}
        public int height { get; set;}
        public string action { get; set; }
    }
    public class EntityData
    {
        public string type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class BackgroundData//For Json Background Data
    {
        public string texture { get; set; } // To identify chosen texture
    }


    public class StageData
    {
        public List<BlockData> blocks { get; set; }
        public List<EntityData> entities { get; set; }
        public BackgroundData background { get; set; }//For Json Background Data
    }

    public enum StructureType
    {
        Wall,
        Spikes,
        Door,
    }

    static class LevelEditor
    {
        public static bool EditMode = false;
        private static MouseState previousMouseState;
        private static Vector2 startPosition;
        private static readonly int GRID_SIZE = 32;
        private static List<IBlock> deletedBlocks= new List<IBlock>();

        // Selected wall, spike, etc.
        public static StructureType selectedStructure { get; set; }
        public static Color selectedColor { get; set; }

        /// <summary>
        /// Toggles the functionallity of the Level Editor
        /// </summary>
        public static void ToggleEditor()
        {
            EditMode = !EditMode;
            selectedStructure = StructureType.Wall;
            selectedColor = Color.White;
        }

        // TODO: refactor into a factory/use IBlocks instead of hard coded types
        public static void BuildWall(Stage stage)
        {
            Wall newWall = new Wall(OutlineBuilding(stage), selectedColor);

            if (newWall.HitBox.Width > 0 && newWall.HitBox.Height > 0)
                stage.StageBlocks.Add(newWall);
        }

        public static void BuildSpikes(Stage stage)
        {
            Spikes newSpike = new Spikes(OutlineBuilding(stage));
            
            if (newSpike.HitBox.Width > 0 && newSpike.HitBox.Height > 0)
                stage.StageBlocks.Add(newSpike);
        }

        /// <summary>
        /// When Level Editing mod is active, create a wall wherever you click left mouse button and ends where you release it. The to-be-drawn wall will be outlined in Pink
        /// Even if Debug Mode is turned off the pink outline will remain.
        /// 
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="graphics"></param>
        /// <param name="spriteBatch"></param>
        private static Rectangle OutlineBuilding(Stage stage)
        {
            MouseState currentMouseState = Mouse.GetState();
            
            // Can be simiplified to Vector2 ... = new(); since we know the type. Style guide discussion required.
            Vector2 topLeftPosition = new Vector2();
            Vector2 size = new Vector2();

            Rectangle alignedRect = new Rectangle();

            // If the left mouse button WAS NOT pressed last update AND IS pressed this update, store coordinates
            if (previousMouseState.LeftButton == ButtonState.Released &&
                currentMouseState.LeftButton == ButtonState.Pressed)
            {
                startPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            }
            // If the left mouse button IS STILL pressed last update AND IS pressed this update, draw an outline rectangle of the to-be Wall
            else if (previousMouseState.LeftButton == ButtonState.Pressed &&
                    currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 endPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
                topLeftPosition.X = Math.Min(startPosition.X, endPosition.X);
                topLeftPosition.Y = Math.Min(startPosition.Y, endPosition.Y);
                size = new Vector2(Math.Abs(startPosition.X - endPosition.X), Math.Abs(startPosition.Y - endPosition.Y));

                // Used for drawing the outline of the to be created wall
                Rectangle tempRect = new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, (int)size.X, (int)size.Y);

                GameDebug.DrawBoundingBox(tempRect, Color.DeepPink, 5);
            }
            // If the left mouse button IS ALREADY pressed and WAS RELEASED this update, store coordinates for end position
            else if (previousMouseState.LeftButton == ButtonState.Pressed &&
                     currentMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 endPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
                topLeftPosition.X = Math.Min(startPosition.X, endPosition.X);
                topLeftPosition.Y = Math.Min(startPosition.Y, endPosition.Y);
                size = new Vector2(Math.Abs(startPosition.X - endPosition.X), Math.Abs(startPosition.Y - endPosition.Y));

                // Align the rectangle to the grid
                alignedRect = AlignRectToGrid(new Rectangle(
                                             (int)topLeftPosition.X, 
                                             (int)topLeftPosition.Y, 
                                             (int)size.X, 
                                             (int)size.Y), 
                                             GRID_SIZE);
            }
            previousMouseState = currentMouseState;

            return alignedRect;
        }

        /// <summary>
        /// Whilst holding "D", Left click walls to mark them. Right click marked walls to unmark.
        /// Any walls marked when "D" is held down and "Enter" is pressed will be deleted from the stage
        /// and added to the deletedWalls list for the purposes of Undo/Redo
        /// </summary>
        /// <param name="blocks"></param>
        public static void DeleteBlock(List<IBlock> blocks)
        {
            foreach (var block in blocks)
            {
                // Check each wall to see if the mouse is over it
                if (block.HitBox.Contains(Input.MouseState.Position))
                {
                    // If the Left mouse button was clicked, highlight it with a different color and add it to to be deleted walls
                    if (Input.MouseState.LeftButton == ButtonState.Pressed && !deletedBlocks.Contains(block))
                    {
                        block.Texture.SetData<Color>(new Color[] { Color.Red });
                        deletedBlocks.Add(block);
                    }
                    // If Right clicked, return the color to White and remove it from to be deleted walls
                    else if (Input.MouseState.RightButton == ButtonState.Pressed && deletedBlocks.Contains(block))
                    {
                        block.Texture.SetData<Color>(new Color[] { Color.White });
                        deletedBlocks.Remove(block);
                    }
                }
            }
            // Upon pressing Enter, remove all the highlighted walls. Save in deleted list for Undoing
            if (Input.WasKeyPressed(Keys.Enter))
            {
                blocks.RemoveAll(x => deletedBlocks.Contains(x));

                // reset wall colors for newly deleted walls
                foreach (var block in deletedBlocks.Where(x => !blocks.Contains(x)))
                {
                    block.Texture.SetData(new Color[] { Color.White });
                }
            }
        }

        /// <summary>
        /// If there have been walls added to the deletedWalls list, Add the last wall in deletedWalls
        /// to the stage walls.
        /// Remove the last wall from the deleteWalls list.
        /// </summary>
        /// <param name="blocks"></param>
        public static void UndoDeletedWall(List<IBlock> blocks)
        {
            if (deletedBlocks.Count > 0)
            {
                blocks.Add(deletedBlocks.LastOrDefault());
                deletedBlocks.Remove(deletedBlocks.LastOrDefault());
            }
        }

        /// <summary>
        /// Adds the last wall in stage walls to the deletedWalls list. Can be performed without previously deleting
        /// walls. Use carefully.
        /// Walls are additionally removed from the stage walls.
        /// </summary>
        /// <param name="blocks"></param>
        public static void RedoDeletedWall(List<IBlock> blocks)
        {
            if (blocks.Count > 0)
            {
                deletedBlocks.Add(blocks.LastOrDefault());
                blocks.Remove(deletedBlocks.LastOrDefault());
            }
        }

        /// <summary>
        /// Aligns any passed rectangles/hitboxes to the grid based on the passed size. GridSize is a constant.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="gridSize"></param>
        /// <returns></returns>
        public static Rectangle AlignRectToGrid(Rectangle rect, int gridSize)
        {
            // Calculate the position of the closest grid square
            int snappedX = (int)Math.Round((double)rect.X / gridSize) * gridSize;
            int snappedY = (int)Math.Round((double)rect.Y / gridSize) * gridSize;

            // Calculate the width and height of the rectangle in grid units
            int snappedWidth = (int)Math.Round((double)rect.Width / gridSize) * gridSize;
            int snappedHeight = (int)Math.Round((double)rect.Height / gridSize) * gridSize;

            // Update with new snapped position/size
            rect.X = snappedX;
            rect.Y = snappedY;
            rect.Width = snappedWidth;
            rect.Height = snappedHeight;

            return rect;
        }

        public static void DrawGridLines(Color color)
        {
            // Calculate the number of grid squares in each direction
            int numHorizontalGridSquares = TKGame.ScreenWidth / GRID_SIZE;
            int numVerticalGridSquares = TKGame.ScreenHeight / GRID_SIZE;

            // Draw the horizontal grid lines
            for (int i = 0; i < numVerticalGridSquares; i++)
            {
                int y = i * GRID_SIZE;
                GameDebug.DrawBoundingBox(new Rectangle(0, y, TKGame.ScreenWidth, 1), color, 1);
            }

            // Draw the vertical grid lines
            for (int i = 0; i < numHorizontalGridSquares; i++)
            {
                int x = i * GRID_SIZE;
                GameDebug.DrawBoundingBox(new Rectangle(x, 0, 1, TKGame.ScreenHeight), color, 1);
            }
        }

        /// <summary>
        /// Goes through each wall within the given stage and serializes them into a JSON format for storage.
        /// This allows the functionality of saving stages for later use.
        /// Stages will automatically be saved to "/Level Editor Content/Stages/".
        /// 
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="newStageName"></param>
        public static void SaveStageDataToJSON(Stage stage, string newStageName)
        {
            TKGame.paused = true;

            var stageData = new StageData();

            // Initialize the lists of each type of data
            stageData.blocks = new List<BlockData>();
            stageData.entities = new List<EntityData>();

            // For each wall, Add the data to the WallData list
            foreach (IBlock block in stage.StageBlocks)
            {
                if (block.HitBox.Width != 0 && block.HitBox.Height != 0)
                {
                    BlockData blockdata = new BlockData()
                    {
                        X = block.HitBox.X,
                        Y = block.HitBox.Y,
                        width = block.HitBox.Width,
                        height = block.HitBox.Height,
                        action = block.Action,
                    };

                    stageData.blocks.Add(blockdata);
                }
            }

            // For each entity, Add the data to the EntityData list
            foreach (Entity entity in stage.StageEntities)
            {
                if (entity.HitBox.Width != 0 && entity.HitBox.Height != 0 && entity != Player.Instance)
                {
                    EntityData entitydata = new EntityData()
                    {
                        X = entity.HitBox.X,
                        Y = entity.HitBox.Y,
                        type = entity.entityName,
                    };
                    stageData.entities.Add(entitydata);
                }
            }

            stageData.background = new BackgroundData()
            {
                texture = TKGame.levelComponent.GetCurrentStage().Background.BackgroundName,
            };

            // Serializes the data set. The Options make the output human-readable.
            string json = JsonSerializer.Serialize(stageData, new JsonSerializerOptions
            {
                WriteIndented= true,
            });

            //string backgroundJson = JsonSerializer.Serialize(stageBackground.BackgroundTexture, new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //});
       
            // Saves the json into the Level Editor Content Folder for now.
            string directory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Level Editor Content/Stages"));
            // = "[Your path into TKGames]\\TKGames\\TKGame\\Level Editor Content\\Stages\\"
            // ^ So that we can change it later easily

            // Creates the Directory if it doesn't exist.
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string path = Path.Combine(directory, newStageName + ".json");
            for (int i = 1; i <= 100; i++)
            {
                if (File.Exists(path))
                    path = Path.Combine(directory, newStageName + "_" + i.ToString() + ".json");
                else
                    break;
            }

            File.WriteAllText(path, json);
        }

        /// <summary>
        /// This will load a given stage name from the list of stages within "/Level Editor Content/Stages/".
        /// The JSON is deserialized and for each set of WallData, a wall is created and loaded up into a Stage which is returned for assignment.
        /// </summary>
        /// <param name="stageName"></param>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public static Stage LoadStageDataFromJSON(string stageName)
        {
            TKGame.paused = true;

            Stage newStage = new Stage();

            string stagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Level Editor Content/Stages/" + stageName));

            string jsonString = System.IO.File.ReadAllText(stagePath);

            StageData levelData = JsonSerializer.Deserialize<StageData>(jsonString);

            foreach (BlockData blockData in levelData.blocks)
            {
                IBlock newBlock;
                if (blockData.type == "Wall")
                {
                    newBlock = new Wall(blockData.X, blockData.Y, blockData.width, blockData.height, Color.White);
                }
                else if (blockData.type == "Spikes")
                {
                    newBlock = new Spikes(blockData.X, blockData.Y, blockData.width, blockData.height);
                }
                else if (blockData.type == "Door")
                {
                    newBlock = new Door(blockData.X, blockData.Y, blockData.width, blockData.height, blockData.action);
                }
                // TODO:
                //else if (blockData.type == "Platform")
                //{
                //    newBlock = new Platform(blockData.X, blockData.Y, blockData.width, blockData.height, Color.White);
                //}
                else
                {
                    newBlock = new Wall(blockData.X, blockData.Y, blockData.width, blockData.height, Color.White);
                }
                newStage.StageBlocks.Add(newBlock);
            }

            foreach (EntityData entityData in levelData.entities)
            {
                // TODO: make this not hard coded
                switch (entityData.type)
                {
                    case "KnightEnemy":
                        EnemyFactory knightFactory = new KnightEnemyFactory();
                        Enemy knight = knightFactory.CreateEnemy();
                        knight.Position.X = entityData.X;
                        knight.Position.Y = entityData.Y;
                        newStage.StageEntities.Add(knight);
                        break;

                    case "GoblinEnemy":
                        EnemyFactory goblinFactory = new GoblinEnemyFactory();
                        Enemy goblin = goblinFactory.CreateEnemy();
                        goblin.Position.X = entityData.X;
                        goblin.Position.Y = entityData.Y;
                        newStage.StageEntities.Add(goblin);
                        break;

                    case "PotionItem":
                        ItemFactory potionFactory = new PotionItemFactory();
                        Item potion = potionFactory.CreateItem();
                        potion.Position.X = entityData.X;
                        potion.Position.Y = entityData.Y;
                        newStage.StageEntities.Add(potion);
                        break;

                    case "FireStoneItem":
                        ItemFactory fireStoneFactory = new FireStoneItemFactory();
                        Item fireStone = fireStoneFactory.CreateItem();
                        fireStone.Position.X = entityData.X;
                        fireStone.Position.Y = entityData.Y;
                        newStage.StageEntities.Add(fireStone);
                        break;

                    case "IceItem":
                        ItemFactory iceItemFactory = new IceItemFactory();
                        Item ice = iceItemFactory.CreateItem();
                        ice.Position.X = entityData.X;
                        ice.Position.Y = entityData.Y;
                        newStage.StageEntities.Add(ice);
                        break;

                    case "PoisonItem":
                        ItemFactory poisonItemFactory = new PoisonItemFactory();
                        Item poison = poisonItemFactory.CreateItem();
                        poison.Position.X = entityData.X;
                        poison.Position.Y = entityData.Y;
                        newStage.StageEntities.Add(poison);
                        break;

                    default:
                        // TODO: Handle unrecognized entity type
                        break;
                }
            }

            newStage.Background = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight, levelData.background.texture);//Creates new Background Object
            switch (newStage.Background.BackgroundName)
            { 
                case "cobble":
                    newStage.Background.BackgroundTexture = Art.BackgroundTexture1;
                    break;
                case "ruins":
                    newStage.Background.BackgroundTexture = Art.BackgroundTexture2;
                    break;
                default:
                    newStage.Background.BackgroundTexture = Art.BackgroundTexture3;
                    break;
            }

            return newStage;
        }
    }
}
