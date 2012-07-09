using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class FollowPath : Seek
    {
        #region Variables

        /// <summary>
        /// The path to follow
        /// </summary>
        List<Node> path = new List<Node>();
        
        /// <summary>
        /// Last target polygon
        /// </summary>
        Polygon lastPolygon = new Polygon(new List<Vector2>());

        /// <summary>
        /// Heuristic to use
        /// </summary>
        public char heuristic = 'c';

        #endregion

        #region Methods

        public override SteeringOutput GetSteering()
        {
            // If the characters are in the same polygon, do nothing
            Polygon targetPolygon = GetPolygon(target);
            if (lastPolygon.center != targetPolygon.center)
            {
                // Generate a new path
                AStar star = new AStar(GameMode.movement);

                Node c = GetNearestNode(character);
                Node t = GetNearestNode(target);

                if (heuristic == 's')
                    path = star.Pathfind(c, t, new SafestHeuristic(t));
                else
                    path = star.Pathfind(c, t, new ClosestHeuristic(t));

                lastPolygon = targetPolygon;
            }

            return base.GetSteering();
        }

        public override SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            // If the characters are in the same polygon, do nothing
            Polygon targetPolygon = GetPolygon(target);
            if (lastPolygon.center != targetPolygon.center)
            {
                // Generate a new path
                AStar star = new AStar(GameMode.movement);

                Node c = GetNearestNode(character);
                Node t = GetNearestNode(target);

                if (heuristic == 's')
                    path = star.Pathfind(c, t, new SafestHeuristic(t));
                else
                    path = star.Pathfind(c, t, new ClosestHeuristic(t));

                lastPolygon = targetPolygon;
            }

            return base.GetSteering(character, target);
        }

        protected override SteeringOutput SteeringGenerator()
        {
            // Find the next position in the path
            Node next = GetNextNode();

            // If the final node was reached, return simple seek
            if (next.id == -1)
                base.target.position = new Vector3(target.position.X, target.position.Y, 0);
            // Else, set the new target
            else
                base.target.position = new Vector3(next.point.X, next.point.Y, 0);

    //        if (SteeringBehavior.DEBUG)
                Debug();

            return base.SteeringGenerator();
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

        protected Node GetNextNode()
        {
            Node node = GetNearestNode(this.character);

            if (node.point == path[path.Count - 1].point)
                return new Node(Vector2.Zero, -1);

            for (int i = 0; i != path.Count - 1; ++i)
                if (node.point == path[i].point)
                    return path[i + 1];

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

        #region Debug

        void Debug()
        {
            foreach (Node node in GameMode.movement.nodes)
            {
                if (path.Contains(node))
                    node.color = Color.Blue;
                else
                    node.color = Color.Red;
            }
        }

        #endregion
    }
}