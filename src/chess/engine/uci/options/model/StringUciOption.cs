
using System;
using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class StringUciOption : UciOption
    {
        public string Name { get; private set; }

        public string DefaultValue { get; private set; }
        public string Value { get; private set; }

        public static StringUciOption ParseFromParts(string name, string defaultValue)
        {
            return new StringUciOption(name, defaultValue);
        }

        public StringUciOption(string name, string defaultValue)
        {
            Name = name;
            DefaultValue = Value = defaultValue ?? "";
        }

        public StringUciOption(string name, string defaultValue, string value)
        {
            Name = name;
            DefaultValue = defaultValue ?? "";
            Value = value ?? "";
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.String;
        }

        public override bool IsDefault()
        {
            return DefaultValue == Value;
        }

        public override void SetValue(UciOption other)
        {
            if (this.GetType() != other.GetType())
            {
                throw new ArgumentException("Option type different");
            }

            Value = ((StringUciOption)other).Value;
        }
        public override void SetValue(string value)
        {
            Value = value;
        }

        public override UciOptionControlPanel CreateControlPanel()
        {
            var control = new System.Windows.Forms.TextBox
            {
                Text = DefaultValue,
                Width = 100
            };

            return new UciOptionControlPanel(Name, control);
        }

        public override string ValueToString()
        {
            return Value;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override KeyValuePair<string, string> GetKeyValuePair()
        {
            return new KeyValuePair<string, string>(
                Name,
                Value
                );
        }

        public override string GetName()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is StringUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }

        public override int GetHashCode()
        {
            return (Name, Value).GetHashCode();
        }
        public override UciOptionLinkedControl CreateLinkedControl()
        {
            return new StringUciOptionLinkedControl(this);
        }

        public override UciOption Copy()
        {
            return new StringUciOption(Name, DefaultValue, Value);
        }
    }
}
