using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class UciEngineProfile
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string FileHash { get; private set; }
        public IList<JsonValue> OverridedOptions { get; private set; }

        private static bool IsValidUciEngine(string path)
        {
            try
            {
                UciEngineProxy engine = new UciEngineProxy(path);
                engine.Quit();
                return true;
            }
            catch(Exception)
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

        public UciEngineProfile(string name, string path)
        {
            if (!IsValidUciEngine(path))
            {
                throw new ArgumentException("Not a valid UCI engine.");
            }

            Name = name;
            Path = path;
            FileHash = ComputeMD5(path);

            OverridedOptions = new List<JsonValue>();
        }

        public UciEngineProfile(JsonValue json)
        {
            Name = json["name"];
            Path = json["path"];
            FileHash = json["hash"];
            if (FileHash != ComputeMD5(Path))
            {
                throw new FileNotFoundException("The filehash differs from the one of the installed engine.");
            }

            OverridedOptions = OverridedOptionsFromJson(json["options"]);
        }

        public void OverrideOptions(UciEngineProxy engine)
        {
            engine.OverrideOptions(OverridedOptions);
        }

        public static IList<JsonValue> OverridedOptionsFromJson(JsonValue json)
        {
            var overrides = new List<JsonValue>();
            foreach (JsonValue opt in json)
            {
                overrides.Add(opt);
            }
            return overrides;
        }

        public JsonValue OverridedOptionsToJson()
        {
            return new JsonArray(OverridedOptions);
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

        public void SetOverridedOptions(IList<JsonValue> list)
        {
            OverridedOptions = list;
        }
    }
}
