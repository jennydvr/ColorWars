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

        /// <summary>
        /// Nodes
        /// </summary>
        static public List<Point> nodes = new List<Point>();

        /// <summary>
        /// Vision Graph
        /// </summary>
        static public Graph vision = new Graph();

        #endregion

        #region Variables

        // Buttons
        EnemiesButton enButton;
        ObstaclesButton obsButton;
        PlayerButton plButton;
        PolygonButton polyButton;
        SaveButton saButton;
        NodesButton nButton;
        ArcButton aButton;
        SaveVisionButton visButton;

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
            Save,
            Arc,
            SaveVision
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
            aButton = new ArcButton(content, new Vector2(400, 50));
            saButton = new SaveButton(content, new Vector2(450, 50));
            visButton = new SaveVisionButton(content, new Vector2(500, 50));
        }

        public override void LoadContent()
        {
            ReadXML();

            // Fill the nodes
            for (int i = 0; i != polygons.Count; ++i)
                nodes.Add(new Point(polygons[i].center));

            // Finally create the graph
            movement = new Graph();
            movement.arcs = new float[nodes.Count, nodes.Count];

            for (int i = 0; i != polygons.Count; ++i)
                movement.nodes.Add(new Node(polygons[i].center, i));

            for (int i = 0; i != GameMode.polygons.Count; ++i)
                for (int j = i; j < GameMode.polygons.Count; ++j)
                    movement.arcs[i, j] = movement.arcs[j, i] = float.PositiveInfinity;

            // Load obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.LoadContent(content);

            // Load player
            dotty.LoadContent(content);

            // Load enemies
            foreach (Enemy squorre in squorres)
                squorre.LoadContent(content);

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
            movement.Draw(content, batch);

            // Draw buttons
            plButton.Draw(batch);
            enButton.Draw(batch);
            obsButton.Draw(batch);
            polyButton.Draw(batch);
            saButton.Draw(batch);
            nButton.Draw(batch);
            aButton.Draw(batch);
            visButton.Draw(batch);

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
            else if (aButton.IsClicked(currentMouseState))
                currentState = State.Arc;
            else if (visButton.IsClicked(currentMouseState))
                currentState = State.SaveVision;
        }

        protected void Action(MouseState currentMouseState)
        {
            if (OnlyPressedButtonIs(enButton, currentMouseState) & currentState == State.Enemies)
                UpdateButton(enButton, currentMouseState);
            else if (OnlyPressedButtonIs(obsButton, currentMouseState) & currentState == State.Obstacles)
                UpdateButton(obsButton, currentMouseState);
            else if (OnlyPressedButtonIs(plButton, currentMouseState) & currentState == State.Player)
                UpdateButton(plButton, currentMouseState);
            else if (OnlyPressedButtonIs(polyButton, currentMouseState) & currentState == State.Polygon)
                UpdateButton(polyButton, currentMouseState);
            else if (OnlyPressedButtonIs(saButton, currentMouseState) & currentState == State.Save)
                UpdateButton(saButton, currentMouseState);
            else if (OnlyPressedButtonIs(nButton, currentMouseState) & currentState == State.Node)
                UpdateButton(nButton, currentMouseState);
            else if (OnlyPressedButtonIs(aButton, currentMouseState) & currentState == State.Arc)
                UpdateButton(aButton, currentMouseState);
            else if (OnlyPressedButtonIs(visButton, currentMouseState) & currentState == State.Arc)
                UpdateButton(visButton, currentMouseState);
        }

        protected void UpdateButton(Button button, MouseState currentMouseState)
        {
            if (!(currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released))
                return;

            button.Action(currentMouseState);
        }

        protected bool OnlyPressedButtonIs(Button button, MouseState currentMouseState)
        {
            // Add to the list all the buttons
            List<Button> list = new List<Button>();
            list.Add(plButton);
            list.Add(enButton);
            list.Add(obsButton);
            list.Add(polyButton);
            list.Add(nButton);
            list.Add(aButton);
            list.Add(saButton);
            list.Add(visButton);

            foreach (Button other in list)
                if (!button.GetType().Equals(other.GetType()) && other.IsClicked(currentMouseState))
                    return false;

            return true;
        }

        #endregion
    }
}