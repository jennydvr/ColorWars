using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Waypoint
    {
        #region Variables

        /// <summary>
        /// Bound of the node
        /// </summary>
        public Rectangle rectangle;

        /// <summary>
        /// Kinematic of the position of the waypoint
        /// </summary>
        public Kinematic kinematic = new Kinematic();

        #endregion

        #region Regular stuff

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point">The center of the node</param>
        public Waypoint(Vector2 point)
        {
            this.rectangle = new Rectangle((int)point.X - 5, (int)point.Y - 5, 10, 10);
            this.kinematic.position = new Vector3(point.X, point.Y, 0);
        }

        /// <summary>
        /// Draws this node
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="batch">Sprite batch</param>
        public void Draw(ContentManager content, SpriteBatch batch, int id)
        {
            Texture2D texture = content.Load<Texture2D>("cuadrito");
            batch.Draw(texture, rectangle, Color.Green);
            Gearset.GS.ShowMark(id + "", new Vector2(kinematic.position.X, kinematic.position.Y));
        }

        #endregion

        #region Waypoint Value

        /// <summary>
        /// Calculates the value of the waypoint
        /// </summary>
        /// <param name="dotty">Kinematic of dotty</param>
        /// <param name="squorre">Kinematic of squorre</param>
        /// <returns>A float that represents how good is this waypoint</returns>
        public float Value(Kinematic dotty, Kinematic squorre)
        {
            AStar star = new AStar(GameMode.movement);
            Node w = GetNearestNode(kinematic);

            // Calculate the value from dotty to the waypoint
            Node d = GetNearestNode(dotty);
            List<Node> dottyPath = star.Pathfind(d, w, new SafestHeuristic(w));

            float dottyValue = 0;
            for (int i = 0; i != dottyPath.Count - 1; ++i)
                dottyValue += Node.Distance(dottyPath[i], dottyPath[i + 1]);

            // Calculate the value from squorre to the waypoint
            Node s = GetNearestNode(squorre);
            List<Node> squorrePath = star.Pathfind(s, w, new SafestHeuristic(w));

            float squorreValue = 0;
            for (int i = 0; i != squorrePath.Count - 1; ++i)
                squorreValue += Node.Distance(squorrePath[i], squorrePath[i + 1]);

            // The value shall be the difference between the length of dotty's path
            // and the length of squorre's path
            return 2 * dottyValue - squorreValue;
        }

        #endregion

        #region Pathfinding Functions

        protected Node GetNearestNode(Kinematic character)
        {
            Vector2 pos = GetPolygon(character).center;

            foreach (Node node in GameMode.movement.nodes)
                if (pos == node.point)
                    return node;

            return new Node(Vector2.Zero, -1);
        }

        protected Polygon GetPolygon(Kinematic character)
        {
            Vector2 vect = new Vector2(character.position.X, character.position.Y);

            foreach (Polygon poly in GameMode.polygons)
                if (poly.Contains(vect))
                    return poly;

            return new Polygon(new List<Vector2>());
        }

        #endregion

        #region Others

        /// <summary>
        /// Checks if a point is related to this waypoint
        /// </summary>
        /// <param name="spot">Point to check</param>
        /// <returns>True if is within this waypoint, false otherwise</returns>
        public bool Contains(Vector2 spot)
        {
            return rectangle.Contains((int)spot.X, (int)spot.Y);
        }

        #endregion
    }
}