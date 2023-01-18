using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TKGame
{
    class Player : Entity
    {
        private static Player instance;
        public static Player Instance
        {
            get
            {
                // Creates the player if it doesn't already exist
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }

        private Player()
        {
            image = Art.Player;
        }

        public override void Update()
        {
            // Player Movement
            const float movementSpeed = 10;

        }
    }
}
