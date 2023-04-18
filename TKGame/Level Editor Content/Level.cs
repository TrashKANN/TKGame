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
    internal class Level
    {
        internal static Dictionary<string, Stage> levelStages { get; set; }
        internal Stage currentStage;
        internal Stage prevStage;
        internal Stage nextStage;

        internal ScreenTransition transition;
        public bool isTransitioning { get; set; }


        internal bool isCurrentStageFirst { get; set; }
        internal bool isCurrentStageFinal { get; set; }

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

        internal Stage GetCurrentStage() { return currentStage; }
        internal Stage GetPreviousStage() { return prevStage; }
        internal Stage GetNextStage() { return nextStage; }
        internal Dictionary<string, Stage> GetStages() { return levelStages; }
        internal void SetCurrentStage(Stage stage) { currentStage = stage; }
    }
}
