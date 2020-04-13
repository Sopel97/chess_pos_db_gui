namespace chess_pos_db_gui
{
    public abstract class UciOptionLinkedControl
    {
        public UciOptionControlPanel LinkedControl { get; private set; }

        protected UciOptionLinkedControl(UciOption opt)
        {
            LinkedControl = opt.CreateControlPanel();
        }

        public System.Windows.Forms.Control GetControl()
        {
            return LinkedControl.Control;
        }

        public System.Windows.Forms.Panel GetPanel()
        {
            return LinkedControl.Panel;
        }

        public abstract void ResetControlValue();

        public abstract void UpdateLinkedOptionValue();
    }
}
