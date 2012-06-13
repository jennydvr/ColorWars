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
        List<Node> path;
        
        #endregion

        #region Methods

        
        public override SteeringOutput GetSteering()
        {
            // If the characters are in the same polygon, do nothing
            if (GetPolygon(character).center == GetPolygon(target).center)
                return new SteeringOutput();

            // Generate the path
            if (path.Count == 0)
                path = AStar.Pathfind(GetNearestNode(character), GetNearestNode(target));

            return base.GetSteering(character, target);
        }

        public override SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            // If the characters are in the same polygon, do nothing
            if (GetPolygon(character).center == GetPolygon(target).center)
                return new SteeringOutput();

            // Generate the path
            path = AStar.Pathfind(GetNearestNode(character), GetNearestNode(target));

            return base.GetSteering(character, target);
        }

        protected override SteeringOutput SteeringGenerator()
        {
            // Find the next position in the path
            Node next = GetNextNode();

            // If the final node was reached, return nothing
            if (next.id == -1)
                return new SteeringOutput();

            // Else, set the new target
            base.target.position = new Vector3(next.point.X, next.point.Y, 0);

            return base.SteeringGenerator();
        }

        #endregion

        #region Pathfinding Functions

        protected Node GetNearestNode(Kinematic character)
        {
            Polygon polygon = GetPolygon(character);
            Node ans = new Node(Vector2.Zero, -1);

            foreach (Node node in GameMode.graph.nodes)
                if (polygon.center == node.point)
                    ans = node;

            return ans;
        }

        protected Node GetNextNode()
        {
            Polygon polygon = GetPolygon(this.character);
            Node ans = new Node(Vector2.Zero, -1);

            if (polygon.center == path[path.Count - 1].point)
                return ans;

            for (int i = 0; i != path.Count - 1; ++i)
                if (polygon.center == path[i].point)
                    ans = path[i + 1];

            return ans;
        }

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