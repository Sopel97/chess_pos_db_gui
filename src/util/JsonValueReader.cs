using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui.src.util
{
    public class JsonValueReader
    {
        public static float ReadPercentToFloat01(JsonValue json)
        {
            string s = ((string)json).Replace(" ", "");
            if (s.Last() != '%')
            {
                throw new ArgumentException("");
            }

            s = s.Remove(s.Length - 1);

            return float.Parse(s) * 0.01f;
        }
    }
}
