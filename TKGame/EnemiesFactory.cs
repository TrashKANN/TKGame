using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Enemy;

namespace TKGame
{
    public class EnemiesFactory
    {
        public IEnemies CreateEnemy(string enemyType)
        {
            switch (enemyType)
            {
                case "Goblin":
                    return new Goblin();
                case "Knight":
                    return new Goblin();
                default: 
                    return new Goblin();
            }
        }
    }
}
