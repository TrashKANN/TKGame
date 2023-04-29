using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Level_Editor_Content;

namespace TKGame.Components.Interface
{
    public interface ILevelComponent : IComponent
    {
        public void LevelComponent() { }
        public void Update();
        public void AddLevel(Level newLevel);
        public Level GetCurrentLevel();
        public Stage GetCurrentStage();
        public Stage GetPreviousStage();
        public Stage GetNextStage();
        public bool IsCurrentStageFirst();
        public bool IsCurrentStageFinal();
        public void SetCurrentStage(Stage stage);
        public void GoToNextStage();
        public void GoToPreviousStage();
    }
}
