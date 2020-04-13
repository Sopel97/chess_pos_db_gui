using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public enum GameResult { WhiteWin, BlackWin, Draw };

    public struct GameResultWordFormat { }
    public struct GameResultPgnFormat { }
    public struct GameResultLetterFormat { }
    public struct GameResultPgnUnicodeFormat { }

    public static class GameResultHelper
    {
        public static string Stringify(this GameResult result, GameResultWordFormat f)
        {
            switch(result)
            {
                case GameResult.WhiteWin:
                    return "win";
                case GameResult.BlackWin:
                    return "loss";
                case GameResult.Draw:
                    return "draw";
            }

            throw new ArgumentException();
        }

        public static string Stringify(this GameResult result, GameResultPgnFormat f)
        {
            switch (result)
            {
                case GameResult.WhiteWin:
                    return "1-0";
                case GameResult.BlackWin:
                    return "0-1";
                case GameResult.Draw:
                    return "1/2-1/2";
            }

            throw new ArgumentException();
        }

        public static string Stringify(this GameResult result, GameResultPgnUnicodeFormat f)
        {
            switch (result)
            {
                case GameResult.WhiteWin:
                    return "1-0";
                case GameResult.BlackWin:
                    return "0-1";
                case GameResult.Draw:
                    return "½-½";
            }

            throw new ArgumentException();
        }

        public static string Stringify(this GameResult result, GameResultLetterFormat f)
        {
            switch (result)
            {
                case GameResult.WhiteWin:
                    return "W";
                case GameResult.BlackWin:
                    return "L";
                case GameResult.Draw:
                    return "D";
            }

            throw new ArgumentException();
        }

        public static Optional<GameResult> FromString(GameResultWordFormat f, string str)
        {
            switch (str)
            {
                case "win":
                    return Optional<GameResult>.Create(GameResult.WhiteWin);
                case "loss":
                    return Optional<GameResult>.Create(GameResult.BlackWin);
                case "draw":
                    return Optional<GameResult>.Create(GameResult.Draw);
            }

            return Optional<GameResult>.CreateEmpty();
        }
        public static Optional<GameResult> FromString(GameResultPgnFormat f, string str)
        {
            switch (str)
            {
                case "1-0":
                    return Optional<GameResult>.Create(GameResult.WhiteWin);
                case "0-1":
                    return Optional<GameResult>.Create(GameResult.BlackWin);
                case "1/2-1/2":
                    return Optional<GameResult>.Create(GameResult.Draw);
            }

            return Optional<GameResult>.CreateEmpty();
        }
    }
}
