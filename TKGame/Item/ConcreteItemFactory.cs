using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    public class PotionItemFactory : ItemFactory
    {
        public override Item CreateItem()
        {
            return new PotionItem();
        }
    }
}
