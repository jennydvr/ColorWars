using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    static class AStar
    {
        public static List<Node> open;
        public static List<Node> closed;
        public static Heuristic heuristic;
        public static float[] gScore;
        public static float[] fScore;
        public static Node[] parent;

        private static void Initialize(Node start, Node goal)
        {
            open = new List<Node>();
            closed = new List<Node>();
            heuristic = new Heuristic(goal);

            gScore = new float[GameMode.graph.nodes.Count];
            fScore = new float[GameMode.graph.nodes.Count];
            parent = new Node[GameMode.graph.nodes.Count];

            open.Add(start);
            gScore[start.id] = 0;
            fScore[start.id] = heuristic.estimate(start);
        }

        public static List<Node> Pathfind(Node start, Node goal)
        {
            Initialize(start, goal);

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

                for (int i = 0; i != GameMode.graph.nodes.Count; ++i)
                {
                    Node neighboor = GameMode.graph.nodes[i];
                    float tentative = GameMode.graph.arcs[current.id, i];

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

        private static List<Node> BuildPath(Node current, Node start)
        {
            List<Node> path = new List<Node>();

            while (current != start)
            {
                current.color = Color.Blue;
                path.Add(current);
                current = parent[current.id];
            }

            current.color = Color.Blue;
            path.Add(current);
            path.Reverse();
            return path;
        }

    }
}