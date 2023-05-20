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
        public static MenuState CurrentMenuState { get; private set; }
        private static Dictionary<MenuState, IMenu> menus;
        public static Dictionary<MenuState, IMenu> Menus { get { return menus; } }

        static MenuHandler()
        {
            menus = new Dictionary<MenuState, IMenu>()
            {
                { MenuState.MAIN_MENU,      new MainMenu()     },
                { MenuState.GAME_MENU,      new GameMenu()     },
                { MenuState.SETTINGS_MENU,  new SettingsMenu() },
                { MenuState.PAUSE_MENU,     new PauseMenu()    },
            };
            SwitchToMenu(MenuState.MAIN_MENU);
            CurrentMenuState = MenuState.MAIN_MENU;
        }

        public static void SwitchToMenu(MenuState state)
        {
            try
            {
                Desktop.Root = (Widget)menus[state].Container;
                CurrentMenuState = state;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void UpdateMenus()
        {
            DebugMenu.Update();
            PauseMenu.Update();
            (menus[MenuState.GAME_MENU] as GameMenu).Update();
        }
    }
}
