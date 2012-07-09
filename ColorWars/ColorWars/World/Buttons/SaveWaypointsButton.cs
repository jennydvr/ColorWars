using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class SaveWaypointsButton : Button
    {
        public SaveWaypointsButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            // The file:
            string filename = "\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\waypoints.xml";

            // Create a new file and open it
            XmlTextWriter textWriter = new XmlTextWriter(filename, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.WriteStartDocument();

            // Write waypoints
            textWriter.WriteStartElement("waypoints");

            foreach (Waypoint waypoint in GameMode.waypoints)
            {
                textWriter.WriteStartElement("waypoint");

                textWriter.WriteStartElement("x");
                textWriter.WriteValue(waypoint.kinematic.position.X);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("y");
                textWriter.WriteValue(waypoint.kinematic.position.Y);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
            }

            textWriter.WriteEndElement();

            // Ends the document and close writer
            textWriter.WriteEndDocument();
            textWriter.Close();
        }
    }
}