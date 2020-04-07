using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class EngineProfileStorage
    {
        private string ProfileListPath { get; set; }
        public IList<UciEngineProfile> Profiles { get; private set; }

        public EngineProfileStorage(string path)
        {
            ProfileListPath = path;
            Profiles = new List<UciEngineProfile>();
            DeserializeEngineList();
        }

        public void AddProfile(UciEngineProfile profile)
        {
            if (Profiles.FirstOrDefault(p => p.Name == profile.Name) != null)
            {
                throw new ArgumentException("Name already used");
            }

            profile.SetParent(this);
            Profiles.Add(profile);
            SerializeEngineList();
        }

        public void RemoveProfile(string name)
        {
            var obj = Profiles.FirstOrDefault(p => p.Name == name);
            obj.SetParent(null);
            Profiles.Remove(obj);
            SerializeEngineList();
        }

        public void OnProfileUpdated(UciEngineProfile profile)
        {
            SerializeEngineList();
        }

        private void DeserializeEngineList()
        {
            if (File.Exists(ProfileListPath))
            {
                var profileListJson = JsonValue.Parse(File.ReadAllText(ProfileListPath));
                foreach (JsonValue json in profileListJson)
                {
                    Profiles.Add(new UciEngineProfile(this, json));
                }
            }
        }

        private void SerializeEngineList()
        {
            var json = new JsonArray();
            foreach (var profile in Profiles)
            {
                json.Add(profile.ToJson());
            }
            File.WriteAllText(ProfileListPath, json.ToString());
        }
    }
}
