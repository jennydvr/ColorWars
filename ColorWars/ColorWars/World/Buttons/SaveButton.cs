using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
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
            string filename = "\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\mapa.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            
            // Start writing
            file.WriteLine(EditorMode.dotty.ToString());

            foreach (Enemy enemy in EditorMode.squorres)
                file.WriteLine(enemy.ToString());

            foreach (Obstacle obstacle in EditorMode.obstacles)
                file.WriteLine(obstacle.ToString());

            foreach (Triangle triangle in EditorMode.triangles)
                triangle.WriteInFile(file);

            // Now save the file
            file.Close();
        }
    }
}
