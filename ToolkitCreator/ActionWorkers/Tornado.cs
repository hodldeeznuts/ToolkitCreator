using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ToolkitCreator.ActionWorkers
{
    public class Tornado : ActionWorker
    {
        public override void Execute(string displayName)
        {
            CellRect cellRect = CellRect.WholeMap(Find.CurrentMap).ContractedBy(30);
            if (cellRect.IsEmpty)
            {
                cellRect = CellRect.WholeMap(Find.CurrentMap);
            }

			if (!CellFinder.TryFindRandomCellInsideWith(cellRect, (IntVec3 x) => CanSpawnTornadoAt(x, Find.CurrentMap), out IntVec3 location))
			{
				Log.Error("Cannot find location to spawn tornado");
				return;
			}
			RimWorld.Tornado tornado = (RimWorld.Tornado)GenSpawn.Spawn(ThingDefOf.Tornado, location, Find.CurrentMap, WipeMode.Vanish);
			Find.LetterStack.ReceiveLetter("Tornado", "A mobile, destructive vortext of violently rotating winds have appeared. Seek shelter or flee!", LetterDefOf.ThreatBig);
        }

		protected bool CanSpawnTornadoAt(IntVec3 c, Map map)
		{
			if (c.Fogged(map))
			{
				return false;
			}
			int num = GenRadial.NumCellsInRadius(7f);
			for (int i = 0; i < num; i++)
			{
				IntVec3 c2 = c + GenRadial.RadialPattern[i];
				if (c2.InBounds(map))
				{
					if (this.AnyPawnOfPlayerFactionAt(c2, map))
					{
						return false;
					}
					RoofDef roofDef = map.roofGrid.RoofAt(c2);
					if (roofDef != null && roofDef.isThickRoof)
					{
						return false;
					}
				}
			}
			return true;
		}

		protected bool AnyPawnOfPlayerFactionAt(IntVec3 c, Map map)
		{
			List<Thing> thingList = c.GetThingList(map);
			for (int i = 0; i < thingList.Count; i++)
			{
				Pawn pawn = thingList[i] as Pawn;
				if (pawn != null && pawn.Faction == Faction.OfPlayer)
				{
					return true;
				}
			}
			return false;
		}
	}
}
