using FontStashSharp;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.World;

namespace TKGame.Items
{
    class ItemText : IItemUI
    {
        public IMultipleItemsContainer multipleItemsContainer => throw new NotImplementedException();

        private static TextBox itemInfo;
        private static Grid itemDisplayGrid;
        private static FontSystem itemDisplayFontSystem;

        private static readonly int NUM_GRID_ROWS = 1;
        private static readonly int ITEM_DISPLAY_FONT_SIZE = 65;

        private static readonly Color ITEM_DISPLAY_TEXT_COLOR = Color.Gold;

        private static readonly string ITEM_DISPLAY_STYLE_NAME = "Item Picked Up";

        public ItemText()
        {
            //itemInfo = new TextBox();
            //itemDisplayGrid = new Grid();
            //itemDisplayFontSystem = new FontSystem();
            Initialize();
            LoadContent();
        } 

        private static void Initialize()
        {

        }

        private static void LoadContent()
        {

        }
    }
}
