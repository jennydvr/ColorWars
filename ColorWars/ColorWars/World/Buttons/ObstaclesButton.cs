using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class ObstaclesButton : Button
    {
        public ObstaclesButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            Vector2 point = new Vector2(mouse.X, mouse.Y);

            foreach (Obstacle obs in GameMode.obstacles)
            {
                Vector3[] corners = obs.box.GetCorners();

                for (int i = 0; i != 4; ++i)
                {
                    Rectangle rect = new Rectangle((int)corners[i].X - 5, (int)corners[i].Y - 5, 10, 10);

                    if (rect.Contains((int)point.X, (int)point.Y))
                    {
                        point = new Vector2(corners[i].X, corners[i].Y);
                        break;
                    }
                }
            }

            Obstacle obstacle = new Obstacle(point.X, point.Y);
            obstacle.LoadContent(content);
            GameMode.obstacles.Add(obstacle);
        }
    }
}
