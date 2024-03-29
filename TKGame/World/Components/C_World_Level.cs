﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;
using System.Text.RegularExpressions;

namespace TKGame.World.Components
{
    internal class C_World_Level : ILevelComponent
    {
        #region Members
        private List<Level> levels;
        private Level currentLevel;
        #endregion Members

        #region Properties
        ComponentType IComponent.Type => ComponentType.Level;
        public List<Level> Levels { get { return levels; } }
        public Level CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
        #endregion Properties

        /// <summary>
        /// Initializes the World_LevelComponent with a list of levels and sets the current level to the first level in the list.
        /// </summary>
        /// <param name="levels"></param>
        public C_World_Level(List<Level> levels)
        {
            this.levels = levels;
            currentLevel = levels.FirstOrDefault();
        }

        /// <summary>
        /// Takes a level and adds it to the list of levels.
        /// </summary>
        /// <param name="newLevel"></param>
        public void AddLevel(Level newLevel)
        {
            levels.Add(newLevel);
            if (currentLevel == null)
                currentLevel = newLevel;
        }

        void ILevelComponent.Update()
        {
            currentLevel.Update();
        }

        public Level GetCurrentLevel() { return currentLevel; }
        public Stage GetCurrentStage() { return currentLevel.GetCurrentStage(); }
        Stage ILevelComponent.GetPreviousStage() { return currentLevel.GetPreviousStage(); }
        Stage ILevelComponent.GetNextStage() { return currentLevel.GetNextStage(); }
        bool ILevelComponent.IsCurrentStageFirst() { return currentLevel.isCurrentStageFirst; }
        bool ILevelComponent.IsCurrentStageFinal() { return currentLevel.isCurrentStageFinal; }
        void ILevelComponent.SetCurrentStage(Stage stage) { currentLevel.SetCurrentStage(stage); }

        /// <summary>
        /// Sets the current stage to the next stage in the level. If the current stage is the final stage, the current stage is set to the first stage.
        /// 
        /// </summary>
        void ILevelComponent.GoToNextStage()
        {
            Dictionary<string, Stage> levelStages = currentLevel.GetStages();

            // If the current stage is not the final stage, set the current stage to the next stage.
            if (!currentLevel.isCurrentStageFinal)
            {
                // *Linked list flash backs*
                currentLevel.isCurrentStageFirst = false;
                currentLevel.prevStage = currentLevel.currentStage;
                currentLevel.SetCurrentStage(currentLevel.nextStage);

                Players.Player.Instance.Position.X = 100;

                currentLevel.currentStage.Initialize();

                // If the last stage in the stage list is the current stage, set the isCurrnetStageFinal to true and set the next stage to null.
                // Else, set the isCurrentStageFinal to false and set the next stage to the next stage in the level.
                if (levelStages.Last().Value == currentLevel.currentStage)
                {
                    currentLevel.isCurrentStageFinal = true;
                    currentLevel.nextStage = null;
                }
                else
                {
                    currentLevel.isCurrentStageFinal = false;

                    int currentRoomNum = int.Parse(Regex.Match(currentLevel.currentStage.stageName, @"\d+").Value);
                    string nextStageName = "stage" + (currentRoomNum + 1).ToString();

                    currentLevel.nextStage = levelStages[nextStageName];
                }

                // Play transision
            }
        }

        /// <summary>
        /// Sets the current stage to the previous stage in the level.
        /// </summary>
        void ILevelComponent.GoToPreviousStage()
        {
            Dictionary<string, Stage> levelStages = currentLevel.GetStages();

            // If the current stage is not the first stage, set the current stage to the previous stage.
            if (!currentLevel.isCurrentStageFirst)
            {
                currentLevel.isCurrentStageFinal = false;

                currentLevel.nextStage = currentLevel.currentStage;
                currentLevel.SetCurrentStage(currentLevel.prevStage);

                Players.Player.Instance.Position.X = TKGame.ScreenWidth - 100;

                currentLevel.currentStage.Initialize();

                // If the first stage in the stage list is the current stage, set the isCurrnetStageFirst to true and set the previous stage to null.
                // Else, set the isCurrentStageFirst to false and set the previous stage to the previous stage in the level.
                if (levelStages.First().Value == currentLevel.currentStage)
                {
                    currentLevel.isCurrentStageFirst = true;
                    currentLevel.prevStage = null;
                }
                else
                {
                    currentLevel.isCurrentStageFirst = false;

                    int currentRoomNum = int.Parse(Regex.Match(currentLevel.currentStage.stageName, @"\d+").Value);
                    string prevStageName = "stage" + (currentRoomNum - 1).ToString();

                    currentLevel.prevStage = levelStages[prevStageName];
                }

                // Play transision

            }
        }
    }
}
