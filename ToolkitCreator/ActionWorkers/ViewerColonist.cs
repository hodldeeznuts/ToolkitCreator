using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitCreator.ActionWorkers;
using Verse;

namespace ToolkitCreator.ActionWorkers
{
    public class ViewerColonist : ActionWorker
    {
        public override void Execute(string displayName)
        {
            Map map = Find.CurrentMap;

            bool cell = CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => map.reachability.CanReachColony(c) && !c.Fogged(map), map, CellFinder.EdgeRoadChance_Neutral, out IntVec3 location);

            if (!cell)
            {
                Log.Error("Could not find cell for pawn to enter from");
                return;
            }

            PawnGenerationRequest pawnGeneration = new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, Faction.OfPlayer, PawnGenerationContext.NonPlayer, map.Tile, true, false, false, false, true, true, 1, true, true, true, false, false, false);
            Pawn pawn = PawnGenerator.GeneratePawn(pawnGeneration);
            NameTriple old = pawn.Name as NameTriple;
            pawn.Name = new NameTriple(old.First, displayName, old.Last);
            GenSpawn.Spawn(pawn, location, map, WipeMode.Vanish);


            TaggedString label = "Viewer Joins";
            TaggedString text = $"A new pawn named {displayName} has joined your colony.";
            PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref text, ref label, pawn);
            Find.LetterStack.ReceiveLetter(label, text, LetterDefOf.PositiveEvent, pawn, null, null);            
        }
    }
}
