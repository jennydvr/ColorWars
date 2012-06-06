using Gearset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Triangle
    {
        #region Variables

        /// <summary>
        /// Vertexes of the triangle
        /// </summary>
        protected Vector2 point1;
        protected Vector2 point2;
        protected Vector2 point3;

        /// <summary>
        /// Center of the triangle
        /// </summary>
        protected Vector2 center;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point1">First vertex of the triangle</param>
        /// <param name="point2">Second vertex of the triangle</param>
        /// <param name="point3">Third vertex of the triangle</param>
        public Triangle(Vector2 point1, Vector2 point2, Vector2 point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;

            this.center.X = (point1.X + point2.X + point3.X) / 3;
            this.center.Y = (point1.Y + point2.Y + point3.Y) / 3;
        }

        #endregion

        #region Output

        /// <summary>
        /// Draws the rectangle
        /// </summary>
        /// <param name="batch">Sprite batch</param>
        /// <param name="id">Id of the triangle</param>
        /// <param name="content">Content manager</param>
        public void Draw(SpriteBatch batch, int id, ContentManager content)
        {
            // Draws the line
            GS.ShowLine("T" + id + "1", point1, point2, Color.Gray);
            GS.ShowLine("T" + id + "2", point2, point3, Color.Gray);
            GS.ShowLine("T" + id + "3", point1, point3, Color.Gray);

            // Draws the center
            Texture2D cuadro = content.Load<Texture2D>("cuadrito");
            batch.Draw(cuadro, center - new Vector2(cuadro.Width, cuadro.Height) / 2, Color.Blue);
        }

        /// <summary>
        /// Returns the string representation of the triangle
        /// </summary>
        /// <returns>String representing the triangle</returns>
        public void WriteInFile(System.IO.StreamWriter file)
        {
            file.WriteLine("<triangle>");
            file.WriteLine("    <point> " + point1.X + " " + point1.Y + " </point>");
            file.WriteLine("    <point> " + point2.X + " " + point2.Y + " </point>");
            file.WriteLine("    <point> " + point3.X + " " + point3.Y + " </point>");
            file.WriteLine("</triangle>");
        }

        #endregion
    }
}