using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app.board
{
    class BoardTheme
    {
        public Image WhiteSquare { get; private set; }
        public Image BlackSquare { get; private set; }

        public BoardTheme(string path)
        {
            try
            {
                WhiteSquare = Image.FromFile(path + "/white_square.png");
                BlackSquare = Image.FromFile(path + "/black_square.png");
            }
            catch (System.IO.FileNotFoundException)
            {
                SetDefaultImages();
            }
        }

        private void SetDefaultImages()
        {
            const int size = 80;

            var w = new Bitmap(size, size);
            Graphics.FromImage(w).Clear(Color.FromArgb(0xf0d9b5));

            var b = new Bitmap(size, size);
            Graphics.FromImage(w).Clear(Color.FromArgb(0xb58863));

            WhiteSquare = w;
            BlackSquare = b;
        }
    }
}
