namespace chess_pos_db_gui
{
    public class ComboUciOptionLinkedControl : UciOptionLinkedControl
    {
        private ComboUciOption Option { get; set; }
        private System.Windows.Forms.ComboBox Control => (System.Windows.Forms.ComboBox)LinkedControl.Control;

        public ComboUciOptionLinkedControl(ComboUciOption opt) :
            base(opt)
        {
            Option = opt;
            ResetControlValue();
        }

        public override void ResetControlValue()
        {
            Control.SelectedItem = Option.Value;
        }

        public override void UpdateLinkedOptionValue()
        {
            Option.SetValue((string)Control.SelectedItem);
        }
    }
}
