using System;
using System.Collections.Generic;
using System.Linq;
using ToolkitCreator.ActionWorkers;
using TwitchLib.Client.Models;
using Verse;

namespace ToolkitCreator
{
    public static class Events
    {
        public static List<Event> All { get; set; } = new List<Event>();
    }

    [Serializable]
    public class Event : Def
    {
        // bits
        public bool bitTrigger = false;

        public bool exactBitAmount = true;

        public int bitsToTrigger = 100;

        public int bitsMinimum = 100;
        public int bitsMaximum = 499;

        public bool bitsInfinite = false;

        // subs
        public bool subTrigger = false;

        public bool giftSubs = true;

        public bool reSubs = true;

        public bool firstTimeSubs = true;

        public bool communitySubs = false;

        public bool tierOneSubs = true;
        public bool tierTwoSubs = false;
        public bool tierThreeSubs = false;

        // tips
        public bool tipTrigger = false;

        public bool exactTipAmount = true;

        public double tipToTrigger = 1.00d;

        public double tipMinimum = 1.00d;
        public double tipMaximum = 1.00d;

        public bool tipInfinite = false;

        public List<string> actionDefs = new List<string>();

        public Event()
        {
            CreateDefaultName();

            if (!Events.All.Contains(this))
            {
                Events.All.Add(this);
            }
        }

        private void CreateDefaultName(int id = 1)
        {
            if (Events.All.Where((evt) => evt.defName == $"NewEvent{id}").FirstOrDefault() != null)
            {
                CreateDefaultName(id + 1);
            }
            else
            {
                defName = $"NewEvent{id}";
                label = defName;
            }
        }

        public void FireEvents(string displayName = "")
        {
            if (actionDefs.Count < 1)
            {
                Log.Error("Event triggered but no action is set");
            }

            foreach (string actionDefName in actionDefs)
            {
                Action action = DefDatabase<Action>.GetNamed(actionDefName);
                if (action == null)
                {
                    Log.Error($"Action {actionDefName} for Event {this.label} experienced an error, please report to dev");
                    return;
                }

                ((ActionWorker)Activator.CreateInstance(action.actionWorker)).Execute(displayName);
            }
        }
    }
}