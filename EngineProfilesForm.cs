using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineProfilesForm : Form
    {
        private static readonly string ProfileListPath = "data/engine_profiles.json";

        private IList<UciEngineProfile> Profiles { get; set; }


        public EngineProfilesForm()
        {
            InitializeComponent();

            Profiles = new List<UciEngineProfile>();
            DeserializeEngineList();
        }

        private void DeserializeEngineList()
        {
            if (File.Exists(ProfileListPath))
            {
                var profileListJson = JsonValue.Parse(File.ReadAllText(ProfileListPath));
                foreach (JsonValue json in profileListJson)
                {
                    Profiles.Add(new UciEngineProfile(json));
                }
            }
        }

        private void SerializeEngineList()
        {
            var json = new JsonArray();
            foreach(var profile in Profiles)
            {
                json.Add(profile.ToJson());
            }
            File.WriteAllText(ProfileListPath, json.ToString());
        }

        private void EngineProfilesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeEngineList();
        }
    }
}
