using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ColorWars
{
    class NodesButton : Button
    {
        public NodesButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            Vector2 point = new Vector2(mouse.X, mouse.Y);
            bool isOnList = false;

            // Check if point is already on nodes list
            foreach (Point node in EditorMode.nodes)
            {
                if (!isOnList & node.Contains(point))
                {
                    point = node.point;
                    isOnList = true;
                }
            }

            // If it is not, add it
            if (!isOnList)
                EditorMode.nodes.Add(new Point(point));
        }
    }
}
