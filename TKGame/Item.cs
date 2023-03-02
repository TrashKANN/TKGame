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

namespace TKGame
{
    class Item : Entity
    {
        static Item instance;
        private static object syncRoot = new object();

        /// <summary>
        /// lock Item instance
        /// </summary>
        public static Item Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Item();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Item constructor
        /// </summary>
        private Item()
        {
            entityTexture = Art.ItemTexture;
            Position = new Vector2(1200, 840);
            entityName = "item"; 
        }

        /// <summary>
        /// Update method for Item
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            PickUpItem();
        }

        /// <summary>
        /// Method to simulate picking up item
        /// Checks if player is within range of item,
        /// if so, then transports item to corner
        /// to simulate inventory
        /// </summary>
        public void PickUpItem()
        {
            Player player = Player.Instance;

            if (player != null)
            {
                // if player is within range of item spawn position
                if (player.Position.X >= Position.X - 20 &&
                    player.Position.X <= Position.X + 20 &&
                    player.Position.Y >= Position.Y - 80 &&
                    player.Position.Y <= Position.Y + 80)    // TODO: CHANGE FROM MAGIC NUMBERS TO COLLISION DETECTION
                {
                    Position = new Vector2(1500, 100);       // temporarily move item to corner to imitate inventory
                }
            }
        }

        /// <summary>
        /// Draw method for Item
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
