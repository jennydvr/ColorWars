using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class EditorMode
    {
        #region Items of the game

        // Obstacles of the game
        Background background;
        static public List<Obstacle> obstacles;

        // Characters
        static public Player dotty;
        static public List<Enemy> squorres;

        // Polygons
        static public List<Triangle> triangles = new List<Triangle>();
        static public List<Node> nodes = new List<Node>();

        #endregion

        #region Variables

        // Buttons
        EnemiesButton enButton;
        ObstaclesButton obsButton;
        PlayerButton plButton;
        TriangleButton trButton;
        SaveButton saButton;

        // Other stuff
        MouseState previousMouseState;
        public ContentManager content;

        // States
        enum State
        {
            None, 
            Player,
            Enemies,
            Obstacles,
            Triangles,
            Save
        }
        State currentState = State.None;

        #endregion

        #region Initialization

        public EditorMode(ContentManager content)
        {
            // Initialize stuff
            background = new Background();
            dotty = new Player();
            squorres = new List<Enemy>();
            obstacles = new List<Obstacle>();

            // Initialize buttons
            plButton = new PlayerButton(content, new Vector2(150, 50));
            enButton = new EnemiesButton(content, new Vector2(200, 50));
            obsButton = new ObstaclesButton(content, new Vector2(250, 50));
            trButton = new TriangleButton(content, new Vector2(300, 50));
            saButton = new SaveButton(content, new Vector2(350, 50));

            // Initialize other stuff
            this.content = content;
        }

        public void LoadContent()
        {
            dotty.LoadContent(content);
        }

        #endregion

        #region Update and Drawing

        public void Update(GameTime time)
        {
            MouseState currentMouseState = Mouse.GetState();

            // Make an action according to the state
            Action(currentMouseState);
            UpdateStates(currentMouseState);

            previousMouseState = currentMouseState;
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.Draw(batch);

            // Draw player
            dotty.Draw(batch);

            // Draw enemies
            foreach (Enemy squorre in squorres)
                squorre.Draw(batch);

            // Draw triangles
            for (int i = 0; i != triangles.Count; ++i)
                triangles[i].Draw(batch, i, content);

            // Draw nodes
            foreach (Node node in nodes)
                node.Draw(content, batch);

            // Draw buttons
            plButton.Draw(batch);
            enButton.Draw(batch);
            obsButton.Draw(batch);
            trButton.Draw(batch);
            saButton.Draw(batch);

            // Show state
            Gearset.GS.Show("Estado", currentState);
        }

        #endregion

        #region Input Manager

        protected void UpdateStates(MouseState currentMouseState)
        {
            if (!(currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released))
                return;

            if (enButton.IsClicked(currentMouseState))
                currentState = State.Enemies;
            else if (obsButton.IsClicked(currentMouseState))
                currentState = State.Obstacles;
            else if (plButton.IsClicked(currentMouseState))
                currentState = State.Player;
            else if (trButton.IsClicked(currentMouseState))
                currentState = State.Triangles;
            else if (saButton.IsClicked(currentMouseState))
                currentState = State.Save;
        }

        protected void Action(MouseState currentMouseState)
        {
            if (!saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !trButton.IsClicked(currentMouseState) & currentState == State.Enemies)
                UpdateButton(enButton, currentMouseState);
            else if (!saButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !trButton.IsClicked(currentMouseState) & currentState == State.Obstacles)
                UpdateButton(obsButton, currentMouseState);
            else if (!saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & !trButton.IsClicked(currentMouseState) & currentState == State.Player)
                UpdateButton(plButton, currentMouseState);
            else if (!saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & currentState == State.Triangles)
                UpdateButton(trButton, currentMouseState);
            else if (!obsButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !trButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & currentState == State.Save)
                UpdateButton(saButton, currentMouseState);
        }

        protected void UpdateButton(Button button, MouseState currentMouseState)
        {
            if (!(currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released))
                return;

            button.Action(currentMouseState);
        }

        #endregion
    }
}