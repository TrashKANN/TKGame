using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;
using TKGame.BackEnd;

namespace TKGame.UI
{
    public static class MenuHandler
    {
        public enum MenuState
        {
            MAIN_MENU,
            GAME_MENU,
            SETTINGS_MENU,
            PAUSE_MENU
        }

        public static Desktop Desktop { get; private set; } = new Desktop();
        private static IMenu CurrentMenu { get; set; }
        private static Dictionary<MenuState, IMenu> menus;

        static MenuHandler()
        {
            //CurrentMenu = menus[MenuState.MAIN_MENU];
            menus = new Dictionary<MenuState, IMenu>()
            {
                { MenuState.MAIN_MENU,      new MainMenu()     },
                { MenuState.GAME_MENU,      new DebugMenu()    },
                { MenuState.SETTINGS_MENU,  new SettingsMenu() },
                { MenuState.PAUSE_MENU,     new PauseMenu()    },
            };
            SwitchToMenu(MenuState.MAIN_MENU);
        }

        public static void SwitchToMenu(MenuState state)
        {
            try
            {
                Desktop.Root = (Widget)menus[state].Container;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void UpdateMenus()
        {
            DebugMenu.Update();
        }
    }
}
