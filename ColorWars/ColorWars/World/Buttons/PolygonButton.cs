using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class PolygonButton : Button
    {
        private List<Vector2> points;
        static public bool creando = false;

        public PolygonButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
            points = new List<Vector2>();
        }

        public override void Action(MouseState mouse)
        {
            if (!creando)
            {
                if (points.Count == 0)
                    return;

                foreach (Point node in EditorMode.nodes)
                    node.color = Color.Red;

                GameMode.polygons.Add(new Polygon(points));
                points.Clear();
                return;
            }

            Vector2 point = new Vector2(mouse.X, mouse.Y);
            bool isOnList = false;

            // Check if point is already on nodes list
            foreach (Point node in EditorMode.nodes)
            {
                if (!isOnList & node.Contains(point))
                {
                    point = node.point;
                    isOnList = true;
                    node.color = Color.Yellow;
                }
            }

            // If it is not, return
            if (!isOnList)
                return;

            // Add it to the new rectangle
            points.Add(point);
        }
    }
}