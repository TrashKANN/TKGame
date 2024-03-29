﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TKGame.Level_Editor_Content;
using System.Collections.Generic;
using System.Collections;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.PowerUps;
using TKGame.Status_Effects;
using TKGame.BackEnd;
using System.Linq;
using System;
using TKGame.Players;


namespace TKGame
{
    public enum EntityType
    { 
        Player,
        Enemy,
        Projectile,
        Item,
        PowerUp,
    }
    public abstract class Entity : ICollideComponent
    {
        // dict[ComponentType] = List<IComponent> for multiple components of the same type (Stacking Burning effects, etc.)
        public Dictionary<ComponentType, List<IComponent>> components;

        internal Texture2D entityTexture;

        // Move to a Transform class later instead of having it only in the Entity class
        public Vector2 Position, Velocity;
        public Rectangle hitBox;
        public SpriteEffects Orientation; // Flip Horizontal/Vertical
        // used for Drawing Sprites
        public Color color = Color.White;
        public bool IsExpired;
        public string entityName; // to identify each entity by name
        public EntityType entityType;

        public bool IsOnGround { get; set; }
        public bool CollidedVertically { get; set; }
        public bool CollidedHorizontally { get; set; }

        public Weapon weapon;
        public float health;
        public Rectangle healthBar;
        public Texture2D healthTexture;
        public bool needsHealth = false;
        public float originalHealth;
        public bool isEnemy = false;

        #region Properties
        public Vector2 Size
        {
            get 
            {
                return entityTexture == null ? Vector2.Zero : new Vector2(entityTexture.Width, entityTexture.Height);
            }
        }
        public Rectangle HitBox { get { return hitBox; } set { hitBox = value; } }
        public float MOVEMENT_SPEED { get; internal set; }

        public ComponentType Type => throw new NotImplementedException();
        #endregion Properties


        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Adds the component to the Entity's list of components. If there exists an instance of the added component's type,
        /// then add it to the list of components of that type. Otherwise, create a new list of components of that type and add it.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(IComponent component)
        {
            if (components.ContainsKey(component.Type))
            {
                components[component.Type].Add(component);
            }
            else
            {
                components.Add(component.Type, new List<IComponent> { component });
            }
        }

        public bool RemoveComponent(IComponent component)
        {
            if (components.ContainsKey(component.Type))
            {
                return components[component.Type].Remove(component);
            }
            return false;
        }

        public List<IStatusComponent> GetStatusEffects()
        {
            return components.Values.SelectMany(x => x).OfType<IStatusComponent>().ToList();
        }

        public bool isDead()
        {
            if (this.health <= 0)
                this.IsExpired = true;
            return this.IsExpired;
        }

