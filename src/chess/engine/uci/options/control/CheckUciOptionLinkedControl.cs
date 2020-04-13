namespace chess_pos_db_gui
{
    public class CheckUciOptionLinkedControl : UciOptionLinkedControl
    {
        private CheckUciOption Option { get; set; }

        private System.Windows.Forms.CheckBox Control => (System.Windows.Forms.CheckBox)LinkedControl.Control;


        public CheckUciOptionLinkedControl(CheckUciOption opt) :
            base(opt)
        {
            Option = opt;
            ResetControlValue();
        }

        public override void ResetControlValue()
        {
            Control.Checked = Option.Value;
        }

        public override void UpdateLinkedOptionValue()
        {
            Option.SetValue(Control.Checked);
        }
    }
}
