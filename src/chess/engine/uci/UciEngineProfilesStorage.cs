using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class UciEngineProfileStorage
    {
        private string ProfileListPath { get; set; }
        public IList<UciEngineProfile> Profiles { get; private set; }

        public UciEngineProfileStorage(string path)
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

#pragma warning disable IDE0060 // Remove unused parameter
        public void OnProfileUpdated(UciEngineProfile profile)
#pragma warning restore IDE0060 // Remove unused parameter
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
            Directory.CreateDirectory(Path.GetDirectoryName(ProfileListPath));
            File.WriteAllText(ProfileListPath, json.ToString());
        }
    }
}
