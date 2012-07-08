using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Obstacle
    {
        #region Constants

        protected const string ASSETNAME = "obstacle";
        protected const int WIDTH = 50;
        protected const int HEIGTH = 50;

        #endregion

        #region Variables

        protected Texture2D texture;
        public BoundingBox box;
        public Vector3 position = Vector3.Zero;

        #endregion

        #region Constructors
        
        public Obstacle(float x, float y)
        {
            // Initialize position
            position.X = x;
            position.Y = y;

            // Create the collision box and add it to the collision detector
            Vector3 boxMin = new Vector3((int)position.X, (int)position.Y, -10);
            Vector3 boxMax = boxMin + new Vector3(WIDTH, HEIGTH, 20);
            box = new BoundingBox(boxMin, boxMax);

            CollisionDetector.obstacles.Add(box);
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(ASSETNAME);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGTH), Color.White);
        }

        #endregion

        #region Others

        public override string ToString()
        {
            return "<obstacle> " + position.X + " " + position.Y + " </obstacle>\n";
        }

        #endregion
    }
}