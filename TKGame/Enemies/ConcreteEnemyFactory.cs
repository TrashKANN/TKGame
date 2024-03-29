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
    public class KnightEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy()
        {
            return new KnightEnemy();
        }
    }
    public class GoblinEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy()
        {
            return new GoblinEnemy();
        }
    }
}
