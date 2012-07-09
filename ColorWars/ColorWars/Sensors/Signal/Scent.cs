using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        /// <param name="order">When was this parameter called</param>
        public Scent(int id, float intensity, Node origin, int order)
            : base(id, intensity, origin, order)
        {
            this.graph = GameMode.smells;
            this.location = graph.nodes[id].point;

            // If the origin is null, you are generating the smell
            if (this.origin == null)
                this.origin = graph.nodes[id];
            
            // The factor will be the inverse of the maximum cost
            maximum = 0;

            for (int i = 0; i != graph.nodes.Count; ++i)
                for (int j = 0; j != graph.nodes.Count; ++j)
                    if (!float.IsPositiveInfinity(graph.arcs[i, j]) & graph.arcs[i, j] > maximum)
                        maximum = graph.arcs[i, j];

            this.factor = maximum * 0.125f;
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
                    // If there is a scent with less intensity, replace it
                    if (scent.intensity < newIntensity)
                    {
                        scent.intensity = newIntensity;
                        scent.endInterval = maxtime * newIntensity * 3.5f;
                    //    scent.origin = graph.nodes[id];
                        scent.origin = origin;
                        scent.Propagate();
                    }

                    ok = false;
                }

                // If there wasn't a scent in the node, add one
                if (ok)
                {
                 //   Scent s = new Scent(i, newIntensity, graph.nodes[id], i);
                    Scent s = new Scent(i, newIntensity, origin, i);
                    graph.nodes[i].AddSignal(s);
                    s.Propagate();
                }
            }
        }

        #endregion
    }
}