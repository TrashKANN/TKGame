using System.Linq;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.TextureAtlases;
using TKGame.BackEnd;
using FontStashSharp;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using TKGame.Players;
using TKGame.Components.Interface;
using System.Collections.Generic;
using TKGame.PowerUps.Components.FirePowerUps;

namespace TKGame.UI
{
    public class GameMenu : IMenu
    {
        // IMenu.Container
        public IMultipleItemsContainer Container { get { return grid; } }

        // Private member variables
        private Grid grid;
        private DebugMenu debugMenu;
        private Panel panel;
        private HorizontalStackPanel panelHsp;
        private FontSystem fontSystem;
        private Label playerHealthLabel;
        private Texture2D previousPlayerWeaponTexture;
        private Dictionary<string, Grid> infoGridDict;

        // Private constants
		private readonly int numCols = 1;
        private readonly int numRows = 2;
        private readonly int fontSize = 24;
        private readonly int playerHpFontSize = 48;

        /// <summary>
        /// Initialize and load content when the menu is constructed
        /// </summary>
        public GameMenu()
        {
            Initialize();
            LoadContent();
        }

        /// <summary>
        /// Create all the appropriate member variables
        /// </summary>
        private void Initialize()
        {
            grid = new Grid();
            debugMenu = new DebugMenu();
            panel = new Panel();
            panelHsp = new HorizontalStackPanel();
            playerHealthLabel = new Label();
			infoGridDict = new Dictionary<string, Grid>();
            previousPlayerWeaponTexture = null;

			byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
			fontSystem = new FontSystem();
			fontSystem.AddFont(ttfData);
		}

        /// <summary>
        /// Setup all the UI components
        /// </summary>
        private void LoadContent()
        {
            // Add rows to grid
            for (int i = 0; i < numCols; i++)
            {
                grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            }
            
            // Add columns to grid
            for (int i = 0; i < numRows; i++)
            {
                grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            }

            // Configure panel
            panel.GridRow = 0;
            panel.GridColumn = 0;
            panel.Height = 125;
            panel.Width = TKGame.ScreenWidth;
            panel.Margin = new Myra.Graphics2D.Thickness(100, 50, 0, 0);
            grid.Widgets.Add(panel);

            // Configure panel's horizontal stackpanel (hsp)
            panelHsp.Spacing = 10;
            panel.Widgets.Add(panelHsp);

            // Add player health to panel's hsp
            playerHealthLabel.Font = fontSystem.GetFont(playerHpFontSize);
            AddWidgetToHeaderPanel(playerHealthLabel, "HP");
            
            // Add weapon and powerup images to panel's hsp
            // Powerups are marked as not visible on startup
            AddWidgetToHeaderPanel(CreateImageWidget(Art.WeaponTexture, 60, 60), "Weapon");
			AddWidgetToHeaderPanel(CreateImageWidget(Art.SunBurstTexture, 60, 35, new Rectangle(0, 0, 400, 153)), "Q", false);
			AddWidgetToHeaderPanel(CreateImageWidget(Art.FireBallTexture, 60, 35), "E", false);
			AddWidgetToHeaderPanel(CreateImageWidget(Art.BurningTexture, 60, 60), "Shift", false);
            // AddWidgetToHeaderPanel(CreateImageWidget(Art.ChilledTexture, 60, 60), "I", false);
            // AddWidgetToHeaderPanel(CreateImageWidget(Art.ShockedTexture, 60, 60), "O", false);

            // Add debug menu to the game menu
			(debugMenu.Container as VerticalStackPanel).GridRow = 1;
            (debugMenu.Container as VerticalStackPanel).GridColumn = 0;
            grid.Widgets.Add(debugMenu.Container as VerticalStackPanel);

        }

        /// <summary>
        /// Create an Image widget given texture, width/height of image on screen, and an optional id (name)
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private Image CreateImageWidget(Texture2D texture, int width, int height)
        {
            return CreateImageWidget(texture, width, height, new Rectangle(0, 0, texture.Width, texture.Height));
        }

        /// <summary>
        /// Create an Image widget given texture, width/height of image on screen, 
        /// section of texture to render in image, an optional id (name)
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="textureBounds"></param>
        /// <param name="name"></param>
        /// <returns></returns>
		private Image CreateImageWidget(Texture2D texture, int width, int height, Rectangle textureBounds)
		{
			Image image = new Image();
			image.Renderable = new TextureRegion(texture, textureBounds);
			image.Width = width;
			image.Height = height;

			return image;
		}

