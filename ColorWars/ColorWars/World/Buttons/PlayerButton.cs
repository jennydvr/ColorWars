using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class PlayerButton : Button
    {
        public PlayerButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            EditorMode.dotty.kinematic.position = new Vector3(mouse.X, mouse.Y, 0);
        }
    }
}
