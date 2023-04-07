using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Level_Editor_Content;

namespace TKGame.Components.Interface
{
    public interface LevelComponent
    {
        virtual public void LevelComponent() { }
        abstract internal void Update();
        abstract internal void AddLevel(Level newLevel);
        abstract internal Level GetCurrentLevel();
        abstract internal Stage GetCurrentStage();
    }
}
