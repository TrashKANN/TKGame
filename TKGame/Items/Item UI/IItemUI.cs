using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.World;

namespace TKGame.Items
{
    public interface IItemUI
    {
        public IMultipleItemsContainer multipleItemsContainer { get; }
    }
}
