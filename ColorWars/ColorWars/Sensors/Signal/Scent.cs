using Microsoft.Xna.Framework;

namespace ColorWars
{
    class Scent : Signal
    {
        #region Variables

        /// <summary>
        /// Maximum arc value
        /// </summary>
        protected float maximum;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID of the node</param>
        /// <param name="intensity">Intensity</param>
        public Scent(int id, float intensity, Node origin)
            : base(id, intensity, origin)
        {
            this.graph = GameMode.smells;
            
            // The factor will be the inverse of the maximum cost
            maximum = 0;

            for (int i = 0; i != graph.nodes.Count; ++i)
                for (int j = 0; j != graph.nodes.Count; ++j)
                    if (!float.IsPositiveInfinity(graph.arcs[i, j]) & graph.arcs[i, j] > maximum)
                        maximum = graph.arcs[i, j];

            this.factor = maximum * 0.125f;

            Gearset.GS.Show("factor", factor);
        }

        #endregion

        #region Propagation

        /// <summary>
        /// Propagates a given signal
        /// </summary>
        public void Propagate()
        {
            if (intensity <= 0.1f)
                return;

            for (int i = 0; i != graph.nodes.Count; ++i)
            {
                // If the nodes are not neighboors, continue
                if (float.IsPositiveInfinity(graph.arcs[id, i]))
                    continue;

                // Calculate the intensity of the new signal
                float newIntensity = MathHelper.Min(intensity * factor / graph.arcs[id, i], 0.9f);

                // Now check the signals in the node
                bool ok = true;
                
                foreach (Scent scent in graph.nodes[i].signals)
                {
                    if (scent.intensity < newIntensity)
                    {
                        scent.intensity = newIntensity;
                        scent.endInterval = maxtime * newIntensity;
                        scent.Propagate();
                        scent.origin = graph.nodes[id];
                    }

                    ok = false;
                }

                // If there wasn't a scent in the node, add one
                if (ok)
                {
                    Scent s = new Scent(i, newIntensity, graph.nodes[id]);
                    graph.nodes[i].AddSignal(s);
                    s.Propagate();
                }
            }
        }

        #endregion
    }
}