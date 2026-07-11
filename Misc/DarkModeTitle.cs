using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sandstone_Launcher
{
    internal static class DarkModeTitle
    {
        [DllImport("dwmapi")]
        static private extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

        static public bool SetDarkMode(IntPtr Handle, bool Enabled)
        {
            int val = Enabled ? 1 : 0;
            if (Is10AndBuild(18985))
                return DwmSetWindowAttribute(Handle, 20, ref val, sizeof(int)) == 0;
            else if (Is10AndBuild(17763))
                return DwmSetWindowAttribute(Handle, 19, ref val, sizeof(int)) == 0;
            return false;
        }

        static private bool Is10AndBuild(int build)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}
