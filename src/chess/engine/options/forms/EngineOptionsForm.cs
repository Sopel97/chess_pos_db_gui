using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineOptionsForm : Form
    {
        private IList<UciOptionLinkedControl> OptionControls { get; set; }

        public bool Discard { get; private set; }

        public EngineOptionsForm(IList<UciOption> options)
        {
            InitializeComponent();

            SuspendLayout();
            OptionControls = new List<UciOptionLinkedControl>();
            AddControlsForOptions(options);
            ResumeLayout();
        }

        private void AddControlsForOptions(IList<UciOption> options)
        {
            foreach (var opt in options)
            {
                var control = opt.CreateLinkedControl();
                OptionControls.Add(control);
                optionsFlowLayoutPanel.Controls.Add(control.GetPanel());
            }
        }

        private void SaveChanges()
        {
            foreach (var opt in OptionControls)
            {
                opt.UpdateLinkedOptionValue();
            }
            Discard = false;
        }

        private void DiscardChanges()
        {
            foreach (var opt in OptionControls)
            {
                opt.ResetControlValue();
            }
            Discard = true;
        }

        private void EngineOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DiscardChanges();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            Hide();
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            DiscardChanges();
            Hide();
        }
    }
}
