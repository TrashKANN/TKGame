using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.PowerUps;
using TKGame.Status_Effects;

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
                                                  new Dictionary<AttackType, IAttackComponent>()
                                                  {
                                                      { AttackType.Primary, null },
                                                      { AttackType.Special, null },
                                                      { AttackType.Ultimate, null },
                                                      { AttackType.Movement, null},
                                                  });
                    }

                return instance;
            }
        }

        /// <summary>
        /// Player components.
        /// </summary>
        private Player( IInputComponent input_, 
                        IPhysicsComponent physics_, 
                        IGraphicsComponent graphics_,
                        Dictionary<AttackType, IAttackComponent> atks)
        {
            components = new Dictionary<ComponentType, IComponent>
            {
                { ComponentType.Input, input_ },
                { ComponentType.Physics, physics_ },
                { ComponentType.Graphics, graphics_ },
                { ComponentType.AttackPrimary, atks[AttackType.Primary] },
                { ComponentType.AttackSpecial, atks[AttackType.Special] },
                { ComponentType.AttackUltimate, atks[AttackType.Ultimate] },
                { ComponentType.AttackMovement, atks[AttackType.Movement] },
            };
            
            input = input_;
            physics = physics_;
            graphics = graphics_;
            attacks = atks;
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

        public List<IAttackComponent> GetAttackComponents()
        {
            return new List<IAttackComponent>()
            {
                (IAttackComponent)components[ComponentType.AttackPrimary],
                (IAttackComponent)components[ComponentType.AttackSpecial],
                (IAttackComponent)components[ComponentType.AttackUltimate],
                (IAttackComponent)components[ComponentType.AttackMovement]
            };
        }

        public void PickUpItem(IAttackComponent item)
        {
            switch (item.AttackType)
            {
                case AttackType.Primary:
                    components[ComponentType.AttackPrimary] = item; //(IPrimaryAttackComponent)item;
                    attacks[AttackType.Primary] = item;
                    break;
                case AttackType.Special:
                    components[ComponentType.AttackSpecial] = item; //(ISpecialAttackComponent)item;
                    attacks[AttackType.Special] = item;
                    break;
                case AttackType.Ultimate:
                    components[ComponentType.AttackUltimate] = item; //(IUltimateAttackComponent)item;
                    attacks[AttackType.Ultimate] = item;
                    break;
                case AttackType.Movement:
                    components[ComponentType.AttackMovement] = item; //(IMovementAttackComponent)item;
                    attacks[AttackType.Movement] = item;
                    break;
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
            weapon.Update(this);

            foreach (IAttackComponent atk in attacks.Values)
            {
                if (atk != null)
                {
                    atk.Update(this);
                }
            }
            //components[ComponentType.AttackSpecial]

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
    }
}
