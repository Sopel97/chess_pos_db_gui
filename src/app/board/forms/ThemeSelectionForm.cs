using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui.src.app.board.forms
{
    public partial class ThemeSelectionForm : Form
    {
        private ThemeDatabase Themes { get; set; }

        public BoardTheme SelectedBoardTheme { get; set; }
        public PieceTheme SelectedPieceTheme { get; set; }
     
        public ThemeSelectionForm(ThemeDatabase db, BoardTheme currentBoardTheme, PieceTheme currentPieceTheme)
        {
            InitializeComponent();

            Themes = db;
            SelectedBoardTheme = currentBoardTheme;
            SelectedPieceTheme = currentPieceTheme;

            confirmButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;

            foreach (var kv in Themes.BoardThemes)
            {
                boardThemesListBox.Items.Add(kv.Key);
            }

            foreach (var kv in Themes.PieceThemes)
            {
                pieceThemesListBox.Items.Add(kv.Key);
            }

            boardThemesListBox.SelectedItem = SelectedBoardTheme.Name;
            pieceThemesListBox.SelectedItem = SelectedPieceTheme.Name;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BoardThemesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedBoardTheme = Themes.BoardThemes[(string)boardThemesListBox.SelectedItem];
        }

        private void PieceThemesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPieceTheme = Themes.PieceThemes[(string)pieceThemesListBox.SelectedItem];
        }
    }
}
