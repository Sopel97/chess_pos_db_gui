using chess_pos_db_gui.src.util;
using System;
using System.Linq;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public enum EngineProfilesFormMode
    {
        Manage,
        Select
    }

    public partial class EngineProfilesForm : Form
    {
        private class SerializableSettings : JsonSerializable<SerializableSettings>
        {
            public int FormWidth { get; set; } = 400;
            public int FormHeight { get; set; } = 300;
        }

        private static readonly string settingsPath = "data/engine_profiles_form/settings.json";

        private UciEngineProfileStorage Profiles { get; set; }

        public UciEngineProfile SelectedProfile { get; private set; }

        public EngineProfilesFormMode Mode { get; private set; }

        public EngineProfilesForm(UciEngineProfileStorage profiles, EngineProfilesFormMode mode)
        {
            InitializeComponent();

            Icon = Properties.Resources.application_icon;

            Mode = mode;
            if (Mode == EngineProfilesFormMode.Manage)
            {
                confirmButton.Visible = false;
            }
            else
            {
                optionsButton.Visible = false;
            }

            Profiles = profiles;

            FillProfileListBox();
        }

        private void FillProfileListBox()
        {
            foreach (var profile in Profiles.Profiles)
            {
                profilesListBox.Items.Add(profile.Name);
            }
        }

        private UciEngineProfile GetProfileByName(string name)
        {
            return Profiles.GetByName(name);
        }

        private void RemoveProfileByName(string name)
        {
            Profiles.RemoveProfileByName(name);
            profilesListBox.Items.Remove(name);
        }

        private void AddProfile(UciEngineProfile profile)
        {
            if (profilesListBox.Items.Contains(profile.Name))
            {
                throw new ArgumentException("Name already used");
            }

            Profiles.AddProfile(profile);
            profilesListBox.Items.Add(profile.Name);
        }

        private void AddProfileButton_Click(object sender, EventArgs e)
        {
            var form = new CreateEngineProfileForm(Profiles);
            form.ShowDialog();
            var newProfile = form.Profile;
            if (newProfile != null)
            {
                AddProfile(newProfile);
            }
        }

        private void RemoveProfileButton_Click(object sender, EventArgs e)
        {
            var selection = profilesListBox.SelectedItem;
            if (selection != null)
            {
                RemoveProfileByName((string)selection);
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            var selection = profilesListBox.SelectedItem;
            if (selection != null)
            {
                SelectedProfile = GetProfileByName((string)selection);
                Close();
            }
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            var selection = profilesListBox.SelectedItem;
            if (selection != null)
            {
                var profileName = (string)selection;
                var profile = GetProfileByName(profileName);
                var engine = new UciEngineProxy(profile);
                using (var optionsForm = new EngineOptionsForm(engine.ScratchOptions))
                {
                    optionsForm.ShowDialog();

                    if (!optionsForm.Discard)
                    {
                        engine.UpdateUciOptions();
                    }
                }

                engine.Quit();
            }
        }

        private void SaveSettings()
        {
            var settings = GatherSerializableSettings();
            settings.Serialize(settingsPath);
        }

        private void LoadSettings()
        {
            var settings = SerializableSettings.Deserialize(settingsPath);
            ApplySerializableSettings(settings);
        }

        private void ApplySerializableSettings(SerializableSettings settings)
        {
            Width = settings.FormWidth;
            Height = settings.FormHeight;
        }

        private SerializableSettings GatherSerializableSettings()
        {
            return new SerializableSettings
            {
                FormWidth = Width,
                FormHeight = Height
            };
        }

        private void EngineProfilesForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void EngineProfilesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }
}
