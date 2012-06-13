using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class EnemiesButton : Button
    {
        public EnemiesButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            Enemy squorre = new Enemy(mouse.X, mouse.Y);
            squorre.LoadContent(content);
            GameMode.squorres.Add(squorre);
        }
    }
}
