using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    public class Knight : IEnemies
    {
        public string GetEnemyType()
        {
            return "Knight";
        }
    }
}
