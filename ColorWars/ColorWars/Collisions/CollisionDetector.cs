using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gearset;

namespace ColorWars
{
    /// <summary>
    /// Calculates collisions in the game
    /// </summary>
    static class CollisionDetector
    {
        #region Variables

        /// <summary>
        /// Saves the obstacles of the game
        /// </summary>
        static public List<BoundingBox> obstacles = new List<BoundingBox>();

        /// <summary>
        /// Saves dotty and the squorres
        /// </summary>
        static public List<BoundingSphere> players = new List<BoundingSphere>();

        /// <summary>
        /// Saves the paintballs in the game
        /// </summary>
        static public List<Paintball> balls = new List<Paintball>();

        /// <summary>
        /// Saves the thinnerballs in the game
        /// </summary>
        static public List<Thinner> thinners = new List<Thinner>();

        #endregion

        #region Methods

        /// <summary>
        /// Check if a character is collisioning with something
        /// </summary>
        /// <param name="sphere">Character's sphere</param>
        /// <returns>True if collisioning, false otherwise</returns>
        static public bool CheckCollisions(BoundingSphere sphere)
        {
            // Check with obstacles
            foreach (BoundingBox box in obstacles)
            {
                if (box.Intersects(sphere))
                {
                    return true;
                }
            }

            // Check with other players
            foreach (BoundingSphere player in players)
            {
                if (player.Intersects(sphere))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks for collisions with obstacles
        /// </summary>
        /// <param name="sphere">Bounding sphere</param>
        /// <returns>True if there is a collision, false otherwise</returns>
        static public bool CheckObstaclesCollisions(BoundingSphere sphere)
        {
            // Check with obstacles
            foreach (BoundingBox box in obstacles)
                if (box.Intersects(sphere))
                    return true;

            return false;
        }

        /// <summary>
        /// Check for collisions with paintballs
        /// The collision will only apply if the color of the paintball is different
        /// from the color - parameter
        /// </summary>
        /// <param name="sphere">Character's bounds</param>
        /// <param name="color">Character's color</param>
        /// <returns></returns>
        static public bool CheckPaintballsCollisions(BoundingSphere sphere, Color color)
        {
            foreach (Paintball ball in balls)
                if (ball.color == color & ball.bound.Intersects(sphere))
                    return true;

            return false;
        }

        /// <summary>
        /// Check for collisions with thinnerballs
        /// </summary>
        /// <param name="sphere">Character's bounds</param>
        /// <returns></returns>
        static public bool CheckThinnerballsCollisions(BoundingSphere sphere)
        {
            foreach (Thinner ball in thinners)
                if (ball.bound.Intersects(sphere))
                {
                    ball.activated = false;
                    return true;
                }

            return false;
        }

        /// <summary>
        /// Calculates a collision in the specified range, between position and moveAmount
        /// </summary>
        /// <param name="position">Initial position of the ray</param>
        /// <param name="moveAmount">Range to check</param>
        /// <returns>A collision if found</returns>
        static public Collision GetCollision(Vector3 position, Vector3 moveAmount)
        {
            // Stuff to build the ray
            Vector3 unit = new Vector3(moveAmount.X, moveAmount.Y, moveAmount.Z);
            unit.Normalize();

            Ray ray = new Ray(position, unit);
            BoundingBox minBox = new BoundingBox();
            float min = float.PositiveInfinity;

            // Find a collision with obstacles only
            foreach (BoundingBox box in obstacles)
            {
                Vector3[] corners = box.GetCorners();

                float? res = box.Intersects(ray);

                if (res.HasValue && res < min)
                {
                    min = (float)res;
                    minBox = box;
                }
            }

            Collision collision = new Collision();

            // Check if there is a collision
            if (!float.IsPositiveInfinity(min))
            {
                // Find the collision spot and compare it to the move amount
                Vector3 spot = ray.Position + ray.Direction * min;
                spot.Z = 0;

                // If the spot is within the move amount range, save it in the collision
                if ((spot - position).Length() <= moveAmount.Length())
                {
                    collision.position = spot;
                    collision.normal = GetNormal(spot, minBox);
                }
            }

            return collision;
        }

        /// <summary>
        /// Calculates a collision in the specified range, between position and moveAmount
        /// </summary>
        /// <param name="position">Initial position of the ray</param>
        /// <param name="moveAmount">Range to check</param>
        /// <returns>A collision if found</returns>
        static public Collision GetCollision(Vector3 position, Vector3 moveAmount, Character owner)
        {
            // Stuff to build the ray
            Vector3 unit = new Vector3(moveAmount.X, moveAmount.Y, moveAmount.Z);
            unit.Normalize();

            Ray ray = new Ray(position, unit);
            Vector3 minNormal = Vector3.Zero;
            float min = float.PositiveInfinity;

            // Find a collision with enemies only
            foreach (Enemy squorre in GameMode.squorres)
            {
                if (squorre == owner)
                    continue;

                float? res = squorre.bound.Intersects(ray);

                if (res.HasValue && res < min)
                {
                    min = (float)res;
                    minNormal = new Vector3(moveAmount.X, moveAmount.Y, moveAmount.Z) * -1;
                    minNormal.Normalize();
                }
            }

            Collision collision = new Collision();

            // Check if there is a collision
            if (!float.IsPositiveInfinity(min))
            {
                // Find the collision spot and compare it to the move amount
                Vector3 spot = ray.Position + ray.Direction * min;
                spot.Z = 0;

                // If the spot is within the move amount range, save it in the collision
                if ((spot - position).Length() <= moveAmount.Length())
                {
                    collision.position = spot;
                    collision.normal = minNormal;
                }
            }

            return collision;
        }

        /// <summary>
        /// Calculates the normal of a box, in the given spot
        /// </summary>
        /// <param name="point">Point to check out</param>
        /// <param name="box">Box to calculate normal</param>
        /// <returns>Normal vector in the given point</returns>
        static private Vector3 GetNormal(Vector3 point, BoundingBox box)
        {
            Vector3[] corners = box.GetCorners();

            float bottom = corners[0].Y;
            float top = corners[3].Y;
            float left = corners[0].X;
            float right = corners[1].X;

            if (top == point.Y)
            {
                return Vector3.Down;
            }
            else if (bottom == point.Y)
            {
                return Vector3.Up;
            }

            if (right == point.X)
            {
                return Vector3.Right;
            }
            else if (left == point.X)
            {
                return Vector3.Left;
            }

            return Vector3.Zero;
        }

        #endregion
    }
}