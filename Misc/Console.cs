using System;

namespace Sandstone_Launcher
{
    static class Logger
    {
        public static void Log(object Value)
        {
            Console.ResetColor();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]: {Value?.ToString()}");
        }
        public static void Err(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERR] [{DateTime.Now:HH:mm:ss}]: {Value?.ToString()}");
        }
        public static void Warn(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WRN] [{DateTime.Now:HH:mm:ss}]: {Value?.ToString()}");
        }
    }
}
