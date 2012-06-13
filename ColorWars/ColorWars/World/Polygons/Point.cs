using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Point
    {
        #region Variables

        /// <summary>
        /// Bound of the node
        /// </summary>
        public Rectangle rectangle;

        /// <summary>
        /// Spot corresponding to the center of the node
        /// </summary>
        public Vector2 point;

        /// <summary>
        /// Color of the node: yellow for hoovered, red for normal
        /// </summary>
        public Color color;

        #endregion

        #region Regular stuff

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point">The center of the node</param>
        public Point(Vector2 point)
        {
            this.point = point;
            this.rectangle = new Rectangle((int)point.X - 5, (int)point.Y - 5, 10, 10);
            this.color = Color.Red;
        }

        /// <summary>
        /// Draws this node
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="batch">Sprite batch</param>
        public void Draw(ContentManager content, SpriteBatch batch)
        {
            Texture2D texture = content.Load<Texture2D>("cuadrito");
            batch.Draw(texture, rectangle, color);
        }

        #endregion

        #region Others

        /// <summary>
        /// Checks if a point is related to this node
        /// </summary>
        /// <param name="spot">Point to check</param>
        /// <returns>True if is within this node, false otherwise</returns>
        public bool Contains(Vector2 spot)
        {
            return rectangle.Contains((int)spot.X, (int)spot.Y);
        }

        #endregion
    }
}
