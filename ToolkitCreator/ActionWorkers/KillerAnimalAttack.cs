using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ToolkitCreator.ActionWorkers
{
    public class KillerAnimalAttack : ActionWorker
    {
        public override void Execute(string displayName)
        {
            Map map = Find.CurrentMap;

            if (map == null)
            {
                Log.Error("Could not find map for manhunter pack");
                return;
            }

            IncidentDef incident = IncidentDefOf.ManhunterPack;
            IncidentParms parms = StorytellerUtility.DefaultParmsNow(incident.category, map);
            incident.Worker.TryExecute(parms);
        }
    }
}
