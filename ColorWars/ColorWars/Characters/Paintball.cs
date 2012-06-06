using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Paintball
    {
        #region Constants

        protected const string ASSET = "ball";
        protected const int WIDTH = 10;
        protected const int HEIGHT = 10;
        protected const int MAX_SPEED = 150;

        #endregion

        #region Variables

        protected Texture2D texture;
        public Kinematic kinematic = new Kinematic();
        public BoundingSphere bound;
        public bool activated = true;
        public Color color;

        #endregion Variables

        #region Constructors

        public Paintball(ContentManager content, Kinematic k, Color c)
        {
            // Load the texture
            texture = content.Load<Texture2D>(ASSET);
            color = c;

            // Initialize the kinematics
            kinematic = k;

            kinematic.velocity += kinematic.velocity * 2;
            kinematic.velocity.Z += 1.5f;
            kinematic.rotation = 0;

            // Initialize the bounding box
            bound = new BoundingSphere(kinematic.position, Math.Max(texture.Height, texture.Width));
        }

        #endregion

        #region Methods

        public void Update(GameTime time)
        {          
            // Refresh the collision sphere coordinates
            bound.Center.X = (int)kinematic.position.X;
            bound.Center.Y = (int)kinematic.position.Y;

            UpdateKinematics(time);

            // If the paintball is still alive, check if it has not hitted any obstacle
            if (activated)
                activated = !CollisionDetector.CheckObstaclesCollisions(bound);
        }

        private void UpdateKinematics(GameTime time)
        {
            kinematic.velocity.Z -= 0.981f * (float)time.ElapsedGameTime.TotalSeconds;

            kinematic.Update(new SteeringOutput(), MAX_SPEED, time);

            if (kinematic.position.Z <= 0)
            {
                kinematic.position.Z = 0;
                kinematic.velocity = Vector3.Zero;
                activated = false;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(kinematic.position.X, kinematic.position.Y),
                null, color, kinematic.orientation, Vector2.Zero, 0.75f + kinematic.position.Z, SpriteEffects.None, 0);
        }

        #endregion
    }
}