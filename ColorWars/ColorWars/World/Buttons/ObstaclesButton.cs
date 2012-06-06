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
            Obstacle obstacle = new Obstacle(mouse.X, mouse.Y);
            obstacle.LoadContent(content);
            EditorMode.obstacles.Add(obstacle);
        }
    }
}
