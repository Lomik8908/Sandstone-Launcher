using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;

namespace Sandstone_Launcher
{
    static class Logger
    {
        public static void Log(object Value)
        {
            Console.ResetColor();
            Console.Out.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}]: {Value?.ToString()}");
        }
        public static void Err(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}] [ERR]: {Value?.ToString()}");
        }
        public static void Warn(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Out.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}] [WRN]: {Value?.ToString()}");
        }
        public static void ErrorLine(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLineAsync(Value?.ToString());
        }
    }
    static class Conhost {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        static public void ShowConsole() {
            AllocConsole();
            Console.Title = "Sandstone Console";
            var hWnd = GetConsoleWindow();
            var hMenu = GetSystemMenu(hWnd, false);
            DeleteMenu(hMenu, 0xF060, 0x00000000);
            using (var proc = Process.GetCurrentProcess())
                AttachConsole((uint)proc.Id);
        }
        static public void HideConsole() => FreeConsole();
    }
}
