using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Enemy
{
    public abstract class EnemyFactory
    {
        public abstract Enemy CreateEnemy();
    }
}
