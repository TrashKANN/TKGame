﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TKGame.Level_Editor_Content;
using System.Collections.Generic;
using System.Collections;

namespace TKGame
{
    public abstract class Entity : ICollidable
    {
        protected Texture2D entityTexture;

        // Move to a Transform class later instead of having it only in the Entity class
        public Vector2 Position, Velocity;
        public Rectangle hitBox;
        public Rectangle HitBox { get { return hitBox; } set { hitBox = value; } }
        public SpriteEffects Orientation; // Flip Horizontal/Vertical
        // used for Drawing Sprites
        public Color color = Color.White;
        public bool IsExpired;

        public Vector2 Size
        {
            get 
            {
                return entityTexture == null ? Vector2.Zero : new Vector2(entityTexture.Width, entityTexture.Height);
            }
        }


        public abstract void Update(GameTime gameTime);

        #region Intersection Hitboxes for Debugging
        public class Intersection
        {
            public Rectangle hitbox;
            public Color color;

            public Intersection(Rectangle hitbox, Color color)
            {
                this.hitbox = hitbox;
                this.color = color;
            }
        }

        public List<Intersection> collisions = new List<Intersection>();
        public static void DrawCollisionIntersections(SpriteBatch spriteBatch, List<Intersection> intersections)
        {
            foreach (Intersection inter in intersections)
            {
                GameDebug.DrawBoundingBox(spriteBatch, inter.hitbox, inter.color, 5);
            }
        }
        #endregion Intersection Hitboxes

        /// <summary>
        /// Uses the Entity's hitbox and iterates through each hitbox passed to it and adjusts the Entity's position
        /// outside of the hitbox it collides with.
        /// </summary>
        /// <param name="stage"></param>
        public void Collide<T>(List<T> collidables) where T : ICollidable
        {

            //TODO: Collide with other entities.


            foreach (var hitbox in collidables)
            {
                if (HitBox.Intersects(hitbox.HitBox))
                {
                    // Calculate the depth of the intersection between Player and each Wall
                    Rectangle intersection = Rectangle.Intersect(HitBox, hitbox.HitBox);
                    Vector2 depth = new Vector2(intersection.Width, intersection.Height);

                    //collisions.Add(new Intersection(intersection, Color.Red));
                    //collisions.Add(new Intersection(HitBox, Color.Orange));
                    //collisions.Add(new Intersection(wall.Rect, Color.Green));

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
                        }
                        else
                        {
                            // Player is below the wall
                            Position.Y += (int)depth.Y;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Collide with a single hitbox. Used for special triggers such as doors, items, etc.
        /// </summary>
        /// <param name="hitbox"></param>
        public bool Collide<T>(T hitbox) where T : ICollidable
        {
            if (HitBox.Intersects(hitbox.HitBox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Draws the entity sprites with the default values. More parameters can be added to Draw() later to augment these later.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityTexture, Position, null, color, 0, Size / 2f, 1f, Orientation, 0);
        }
    }
}
