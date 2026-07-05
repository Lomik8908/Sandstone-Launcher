using System.Windows.Forms;

namespace Sandstone_Launcher
{
    internal class SharedMethods
    {
        static public void HandleScroll(object sender, MouseEventArgs e)
        {
            if (!(sender is ComboBox box) || !box.DroppedDown)
                ((HandledMouseEventArgs)e).Handled = true;
        }
    }
}
