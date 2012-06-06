using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class Background
    {
        #region Constants

        /// <summary>
        /// Screen width
        /// </summary>
        protected const int WIDTH = 1024;

        /// <summary>
        /// Screen heigth
        /// </summary>
        protected const int HEIGTH = 768;

        #endregion

        #region Variables

        /// <summary>
        /// Upper bound of the game
        /// </summary>
        public BoundingBox upperBound;

        /// <summary>
        /// Lower bound of the game
        /// </summary>
        public BoundingBox lowerBound;

        /// <summary>
        /// Left bound of the game
        /// </summary>
        public BoundingBox leftBound;

        /// <summary>
        /// Right bound of the game
        /// </summary>
        public BoundingBox rightBound;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Background()
        {
            // Initialize the bounding boxes
            Vector3 boxMin = new Vector3(0, 0, -1);
            Vector3 boxMax = boxMin + new Vector3(WIDTH, 95, 2);

            upperBound = new BoundingBox(boxMin, boxMax);

            boxMin = new Vector3(0, HEIGTH, -1);
            boxMax = boxMin + new Vector3(WIDTH, 100, 2);

            lowerBound = new BoundingBox(boxMin, boxMax);

            boxMin = new Vector3(-100, 0, -1);
            boxMax = boxMin + new Vector3(100, HEIGTH, 2);

            leftBound = new BoundingBox(boxMin, boxMax);

            boxMin = new Vector3(WIDTH, 0, -1);
            boxMax = boxMin + new Vector3(100, HEIGTH, 2);

            rightBound = new BoundingBox(boxMin, boxMax);

            // Add them to the collision detector as obstacles
            CollisionDetector.obstacles.Add(upperBound);
            CollisionDetector.obstacles.Add(lowerBound);
            CollisionDetector.obstacles.Add(leftBound);
            CollisionDetector.obstacles.Add(rightBound);
        }

        #endregion
    }
}