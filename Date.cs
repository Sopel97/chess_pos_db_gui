using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    struct Date
    {
        public Optional<ushort> Year { get; set; }
        public Optional<byte> Month { get; set; }
        public Optional<byte> Day { get; set; }

        public static Date FromJson(JsonValue json)
        {
            return Date.FromString(json);
        }

        public static Date FromString(string str)
        {
            string[] parts = str.Split('.');
            if (parts.Length != 3) throw new ArgumentException();

            return new Date
            {
                Year = UInt16.TryParse(str, out ushort year) ? Optional<ushort>.Create(year) : Optional<ushort>.CreateEmpty(),
                Month = Byte.TryParse(str, out byte month) ? Optional<byte>.Create(month) : Optional<byte>.CreateEmpty(),
                Day = Byte.TryParse(str, out byte day) ? Optional<byte>.Create(day) : Optional<byte>.CreateEmpty()
            };
        }

        public Date(UInt16 year, Byte month, Byte day)
        {
            Year = Optional<ushort>.Create(year);
            Month = Optional<byte>.Create(month);
            Day = Optional<byte>.Create(day);
        }
    }
}
