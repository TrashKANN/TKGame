using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Enemy
{
    public class KnightEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy()
        {
            return new KnightEnemy();
        }
    }

    //public class DoomguyEnemy : EnemyFactory
    //{
    //    public override Enemy CreateEnemy()
    //    {
    //        return new DoomguyEnemy();
    //    }
    //}
}
