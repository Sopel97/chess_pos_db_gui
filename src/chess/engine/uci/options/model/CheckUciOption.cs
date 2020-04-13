
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

        public CheckUciOption(string name, bool defaultValue, bool value)
        {
            Name = name;
            DefaultValue = defaultValue;
            Value = value;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Check;
        }

        public override bool IsDefault()
        {
            return DefaultValue == Value;
        }

        public override void SetValue(UciOption other)
        {
            if (GetType() != other.GetType())
            {
                throw new ArgumentException("Option type different");
            }

            Value = ((CheckUciOption)other).Value;
        }

        public override void SetValue(string value)
        {
            Value = bool.Parse(value);
        }

        public void SetValue(bool value)
        {
            Value = value;
        }

        public override UciOptionControlPanel CreateControlPanel()
        {
            var control = new System.Windows.Forms.CheckBox
            {
                Checked = Value,
                Enabled = true,
                Visible = true
            };

            return new UciOptionControlPanel(Name, control);
        }

        public override string ValueToString()
        {
            return Value ? "true" : "false";
        }

        public override KeyValuePair<string, string> GetKeyValuePair()
        {
            return new KeyValuePair<string, string>(
                Name,
                ValueToString()
                );
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
            return (Name, Value).GetHashCode();
        }

        public override UciOptionLinkedControl CreateLinkedControl()
        {
            return new CheckUciOptionLinkedControl(this);
        }

        public override UciOption Copy()
        {
            return new CheckUciOption(Name, DefaultValue, Value);
        }
    }
}
