using Microsoft.Xna.Framework;

namespace ColorWars
{
    class Node : Point
    {
        #region Variables

        public int id;

        #endregion

        #region Initialize

        public Node(Vector2 point, int id)
            : base(point)
        {
            this.id = id;
            this.color = Color.Green;
        }

        #endregion

        #region Statics

        static public float Distance(Node first, Node second)
        {
            return Vector2.Distance(first.point, second.point);
        }

        #endregion
    }
}
