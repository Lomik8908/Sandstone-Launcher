using System.Collections.Generic;
using System.Text;

namespace Sandstone_Launcher
{
    static class ArgSplitter
    {
        static public List<string> Split(string Text)
        {
            if (string.IsNullOrWhiteSpace(Text)) return new List<string>();
            bool Quotes = false;
            List<string> Final = new List<string>();
            StringBuilder Build = new StringBuilder();
            foreach (char Ch in Text)
            {
                if (Ch == '"')
                {
                    Quotes = !Quotes;
                }
                else if (Ch == ' ' && !Quotes)
                {
                    if (Build.Length > 0)
                    {
                        Final.Add(Build.ToString());
                        Build.Clear();
                    }
                }
                else
                {
                    Build.Append(Ch);
                }
            }
            if (Build.Length > 0)
                Final.Add(Build.ToString());
            return Final;
        }
    }
}
