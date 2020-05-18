using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    public class ToolkitCreatorSettings : ModSettings
    {
        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            if (listing.ButtonText("Events"))
            {
                EventsWindow window = new EventsWindow();
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            }

            if (listing.ButtonText("DevWindow"))
            {
                DevWindow window = new DevWindow();
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            }

            listing.End();
        }

        public override void ExposeData()
        {
        }
    }
}