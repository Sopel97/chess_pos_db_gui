﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineOptionsForm : Form
    {
        private IList<UciOptionLinkedControl> OptionControls { get; set; }

        public bool Discard { get; private set; }

        private bool CloseInsteadOfHide { get; set; }

        public EngineOptionsForm(IList<UciOption> options, bool close = false)
        {
            InitializeComponent();

            Icon = Properties.Resources.application_icon;

            saveButton.DialogResult = DialogResult.OK;
            discardButton.DialogResult = DialogResult.Cancel;

            CloseInsteadOfHide = close;

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
            Discard = true;
        }

        private void EngineOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                DiscardChanges();
                e.Cancel = true;
                Hide();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            HideOrClose();
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            DiscardChanges();
            HideOrClose();
        }

        private void HideOrClose()
        {
            if (CloseInsteadOfHide)
            {
                Close();
            }
            else
            {
                Hide();
            }
        }

        private void EngineOptionsForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                foreach (var opt in OptionControls)
                {
                    opt.ResetControlValue();
                    opt.Enable();
                }
            }
            else
            {
                foreach (var opt in OptionControls)
                {
                    opt.Disable();
                }
            }
        }
    }
}
