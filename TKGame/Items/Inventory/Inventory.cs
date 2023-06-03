using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Items
{
    // container class 'Inventory' 
    // uses List<Item> '_items' to store items
    public class Inventory
    {
        // private list to store items
        private List<Item> _items;

        // constructor to create a new list
        public Inventory() 
        {
            _items = new List<Item>();
        }

        // allows items to be added to the inventory
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        // allows items to be removed from the inventory
        public void RemoveItem(Item item) 
        { 
            _items.Remove(item);
        }

        // returns a list of all the items in the inventory
        public List<Item> GetItems() 
        { 
            return _items; 
        }
    }
}
