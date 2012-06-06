using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class TriangleButton : Button
    {
        private List<Vector2> points;

        public TriangleButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
            points = new List<Vector2>();
        }

        public override void Action(MouseState mouse)
        {
            Vector2 point = new Vector2(mouse.X, mouse.Y);
            bool isOnList = false;

            // Check if point is already on nodes list
            foreach (Node node in EditorMode.nodes)
            {
                node.color = Color.Red;

                if (!isOnList & node.Contains(point))
                {
                    point = node.point;
                    isOnList = true;
                    node.color = Color.Yellow;
                }
            }

            // If it is not, add it
            if (!isOnList)
                EditorMode.nodes.Add(new Node(point));

            // Add it to the new rectangle
            points.Add(new Vector2(mouse.X, mouse.Y));

            // If there are enough points to make a triangle, add it to the list
            if (points.Count == 3)
            {
                EditorMode.triangles.Add(new Triangle(points[0], points[1], points[2]));
                points.Clear();
            }
        }
    }
}