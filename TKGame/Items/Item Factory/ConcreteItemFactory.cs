using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Items.FireStone;
using TKGame.Items.Potion;

namespace TKGame.Items
{
    // concrete potion item factory
    public class PotionItemFactory : ItemFactory
    {
        // returns a new item of type potion
        public override Item CreateItem()
        {
            return new PotionItem();
        }
    }

    // concrete firestone item factory
    public class FireStoneItemFactory : ItemFactory
    {
        // returns a new item of type firestone
        public override Item CreateItem()
        {
            return new FireStoneItem();
        }
    }
}
