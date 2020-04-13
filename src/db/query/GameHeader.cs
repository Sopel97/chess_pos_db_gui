using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Json;

namespace chess_pos_db_gui
{
    public class GameHeader
    {
        public uint GameId { get; set; }
        public GameResult Result { get; set; }
        public Date Date { get; set; }
        public Eco Eco { get; set; }
        public Optional<ushort> PlyCount { get; set; }
        public string Event { get; set; }
        public string White { get; set; }
        public string Black { get; set; }

        public static GameHeader FromJson(JsonValue json)
        {
            return new GameHeader(
                json["game_id"],
                GameResultHelper.FromString(new GameResultPgnFormat(), json["result"]).First(),
                Date.FromJson(json["date"]),
                Eco.FromJson(json["eco"]),
                json.ContainsKey("ply_count") ? Optional<ushort>.Create(json["ply_count"]) : Optional<ushort>.CreateEmpty(),
                json["event"],
                json["white"],
                json["black"]
                );
        }

        public GameHeader(
            uint gameId, 
            GameResult result, 
            Date date, 
            Eco eco, 
            Optional<ushort> plyCount, 
            string @event, 
            string white, 
            string black
            )
        {
            GameId = gameId;
            Result = result;
            Date = date;
            Eco = eco;
            PlyCount = plyCount;
            Event = @event;
            White = white;
            Black = black;
        }

        public bool IsBefore(GameHeader gameHeader)
        {
            if (this.Date.IsBefore(gameHeader.Date)) return true;
            if (gameHeader.Date.IsBefore(this.Date)) return false;

            return GameId < gameHeader.GameId;
        }
    }
}
