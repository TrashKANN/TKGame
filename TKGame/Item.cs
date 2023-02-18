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
            entityName = "DiamondSword"; // naming
        }

        /// <summary>
        /// Update method for Item
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Player player = EntityManager.GetEntities().FirstOrDefault(x => x.entityName == "Player" && x is Player) as Player;
            // TODO: check if player is null
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
