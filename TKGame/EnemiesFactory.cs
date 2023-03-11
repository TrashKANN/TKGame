using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    public class EnemiesFactory
    {
        public static IEnemies GetEnemy(string enemyType)
        {
            IEnemies enemyDetails = null;

            if (enemyType == "Goblin")
            {
                enemyDetails = new Goblin();
            }
            else if (enemyType == "Knight")
            {
                enemyDetails = new Knight();
            }

            return enemyDetails;
        }
    }
}
