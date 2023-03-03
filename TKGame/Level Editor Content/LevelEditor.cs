﻿using System;
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
        public int dataWidth { get; set;}
        public int dataHeight { get; set;}
    }
    static class LevelEditor
    {
        private static MouseState previousMouseState;
        private static Vector2 startPosition;
        internal static bool EditMode = false;
        private static readonly int GRID_SIZE = 32;

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
        public static void BuildWall(Stage stage, GraphicsDevice graphics, SpriteBatch spriteBatch)
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

                // Align the rectangle to the grid
                Rectangle alignedRect = AlignRectToGrid(new Rectangle(
                                                        (int)topLeftPosition.X, 
                                                        (int)topLeftPosition.Y, 
                                                        (int)size.X, 
                                                        (int)size.Y), 
                                                        GRID_SIZE);

                Wall newWall = new Wall(alignedRect, graphics);


                stage.walls.Add(newWall);
            }

            previousMouseState = currentMouseState;
        }

        /// <summary>
        /// Aligns any passed rectangles/hitboxes to the grid based on the passed size. GridSize is a constant.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="gridSize"></param>
        /// <returns></returns>
        internal static Rectangle AlignRectToGrid(Rectangle rect, int gridSize)
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

        internal static void DrawGridLines(SpriteBatch spriteBatch, int screenWidth, int screenHeight, Color color)
        {
            // Calculate the number of grid squares in each direction
            int numHorizontalGridSquares = screenWidth / GRID_SIZE;
            int numVerticalGridSquares = screenHeight / GRID_SIZE;

            // Draw the horizontal grid lines
            for (int i = 0; i < numVerticalGridSquares; i++)
            {
                int y = i * GRID_SIZE;
                GameDebug.DrawBoundingBox(spriteBatch, new Rectangle(0, y, screenWidth, 1), color, 1);
            }

            // Draw the vertical grid lines
            for (int i = 0; i < numHorizontalGridSquares; i++)
            {
                int x = i * GRID_SIZE;
                GameDebug.DrawBoundingBox(spriteBatch, new Rectangle(x, 0, 1, screenHeight), color, 1);
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
            // TO DO: Add a Stage name input, Probably take user input to determine the name it is saved as.
            List<WallData> wallDataList = new List<WallData>();

            // For each wall, Add the data to the WallData list
            foreach (Wall wall in stage.walls)
            {
                if (wall.HitBox.Width != 0 && wall.HitBox.Height != 0)
                {
                    WallData walldata = new WallData()
                    {
                        X = wall.HitBox.X,
                        Y = wall.HitBox.Y,
                        dataWidth = wall.HitBox.Width,
                        dataHeight = wall.HitBox.Height,
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
        public static Stage LoadStageDataFromJSON(string stageName, GraphicsDevice graphics)
        {
            Stage newStage = new Stage(graphics);

            string stagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Level Editor Content/Stages/" + stageName));

            string json = System.IO.File.ReadAllText(stagePath);

            List<WallData> jsonStageData = JsonSerializer.Deserialize<WallData[]>(json).ToList();

            foreach (WallData wallData in jsonStageData)
            {
                Wall newWall = new Wall(wallData.X, wallData.Y, wallData.dataWidth, wallData.dataHeight, graphics);
                newStage.walls.Add(newWall);
            }

            return newStage;
        }
    }
}
