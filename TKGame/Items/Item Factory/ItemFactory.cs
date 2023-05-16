using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Items;

namespace TKGame
{
    public abstract class ItemFactory
    {
        public abstract Item CreateItem();
    }
}
