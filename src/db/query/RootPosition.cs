using System.Json;

namespace chess_pos_db_gui
{
    public class RootPosition
    {
        public string Fen { get; set; }
        public Optional<string> Move { get; set; }

        public static RootPosition FromJson(JsonValue json)
        {
            return new RootPosition(
                json["fen"],
                json.ContainsKey("move") ? Optional<string>.Create(json["move"]) : Optional<string>.CreateEmpty()
            );
        }

        public RootPosition(string fen)
        {
            Fen = fen;
            Move = Optional<string>.CreateEmpty();
        }

        public RootPosition(string fen, Optional<string> move)
        {
            Fen = fen;
            Move = move;
        }
    }
}
