using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ColorWars
{
    class Sensor : Condition
    {
        #region Variables

        /// <summary>
        /// Says whether a sensor is activated or not
        /// </summary>
        protected bool activated = false;

        /// <summary>
        /// Graph of the sensor
        /// </summary>
        protected Graph graph;

        /// <summary>
        /// Origin node
        /// </summary>
        public Node origin;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character owner of this sensor</param>
        public Sensor(Character character) :
            base(character)
        {
        }

        #endregion

        #region Sensor Stuff

        /// <summary>
        /// Checks if a sensor is activated
        /// </summary>
        public virtual void Detect()
        {
        }

        #endregion

        #region Condition Stuff

        public override bool Test()
        {
            return activated;
        }

        #endregion

        #region Auxiliars

        /// <summary>
        /// Calculates the node of this character
        /// </summary>
        /// <param name="character">Kinematic of the character</param>
        /// <returns>The node of the character</returns>
        protected Node GetNearestNode(Kinematic character)
        {
            Vector2 pos = GetPolygon(character).center;

            foreach (Node node in graph.nodes)
                if (pos == node.point)
                    return node;

            return new Node(Vector2.Zero, -1);
        }

        /// <summary>
        /// Calculates the character's polygon
        /// </summary>
        /// <param name="character">Kinematic of the character</param>
        /// <returns>The polygon of the character</returns>
        protected Polygon GetPolygon(Kinematic character)
        {
            float min = float.PositiveInfinity;
            Polygon polygon = new Polygon(new List<Vector2>());
            Vector2 vect = new Vector2(character.position.X, character.position.Y);

            foreach (Polygon poly in GameMode.polygons)
            {
                float diff = Vector2.Distance(poly.center, vect);

                if (poly.Contains(vect) && diff < min)
                {
                    min = diff;
                    polygon = poly;
                }
            }

            return polygon;
        }

        #endregion
    }
}