using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TKGame.Animations;

namespace TKGame.Level_Editor_Content
{
    public class Level
    {
        public static Dictionary<string, Stage> levelStages { get; set; }
        public Stage currentStage;
        public Stage prevStage;
        public Stage nextStage;

        public ScreenTransition transition;
        public bool isTransitioning { get; set; }


        public bool isCurrentStageFirst { get; set; }
        public bool isCurrentStageFinal { get; set; }

        /// <summary>
        /// Initializes the level with a dictionary of stages. Assumes that the first stage in the dictionary is the first stage in the level.
        /// </summary>
        /// <param name="stages"></param>
        public Level(Dictionary<string, Stage> stages)
        {
            levelStages = new Dictionary<string, Stage>(stages) { };

            transition = new ScreenTransition();
            isTransitioning = false;

            currentStage = levelStages.FirstOrDefault().Value;

            prevStage = null;

            nextStage = levelStages.ElementAt(1).Value;

            isCurrentStageFirst = true;
            isCurrentStageFinal = false;
        }

        public void Update()
        {
            currentStage.Update();
        }

        public Stage GetCurrentStage() { return currentStage; }
        public Stage GetPreviousStage() { return prevStage; }
        public Stage GetNextStage() { return nextStage; }
        public Dictionary<string, Stage> GetStages() { return levelStages; }
        public void SetCurrentStage(Stage stage) { currentStage = stage; }
    }
}
