using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ColorWars
{
    class ArcButton : Button
    {
        private List<Vector2> points;

        public ArcButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
            points = new List<Vector2>();
        }

        public override void Action(MouseState mouse)
        {
            if (points.Count == 2)
            {
                int i = 0, j = 0;

                foreach (Node node in GameMode.movement.nodes)
                    if (node.point == points[0])
                    {
                        i = node.id;
                        break;
                    }

                foreach (Node node in GameMode.movement.nodes)
                    if (node.point == points[1])
                    {
                        j = node.id;
                        break;
                    }

                GameMode.movement.arcs[i, j] = GameMode.movement.arcs[j, i] = Vector2.Distance(points[0], points[1]);
                points.Clear();
            }

            Vector2 point = new Vector2(mouse.X, mouse.Y);
            bool isOnList = false;

            // Check if point is already on nodes list
            foreach (Point node in EditorMode.nodes)
                if (!isOnList & node.Contains(point))
                {
                    point = node.point;
                    isOnList = true;
                }

            // If it is not, return
            if (!isOnList)
                return;

            // Add it to the new rectangle
            points.Add(point);
        }
    }
}