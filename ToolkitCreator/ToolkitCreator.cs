using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    public class ToolkitCreator : Mod
    {
        public ToolkitCreator(ModContentPack content) : base(content)
        {
        }

        public override string SettingsCategory() => "ToolkitCreator";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            GetSettings<ToolkitCreatorSettings>().DoWindowContents(inRect);
        }
    }
}