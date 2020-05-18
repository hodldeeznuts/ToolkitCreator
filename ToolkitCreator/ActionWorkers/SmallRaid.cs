using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ToolkitCreator.ActionWorkers
{
    public class SmallRaid : ActionWorker
    {
        public override void Execute(string displayName)
        {
            Map map = Find.CurrentMap;

            IncidentDefOf.RaidEnemy.Worker.TryExecute(StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatSmall, map));
        }
    }
}
