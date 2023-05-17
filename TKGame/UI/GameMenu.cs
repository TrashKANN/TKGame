using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra.Graphics2D.UI;

namespace TKGame.UI
{
    public class GameMenu : IMenu
    {
        // TODO: Implement gameplay menu
        public IMultipleItemsContainer Container { get { return grid; } }
        private Grid grid;
        private DebugMenu debugMenu;

        public GameMenu()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            grid = new Grid();
            debugMenu = new DebugMenu();
        }

        private void LoadContent()
        {
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));

            (debugMenu.Container as VerticalStackPanel).GridRow = 1;
            (debugMenu.Container as VerticalStackPanel).GridColumn = 0;
            grid.Widgets.Add(debugMenu.Container as VerticalStackPanel);
        }
    }
}
