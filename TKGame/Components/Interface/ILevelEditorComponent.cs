﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public interface ILevelEditorComponent : IComponent
    {
        public void LevelEditorComponent() { }
        public void Update();
    }
}
