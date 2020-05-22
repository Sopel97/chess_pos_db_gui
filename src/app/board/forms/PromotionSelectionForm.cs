using ChessDotNet;
using ChessDotNet.Pieces;
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
    public partial class PromotionSelectionForm : Form
    {
        private PieceTheme PieceImages { get; set; }

        private Player SideToMove { get; set; }

        public char? PromotedPieceType { get; set; }

        public PromotionSelectionForm(PieceTheme theme, Player player)
        {
            InitializeComponent();

            Icon = Properties.Resources.application_icon;

            PieceImages = theme;
            SideToMove = player;

            knightPromotionButton.Image = PieceImages.GetImageForPiece(new Knight(player));
            bishopPromotionButton.Image = PieceImages.GetImageForPiece(new Bishop(player));
            rookPromotionButton.Image = PieceImages.GetImageForPiece(new Rook(player));
            queenPromotionButton.Image = PieceImages.GetImageForPiece(new Queen(player));

            knightPromotionButton.DialogResult = DialogResult.OK;
            bishopPromotionButton.DialogResult = DialogResult.OK;
            rookPromotionButton.DialogResult = DialogResult.OK;
            queenPromotionButton.DialogResult = DialogResult.OK;
        }

        private void KnightPromotionButton_Click(object sender, EventArgs e)
        {
            PromotedPieceType = 'N';
            Close();
        }

        private void BishopPromotionButton_Click(object sender, EventArgs e)
        {
            PromotedPieceType = 'B';
            Close();
        }

        private void RookPromotionButton_Click(object sender, EventArgs e)
        {
            PromotedPieceType = 'R';
            Close();
        }

        private void QueenPromotionButton_Click(object sender, EventArgs e)
        {
            PromotedPieceType = 'Q';
            Close();
        }
    }
}
