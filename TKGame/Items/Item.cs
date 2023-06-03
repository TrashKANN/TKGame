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
    public abstract class Item : Entity
    {
        // uniquely identify each item by a name
        public string Name { get; set; }
        // integer for stats associated with each item
        public int Stats { get; set; }
    }
}