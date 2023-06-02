using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.Players.Components;
using TKGame.World;
using TKGame.Status_Effects;

namespace TKGame.Players
{
    public class Player : Entity
    {
        private static Player instance;
        private static object syncRoot = new object();
        #region Components

        #endregion Components
//public IGraphicsComponent weaponAttack;
        public bool isJumping = false;
        public bool isLookingLeft = false;
        public bool isCrouched = false;

        public int FramesSinceJump { get; set; }

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
                                                  new C_Player_Graphics());
                    }

                return instance;
            }
        }

        /// <summary>
        /// Player components.
        /// </summary>
        private Player(IInputComponent input_,
                        IPhysicsComponent physics_,
                        IGraphicsComponent graphics_)
        {
            components = new Dictionary<ComponentType, List<IComponent>>
            {
                { ComponentType.Input,          new List<IComponent> { input_ } },
                { ComponentType.Physics,        new List<IComponent> { physics_ } },
                { ComponentType.Graphics,       new List<IComponent> { graphics_ } },
                { ComponentType.AttackPrimary,  new List<IComponent>() },
                { ComponentType.AttackSpecial,  new List<IComponent>() },
                { ComponentType.AttackUltimate, new List<IComponent>() },
                { ComponentType.AttackMovement, new List<IComponent>() },
            };

            health = 100; //Sets player health
            originalHealth = health;
            needsHealth = true;
            weapon = new Sword();
           // weaponAttack = new C_Player_WeaponAttack(); //Instantiates WeaponAttack Component
            weapon.Activate();

            entityTexture = Art.PlayerTexture;
            MOVEMENT_SPEED = 500f;
            IsOnGround = false;
            CollidedVertically = false;
            CollidedHorizontally = false;
            FramesSinceJump = 0;
            // Figure out how to not hard code for now
            // Starts at (1560, 450) at the middle on the floor level
            entityName = "player"; // name for player class
            Position = new Vector2(TKGame.ScreenWidth / 2, TKGame.ScreenHeight - 111);
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }

        public List<IAttackComponent> GetAttackComponents()
        {
            return components.Values.SelectMany(x => x).OfType<IAttackComponent>().ToList();
        }

        public void PickUpPowerUp(IAttackComponent powerup)
        {
            var temp = GetAttackComponents();
            if (temp.Any(existingPowerup => existingPowerup.GetType() == powerup.GetType()))
                return;
            switch (powerup.AttackType)
            {
                case AttackType.Primary:
                    components[ComponentType.AttackPrimary].Add(powerup);
                    break;
                case AttackType.Special:
                    components[ComponentType.AttackSpecial].Add(powerup);
                    break;
                case AttackType.Ultimate:
                    components[ComponentType.AttackUltimate].Add(powerup);
                    break;
                case AttackType.Movement:
                    components[ComponentType.AttackMovement].Add(powerup);
                    break;
            }
        }


        /// <summary>
        /// Updates each component the Player owns.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // This is a bit silly looking but it isn't completely unorthidox
            // It assumes the type of IComponent which DOES NOT have an Update method, but the IInputComponent, etc... does.
            components[ComponentType.Input].OfType<IInputComponent>().First().Update(this);
            components[ComponentType.Physics].OfType<IPhysicsComponent>().First().Update(this, gameTime);
            //weaponAttack.Update(this);
            weapon.Update(this);


            var attacks = GetAttackComponents();
            foreach (var attack in attacks)
            {
                if (attack != null)
                    attack.Update(this);
            }

            components[ComponentType.Graphics].OfType<IGraphicsComponent>().First().Update(this);
        }

        /// <summary>
        /// Draws each Player Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            weapon.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
