
using System;
using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class ComboUciOption : UciOption
    {
        public string Name { get; private set; }

        public string DefaultValue { get; private set; }
        public IList<string> Vars { get; private set; }

        private string _Value;
        public string Value
        {
            get => _Value;
            
            private set
            {
                if (value != null && !Vars.Contains(value))
                {
                    throw new ArgumentException("Value not in vars.");
                }
                else
                {
                    _Value = value;
                }
            }
        }

        public static ComboUciOption ParseFromParts(string name, string defaultValue, IList<string> var)
        {
            return new ComboUciOption(name, defaultValue, var);
        }

        public ComboUciOption(string name, string defaultValue, IList<string> var)
        {
            Name = name;
            Vars = var;
            DefaultValue = Value = defaultValue;
        }

        public ComboUciOption(string name, string defaultValue, IList<string> var, string value)
        {
            Name = name;
            DefaultValue = defaultValue;
            Vars = var;
            Value = value;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Combo;
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

            Value = ((ComboUciOption)other).Value;
        }

        public override void SetValue(string value)
        {
            Value = value;
        }

        public override UciOptionControlPanel CreateControlPanel()
        {
            var control = new System.Windows.Forms.ComboBox();
            foreach (var var in Vars)
            {
                control.Items.Add(var);
            }
            control.SelectedItem = Value;

            return new UciOptionControlPanel(Name, control);
        }

        public override string ValueToString()
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
            return (Name, Value).GetHashCode();
        }

        public override UciOptionLinkedControl CreateLinkedControl()
        {
            return new ComboUciOptionLinkedControl(this);
        }

        public override UciOption Copy()
        {
            return new ComboUciOption(Name, DefaultValue, Vars, Value);
        }
    }
}
