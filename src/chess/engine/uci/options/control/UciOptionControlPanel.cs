namespace chess_pos_db_gui
{
    public class UciOptionControlPanel
    {
        public System.Windows.Forms.TableLayoutPanel Panel { get; private set; }
        public System.Windows.Forms.Label Label { get; private set; }
        public System.Windows.Forms.Control Control { get; private set; }

        public UciOptionControlPanel(string name, System.Windows.Forms.Control control)
        {
            Panel = new System.Windows.Forms.TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Padding = new System.Windows.Forms.Padding(0, 0, 10, 0),
                AutoSize = true
            };

            Control = control;

            var label = new System.Windows.Forms.Label
            {
                Text = name,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Width = 128
            };

            Panel.Controls.Add(label);
            Panel.Controls.Add(Control);
        }
    }
}
