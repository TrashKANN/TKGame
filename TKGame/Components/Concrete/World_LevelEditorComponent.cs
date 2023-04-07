using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;

namespace TKGame.Components.Concrete
{
    internal class World_LevelEditorComponent : LevelEditorComponent
    {
        void LevelEditorComponent.Update()
        {
            Stage currentStage = TKGame.levelComponent.GetCurrentStage();

            // I really hate this, needs to be refactored. Asking Thomas might be easiest
            if (Input.KeyboardState.IsKeyDown(Keys.W))
            {
                LevelEditor.BuildWall(currentStage, TKGame.Graphics.GraphicsDevice, TKGame.SpriteBatch);
            }
            // D (Hold) + LClick = Mark; + RClick = UnMark; + Enter = Delete Mar
            else if (Input.KeyboardState.IsKeyDown(Keys.D))
            {
                LevelEditor.DeleteWall(currentStage.StageWalls);
            }
            // Ctrl + Z = Undo last wall deleted
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.Z))
            {
                LevelEditor.UndoDeletedWall(currentStage.StageWalls);
            }
            // Ctrl + Y = Redo last wall deleted
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.Y))
            {
                LevelEditor.RedoDeletedWall(currentStage.StageWalls);
            }
            LevelEditor.DrawGridLines((int)TKGame.ScreenSize.X, (int)TKGame.ScreenSize.Y, Color.Black);
        }
    }
}
