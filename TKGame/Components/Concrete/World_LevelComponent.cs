using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;
using System.Text.RegularExpressions;

namespace TKGame.Components.Concrete
{
    internal class World_LevelComponent : LevelComponent
    {
        #region Members
        private List<Level> levels;
        private Level currentLevel;
        #endregion Members

        #region Properties
        public List<Level> Levels { get { return levels; } }
        public Level CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
        #endregion Properties

        public World_LevelComponent(List<Level> levels)
        {
            this.levels = levels;
            currentLevel = levels.FirstOrDefault();
        }

        public void AddLevel(Level newLevel)
        {
            levels.Add(newLevel);
            if (currentLevel == null)
                currentLevel = newLevel;
        }

        void LevelComponent.Update()
        {
            currentLevel.Update();
        }

        public Level GetCurrentLevel() { return currentLevel; }
        public Stage GetCurrentStage() { return currentLevel.GetCurrentStage();}
        Stage LevelComponent.GetPreviousStage() { return currentLevel.GetPreviousStage(); }
        Stage LevelComponent.GetNextStage() { return currentLevel.GetNextStage(); }
        bool LevelComponent.IsCurrentStageFirst() { return currentLevel.isCurrentStageFirst; }
        bool LevelComponent.IsCurrentStageFinal() { return currentLevel.isCurrentStageFinal; }
        void LevelComponent.SetCurrentStage(Stage stage) { currentLevel.SetCurrentStage(stage); }
        void LevelComponent.GoToNextStage()
        {
            Dictionary<string, Stage> levelStages = currentLevel.GetStages();
            
            if (!currentLevel.isCurrentStageFinal)
            {
                currentLevel.isCurrentStageFirst = false;
                currentLevel.prevStage = currentLevel.currentStage;
                currentLevel.SetCurrentStage(currentLevel.nextStage);

                Player.Instance.Position.X = 100;

                currentLevel.currentStage.Initialize();

                if (levelStages.Last().Value == currentLevel.currentStage)
                {
                    currentLevel.isCurrentStageFinal = true;
                    currentLevel.nextStage = null;
                }
                else
                {
                    currentLevel.isCurrentStageFinal = false;

                    int currentRoomNum = int.Parse(Regex.Match(currentLevel.currentStage.stageName, @"\d+").Value);
                    string nextStageName = "room" + (currentRoomNum + 1).ToString();

                    currentLevel.nextStage = levelStages[nextStageName];
                }

                // Play transision
            }
        }
        void LevelComponent.GoToPreviousStage()
        {
            Dictionary<string, Stage> levelStages = currentLevel.GetStages();

            if (!currentLevel.isCurrentStageFirst)
            {
                currentLevel.isCurrentStageFinal = false;

                currentLevel.nextStage = currentLevel.currentStage;
                currentLevel.SetCurrentStage(currentLevel.prevStage);

                Player.Instance.Position.X = TKGame.ScreenWidth - 100;

                currentLevel.currentStage.Initialize();

                if (levelStages.First().Value == currentLevel.currentStage)
                {
                    currentLevel.isCurrentStageFirst = true;
                    currentLevel.prevStage = null;
                }
                else
                {
                    currentLevel.isCurrentStageFirst = false;

                    int currentRoomNum = int.Parse(Regex.Match(currentLevel.currentStage.stageName, @"\d+").Value);
                    string prevStageName = "room" + (currentRoomNum - 1).ToString();

                    currentLevel.prevStage = levelStages[prevStageName];
                }

                // Play transision

            }
        }
    }
}
