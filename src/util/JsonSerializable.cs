using Newtonsoft.Json;
using System.IO;

namespace chess_pos_db_gui.src.util
{
    class JsonSerializable<T> where T : JsonSerializable<T>, new()
    {
        public static T Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    var serializer = MakeSerializer();
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            else
            {
                return new T();
            }
        }

        public void Serialize(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                var serializer = MakeSerializer();
                serializer.Serialize(writer, (T)this);
            }
        }

        private static JsonSerializer MakeSerializer()
        {
            return new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
