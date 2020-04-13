
using System;
using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class SpinUciOption : UciOption
    {
        public string Name { get; private set; }
        public long DefaultValue { get; private set; }
        public long Value { get; private set; }
        public long Min { get; private set; }
        public long Max { get; private set; }


        public static SpinUciOption ParseFromParts(string name, string defaultValue, string min, string max)
        {
            var minopt = min == null ? Optional<long>.CreateEmpty() : Optional<long>.Create(long.Parse(min));
            var maxopt = max == null ? Optional<long>.CreateEmpty() : Optional<long>.Create(long.Parse(max));
            var def = defaultValue == null ? minopt.Or(0) : long.Parse(defaultValue);
            return new SpinUciOption(
                name,
                def,
                minopt.Or(def),
                maxopt.Or(def)
                );
        }
        public SpinUciOption(string name, long defaultValue, long min, long max)
        {
            Name = name;
            Min = min;
            Max = max;
            DefaultValue = Value = defaultValue;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Spin;
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

            Value = ((SpinUciOption)other).Value;
        }
        public override void SetValue(string value)
        {
            Value = long.Parse(value);
        }

        public void SetValue(long value)
        {
            Value = value;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override UciOptionControlPanel CreateControl()
        {
            var control = new System.Windows.Forms.NumericUpDown
            {
                Minimum = Min,
                Maximum = Max,
                Value = Value,
                DecimalPlaces = 0,
                Increment = 1,
                AutoSize = true
            };

            return new UciOptionControlPanel(Name, control);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        public override JsonValue NameValueToJson()
        {
            return new JsonObject(
                new KeyValuePair<string, JsonValue>[]{
                    new KeyValuePair<string, JsonValue>( "name", Name ),
                    new KeyValuePair<string, JsonValue>( "value", Value.ToString() )
                });
        }

        public override string GetName()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is SpinUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override UciOptionLinkedControl CreateLinkedControl()
        {
            return new SpinUciOptionLinkedControl(this);
        }
    }
}
