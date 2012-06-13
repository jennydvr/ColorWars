using Microsoft.Xna.Framework.Content;

namespace ColorWars
{
    class DebugMode : GameMode
    {
        #region Initialization

        public DebugMode(ContentManager content)
            :base(content)
        {
        }

        #endregion

        #region Auxiliar methods

        public override void AddEnemy(float x, float y)
        {
            squorres.Add(new EnemyDebug(x, y));
        }

        #endregion
    }
}