using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class Player : Character
    {
        #region Constants

        const string ASSETNAME = "Characters/Dotty";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int MAX_SPEED = 150;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        #endregion

        #region Variables

        enum State
        {
            Walking,
            Jumping
        }
        State currentState = State.Walking;

        KeyboardState previousKeyboardState;

        LookWhereYoureGoing behavior;

        #endregion

        #region Initialization

        public Player(float startPositionX, float startPositionY)
            : base(startPositionX, startPositionY)
        {
            behavior = new LookWhereYoureGoing();
            lifeColor = Color.White;
        }

        public void LoadContent(ContentManager cont)
        {
            content = cont;
            base.LoadContent(ASSETNAME);
        }

        #endregion

        #region Regular Functions

        public void Update(GameTime time)
        {
            CollisionDetector.players.Remove(bound);

            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Update player's movement
            UpdateJump(currentKeyboardState, time);
            UpdateWalk(currentKeyboardState, time);
            UpdatePaintballs(currentKeyboardState, time);

            // Update kinematics
            kinematic.Update(behavior.GetSteering(kinematic, new Kinematic()), MAX_SPEED, time);

            previousKeyboardState = currentKeyboardState;

            base.Update();

            CollisionDetector.players.Add(bound);
        }

        #endregion

        #region Movement Manager

        private void UpdateWalk(KeyboardState currentKeyboardState, GameTime time)
        {
            if (currentState == State.Walking)
            {
                // If there are no pressed keys, there is no velocity and the position doesn't change
                if (!currentKeyboardState.IsKeyDown(Keys.Left) && !currentKeyboardState.IsKeyDown(Keys.Right) &&
                    !currentKeyboardState.IsKeyDown(Keys.Up) && !currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    kinematic.velocity = Vector3.Zero;
                    kinematic.rotation = 0;
                    currentFrame = 0;
                    return;
                }

                // Otherwise, the velocity is different from zero and the position changes
                if (currentKeyboardState.IsKeyDown(Keys.Left))
                    kinematic.velocity.X = MAX_SPEED * MOVE_LEFT;
                else if (currentKeyboardState.IsKeyDown(Keys.Right))
                    kinematic.velocity.X = MAX_SPEED * MOVE_RIGHT;

                if (currentKeyboardState.IsKeyDown(Keys.Up))
                    kinematic.velocity.Y = MAX_SPEED * MOVE_UP;
                else if (currentKeyboardState.IsKeyDown(Keys.Down))
                    kinematic.velocity.Y = MAX_SPEED * MOVE_DOWN;
    
                AnimateWalk(time);
            }
        }

        private void UpdateJump(KeyboardState currentKeyboardState, GameTime time)
        {
            if (currentState == State.Jumping && kinematic.position.Z < 0)
            {
                kinematic.position.Z = 0;
                kinematic.velocity.Z = 0;
                currentState = State.Walking;
            }

            if (currentState == State.Walking)
                if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    currentState = State.Jumping;
                    kinematic.velocity.Z = 1;
                }

            if (currentState == State.Jumping)
            {
                kinematic.velocity.Z -= 9.81f * (float)time.ElapsedGameTime.TotalSeconds / 2;
                AnimateJump();
            }
        }

        private void UpdatePaintballs(KeyboardState currentKeyboardState, GameTime time)
        {
            balltimer += (float)time.ElapsedGameTime.TotalMilliseconds;

            if (balltimer > ballinterval && currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) {
                CollisionDetector.balls.Add(new Paintball(content, kinematic.Clone(), Color.DeepSkyBlue));
                balltimer = 0;
            }
        }

        #endregion
    }
}