using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ColorWars
{
    class WaypointButton : Button
    {
        public WaypointButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            Vector2 point = new Vector2(mouse.X, mouse.Y);

            // Check if point is already on nodes list
            foreach (Waypoint node in GameMode.waypoints)
                if (node.Contains(point))
                    return;

            // If it is not, add it
            GameMode.waypoints.Add(new Waypoint(point));
        }
    }
}
