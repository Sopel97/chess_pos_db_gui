using System;
using System.Reflection;

namespace chess_pos_db_gui.src.util
{
    static class WinFormsControlUtil
    {
        public static void MakeDoubleBuffered(System.Windows.Forms.Control control)
        {
            Type dgvType = control.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(control, true, null);
        }
    }
}
