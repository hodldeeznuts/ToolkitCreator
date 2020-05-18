using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitCore.Database;
using ToolkitCreator.SimpleJSON;
using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    [StaticConstructorOnStartup]
    public static class SaveConfig
    {
        static SaveConfig()
        {
            LoadEvents();
        }

        public static void SaveAll()
        {
            SaveEvents();
        }

        public static void LoadAll()
        {
            LoadEvents();
        }

        static void SaveEvents()
        {
            var eventListTemplate = JSON.Parse("{\"Events\":[], \"total\": 0}");
            foreach (Event evt in Events.All)
            {
                var json = JSON.Parse(JsonUtility.ToJson(evt));
                eventListTemplate["Events"].Add(json);
            }

            eventListTemplate["total"] = Events.All.Count;

            Log.Message(eventListTemplate.ToString());

            DatabaseController.SaveFile(eventListTemplate.ToString(), "Events.json");
        }

        static void LoadEvents()
        {
            if (!DatabaseController.LoadFile("Events.json", out string json))
            {
                Log.Message("No Events.json file to load");
                return;
            }

            var events = JSON.Parse(json);

            List<Event> listOfEvents = new List<Event>();

            for (int i = 0; i < events["total"]; i++)
            {
                listOfEvents.Add(JsonUtility.FromJson<Event>(events["Events"][i].ToString()));
            }

            Events.All = listOfEvents;
        }
    }
}
