using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra.Graphics2D.UI;

namespace TKGame.UI
{
    public class PauseMenu : IMenu
    {
        public IMultipleItemsContainer Container { get { return Grid; } }
        private static Grid Grid { get; set; }
    }
}
