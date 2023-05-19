using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.TextureAtlases;
using TKGame.BackEnd;

namespace TKGame.UI
{
    public class GameMenu : IMenu
    {
        public IMultipleItemsContainer Container { get { return grid; } }
        private Grid grid;
        private Grid headerGrid;
        private DebugMenu debugMenu;
        private Panel panel;
        private Image image;
        private HorizontalStackPanel hsp;
        private readonly int numCols = 1;
        private readonly int numRows = 2;


        public GameMenu()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            grid = new Grid();
            grid.ShowGridLines = true;
            headerGrid = new Grid();
            debugMenu = new DebugMenu();
            panel = new Panel();
            image = new Image();
            hsp = new HorizontalStackPanel();
        }

        private void LoadContent()
        {
            for (int i = 0; i < numCols; i++)
            {
                grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            }
            
            for (int i = 0; i < numRows; i++)
            {
                grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            }

            for (int i = 0; i < 3; i++)
            {
                headerGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            }

			headerGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            headerGrid.GridRow = 0;
            headerGrid.GridColumn = 0;
            //grid.Widgets.Add(headerGrid);

			//panel.Background = new SolidBrush(Color.Beige);
            panel.GridRow = 0;
            panel.GridColumn = 0;
            panel.Height = 75;
            panel.Width = TKGame.ScreenWidth;
            grid.Widgets.Add(panel);

            panel.Widgets.Add(hsp);

            image.Renderable = new TextureRegion(
                Art.WeaponTexture, 
                new Rectangle(
                    0,
                    0,
                    Art.WeaponTexture.Width,
                    Art.WeaponTexture.Height));
            image.GridRow = 0;
            image.GridColumn = 0;
            image.Width = 75;
            hsp.Widgets.Add(image);

            (debugMenu.Container as VerticalStackPanel).GridRow = 1;
            (debugMenu.Container as VerticalStackPanel).GridColumn = 0;
            grid.Widgets.Add(debugMenu.Container as VerticalStackPanel);
        }
    }
}
