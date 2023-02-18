using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TKGame.Level_Editor_Content;
using System.Collections.Generic;
using System.Collections;

namespace TKGame
{
    public abstract class Entity
    {
        protected Texture2D entityTexture;

        // Move to a Transform class later instead of having it only in the Entity class
        public Vector2 Position, Velocity;
        public Rectangle HitBox;
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
        public void Collide(Stage stage)
        {
            foreach (var wall in stage.walls)
            {
                bool collision = HitBox.Intersects(wall.Rect);
                if (collision)
                {
                    // Calculate the depth of the intersection between Player and each Wall
                    Rectangle intersection = Rectangle.Intersect(HitBox, wall.Rect);
                    Vector2 depth = new Vector2(intersection.Width, intersection.Height);

                    //collisions.Add(new Intersection(intersection, Color.Red));
                    //collisions.Add(new Intersection(HitBox, Color.Orange));
                    //collisions.Add(new Intersection(wall.Rect, Color.Green));

                    // Determine the direction of intersection
                    if (depth.X < depth.Y)
                    {
                        // Horizontal collision
                        if (HitBox.Center.X < wall.Rect.Center.X)
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
                        if (HitBox.Center.Y < wall.Rect.Center.Y)
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
        /// Draws the entity sprites with the default values. More parameters can be added to Draw() later to augment these later.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityTexture, Position, null, color, 0, Size / 2f, 1f, Orientation, 0);
        }
    }
}
