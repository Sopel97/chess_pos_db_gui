
using System;
using System.Collections.Generic;
using System.Linq;

namespace chess_pos_db_gui
{
    public class UciOptionFactory
    {
        public UciOptionFactory()
        {
        }

        private string NextString(Queue<string> parts)
        {
            return parts.Dequeue();
        }

        private string NextName(Queue<string> parts)
        {
            var breakers = new string[] { "type", "default", "min", "max", "var" };

            string name = parts.Dequeue();

            while (!breakers.Contains(parts.Peek()))
            {
                name += " " + parts.Dequeue();
            }

            return name;
        }

        public UciOption FromString(string str)
        {
            string name = null;
            string type = null;
            string defaultValue = null;
            IList<string> vars = new List<string>();
            string min = null;
            string max = null;

            try
            {
                Queue<string> parts = new Queue<string>(str.Split(new char[] { ' ' }));
                while (parts.Count > 0)
                {
                    string cmd = parts.Dequeue();
                    switch (cmd)
                    {
                        case "name":
                            {
                                name = NextName(parts);
                                break;
                            }

                        case "type":
                            {
                                type = NextString(parts);
                                break;
                            }

                        case "min":
                            {
                                min = NextString(parts);
                                break;
                            }

                        case "max":
                            {
                                max = NextString(parts);
                                break;
                            }

                        case "var":
                            {
                                vars.Add(NextString(parts));
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            if (type == null)
            {
                return null;
            }

            switch (type)
            {
                case "check":
                    return CheckUciOption.ParseFromParts(name, defaultValue);
                case "spin":
                    return SpinUciOption.ParseFromParts(name, defaultValue, min, max);
                case "combo":
                    return ComboUciOption.ParseFromParts(name, defaultValue, vars);
                case "string":
                    return StringUciOption.ParseFromParts(name, defaultValue);
                default:
                    return null;
            }
        }
    }
}
