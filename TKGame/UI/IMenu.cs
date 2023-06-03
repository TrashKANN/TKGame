using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace TKGame.UI
{
    public interface IMenu
    {
        public IMultipleItemsContainer Container { get; }
    }
}
