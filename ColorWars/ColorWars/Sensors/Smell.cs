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
            foreach (Signal signal in node.signals)
                if (signal is Scent)
                {
                    activated = true;

                    // If the origin is null, the signal is in our same polygon
                    if (signal.origin == null)
                        origin = node;
                    else
                        origin = signal.origin;

                    return;
                }
            
            // Else, deactivate the sensor
            activated = false;
        }
        
        #endregion
    }
}