using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ColorWars
{
    /// <summary>
    /// Defines the main attributes of a character
    /// </summary>
    class Character
    {
        #region Constants

        protected const int FRAME_WIDTH = 50;
        protected const int FRAME_HEIGHT = 50;

        #endregion

        #region Variables

        /// <summary>
        /// Variable related to drawing
        /// </summary>
        protected ContentManager content;
        protected Texture2D texture;
        protected Texture2D lifeTexture;
        protected Rectangle sourceRect;
        public Color spriteColor;

        /// <summary>
        /// Variables related to game
        /// </summary>
        public Kinematic kinematic;
        public float life;
        public Color lifeColor;

        /// <summary>
        /// Sensors
        /// </summary>
        public List<Sensor> sensors = new List<Sensor>();

        /// <summary>
        /// Variables related to animation
        /// </summary>
        protected float timer = 0f;
        protected float interval = 50f;
        protected int currentFrame = 0;

        /// <summary>
        /// Variables related to collisions
        /// </summary>
        public BoundingSphere bound;
        public Vector2 lastValidPosition;

        /// <summary>
        /// Timer for the paintballs
        /// </summary>
        protected float ballinterval = 1000;
        protected float balltimer = 1000;

        #endregion

        #region Initialization

        public Character(float startPositionX, float startPositionY)
        {
            kinematic = new Kinematic();
            life = 100;
            sourceRect = new Rectangle(0, 0, FRAME_WIDTH, FRAME_HEIGHT);
            spriteColor = Color.White;

            kinematic.position = new Vector3(startPositionX, startPositionY, 0);
            bound = new BoundingSphere(kinematic.position, 30);
            CollisionDetector.players.Add(bound);
        }

        public void LoadContent(string asset)
        {
            texture = content.Load<Texture2D>(asset);
            lifeTexture = content.Load<Texture2D>("ball");
        }

        #endregion

        #region Regular Functions

        public void Update()
        {
            // Now refresh the frame to draw and the collision rectangle
            sourceRect = new Rectangle(currentFrame * FRAME_WIDTH, 0, FRAME_WIDTH, FRAME_HEIGHT);

            // Refresh the collision sphere coordinates
            bound.Center.X = (int)kinematic.position.X;
            bound.Center.Y = (int)kinematic.position.Y;

            UpdateLastValidPosition();
            UpdateLife();
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw the character
            batch.Draw(texture, new Vector2(kinematic.position.X, kinematic.position.Y),
                sourceRect, spriteColor, kinematic.orientation, new Vector2(FRAME_WIDTH / 2, FRAME_HEIGHT / 2),
                1 + kinematic.position.Z, SpriteEffects.None, 0);

            // Draw its life
            float lifeSize = 1 - life / 100;

            batch.Draw(lifeTexture, new Vector2(kinematic.position.X, kinematic.position.Y),
                null, lifeColor, kinematic.orientation, new Vector2(lifeTexture.Width / 2, lifeTexture.Height / 2),
                lifeSize, SpriteEffects.None, 0);
        }

        #endregion

        #region Collision Functions

        /// <summary>
        /// Updates the last valid position by checking if it collides with anything
        /// </summary>
        private void UpdateLastValidPosition()
        {
            // If there are collisions, reset the position to the last valid position
            if (CollisionDetector.CheckCollisions(bound))
            {
                kinematic.position.X = lastValidPosition.X;
                kinematic.position.Y = lastValidPosition.Y;
            }
            // Else save this position as the last valid position
            else
                lastValidPosition = new Vector2(kinematic.position.X, kinematic.position.Y);
        }

        /// <summary>
        /// Updates the life from this character
        /// </summary>
        private void UpdateLife()
        {
            // Decrease life
            if (life > 0 & CollisionDetector.CheckPaintballsCollisions(bound, lifeColor))
                life -= 0.5f;

            // Increase life
            if (life < 100 & CollisionDetector.CheckThinnerballsCollisions(bound))
                life += 30;

            life = (life > 100) ? 100 : life;
            life = (life < 0) ? 0 : life;
        }

        #endregion

        #region Animation Functions

        protected void AnimateWalk(GameTime time)
        {
            timer += (float)time.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                ++currentFrame;

                if (currentFrame > 3)
                    currentFrame = 0;

                timer = 0;
            }
        }
        
        protected void AnimateJump()
        {
            if (kinematic.velocity.Z >= 0)
                currentFrame = 4;
            else if (kinematic.velocity.Z < 0)
                currentFrame = 5;
            else
                currentFrame = 0;
        }

        #endregion
    }
}