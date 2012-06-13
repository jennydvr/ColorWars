using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class EditorMode : GameMode
    {
        #region Items of the game

        // Polygons
        static public List<Point> nodes = new List<Point>();

        #endregion

        #region Variables

        // Buttons
        EnemiesButton enButton;
        ObstaclesButton obsButton;
        PlayerButton plButton;
        PolygonButton polyButton;
        SaveButton saButton;
        NodesButton nButton;

        // Other stuff
        MouseState previousMouseState;

        // States
        enum State
        {
            None, 
            Player,
            Enemies,
            Obstacles,
            Polygon,
            Node,
            Save
        }
        State currentState = State.None;

        #endregion

        #region Initialization

        public EditorMode(ContentManager content)
            :base(content)
        {
            // Initialize stuff
            background = new Background();
            obstacles = new List<Obstacle>();
            squorres = new List<Enemy>();
            dotty = new Player(100, 100);

            // Initialize buttons
            plButton = new PlayerButton(content, new Vector2(150, 50));
            enButton = new EnemiesButton(content, new Vector2(200, 50));
            obsButton = new ObstaclesButton(content, new Vector2(250, 50));
            polyButton = new PolygonButton(content, new Vector2(300, 50));
            nButton = new NodesButton(content, new Vector2(350, 50));
            saButton = new SaveButton(content, new Vector2(400, 50));
        }

        public override void LoadContent()
        {
            dotty.LoadContent(content);
        }

        #endregion

        #region Update and Drawing

        public override void Update(GameTime time)
        {
            MouseState currentMouseState = Mouse.GetState();

            // Make an action according to the state
            Action(currentMouseState);
            UpdateStates(currentMouseState);

            previousMouseState = currentMouseState;
            graph = new Graph();
        }

        public override void Draw(SpriteBatch batch)
        {
            // Draw obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.Draw(batch);

            // Draw player
            dotty.Draw(batch);

            // Draw enemies
            foreach (Enemy squorre in squorres)
                squorre.Draw(batch);

            // Draw polygons
            for (int i = 0; i != polygons.Count; ++i)
                polygons[i].Draw(batch, i, content);

            // Draw nodes
            foreach (Point node in nodes)
                node.Draw(content, batch);

            // Draw graph
            graph.Draw(content, batch);

            // Draw buttons
            plButton.Draw(batch);
            enButton.Draw(batch);
            obsButton.Draw(batch);
            polyButton.Draw(batch);
            saButton.Draw(batch);
            nButton.Draw(batch);

            // Show state
            Gearset.GS.Show("Estado", currentState);
            Gearset.GS.Show("CreandoP", PolygonButton.creando);
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
            else if (polyButton.IsClicked(currentMouseState))
            {
                PolygonButton.creando = !PolygonButton.creando;
                currentState = State.Polygon;
            }
            else if (saButton.IsClicked(currentMouseState))
                currentState = State.Save;
            else if (nButton.IsClicked(currentMouseState))
                currentState = State.Node;
        }

        protected void Action(MouseState currentMouseState)
        {
            if (!nButton.IsClicked(currentMouseState) & !saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !polyButton.IsClicked(currentMouseState) & currentState == State.Enemies)
                UpdateButton(enButton, currentMouseState);
            else if (!nButton.IsClicked(currentMouseState) & !saButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !polyButton.IsClicked(currentMouseState) & currentState == State.Obstacles)
                UpdateButton(obsButton, currentMouseState);
            else if (!nButton.IsClicked(currentMouseState) & !saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & !polyButton.IsClicked(currentMouseState) & currentState == State.Player)
                UpdateButton(plButton, currentMouseState);
            else if (!nButton.IsClicked(currentMouseState) & !saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & currentState == State.Polygon)
                UpdateButton(polyButton, currentMouseState);
            else if (!nButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !polyButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & currentState == State.Save)
                UpdateButton(saButton, currentMouseState);
            else if (!saButton.IsClicked(currentMouseState) & !obsButton.IsClicked(currentMouseState) & !plButton.IsClicked(currentMouseState) & !polyButton.IsClicked(currentMouseState) & !enButton.IsClicked(currentMouseState) & currentState == State.Node)
                UpdateButton(nButton, currentMouseState);
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