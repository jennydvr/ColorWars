namespace ColorWars
{
    class Smell : Sensor
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of this sensor</param>
        public Smell(Character character) :
            base(character)
        {
            this.graph = GameMode.smells;
        }

        #endregion

        #region Sensor Stuff

        public override void Detect()
        {
            Node node = GetNearestNode(character.kinematic);

            // Check if the node contains any smell signal
            for (int i = 0; i != node.signals.Count; ++i)
            {
                Signal signal = node.signals[i];

                // If there is, check if there are any particles around us
                if (signal is Scent)
                {
                    for (int j = 0; j != signal.points.Count; ++j)
                    {
                        Particle particle = signal.points[j];

                        // If there is a particle near us, follow its origin
                        if ((character.kinematic.position - particle.origin.position).Length() <= 100)
                        {
                            activated = true;
                            origin = particle.origin.Clone();
                            return;
                        }
                    }
                }

            }
            
            // Else, deactivate the sensor
            activated = false;
        }
        
        #endregion
    }
}