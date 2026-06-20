using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Sandstone_Launcher
{
    static public class Backgrounds
    {
        static public BindingList<Background> AllBackgrounds = new BindingList<Background>
        {
            new Background { Name = "None", Image = null },
            new Background { Name = "Burberry", Image = new Lazy<Image>(() => Misc.Backgrounds.burberry, true) },
            new Background { Name = "Cave", Image = new Lazy<Image>(() => Misc.Backgrounds.cave, true) }, // 120mb spike because of them :_<
            new Background { Name = "Playstation", Image = new Lazy<Image>(() => Misc.Backgrounds.playstation, true) },
            new Background { Name = "Monument", Image = new Lazy<Image>(() => Misc.Backgrounds.monument, true) },
            new Background { Name = "Island", Image = new Lazy<Image>(() => Misc.Backgrounds.island, true) },
            new Background { Name = "Aquatic", Image = new Lazy<Image>(() => Misc.Backgrounds.aquatic, true) },
            new Background { Name = "Anniversary", Image = new Lazy<Image>(() => Misc.Backgrounds.anniversary, true) }
        };
        static public readonly HashSet<string> AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".bmp", ".dib", ".gif", ".jpg", ".jpeg", ".jpe", ".jfif", ".jfi", ".jif", ".png", ".tiff" };
        static public readonly string AllowedExtString = "All image files|*.bmp;*.dib;*.gif;*.jpg;*.jpeg;*.jpe;*.jfif;*.jfi;*.jif;*.png;*.tiff|PNG Images (.png)|*.png|JPEG Images (.jpg, .jpeg, .jpe, .jfif, .jfi, .jif)|*.jpg;*.jpeg;*.jpe;*.jfif;*.jfi;*.jif|TIFF Images (.tiff)|*.tiff|GIF Images (.gif)|*.gif|Bitmap Images (.bmp, .dib)|*.bmp;*.dib";
        static public void LoadBackgrounds()
        {
            if (Directory.Exists("Backgrounds"))
                foreach (var File in Directory.GetFiles("Backgrounds"))
                    if (AllowedExtensions.Contains(Path.GetExtension(File))) try
                        {
                            FileInfo info = new FileInfo(File);
                            if (!AllBackgrounds.Any(v => v.Name == info.Name))
                            {
                                Lazy<Image> bitmap = new Lazy<Image>(() => { try { return Image.FromFile(info.FullName); } catch { return null; } }, true);
                                AllBackgrounds.Add(new Background
                                {
                                    Name = info.Name,
                                    Image = bitmap
                                });
                            }
                        }
                        catch (Exception ex) { Logger.Log($"Couldn't load backgound {Path.GetFileName(File)}: {ex.Message}"); }
        }
    }
    public class Background
    {
        public string Name { get; set; }
        public Lazy<Image> Image { get; set; }
    }
}
