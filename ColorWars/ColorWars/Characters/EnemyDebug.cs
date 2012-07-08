using System.Collections.Generic;
using Gearset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class EnemyDebug : Enemy
    {
        #region Variables

        protected KeyboardState previousKeyboardState;
        protected List<SteeringBehavior> behaviors = new List<SteeringBehavior>();
        public int currentBehavior = 0;

        #endregion

        #region Initialization

        public EnemyDebug(float startPositionX, float startPositionY)
            : base(startPositionX, startPositionY)
        {
        }

        protected override void InitializeBehaviors()
        {
            // Set the behavior-debug mode
            SteeringBehavior.DEBUG = true;

            // First initialize the list of behaviors
            behaviors.Add(new Seek());
            behaviors.Add(new Flee());
            behaviors.Add(new Arrive());
            behaviors.Add(new Pursue());
            behaviors.Add(new Evade());
            behaviors.Add(new Face());
            behaviors.Add(new Align());
            behaviors.Add(new VelocityMatch());
            behaviors.Add(new Wander());
            behaviors.Add(new GroupWander(this));
            behaviors.Add(new FollowPath());
            behaviors.Add(new StarCatch(this));
        }

        #endregion

        #region Regular Functions

        protected override void UpdateKinematics(GameTime time, Kinematic target)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Generate an acceleration
            SteeringOutput steering = SteeringGenerator(target);

            // Show the final acceleration
            Vector2 position = new Vector2(kinematic.position.X, kinematic.position.Y);
            GS.ShowVector2("Final", position, new Vector2(steering.linear.X, steering.linear.Y), Color.SteelBlue);

            // Update kinematic and other stuff
            kinematic.Update(steering, MAX_SPEED, time);

            UpdateCurrentBehavior(currentKeyboardState);

            previousKeyboardState = currentKeyboardState;

            GS.Show("Behavior", currentBehavior);

            // If the character is moving, animate it
            if (kinematic.velocity.Length() > 0)
                AnimateWalk(time);

            // Show the enemy's velocity vector
            Vector2 offset = new Vector2(kinematic.velocity.X, kinematic.velocity.Y);
            offset.Normalize();
            offset *= 100;

            GS.ShowVector2("Velocity", position, offset, Color.Tomato);
        }

        protected void UpdateCurrentBehavior(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState != previousKeyboardState & currentKeyboardState.IsKeyDown(Keys.Tab))
            {
                kinematic.velocity = Vector3.Zero;
                kinematic.rotation = 0;

                ++currentBehavior;

                // There are 11 behavior showing
                if (currentBehavior > 12)
                    currentBehavior = 0;
            }
        }

        protected SteeringOutput SteeringGenerator(Kinematic target)
        {
            SteeringOutput steering = new SteeringOutput();

            // The first 8 behaviors need the target
            if (currentBehavior < 8 || currentBehavior == 10 || currentBehavior == 11)
            {
                MAX_SPEED = 100;
                steering = behaviors[currentBehavior].GetSteering(kinematic, target);
            }
            // The others doesn't need a target
            else if (currentBehavior == 8 || currentBehavior == 9)
            {
                MAX_SPEED = 50;
                steering = behaviors[currentBehavior].GetSteering(kinematic, new Kinematic());
            }

            return steering;
        }

        #endregion
    }
}