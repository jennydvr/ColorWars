using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class SaveVisionButton : Button
    {
        public SaveVisionButton(ContentManager content, Vector2 position)
            : base(content, position)
        {
        }

        public override void Action(MouseState mouse)
        {
            // The file:
            string filename = "\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\olor.xml";

            // Create a new file and open it
            XmlTextWriter textWriter = new XmlTextWriter(filename, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.WriteStartDocument();

            // Write game
            textWriter.WriteStartElement("graph");

            // Write arcs
            textWriter.WriteStartElement("arcs");

            for (int i = 0; i != GameMode.polygons.Count; ++i)
            {
                for (int j = i + 1; j < GameMode.polygons.Count; ++j)
                {
                    textWriter.WriteStartElement("arc");

                    textWriter.WriteStartElement("i");
                    textWriter.WriteValue(i);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement("j");
                    textWriter.WriteValue(j);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement("cost");
                    textWriter.WriteValue(GameMode.movement.arcs[i, j]);
                    textWriter.WriteEndElement();

                    textWriter.WriteEndElement();
                }
            }
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();

            // Ends the document and close writer
            textWriter.WriteEndDocument();
            textWriter.Close();
        }
    }
}