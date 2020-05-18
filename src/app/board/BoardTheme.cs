using chess_pos_db_gui.src.util;
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
        public enum RimSide
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public enum RimCorner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public string Name { get; private set; }

        public Image LightSquare { get; private set; }
        public Image DarkSquare { get; private set; }

        public EnumArray<RimSide, Image> LightRimSide { get; private set; }
        public EnumArray<RimSide, Image> DarkRimSide { get; private set; }
        public EnumArray<RimCorner, Image> LightRimCorner { get; private set; }
        public EnumArray<RimCorner, Image> DarkRimCorner { get; private set; }

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
                var lightRimSideLeft = Image.FromFile(path + "/light_rim_side.png");
                var lightRimCornerTopLeft = Image.FromFile(path + "/light_rim_corner.png");

                var darkRimSideLeft = Image.FromFile(path + "/dark_rim_side.png");
                var darkRimCornerTopLeft = Image.FromFile(path + "/dark_rim_corner.png");

                LightRimSide = new EnumArray<RimSide, Image>();
                DarkRimSide = new EnumArray<RimSide, Image>();
                LightRimCorner = new EnumArray<RimCorner, Image>();
                DarkRimCorner = new EnumArray<RimCorner, Image>();

                LightRimSide[RimSide.Left] = lightRimSideLeft;
                LightRimSide[RimSide.Right] = (Image)lightRimSideLeft.Clone();
                LightRimSide[RimSide.Top] = (Image)lightRimSideLeft.Clone();
                LightRimSide[RimSide.Bottom] = (Image)lightRimSideLeft.Clone();

                LightRimSide[RimSide.Top].RotateFlip(RotateFlipType.Rotate90FlipNone);
                LightRimSide[RimSide.Right].RotateFlip(RotateFlipType.Rotate180FlipNone);
                LightRimSide[RimSide.Bottom].RotateFlip(RotateFlipType.Rotate270FlipNone);


                DarkRimSide[RimSide.Left] = darkRimSideLeft;
                DarkRimSide[RimSide.Right] = (Image)darkRimSideLeft.Clone();
                DarkRimSide[RimSide.Top] = (Image)darkRimSideLeft.Clone();
                DarkRimSide[RimSide.Bottom] = (Image)darkRimSideLeft.Clone();

                DarkRimSide[RimSide.Top].RotateFlip(RotateFlipType.Rotate90FlipNone);
                DarkRimSide[RimSide.Right].RotateFlip(RotateFlipType.Rotate180FlipNone);
                DarkRimSide[RimSide.Bottom].RotateFlip(RotateFlipType.Rotate270FlipNone);


                LightRimCorner[RimCorner.TopLeft] = lightRimCornerTopLeft;
                LightRimCorner[RimCorner.TopRight] = (Image)lightRimCornerTopLeft.Clone();
                LightRimCorner[RimCorner.BottomLeft] = (Image)lightRimCornerTopLeft.Clone();
                LightRimCorner[RimCorner.BottomRight] = (Image)lightRimCornerTopLeft.Clone();

                LightRimCorner[RimCorner.TopRight].RotateFlip(RotateFlipType.Rotate90FlipNone);
                LightRimCorner[RimCorner.BottomRight].RotateFlip(RotateFlipType.Rotate180FlipNone);
                LightRimCorner[RimCorner.BottomLeft].RotateFlip(RotateFlipType.Rotate270FlipNone);


                DarkRimCorner[RimCorner.TopLeft] = darkRimCornerTopLeft;
                DarkRimCorner[RimCorner.TopRight] = (Image)darkRimCornerTopLeft.Clone();
                DarkRimCorner[RimCorner.BottomLeft] = (Image)darkRimCornerTopLeft.Clone();
                DarkRimCorner[RimCorner.BottomRight] = (Image)darkRimCornerTopLeft.Clone();

                DarkRimCorner[RimCorner.TopRight].RotateFlip(RotateFlipType.Rotate90FlipNone);
                DarkRimCorner[RimCorner.BottomRight].RotateFlip(RotateFlipType.Rotate180FlipNone);
                DarkRimCorner[RimCorner.BottomLeft].RotateFlip(RotateFlipType.Rotate270FlipNone);
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

            LightRimSide = new EnumArray<RimSide, Image>();
            DarkRimSide = new EnumArray<RimSide, Image>();
            LightRimCorner = new EnumArray<RimCorner, Image>();
            DarkRimCorner = new EnumArray<RimCorner, Image>();


            LightRimSide[RimSide.Left] = w;
            LightRimSide[RimSide.Right] = w;
            LightRimSide[RimSide.Top] = w;
            LightRimSide[RimSide.Bottom] = w;

            DarkRimSide[RimSide.Left] = b;
            DarkRimSide[RimSide.Right] = b;
            DarkRimSide[RimSide.Top] = b;
            DarkRimSide[RimSide.Bottom] = b;

            LightRimCorner[RimCorner.TopLeft] = w;
            LightRimCorner[RimCorner.TopRight] = w;
            LightRimCorner[RimCorner.BottomLeft] = w;
            LightRimCorner[RimCorner.BottomRight] = w;

            DarkRimCorner[RimCorner.TopLeft] = b;
            DarkRimCorner[RimCorner.TopRight] = b;
            DarkRimCorner[RimCorner.BottomLeft] = b;
            DarkRimCorner[RimCorner.BottomRight] = b;
        }
    }
}
