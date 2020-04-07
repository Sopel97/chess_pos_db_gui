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

        public UciEngineProfile SelectedProfile { get; private set; }


        public EngineProfilesForm()
        {
            InitializeComponent();

            Profiles = new List<UciEngineProfile>();
            DeserializeEngineList();

            FillProfileListBox();
        }

        private void FillProfileListBox()
        {
            foreach(var profile in Profiles)
            {
                profilesListBox.Items.Add(profile.Name);
            }
        }

        private UciEngineProfile GetProfileByName(string name)
        {
            return Profiles.First(p => p.Name == name);
        }

        private void RemoveProfileByName(string name)
        {
            Profiles.Remove(GetProfileByName(name));
            profilesListBox.Items.Remove(name);
        }

        private void AddProfile(UciEngineProfile profile)
        {
            if (profilesListBox.Items.Contains(profile.Name))
            {
                throw new ArgumentException("Name already used");
            }

            Profiles.Add(profile);
            profilesListBox.Items.Add(profile.Name);
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

        private void addProfileButton_Click(object sender, EventArgs e)
        {
            var form = new CreateEngineProfileForm(Profiles);
            form.ShowDialog();
            var newProfile = form.Profile;
            if(newProfile != null)
            {
                AddProfile(newProfile);
            }
        }

        private void removeProfileButton_Click(object sender, EventArgs e)
        {
            var selection = profilesListBox.SelectedItem;
            if (selection != null)
            {
                RemoveProfileByName((string)selection);
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var selection = profilesListBox.SelectedItem;
            if (selection != null)
            {
                SelectedProfile = GetProfileByName((string)selection);
                Close();
            }
        }
    }
}
