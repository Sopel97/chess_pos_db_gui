using System.Json;

namespace chess_pos_db_gui
{
    public abstract class UciOption
    {
        abstract public UciOptionType GetOptionType();
        abstract public UciOptionControlPanel CreateControl();
        abstract public string GetName();
        public string GetSetOptionString()
        {
            return string.Format("setoption name {0} value {1}", GetName(), this);
        }

        public abstract void CopyValueFrom(UciOption other);
        public abstract void SetValue(string value);
        public abstract object GetValue();
        public abstract JsonValue NameValueToJson();
        public abstract bool IsDefault();
        public abstract UciOptionLinkedControl CreateLinkedControl();
    }
}
