using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class AStar
    {
        public List<Node> open;
        public List<Node> closed;
        public Heuristic heuristic;
        public float[] gScore;
        public float[] fScore;
        public Node[] parent;
        public Graph graph;

        public AStar(Graph graph)
        {
            this.graph = graph;
        }

        private void Initialize(Node start, Node goal, Heuristic h)
        {
            open = new List<Node>();
            closed = new List<Node>();
            heuristic = h;

            gScore = new float[GameMode.movement.nodes.Count];
            fScore = new float[GameMode.movement.nodes.Count];
            parent = new Node[GameMode.movement.nodes.Count];

            open.Add(start);
            gScore[start.id] = 0;
            fScore[start.id] = heuristic.estimate(start);
        }

        public List<Node> Pathfind(Node start, Node goal, Heuristic h)
        {
            Initialize(start, goal, h);

            // Iterate through processing each node
            while (open.Count > 0)
            {
                // Find smallest element in the open list
                open.OrderBy<Node, float>(node => fScore[node.id]);
                Node current = open[0];

                if (current.id == goal.id)
                    return BuildPath(current, start);

                open.RemoveAt(0);
                closed.Add(current);

                for (int i = 0; i != graph.nodes.Count; ++i)
                {
                    Node neighboor = graph.nodes[i];
                    float tentative = graph.arcs[current.id, i];

                    if (float.IsPositiveInfinity(tentative) | closed.Contains(neighboor))
                        continue;

                    tentative += gScore[current.id];

                    if (!open.Contains(neighboor) | tentative < gScore[neighboor.id])
                    {
                        open.Add(neighboor);
                        parent[neighboor.id] = current;
                        gScore[neighboor.id] = tentative;
                        fScore[neighboor.id] = gScore[neighboor.id] + heuristic.estimate(neighboor);
                    }
                }
            }

            return new List<Node>();
        }

        private List<Node> BuildPath(Node current, Node start)
        {
            List<Node> path = new List<Node>();

            while (current != start)
            {
                path.Add(current);
                current = parent[current.id];
            }

            path.Add(current);
            path.Reverse();
            return path;
        }

    }
}