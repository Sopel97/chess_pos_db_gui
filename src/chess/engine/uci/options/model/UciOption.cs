using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public abstract class UciOption
    {
        abstract public UciOptionType GetOptionType();

        abstract public UciOptionControlPanel CreateControlPanel();

        abstract public string GetName();

        public string GetSetOptionString()
        {
            return string.Format("setoption name {0} value {1}", GetName(), ValueToString());
        }

        public abstract void SetValue(UciOption other);

        public abstract void SetValue(string value);

        public abstract object GetValue();

        public abstract string ValueToString();

        public abstract KeyValuePair<string, string> GetKeyValuePair();

        public abstract bool IsDefault();

        public abstract UciOptionLinkedControl CreateLinkedControl();

        public abstract UciOption Copy();
    }
}
