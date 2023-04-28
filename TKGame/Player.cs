using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.PowerUps;

namespace TKGame
{
    public class Player : Entity
    {
        private static Player instance;
        private static object syncRoot = new object();
        #region Components
        IInputComponent input;
        IPhysicsComponent physics;
        IGraphicsComponent graphics;

        public Dictionary<AttackType, IAttackComponent> attacks;

        #endregion Components

        public bool isJumping = false;
        public bool isLookingLeft = false;

        public static Player Instance
        {
            get
            {
                // Creates the player if it doesn't already exist
                // Uses thread locking to guarantee safety.
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Player(new C_Player_Input(),
                                                  new C_Player_Physics(),
                                                  new C_Player_Graphics(),
                                                  new Dictionary<AttackType, IAttackComponent>());
                    }

                return instance;
            }
        }

        /// <summary>
        /// Player components.
        /// </summary>
        private Player(IInputComponent input_, IPhysicsComponent physics_, IGraphicsComponent graphics_,
                       Dictionary<AttackType, IAttackComponent> atks)
        {
            input = input_;
            physics = physics_;
            graphics = graphics_;
            attacks = new Dictionary<AttackType, IAttackComponent>();
            // using a loop for safety in case the order of the list changes
            for (int i = 0; i < atks.Count; i++)
            {
                attacks.Add(atks[(AttackType)i].AttackType, atks[(AttackType)i]);
            }

            weapon = new Sword();
            weapon.Activate();

            entityTexture = Art.PlayerTexture;
            MOVEMENT_SPEED = 500f;
            // Figure out how to not hard code for now
            // Starts at (1560, 450) at the middle on the floor level
            entityName = "player"; // name for player class
            Position = new Vector2(TKGame.ScreenWidth/2, TKGame.ScreenHeight - 111);
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }


        public void PickUpItem(IAttackComponent item)
        {
            if (item.AttackType == AttackType.Primary)
            {
                attacks[AttackType.Primary] = (IPrimaryAttackComponent)item;
            }
            else if (item.AttackType == AttackType.Special)
            {
                attacks[AttackType.Special] = (ISpecialAttackComponent)item;
            }
            else if (item.AttackType == AttackType.Ultimate)
            {
                attacks[AttackType.Ultimate] = (IUltimateAttackComponent)item;
            }
        }
        

        /// <summary>
        /// Updates each component the Player owns.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            input.Update(this);
            physics.Update(this, gameTime/*, world*/);
            graphics.Update(this);
            weapon.Update(this);

            foreach (IAttackComponent atk in attacks.Values)
            {
                atk.Update(this);
            }

            graphics.Update(this);
        }

        /// <summary>
        /// Draws each Player Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public Rectangle GetHitBox()
        {
            return HitBox;
        }
    }
}
