using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitCore.Interfaces;
using ToolkitCore.Windows;
using Verse;

namespace ToolkitCreator
{
    public class AddonMenu : IAddonMenu
    {
        List<FloatMenuOption> IAddonMenu.MenuOptions() => new List<FloatMenuOption>
        {
            new FloatMenuOption("Settings", delegate ()
            {
                Window_ModSettings window = new Window_ModSettings(LoadedModManager.GetMod<ToolkitCreator>());
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            }),
            new FloatMenuOption("Events", delegate ()
            {
                EventsWindow window = new EventsWindow();
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            })
        };
    }
}
