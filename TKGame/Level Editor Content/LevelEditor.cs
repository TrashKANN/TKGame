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

namespace TKGame.Level_Editor_Content
{
    public class WallData
    {
        public int X { get; set;}
        public int Y { get; set;}
        public int width { get; set;}
        public int height { get; set;}
    }
    static class LevelEditor
    {
        private static MouseState previousMouseState;
        private static Vector2 startPosition;
        internal static bool EditMode = false;

        public static void ToggleEditor()
        {
            EditMode = !EditMode;
        }
        public static void BuildWall(Stage stage, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            MouseState currentMouseState = Mouse.GetState();
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

                Rectangle tempRect = new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, (int)size.X, (int)size.Y);

                GameDebug.DrawBoundingBox(spriteBatch, tempRect, Color.DeepPink, 5);
            }
            // If the left mouse button IS ALREADY pressed and WAS RELEASED this update, store coordinates for end position
            else if (previousMouseState.LeftButton == ButtonState.Pressed &&
                     currentMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 endPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
                topLeftPosition.X = Math.Min(startPosition.X, endPosition.X);
                topLeftPosition.Y = Math.Min(startPosition.Y, endPosition.Y);
                size = new Vector2(Math.Abs(startPosition.X - endPosition.X), Math.Abs(startPosition.Y - endPosition.Y));


                Wall newWall = new Wall(((int)topLeftPosition.X), ((int)topLeftPosition.Y), ((int)size.X), ((int)size.Y), graphics);

                stage.walls.Add(newWall);
            }

            previousMouseState = currentMouseState;
        }

        public static void SaveStageDataToJSON(Stage stage, string newStageName)
        {
            // TO DO: Add a Stage name input, Probably take user input to determine the name it is saved as.
            List<WallData> wallDataList = new List<WallData>();

            // For each wall, Add the data to the WallData list
            foreach (Wall wall in stage.walls)
            {
                if (wall.Rect.Width != 0 && wall.Rect.Height != 0)
                {
                    WallData walldata = new WallData()
                    {
                        X = wall.Rect.X,
                        Y = wall.Rect.Y,
                        width = wall.Rect.Width,
                        height = wall.Rect.Height,
                    };

                    wallDataList.Add(walldata);
                }
            }

            // Serializes the data set. The Options make the output human-readable.
            string json = JsonSerializer.Serialize(wallDataList, new JsonSerializerOptions
            {
                WriteIndented= true,
            });

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

            File.WriteAllText(path, json);
        }

        public static Stage LoadStageDataFromJSON(string stageName, GraphicsDevice graphics)
        {
            Stage newStage = new Stage(graphics);

            string stagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Level Editor Content/Stages/" + stageName));

            string json = System.IO.File.ReadAllText(stagePath);

            List<WallData> jsonStageData = JsonSerializer.Deserialize<WallData[]>(json).ToList();

            foreach (WallData wallData in jsonStageData)
            {
                Wall newWall = new Wall(wallData.X, wallData.Y, wallData.width, wallData.height, graphics);
                newStage.walls.Add(newWall);
            }

            return newStage;
        }
    }
}
