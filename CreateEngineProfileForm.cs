using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class CreateEngineProfileForm : Form
    {
        public UciEngineProfile Profile { get; private set; }
        private IList<UciEngineProfile> Profiles { get; set; }

        public CreateEngineProfileForm(IList<UciEngineProfile> profiles)
        {
            InitializeComponent();
            Profiles = profiles;
        }

        private void setPathButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog browser = new OpenFileDialog())
            {
                browser.Filter = "Executables (*.exe)|*.exe";
                browser.CheckFileExists = true;
                browser.Multiselect = false;
                browser.ValidateNames = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = browser.FileName;
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var name = nameTextBox.Text;
            var path = pathTextBox.Text;
            if (name == "" || path == "")
            {
                MessageBox.Show("Name and path must be non empty");
                return;
            }

            if (Profiles.First(p => p.Name == name) != null)
            {
                MessageBox.Show("Name already in use");
                return;
            }

            Profile = new UciEngineProfile(name, path);
            Close();
        }
    }
}