        /// <summary>
        /// Uses the Entity's hitbox and iterates through each hitbox passed to it and adjusts the Entity's position
        /// outside of the hitbox it collides with.
        /// </summary>
        /// <param name="stage"></param>
        public void Collide<T>(List<T> collidables) where T : ICollideComponent
        {

            //TODO: Collide with other entities.

            foreach (var hitbox in collidables)
            {
                // TODO: Make this more efficient
                if (HitBox.Intersects(hitbox.HitBox) && hitbox is Spikes)
                {
                    this.health -= 0.1f;
                    this.healthBar.Width = (int)(this.health / this.originalHealth * 100);
                }

                if (HitBox.Intersects(hitbox.HitBox) && this.entityType != EntityType.PowerUp && hitbox is not Spikes)
                {
                    // Calculate the depth of the intersection between Player and each Wall
                    Rectangle intersection = Rectangle.Intersect(HitBox, hitbox.HitBox);
                    Vector2 depth = new Vector2(intersection.Width, intersection.Height);

                    // Determine the direction of intersection
                    if (depth.X < depth.Y)
                    {
                        // Horizontal collision
                        if (HitBox.Center.X < hitbox.HitBox.Center.X)
                        {
                            // Player is to the left of the wall
                            Position.X -= (int)depth.X;
                        }
                        else
                        {
                            // Player is to the right of the wall
                            Position.X += (int)depth.X;
                        }
                    }
                    else
                    {
                        // Vertical collision
                        if (HitBox.Center.Y < hitbox.HitBox.Center.Y)
                        {
                            // Player is above the wall
                            Position.Y -= (int)depth.Y;
                            if(this is Player)
                            {
                                Player.Instance.CollidedVertically = true;
                                Player.Instance.IsOnGround = true;
                            }
                        }
                        else
                        {
                            // Player is below the wall
                            Position.Y += (int)depth.Y;
                            if (this is Player) Player.Instance.CollidedVertically = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Collide with a single hitbox. Used for special triggers such as doors, items, etc.
        /// </summary>
        /// <param name="hitbox"></param>
        public bool Collide<T>(T hitbox) where T : ICollideComponent
        {
            return HitBox.Intersects(hitbox.HitBox) ? true : false;
        }

        /// <summary>
        /// Draws the entity sprites with the default values. More parameters can be added to Draw() later to augment these later.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UpdateTexture(), Position, null, color, 0, Size / 2f, 1f, Orientation, 0);
        }

        // Texture Cache to store the final texture of the entity with all the status effects applied to it.
        // Used to reduce the number of times the entity's texture is updated instead of exchanged.
        private Dictionary<HashSet<Texture2D>, Texture2D> textureCache = new Dictionary<HashSet<Texture2D>, Texture2D>();

        /// <summary>
        /// Updates the texture of the entity.
        /// Stores unique textures in a HashSet and checks if the HashSet already exists in the textureCache.
        /// If it does not, Combine the textures of the entity with each new unique status effect texture and store it in the textureCache.
        /// </summary>
        /// <returns></returns>
        private Texture2D UpdateTexture()
        {
            Texture2D finalTexture = entityTexture;

            if (this.entityType == EntityType.Item ||
                this.entityType == EntityType.PowerUp ||
                this.entityType == EntityType.Projectile)
            {
                return finalTexture;
            }

            var statusEffectComponents = this.components.Values.SelectMany(x => x).OfType<IStatusComponent>();
            HashSet<Texture2D> uniqueTextures = new HashSet<Texture2D>();

            foreach (var statusEffect in statusEffectComponents)
            {
                if (statusEffect != null)
                {
                    uniqueTextures.Add(statusEffect.GetEffectTexture());
                }
            }

            if (textureCache.ContainsKey(uniqueTextures))
            {
                finalTexture = textureCache[uniqueTextures];
            }
            else
            {
                foreach (var texture in uniqueTextures)
                {
                    finalTexture = Art.CombineTextures(finalTexture, texture);
                }
                textureCache[uniqueTextures] = finalTexture;
            }

            return finalTexture;
        }

        public void resolveVerticalCollision(Entity entity, float deltaTime)
        {
            bool wasGrounded = isOnGround(entity); // Store the previous grounded state

            if (entity.CollidedVertically)
            {
                // Ground collision
                if (entity.Velocity.Y > 0)
                {
                    entity.IsOnGround = true;
                }
                else
                {
                    entity.IsOnGround = false;
                }
                entity.Velocity.Y = 0;
                entity.CollidedVertically = false;
            }

            // Apply gravity only if the entity is not on the ground and was not previously grounded
            if (!isOnGround(entity) && !wasGrounded)
            {
                entity.Velocity.Y += TKGame.GRAVITY * deltaTime;
            }
            else
            {
                entity.Velocity.Y = 0;
            }
        }



        private bool isOnGround(Entity entity)
        {
            Rectangle extendedHitBox = new Rectangle(entity.HitBox.Left + 1, entity.HitBox.Bottom, entity.HitBox.Width - 2, 10);

            foreach (IBlock block in TKGame.levelComponent.GetCurrentLevel().currentStage.StageBlocks)
            {
                if (extendedHitBox.Intersects(block.HitBox))
                {
                    // Only consider the player grounded if the collision is from below
                    if (entity.Velocity.Y >= 0)
                    {
                        entity.IsOnGround = true;
                        return true;
                    }
                    else if (entity.Velocity.Y < 0 && entity is Player)
                    {
                        // Check if the top of the wall is within reach to allow climbing
                        if (entity.HitBox.Bottom - block.HitBox.Top <= entity.Velocity.Y)
                        {
                            entity.IsOnGround = true;
                            entity.Velocity.Y = 0;
                        }
                    }
                }
            }

            entity.IsOnGround = false; // Reset grounded state if no collision is found
            return false;
        }
    }
}
