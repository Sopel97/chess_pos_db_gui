using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class Entry
    {
        public ulong Count { get; set; }
        public Optional<GameHeader> FirstGame { get; set; }
        public Optional<GameHeader> LastGame { get; set; }

        public static Entry FromJson(JsonValue json)
        {
            return new Entry(
                json["count"],
                json.ContainsKey("first_game") ? Optional<GameHeader>.Create(GameHeader.FromJson(json["first_game"])) : Optional<GameHeader>.CreateEmpty(),
                json.ContainsKey("last_game") ? Optional<GameHeader>.Create(GameHeader.FromJson(json["last_game"])) : Optional<GameHeader>.CreateEmpty()
            );
        }

        public Entry(ulong count)
        {
            Count = count;
        }

        public Entry(ulong count, Optional<GameHeader> firstGame, Optional<GameHeader> lastGame)
        {
            Count = count;
            FirstGame = firstGame;
            LastGame = lastGame;
        }
    }
}
