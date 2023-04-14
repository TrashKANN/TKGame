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
        private static List<Stage> levelStages { get; set; }
        private Stage currentStage;
        private Stage prevStage;
        private Stage nextStage;

        private bool isCurrentStageFirst;
        private bool isCurrentStageFinal;

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


    }
}
