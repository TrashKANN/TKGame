using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;

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
            var levelStages = currentLevel.GetStages();
            
            if (!currentLevel.isCurrentStageFinal)
            {
                currentLevel.prevStage = currentLevel.currentStage;
                currentLevel.SetCurrentStage(currentLevel.nextStage);

                currentLevel.currentStage.Initialize();

                if (levelStages.IndexOf(currentLevel.currentStage) == levelStages.Count - 1)
                {
                    currentLevel.isCurrentStageFinal = true;
                    currentLevel.nextStage = null;
                }
                else
                {
                    currentLevel.isCurrentStageFinal = false;
                    currentLevel.nextStage = levelStages.ElementAt(levelStages.IndexOf(currentLevel.currentStage) + 1);
                }
            }
        }
        void LevelComponent.GoToPreviousStage()
        {
            Stage prevStage = currentLevel.GetPreviousStage();
            Stage currentStage = currentLevel.GetCurrentStage();
            Stage nextStage = currentLevel.GetNextStage();
            var levelStages = currentLevel.GetStages();

            if (!currentLevel.isCurrentStageFirst)
            {
                nextStage = currentStage;
                currentLevel.SetCurrentStage(prevStage);

                currentStage.Initialize();

                if (levelStages.IndexOf(currentStage) > 0)
                {
                    currentLevel.isCurrentStageFirst = false;
                    prevStage = levelStages.ElementAt(levelStages.IndexOf(currentStage) - 1);
                }
                else
                {
                    currentLevel.isCurrentStageFirst = true;
                    prevStage = null;
                }
            }
        }
    }
}
