using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TKGame.Level_Editor_Content
{
    internal class Level
    {
        internal static List<Stage> levelStages { get; set; }
        internal Stage currentStage;
        internal Stage prevStage;
        internal Stage nextStage;

        internal bool isCurrentStageFirst { get; set; }
        internal bool isCurrentStageFinal;

        public Level(List<Stage> stages)
        {
            levelStages = new List<Stage>(stages) { };

            currentStage = levelStages.FirstOrDefault();

            prevStage = null;

            nextStage = levelStages.ElementAt(1);
        }

        public void Update()
        {
            currentStage.Update();
        }

        internal Stage GetCurrentStage() { return currentStage; }
        internal Stage GetPreviousStage() { return prevStage; }
        internal Stage GetNextStage() { return nextStage; }
        internal List<Stage> GetStages() { return levelStages; }
        internal void SetCurrentStage(Stage stage) { currentStage = stage; }
    }
}
