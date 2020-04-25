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
        private Dictionary<string, BoardTheme> BoardThemes { get; set; }
        private Dictionary<string, PieceTheme> PieceThemes { get; set; }

        public ThemeDatabase(string path)
        {
            Path = path;

            BoardThemes = new Dictionary<string, BoardTheme>();
            PieceThemes = new Dictionary<string, PieceTheme>();

            DiscoverThemes();
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
                string directoryName = System.IO.Path.GetFileName(dir.TrimEnd(System.IO.Path.DirectorySeparatorChar));
                try
                {
                    var theme = new BoardTheme(dir);
                    string name = directoryName;
                    try
                    {
                        name = File.ReadAllText(dir + "/name");
                    }
                    catch(Exception)
                    {

                    }

                    BoardThemes.Add(name, theme);
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
                    string name = directoryName;
                    try
                    {
                        name = File.ReadAllText(dir + "/name");
                    }
                    catch (Exception)
                    {

                    }

                    PieceThemes.Add(name, theme);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
