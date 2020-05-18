using System;
using ToolkitCreator.ActionWorkers;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace ToolkitCreator
{
    public class Action : Def
    {
        public Type actionWorker = typeof(ActionWorker);
    }
}