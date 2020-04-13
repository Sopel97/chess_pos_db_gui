using System;
using System.Linq;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class CreateEngineProfileForm : Form
    {
        public UciEngineProfile Profile { get; private set; }
        private UciEngineProfileStorage Profiles { get; set; }

        public CreateEngineProfileForm(UciEngineProfileStorage profiles)
        {
            InitializeComponent();
            Profiles = profiles;
        }

        private void SetPathButton_Click(object sender, EventArgs e)
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            var name = nameTextBox.Text;
            var path = pathTextBox.Text;
            if (name == "" || path == "")
            {
                MessageBox.Show("Name and path must be non empty");
                return;
            }

            if (Profiles.Profiles.FirstOrDefault(p => p.Name == name) != null)
            {
                MessageBox.Show("Name already in use");
                return;
            }

            Profile = new UciEngineProfile(null, name, path);
            Close();
        }
    }
}
