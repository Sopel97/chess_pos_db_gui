using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app.board
{
    public class BoardTheme
    {
        public string Name { get; private set; }

        public Image LightSquare { get; private set; }
        public Image DarkSquare { get; private set; }

        public Image LightRimSide { get; private set; }
        public Image LightRimCorner { get; private set; }
        public Image DarkRimSide { get; private set; }
        public Image DarkRimCorner { get; private set; }

        public BoardThemeConfig Config { get; private set; }

        public BoardTheme(string path)
        {
            try
            {
                LightSquare = Image.FromFile(path + "/light_square.png");
                DarkSquare = Image.FromFile(path + "/dark_square.png");

                Name = System.IO.Path.GetFileName(path.TrimEnd(System.IO.Path.DirectorySeparatorChar));
                try
                {
                    Name = File.ReadAllText(path + "/name");
                }
                catch (Exception)
                {

                }

                Config = BoardThemeConfig.FromJsonFile(path + "/config.json");
            }
            catch (System.IO.FileNotFoundException)
            {
                SetDefaultImages();
            }

            try
            {
                LightRimSide = Image.FromFile(path + "/light_rim_side.png");
                LightRimCorner = Image.FromFile(path + "/light_rim_corner.png");

                DarkRimSide = Image.FromFile(path + "/dark_rim_side.png");
                DarkRimCorner = Image.FromFile(path + "/dark_rim_corner.png");
            }
            catch (System.IO.FileNotFoundException)
            {
                SetDefaultRimImages();
            }
        }

        private void SetDefaultImages()
        {
            const int size = 80;

            var w = new Bitmap(size, size);
            Graphics.FromImage(w).Clear(Color.FromArgb(0xf0d9b5));

            var b = new Bitmap(size, size);
            Graphics.FromImage(w).Clear(Color.FromArgb(0xb58863));

            LightSquare = w;
            DarkSquare = b;
        }

        private void SetDefaultRimImages()
        {
            const int size = 80;

            var w = new Bitmap(size, size);
            Graphics.FromImage(w).Clear(Color.FromArgb(0xf0d9b5));

            var b = new Bitmap(size, size);
            Graphics.FromImage(w).Clear(Color.FromArgb(0xb58863));

            LightRimSide = w;
            LightRimCorner = w;
            DarkRimSide = b;
            DarkRimCorner = b;
        }
    }
}
