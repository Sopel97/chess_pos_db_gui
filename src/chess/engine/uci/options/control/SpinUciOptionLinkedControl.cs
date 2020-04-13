namespace chess_pos_db_gui
{
    public class SpinUciOptionLinkedControl : UciOptionLinkedControl
    {
        private SpinUciOption Option { get; set; }
        private System.Windows.Forms.NumericUpDown Control => (System.Windows.Forms.NumericUpDown)LinkedControl.Control;


        public SpinUciOptionLinkedControl(SpinUciOption opt) :
            base(opt)
        {
            Option = opt;
            ResetControlValue();
        }

        public override void ResetControlValue()
        {
            Control.Value = Option.Value;
        }

        public override void UpdateLinkedOptionValue()
        {
            Option.SetValue((long)Control.Value);
        }
    }
}
