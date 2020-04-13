using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;

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

        public bool ExistsWithName(string name)
        {
            return GetByName(name) != null;
        }

        public void AddProfile(UciEngineProfile profile)
        {
            if (ExistsWithName(profile.Name))
            {
                throw new ArgumentException("Name already used");
            }

            profile.SetParent(this);
            Profiles.Add(profile);
            SerializeEngineList();
        }

        public UciEngineProfile GetByName(string name)
        {
            return Profiles.FirstOrDefault(p => p.Name == name);
        }

        public void RemoveProfileByName(string name)
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
