using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ColorWars
{
    class Enemy : Character
    {
        #region Constants

        protected const string ASSETNAME = "Characters/Squorre";
        protected int MAX_SPEED = 150;

        #endregion

        #region Variables

        private List<SteeringBehavior> behaviors;

        private BasicMachine machine;

        #endregion

        #region Initialization

        public Enemy(float startPositionX, float startPositionY)
            : base()
        {
            kinematic.position = new Vector3(startPositionX, startPositionY, 0);
            bound = new BoundingSphere(kinematic.position, 34);

            lifeColor = Color.DeepSkyBlue;

            // Initialize all the behaviors of the enemy
            behaviors = new List<SteeringBehavior>();
            InitializeBehaviors();
            machine = new BasicMachine(this);

            CollisionDetector.players.Add(bound);
        }

        public void LoadContent(ContentManager cont)
        {
            content = cont;
            base.LoadContent(ASSETNAME);
        }

        protected virtual void InitializeBehaviors()
        {
            //behaviors.Add(new CarefullyPursue());
            behaviors.Add(new FleeDisappear(this));
        }

        #endregion

        #region Regular Functions

        public void Update(GameTime time, Kinematic target)
        {
            CollisionDetector.players.Remove(bound);

            // Dont try to reach the target in the z-axis. You can't jump!
            target.position.Z = target.velocity.Z = 0;

            // Update kinematics
            UpdateKinematics(time, target);

            base.Update();

            CollisionDetector.players.Add(bound);
        }

        protected virtual void UpdateKinematics(GameTime time, Kinematic target)
        {
          //  SteeringOutput steering = behaviors[0].GetSteering(kinematic, target);

            SteeringOutput steering = machine.Update(kinematic, target);

            kinematic.Update(steering, MAX_SPEED, time);

            // If the character is moving, animate it
            if (kinematic.velocity.Length() > 0)
                AnimateWalk(time);
        }

        #endregion

        #region Others

        public override string ToString()
        {
            return "<enemy> " + kinematic.position.X + " " + kinematic.position.Y + " </enemy>\n";
        }

        #endregion
    }
}