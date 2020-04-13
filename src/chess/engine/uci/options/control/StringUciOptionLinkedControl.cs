namespace chess_pos_db_gui
{
    public class StringUciOptionLinkedControl : UciOptionLinkedControl
    {
        private StringUciOption Option { get; set; }
        private System.Windows.Forms.TextBox Control => (System.Windows.Forms.TextBox)LinkedControl.Control;

        public StringUciOptionLinkedControl(StringUciOption opt) :
            base(opt)
        {
            Option = opt;
            ResetControlValue();
        }

        public override void ResetControlValue()
        {
            Control.Text = Option.Value;
        }

        public override void UpdateLinkedOptionValue()
        {
            Option.SetValue(Control.Text);
        }
    }
}
