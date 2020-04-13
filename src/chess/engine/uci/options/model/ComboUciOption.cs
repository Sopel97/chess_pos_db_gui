
using System;
using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class ComboUciOption : UciOption
    {
        public string Name { get; private set; }
        public string DefaultValue { get; private set; }
        public string Value { get; private set; }
        public IList<string> Vars { get; private set; }

        public static ComboUciOption ParseFromParts(string name, string defaultValue, IList<string> var)
        {
            return new ComboUciOption(name, defaultValue, var);
        }

        public ComboUciOption(string name, string defaultValue, IList<string> var)
        {
            Name = name;
            DefaultValue = Value = defaultValue;
            Vars = var;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Combo;
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

            Value = ((ComboUciOption)other).Value;
        }
        public override void SetValue(string value)
        {
            Value = value;
        }

        public override UciOptionControlPanel CreateControl()
        {
            var control = new System.Windows.Forms.ComboBox();
            foreach (var var in Vars)
            {
                control.Items.Add(var);
            }
            control.SelectedItem = Value;

            return new UciOptionControlPanel(Name, control);
        }

        public override string ToString()
        {
            return Value;
        }
        public override JsonValue NameValueToJson()
        {
            return new JsonObject(
                new KeyValuePair<string, JsonValue>[]{
                    new KeyValuePair<string, JsonValue>( "name", Name ),
                    new KeyValuePair<string, JsonValue>( "value", Value )
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
            return obj is ComboUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override UciOptionLinkedControl CreateLinkedControl()
        {
            return new ComboUciOptionLinkedControl(this);
        }
    }
}
