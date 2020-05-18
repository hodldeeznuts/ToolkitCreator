using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitCreator.ActionWorkers
{
    public abstract class ActionWorker
    {
        public abstract void Execute(string displayName);
    }
}
