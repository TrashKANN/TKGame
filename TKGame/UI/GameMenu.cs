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
using TKGame.Players;

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
        private Label playerHealthLabel;

		private readonly int numCols = 1;
        private readonly int numRows = 2;
        private readonly int fontSize = 24;
        private readonly int playerHpFontSize = 48;


        public GameMenu()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            grid = new Grid();
            debugMenu = new DebugMenu();
            panel = new Panel();
            panelHsp = new HorizontalStackPanel();
            playerHealthLabel = new Label();

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

            panel.GridRow = 0;
            panel.GridColumn = 0;
            panel.Height = 125;
            panel.Width = TKGame.ScreenWidth;
            panel.Margin = new Myra.Graphics2D.Thickness(100, 50, 0, 0);
            grid.Widgets.Add(panel);

            panelHsp.Spacing = 10;
            panel.Widgets.Add(panelHsp);

            playerHealthLabel.Font = fontSystem.GetFont(playerHpFontSize);
            AddWidgetToHeaderPanel(playerHealthLabel, "HP");
            
            AddWidgetToHeaderPanel(CreateImageWidget(Art.WeaponTexture, 60, 60), "Weapon");
			AddWidgetToHeaderPanel(CreateImageWidget(Art.FireBallTexture, 60, 35), "Q");
			AddWidgetToHeaderPanel(CreateImageWidget(Art.SunBurstTexture, 60, 35, new Rectangle(0, 0, 400, 153)), "R");
			AddWidgetToHeaderPanel(CreateImageWidget(Art.BurningTexture, 60, 60), "Shift");


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

			return image;
		}

		private void AddWidgetToHeaderPanel(Widget widget, string text = null)
        {
            Grid newGrid = new Grid();

            newGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            newGrid.RowsProportions.Add(
                new Proportion() 
                {
                    Type = ProportionType.Pixels,
                    Value = 60
                });
            newGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            newGrid.RowSpacing = 10;
            newGrid.ColumnSpacing = 10;
            newGrid.VerticalAlignment = VerticalAlignment.Bottom;
            newGrid.Border = new SolidBrush(Color.White);
            newGrid.BorderThickness = new Myra.Graphics2D.Thickness(0, 0, 2, 2);
            newGrid.Padding = new Myra.Graphics2D.Thickness(5);
			newGrid.Background = new SolidBrush(new Color(Color.Black, 50));


			widget.GridColumn = 0;
            widget.GridRow = 0;
            widget.HorizontalAlignment = HorizontalAlignment.Center;
            widget.VerticalAlignment = VerticalAlignment.Bottom;
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

        public void Update()
        {
            int playerHp = Player.Instance.health;

			playerHealthLabel.Text = playerHp.ToString();
            playerHealthLabel.TextColor = (playerHp > 0) ? Color.LimeGreen : Color.Red;
        }
    }
}
