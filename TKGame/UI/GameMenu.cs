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
using FontStashSharp;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.UI
{
    public class GameMenu : IMenu
    {
        public IMultipleItemsContainer Container { get { return grid; } }
        private Grid grid;
        private DebugMenu debugMenu;
        private Panel panel;
        private HorizontalStackPanel panelHsp;
        private FontSystem fontSystem;

		private readonly int numCols = 1;
        private readonly int numRows = 2;
        private readonly int fontSize = 24;


        public GameMenu()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            grid = new Grid();
            grid.ShowGridLines = true;
            debugMenu = new DebugMenu();
            panel = new Panel();
            panelHsp = new HorizontalStackPanel();
            panelHsp.ShowGridLines = true;

			byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
			fontSystem = new FontSystem();
			fontSystem.AddFont(ttfData);
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


            //panel.Background = new SolidBrush(Color.Beige);
            panel.GridRow = 0;
            panel.GridColumn = 0;
            panel.Height = 125;
            panel.Width = TKGame.ScreenWidth;
            panel.Margin = new Myra.Graphics2D.Thickness(100, 50, 0, 0);
            grid.Widgets.Add(panel);

            panelHsp.Spacing = 10;
            panel.Widgets.Add(panelHsp);

            
            AddWidgetToHeaderPanel(CreateImageWidget(Art.WeaponTexture, 70, 70), "Weapon");
			AddWidgetToHeaderPanel(CreateImageWidget(Art.FireBallTexture, 60, 35), "Q");
			AddWidgetToHeaderPanel(CreateImageWidget(Art.SunBurstTexture, 60, 35, new Rectangle(0, 0, 400, 153)), "R");


			(debugMenu.Container as VerticalStackPanel).GridRow = 1;
            (debugMenu.Container as VerticalStackPanel).GridColumn = 0;
            grid.Widgets.Add(debugMenu.Container as VerticalStackPanel);

        }

        private Image CreateImageWidget(Texture2D texture, int width, int height)
        {
            return CreateImageWidget(texture, width, height, new Rectangle(0, 0, texture.Width, texture.Height));
        }

		private Image CreateImageWidget(Texture2D texture, int width, int height, Rectangle textureBounds)
		{
			Image image = new Image();
			image.Renderable = new TextureRegion(texture, textureBounds);
			image.Width = width;
			image.Height = height;
			image.VerticalAlignment = VerticalAlignment.Bottom;

			return image;
		}

		private void AddWidgetToHeaderPanel(Widget widget, string text = null)
        {
            
            Grid newGrid = new Grid();

            newGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            newGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            newGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            newGrid.RowSpacing = 10;
            newGrid.VerticalAlignment = VerticalAlignment.Bottom;

            widget.GridColumn = 0;
            widget.GridRow = 0;
            newGrid.Widgets.Add(widget);

            if (text is not null)
            {
                Label widgetLabel = new Label();
                widgetLabel.Text = text;
                widgetLabel.Font = fontSystem.GetFont(fontSize);
                widgetLabel.HorizontalAlignment = HorizontalAlignment.Center;
                widgetLabel.GridColumn = 0;
                widgetLabel.GridRow = 1;

                newGrid.Widgets.Add(widgetLabel);
            }

			panelHsp.Widgets.Add(newGrid);
        }
    }
}
