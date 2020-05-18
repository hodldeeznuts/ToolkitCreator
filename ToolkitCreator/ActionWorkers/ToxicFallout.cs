using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ToolkitCreator.ActionWorkers
{
    public class ToxicFallout : ActionWorker
    {
        public override void Execute(string displayName)
        {
            IncidentDefOf.ToxicFallout.Worker.TryExecute(StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.Misc, Find.CurrentMap));
        }
    }
}
