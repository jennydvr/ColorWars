using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ColorWars
{
    class Enemy : Character
    {
        #region Constants

        protected const string ASSETNAME = "Characters/Squorre";
        protected int MAX_SPEED = 100;

        #endregion

        #region Variables

        private List<SteeringBehavior> behaviors;

        private BasicMachine machine;

        static bool starMachineAssigned = false;

        #endregion

        #region Initialization

        public Enemy(float startPositionX, float startPositionY)
            : base(startPositionX, startPositionY)
        {
            lifeColor = Color.DeepSkyBlue;

            // Initialize all the behaviors of the enemy
            behaviors = new List<SteeringBehavior>();
            InitializeBehaviors();
            InitializeSensors();

            if (!starMachineAssigned)
            {
                machine = new BasicStarMachine(this);
                starMachineAssigned = true;
            }
            else
                machine = new BasicPursueMachine(this);
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

        protected void InitializeSensors()
        {
            sensors.Add(new Smell(this));
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

            // Increase life a little bit
            if (life < 25)
                life += 0.005f;

            base.Update();

            CollisionDetector.players.Add(bound);
        }

        protected virtual void UpdateKinematics(GameTime time, Kinematic target)
        {
          //  SteeringOutput steering = behaviors[0].GetSteering(kinematic, target);

            // Update the movement
            SteeringOutput steering = machine.Update(kinematic, target);

            kinematic.Update(steering, MAX_SPEED, time);

            // Update the sensors
            foreach (Sensor sensor in sensors)
                sensor.Detect();

            // If the character is close enough, throw a paintball
            UpdatePaintballs(time);

            // If the character is moving, animate it
            if (kinematic.velocity.Length() > 0)
                AnimateWalk(time);

            foreach (Sensor sensor in sensors)
                Gearset.GS.Show("Sensor", sensor.Test());
        }

        private void UpdatePaintballs(GameTime time)
        {
            balltimer += (float)time.ElapsedGameTime.TotalMilliseconds;

            if (balltimer > ballinterval & (kinematic.position - GameMode.dotty.kinematic.position).Length() <= 100)
            {
                CollisionDetector.balls.Add(new Paintball(content, kinematic.Clone(), Color.White));
                balltimer = 0;
            }
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