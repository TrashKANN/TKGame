using System;
using System.Collections.Generic;
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
        }

        void LevelComponent.Update()
        {
            currentLevel.Update();
        }

        public Level GetCurrentLevel() { return currentLevel; }
        public Stage GetCurrentStage() { return currentLevel.GetCurrentStage();}
    }
}
