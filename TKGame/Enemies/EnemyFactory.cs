﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;

namespace TKGame.Enemies
{
    public abstract class EnemyFactory
    {
        public abstract Enemy CreateEnemy();
    }
}
