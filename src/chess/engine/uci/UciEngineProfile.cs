using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Security.Cryptography;

namespace chess_pos_db_gui
{
    public class UciEngineProfile
    {
        private UciEngineProfileStorage Parent { get; set; }

        public string Name { get; private set; }

        public string Path { get; private set; }
        public string FileHash { get; private set; }

        // Doesn't store options that are equal to defaults.
        public IList<KeyValuePair<string, string>> OverridedOptions { get; private set; }

        public UciEngineProfile(UciEngineProfileStorage parent, string name, string path)
        {
            if (!IsValidUciEngine(path))
            {
                throw new ArgumentException("Not a valid UCI engine.");
            }

            Parent = parent;
            Name = name;
            Path = path;
            FileHash = ComputeMD5(path);

            OverridedOptions = new List<KeyValuePair<string, string>>();
        }

        public UciEngineProfile(UciEngineProfileStorage parent, JsonValue json)
        {
            Parent = parent;
            Name = json["name"];
            Path = json["path"];
            FileHash = json["hash"];
            if (FileHash != ComputeMD5(Path))
            {
                throw new FileNotFoundException("The filehash differs from the one of the installed engine.");
            }

            OverridedOptions = OverridedOptionsFromJson(json["options"]);
        }

        private static bool IsValidUciEngine(string path)
        {
            try
            {
                UciEngineProxy engine = new UciEngineProxy(path);
                engine.Quit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string ComputeMD5(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public void SetParent(UciEngineProfileStorage p)
        {
            Parent = p;
        }

        public UciEngineProxy LoadEngine()
        {
            return new UciEngineProxy(this);
        }

        public void ApplyOptionsToEngine(UciEngineProxy engine)
        {
            engine.ApplyOptions(OverridedOptions);
        }

        public static IList<KeyValuePair<string, string>> OverridedOptionsFromJson(JsonValue json)
        {
            var overrides = new List<KeyValuePair<string, string>>();
            foreach (JsonValue opt in json)
            {
                overrides.Add(
                    new KeyValuePair<string, string>(
                        opt["name"],
                        opt["value"]
                        )
                    );
            }
            return overrides;
        }

        public JsonValue OverridedOptionsToJson()
        {
            var arr = new JsonArray();
            foreach(var opt in OverridedOptions)
            {
                arr.Add(
                    new JsonObject(
                        new KeyValuePair<string, JsonValue>("name", opt.Key),
                        new KeyValuePair<string, JsonValue>("value", opt.Value)
                        )
                    );
            }
            return arr;
        }

        public JsonValue ToJson()
        {
            return new JsonObject(
                new KeyValuePair<string, JsonValue>("name", Name),
                new KeyValuePair<string, JsonValue>("path", Path),
                new KeyValuePair<string, JsonValue>("hash", FileHash),
                new KeyValuePair<string, JsonValue>("options", OverridedOptionsToJson())
                );
        }

        public void SetOverridedOptions(IList<KeyValuePair<string, string>> list)
        {
            OverridedOptions = list;

            if (Parent != null)
            {
                Parent.OnProfileUpdated(this);
            }
        }
    }
}
