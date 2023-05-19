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

namespace TKGame.Level_Editor_Content
{
    public class WallData
    {
        public int X { get; set;}
        public int Y { get; set;}
        public int width { get; set;}
        public int height { get; set;}
    }
    public class EntityData
    {
        public string type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class TriggerData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string action { get; set; }
    }

    public class BackgroundData//For Json Background Data
    {
        public string texture { get; set; } // To identify chosen texture
    }


    public class StageData
    {
        public List<WallData> walls { get; set; }
        public List<EntityData> entities { get; set; }
        public List<TriggerData> triggers { get; set; }
        public BackgroundData background { get; set; }//For Json Background Data
    }

    static class LevelEditor
    {
        public static Background levelBackground = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight);
        public static bool EditMode = false;
        private static MouseState previousMouseState;
        private static Vector2 startPosition;
        private static readonly int GRID_SIZE = 32;
        private static List<Wall> deletedWalls= new List<Wall>();

        /// <summary>
        /// Toggles the functionallity of the Level Editor
        /// </summary>
        public static void ToggleEditor()
        {
            EditMode = !EditMode;
        }

        /// <summary>
        /// When Level Editing mod is active, create a wall wherever you click left mouse button and ends where you release it. The to-be-drawn wall will be outlined in Pink
        /// Even if Debug Mode is turned off the pink outline will remain.
        /// 
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="graphics"></param>
        /// <param name="spriteBatch"></param>
        public static void BuildWall(Stage stage)
        {
            MouseState currentMouseState = Mouse.GetState();
            
            // Can be simiplified to Vector2 ... = new(); since we know the type. Style guide discussion required.
            Vector2 topLeftPosition = new Vector2();
            Vector2 size = new Vector2();

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
                Rectangle alignedRect = AlignRectToGrid(new Rectangle(
                                                        (int)topLeftPosition.X, 
                                                        (int)topLeftPosition.Y, 
                                                        (int)size.X, 
                                                        (int)size.Y), 
                                                        GRID_SIZE);

                Wall newWall = new Wall(alignedRect, Color.White, TKGame.Graphics.GraphicsDevice);

                stage.StageWalls.Add(newWall);
            }
            previousMouseState = currentMouseState;
        }

        /// <summary>
        /// Whilst holding "D", Left click walls to mark them. Right click marked walls to unmark.
        /// Any walls marked when "D" is held down and "Enter" is pressed will be deleted from the stage
        /// and added to the deletedWalls list for the purposes of Undo/Redo
        /// </summary>
        /// <param name="walls"></param>
        public static void DeleteWall(List<Wall> walls)
        {
            foreach (var wall in walls)
            {
                // Check each wall to see if the mouse is over it
                if (wall.HitBox.Contains(Input.MouseState.Position))
                {
                    // If the Left mouse button was clicked, highlight it with a different color and add it to to be deleted walls
                    if (Input.MouseState.LeftButton == ButtonState.Pressed && !deletedWalls.Contains(wall))
                    {
                        wall.Texture.SetData<Color>(new Color[] { Color.Red });
                        deletedWalls.Add(wall);
                    }
                    // If Right clicked, return the color to White and remove it from to be deleted walls
                    else if (Input.MouseState.RightButton == ButtonState.Pressed && deletedWalls.Contains(wall))
                    {
                        wall.Texture.SetData<Color>(new Color[] { Color.White });
                        deletedWalls.Remove(wall);
                    }
                }
            }
            // Upon pressing Enter, remove all the highlighted walls. Save in deleted list for Undoing
            if (Input.WasKeyPressed(Keys.Enter))
            {
                walls.RemoveAll(x => deletedWalls.Contains(x));

                // reset wall colors for newly deleted walls
                foreach (var wall in deletedWalls.Where(x => !walls.Contains(x)))
                {
                    wall.Texture.SetData<Color>(new Color[] { Color.White });
                }
            }
        }

        /// <summary>
        /// If there have been walls added to the deletedWalls list, Add the last wall in deletedWalls
        /// to the stage walls.
        /// Remove the last wall from the deleteWalls list.
        /// </summary>
        /// <param name="walls"></param>
        public static void UndoDeletedWall(List<Wall> walls)
        {
            if (deletedWalls.Count > 0)
            {
                walls.Add(deletedWalls.LastOrDefault());
                deletedWalls.Remove(deletedWalls.LastOrDefault());
            }
        }

        /// <summary>
        /// Adds the last wall in stage walls to the deletedWalls list. Can be performed without previously deleting
        /// walls. Use carefully.
        /// Walls are additionally removed from the stage walls.
        /// </summary>
        /// <param name="walls"></param>
        public static void RedoDeletedWall(List<Wall> walls)
        {
            if (walls.Count > 0)
            {
                deletedWalls.Add(walls.LastOrDefault());
                walls.Remove(deletedWalls.LastOrDefault());
            }
        }

        /// <summary>
        /// Aligns any passed rectangles/hitboxes to the grid based on the passed size. GridSize is a constant.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="gridSize"></param>
        /// <returns></returns>
        private static Rectangle AlignRectToGrid(Rectangle rect, int gridSize)
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
            stageData.walls = new List<WallData>();
            stageData.entities = new List<EntityData>();
            stageData.triggers = new List<TriggerData>();

            // For each wall, Add the data to the WallData list
            foreach (Wall wall in stage.StageWalls)
            {
                if (wall.HitBox.Width != 0 && wall.HitBox.Height != 0)
                {
                    WallData walldata = new WallData()
                    {
                        X = wall.HitBox.X,
                        Y = wall.HitBox.Y,
                        width = wall.HitBox.Width,
                        height = wall.HitBox.Height,
                    };

                    stageData.walls.Add(walldata);
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

            // For each trigger, Add the data to the TriggerData list
            foreach (Trigger trigger in stage.StageTriggers)
            {
                if (trigger.HitBox.Width != 0 && trigger.HitBox.Height != 0)
                {
                    TriggerData triggerdata = new TriggerData()
                    {
                        X = trigger.HitBox.X,
                        Y = trigger.HitBox.Y,
                        width = trigger.HitBox.Width,
                        height = trigger.HitBox.Height,
                        action = trigger.Action,
                    };
                    stageData.triggers.Add(triggerdata);
                }
            }

            if (stage.stageName == "room0.json")
            {
                stageData.background = new BackgroundData()
                {
                    texture = "cobble",
                };
            }
            else if (stage.stageName == "room1.json") {
                stageData.background = new BackgroundData()
                {
                    texture = "ruins",
                };
            }
            else
            {
                stageData.background = new BackgroundData()
                {
                    texture = "dungeon",
                };
            }
                

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

            foreach (WallData wallData in levelData.walls)
            {
                Wall newWall = new Wall(wallData.X, wallData.Y, wallData.width, wallData.height, Color.White);
                newStage.StageWalls.Add(newWall);
            }

            foreach (EntityData entityData in levelData.entities)
            {
                //TODO: Refactor for factories
                newStage.StageEntities.Add(new KnightEnemy());
            }

            foreach (TriggerData triggerData in levelData.triggers)
            {
                newStage.StageTriggers.Add(new Trigger(triggerData.X, triggerData.Y, triggerData.width, triggerData.height, triggerData.action));
            }

            newStage.background = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight);//Creates new Background Object
            if (levelData.background.texture == "cobble") //identifies chosen background
                newStage.background.BackgroundTexture = Art.BackgroundTexture1;//sets new stage background
            else if (levelData.background.texture == "ruins")
                newStage.background.BackgroundTexture = Art.BackgroundTexture2;
            else
                newStage.background.BackgroundTexture = Art.BackgroundTexture3;

            return newStage;
        }
    }
}
