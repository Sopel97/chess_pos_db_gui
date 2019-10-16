using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    enum GameLevel { Human, Engine, Server };

    static class GameLevelHelper
    {
        public static string Stringify(this GameLevel result)
        {
            switch (result)
            {
                case GameLevel.Human:
                    return "human";
                case GameLevel.Engine:
                    return "engine";
                case GameLevel.Server:
                    return "server";
            }

            throw new ArgumentException();
        }

        public static Optional<GameLevel> FromString(string str)
        {
            switch (str)
            {
                case "human":
                    return Optional<GameLevel>.Create(GameLevel.Human);
                case "engine":
                    return Optional<GameLevel>.Create(GameLevel.Engine);
                case "server":
                    return Optional<GameLevel>.Create(GameLevel.Server);
            }

            return Optional<GameLevel>.CreateEmpty();
        }
    }
}
