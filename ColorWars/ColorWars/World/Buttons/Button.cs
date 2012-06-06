using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    abstract class Button
    {
        public Vector2 position;
        public Texture2D texture;
        protected ContentManager content;
        protected Rectangle bound;

        public Button(ContentManager content, Vector2 position)
        {
            this.position = position;
            this.texture = content.Load<Texture2D>("ball");
            this.content = content;
            this.bound = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }

        public bool IsClicked(MouseState mouse)
        {
            return bound.Contains(mouse.X, mouse.Y);
        }

        abstract public void Action(MouseState mouse);
    }
}
