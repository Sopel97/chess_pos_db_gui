using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app.board
{
    public class ThemeDatabase
    {
        private string Path { get; set; }

        public Dictionary<string, BoardTheme> BoardThemes { get; private set; }
        public Dictionary<string, PieceTheme> PieceThemes { get; private set; }

        public ThemeDatabase(string path)
        {
            Path = path;

            BoardThemes = new Dictionary<string, BoardTheme>();
            PieceThemes = new Dictionary<string, PieceTheme>();

            DiscoverThemes();
        }

        public BoardTheme GetAnyBoardTheme()
        {
            return BoardThemes.FirstOrDefault().Value;
        }

        public PieceTheme GetAnyPieceTheme()
        {
            return PieceThemes.FirstOrDefault().Value;
        }

        private void DiscoverThemes()
        {
            BoardThemes.Clear();
            PieceThemes.Clear();

            var baseBoardSetsPath = Path + "/board_sets/";
            var basePieceSetsPath = Path + "/piece_sets/";

            var boardThemeDirs = Directory.GetDirectories(baseBoardSetsPath, "*", SearchOption.TopDirectoryOnly);
            foreach (var dir in boardThemeDirs)
            {
                try
                {
                    var theme = new BoardTheme(dir);
                    BoardThemes.Add(theme.Name, theme);
                }
                catch(Exception)
                {

                }
            }

            var pieceThemeDirs = Directory.GetDirectories(basePieceSetsPath, "*", SearchOption.TopDirectoryOnly);
            foreach (var dir in pieceThemeDirs)
            {
                string directoryName = System.IO.Path.GetFileName(dir.TrimEnd(System.IO.Path.DirectorySeparatorChar));
                try
                {
                    var theme = new PieceTheme(dir);
                    PieceThemes.Add(theme.Name, theme);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
