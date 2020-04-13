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
        private UciEngineProfileStorage Profiles { get; set; }

        public UciEngineProfile SelectedProfile { get; private set; }

        public EngineProfilesForm(UciEngineProfileStorage profiles)
        {
            InitializeComponent();

            Profiles = profiles;

            FillProfileListBox();
        }

        private void FillProfileListBox()
        {
            foreach(var profile in Profiles.Profiles)
            {
                profilesListBox.Items.Add(profile.Name);
            }
        }

        private UciEngineProfile GetProfileByName(string name)
        {
            return Profiles.Profiles.First(p => p.Name == name);
        }

        private void RemoveProfileByName(string name)
        {
            Profiles.RemoveProfile(name);
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
            if(newProfile != null)
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
    }
}
