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

        static public string ReplaceFormat(string format, params object[] strings)
        {
            string formatted = format;
            int idx = 0;
            foreach (object obj in strings)
            {
                formatted = formatted.Replace($"{{{idx}}}", obj.ToString());
                idx++;
            }
            return formatted;
        }
    }
}
