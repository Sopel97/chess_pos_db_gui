
using System;
using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class CheckUciOption : UciOption
    {
        public string Name { get; private set; }
        public bool DefaultValue { get; private set; }
        public bool Value { get; private set; }

        public static CheckUciOption ParseFromParts(string name, string defaultValue)
        {
            return new CheckUciOption(name, defaultValue == "true");
        }

        public CheckUciOption(string name, bool defaultValue)
        {
            Name = name;
            DefaultValue = Value = defaultValue;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Check;
        }
        public override bool IsDefault()
        {
            return DefaultValue == Value;
        }

        public override void CopyValueFrom(UciOption other)
        {
            if (this.GetType() != other.GetType())
            {
                throw new ArgumentException("Option type different");
            }

            Value = ((CheckUciOption)other).Value;
        }
        public void SetValue(bool value)
        {
            Value = value;
        }
        public override void SetValue(string value)
        {
            Value = bool.Parse(value);
        }

        public override UciOptionControlPanel CreateControl()
        {
            var control = new System.Windows.Forms.CheckBox
            {
                Checked = Value,
                Enabled = true,
                Visible = true
            };

            return new UciOptionControlPanel(Name, control);
        }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }
        public override JsonValue NameValueToJson()
        {
            return new JsonObject(
                new KeyValuePair<string, JsonValue>[]{
                    new KeyValuePair<string, JsonValue>( "name", Name ),
                    new KeyValuePair<string, JsonValue>( "value", ToString() )
                });
        }

        public override string GetName()
        {
            return Name;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return obj is CheckUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override UciOptionLinkedControl CreateLinkedControl()
        {
            return new CheckUciOptionLinkedControl(this);
        }
    }
}
