using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;

namespace TKGame.World.Components
{
    internal class C_World_LevelEditor : ILevelEditorComponent
    {
        public ComponentType Type { get; }

        void ILevelEditorComponent.Update()
        {
            Stage currentStage = TKGame.levelComponent.GetCurrentStage();

            // I really hate this, needs to be refactored. Asking Thomas might be easiest
            if (Input.KeyboardState.IsKeyDown(Keys.W))
            {
                if (LevelEditor.selectedStructure == StructureType.Wall)
                    LevelEditor.BuildWall(currentStage);

                else if (LevelEditor.selectedStructure == StructureType.Spikes)
                    LevelEditor.BuildSpikes(currentStage);
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
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.S))
            {
                string saveName = "auto_save_" + TKGame.levelComponent.GetCurrentStage().stageName.Replace(".json", ""); // room0.json -> room0
                LevelEditor.SaveStageDataToJSON(TKGame.levelComponent.GetCurrentStage(), saveName);
            }

            #region Structure Selection
            // Ctrl + 1 = Wall; Ctrl + 2 = Spikes; Ctrl + 3 = None
            // TODO: add visual indicator for selected structure
            if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.D1))
            {
                LevelEditor.selectedStructure = StructureType.Wall;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.D2))
            {
                LevelEditor.selectedStructure = StructureType.Spikes;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.D3))
            {
                //LevelEditor.selectedStructure = StructureType.None;
            }
            #endregion

            #region Color Selection
            // Alt + 1 = White; Alt + 2 = Red; Alt + 3 = Green etc
            if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D1))
            {
                LevelEditor.selectedColor = Color.White;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D2))
            {
                LevelEditor.selectedColor = Color.Red;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D3))
            {
                LevelEditor.selectedColor = Color.Green;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D4))
            {
                LevelEditor.selectedColor = Color.Blue;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D5))
            {
                LevelEditor.selectedColor = Color.Yellow;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D6))
            {
                LevelEditor.selectedColor = Color.Purple;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D7))
            {
                LevelEditor.selectedColor = Color.Orange;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D8))
            {
                LevelEditor.selectedColor = Color.Pink;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D9))
            {
                LevelEditor.selectedColor = Color.Brown;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.LeftAlt) && Input.WasKeyPressed(Keys.D0))
            {
                LevelEditor.selectedColor = Color.Black;
            }
            #endregion

            LevelEditor.DrawGridLines(Color.Black);
        }
    }
}
