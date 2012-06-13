using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class SaveButton : Button
    {
        public SaveButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            // The file:
            string filename = "\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\mapa.xml";

            // Create a new file and open it
            XmlTextWriter textWriter = new XmlTextWriter(filename, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.WriteStartDocument();

            // Write game
            textWriter.WriteStartElement("game");

            // Write player
            textWriter.WriteStartElement("player");

            textWriter.WriteStartElement("x");
            textWriter.WriteValue(GameMode.dotty.kinematic.position.X);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("y");
            textWriter.WriteValue(GameMode.dotty.kinematic.position.Y);
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();

            // Write enemies
            textWriter.WriteStartElement("enemies");
            foreach (Enemy enemy in GameMode.squorres)
            {
                textWriter.WriteStartElement("enemy");

                textWriter.WriteStartElement("x");
                textWriter.WriteValue(enemy.kinematic.position.X);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("y");
                textWriter.WriteValue(enemy.kinematic.position.Y);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();

            // Write obstacles
            textWriter.WriteStartElement("obstacles");
            foreach (Obstacle obstacle in GameMode.obstacles)
            {
                textWriter.WriteStartElement("obstacle");

                textWriter.WriteStartElement("x");
                textWriter.WriteValue(obstacle.position.X);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("y");
                textWriter.WriteValue(obstacle.position.Y);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();

            // Write triangles
            textWriter.WriteStartElement("polygons");
            foreach (Polygon polygon in GameMode.polygons)
            {
                textWriter.WriteStartElement("polygon");

                foreach (Vector2 node in polygon.nodes)
                {
                    textWriter.WriteStartElement("node");

                    textWriter.WriteStartElement("x");
                    textWriter.WriteValue(node.X);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement("y");
                    textWriter.WriteValue(node.Y);
                    textWriter.WriteEndElement();

                    textWriter.WriteEndElement();
                }

                textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();

            // Ends the document and close writer
            textWriter.WriteEndDocument();
            textWriter.Close();
        }
    }
}