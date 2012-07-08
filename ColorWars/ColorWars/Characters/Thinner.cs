using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Thinner
    {
        #region Constants

        protected const string ASSET = "ball";
        protected const int WIDTH = 10;
        protected const int HEIGHT = 10;

        #endregion

        #region Variables

        protected Texture2D texture;
        public Vector3 position;
        public BoundingSphere bound;
        public bool activated = true;
        protected float timer = 0;
        protected float interval = 10000;

        #endregion Variables

        #region Constructors

        public Thinner(ContentManager content)
        {
            // Load the texture
            texture = content.Load<Texture2D>(ASSET);

            // Choose a random polygon and draw the thinner there
            int id = GameMode.random.Next(GameMode.polygons.Count);
            position = new Vector3(GameMode.polygons[id].center.X, GameMode.polygons[id].center.Y, 0);

            // Initialize the bounding box
            bound = new BoundingSphere(position, Math.Max(texture.Height, texture.Width));

            // Add a scent to the graph
            Scent s = new Scent(id, 1, null);
            GameMode.smells.nodes[id].AddSignal(s);
            s.Propagate();
        }

        #endregion

        #region Methods

        public void Update(GameTime time)
        {
            // Refresh the timer
            timer += (float)time.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                // Deactivate the thinner
                activated = false;
                timer = 0;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(position.X, position.Y), Color.Green);
        }

        #endregion
    }
}