        /// <summary>
        /// Add specified widget to "header panel" (panel at the top of the UI) given a widget,
        /// optional text to be added as a label below the widget, and optional bool to specify
        /// if the object is visible on when first added.
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="text"></param>
        /// <param name="isVisible"></param>
		private void AddWidgetToHeaderPanel(Widget widget, string text = null, bool isVisible = true)
        {
            Grid newGrid = new Grid();

            // Add 1 column and 2 rows to the new grid. First row is 60 pixels tall
            newGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            newGrid.RowsProportions.Add(
                new Proportion() 
                {
                    Type = ProportionType.Pixels,
                    Value = 60
                });
            newGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            // Set spacing between new grid rows/cols
            newGrid.RowSpacing = 10;
            newGrid.ColumnSpacing = 10;

            // More newGrid configuration
            newGrid.VerticalAlignment = VerticalAlignment.Bottom;
            newGrid.Border = new SolidBrush(Color.White);
            newGrid.BorderThickness = new Myra.Graphics2D.Thickness(0, 0, 2, 2);
            newGrid.Padding = new Myra.Graphics2D.Thickness(5);
			newGrid.Background = new SolidBrush(new Color(Color.Black, 50));
            newGrid.Visible = isVisible;

            // Place the widget and align it
			widget.GridColumn = 0;
            widget.GridRow = 0;
            widget.HorizontalAlignment = HorizontalAlignment.Center;
            widget.VerticalAlignment = VerticalAlignment.Bottom;
            newGrid.Widgets.Add(widget);

            // Add a label in the second row of the new grid if text is not null
            if (text is not null)
            {
                Label widgetLabel = new Label();
                widgetLabel.Text = text;
                widgetLabel.Font = fontSystem.GetFont(fontSize);
                widgetLabel.HorizontalAlignment = HorizontalAlignment.Center;
                widgetLabel.GridColumn = 0;
                widgetLabel.GridRow = 1;

                newGrid.Widgets.Add(widgetLabel);
                infoGridDict[text] = newGrid;
            }

            // Add new grid to panel's hsp
			panelHsp.Widgets.Add(newGrid);
        }

        /// <summary>
        /// Update appropriate data for the GameMenu
        /// </summary>
        public void Update()
        {
            Texture2D currentPlayerWeaponTexture = Player.Instance.weapon.weaponTexture;
            if (previousPlayerWeaponTexture != currentPlayerWeaponTexture)
            {
                ChangeWeaponTexture(currentPlayerWeaponTexture);
                previousPlayerWeaponTexture = currentPlayerWeaponTexture;
			}

            float playerHp = Player.Instance.health;

			playerHealthLabel.Text = playerHp.ToString();
            playerHealthLabel.TextColor = (playerHp > 0) ? Color.LimeGreen : Color.Red;

            List<IAttackComponent> attackComponents = Player.Instance.GetAttackComponents();
			if (attackComponents.Count > 0)
            {
                EnableAttackComponentWidgets(attackComponents);
            }
        }

        /// <summary>
        /// Show powerup images/labels 
        /// </summary>
        private void EnableAttackComponentWidgets(List<IAttackComponent> attackComponents)
        {
            // TODO: Make this more flexible for when other powerups are collected
            foreach (IAttackComponent attackComponent in attackComponents) 
            {
                if      (attackComponent is C_Fire_SpecialAttack)  ShowWidget("Q");
				else if (attackComponent is C_Fire_UltimateAttack) ShowWidget("E");
				else if (attackComponent is C_Fire_MovementAttack) ShowWidget("Shift");
				//else if (attackComponent is C_Ice_PrimaryAttack) ShowWidget("I");
				//else if (attackComponent is C_Ice_SpecialAttack) { }
				//else if (attackComponent is C_Ice_UltimateAttack) { }
				//else if (attackComponent is C_FireStone_PrimaryAttack) ShowWidget("O");
			}
		}

        private void ShowWidget(string id)
        {
            infoGridDict[id].Visible = true;
        }

        /// <summary>
        /// Change the texture of the current weapon the player has
        /// </summary>
        /// <param name="texture"></param>
        public void ChangeWeaponTexture(Texture2D texture)
        {
            foreach (Widget widget in infoGridDict["Weapon"].Widgets)
            {
                if (widget is Image)
                {
                    (widget as Image).Renderable = new TextureRegion(texture, new Rectangle(0, 0, texture.Width, texture.Height));
                }
            }
        }
    }
}
