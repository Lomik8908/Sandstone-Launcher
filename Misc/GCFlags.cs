using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace Sandstone_Launcher
{
    public class GCFlags
    {
        static public BindingList<GCTemplate> GCTemplates = new BindingList<GCTemplate> {
            new GCTemplate { id = "g1gc", name = "Default G1 GC", arguments = new List<string> { "-XX:+AlwaysPreTouch", "-XX:+UseStringDeduplication", "-XX:+UnlockExperimentalVMOptions", "-XX:+UseG1GC", "-XX:G1NewSizePercent=20", "-XX:G1ReservePercent=20", "-XX:MaxGCPauseMillis=50", "-XX:G1HeapRegionSize=32M" } },
            new GCTemplate { id = "m_g1gc", name = "Modified G1 GC", arguments = new List<string> { "-XX:+AlwaysPreTouch", "-XX:+UseStringDeduplication", "-XX:+UnlockExperimentalVMOptions", "-XX:+UseG1GC", "-XX:G1NewSizePercent=30", "-XX:G1ReservePercent=20", "-XX:MaxGCPauseMillis=50", "-XX:G1HeapRegionSize=16M", "-XX:+ParallelRefProcEnabled", "-XX:+DisableExplicitGC", "-XX:G1MaxNewSizePercent=40" } },
            new GCTemplate { id = "s_gc", name = "Shenandoah GC", arguments = new List<string> { "-XX:+AlwaysPreTouch", "-XX:+UseShenandoahGC", "-XX:ShenandoahGCMode=iu" } },
            new GCTemplate { id = "zgc", name = "ZGC", arguments = new List<string> { "-XX:+AlwaysPreTouch", "-XX:+UseStringDeduplication",  "-XX:+UseZGC" } },
            new GCTemplate { id = "cms", name = "Concurrent Mark Sweep GC", arguments = new List<string> { "-XX:+AlwaysPreTouch", "-XX:+UseConcMarkSweepGC", "-XX:+CMSParallelRemarkEnabled", "-XX:+CMSClassUnloadingEnabled", "-XX:+UseCMSInitiatingOccupancyOnly" } }
        };

        static public void LoadCustomFlags()
        {
            if (Directory.Exists("GCFlags"))
                foreach (var GCPath in Directory.GetFiles("GCFlags"))
                    if (Path.GetExtension(GCPath).ToLowerInvariant() == ".json") try
                        {
                            GCTemplate gctemp = JsonSerializer.Deserialize<GCTemplate>(File.ReadAllText(GCPath), Program.defaultJsonOptions);
                            GCTemplates.Add(gctemp);
                        }
                        catch (Exception ex) { Logger.Warn($"Couldn't load GCFlags {Path.GetFileName(GCPath)}: {ex.Message}"); }
        }
    }
    public class GCTemplate
    {
        public List<string> arguments { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
}
