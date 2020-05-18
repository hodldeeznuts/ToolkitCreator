using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitCore;
using TwitchLib.Client.Models.Interfaces;
using Verse;

namespace ToolkitCreator
{
    public class ChatInterface : TwitchInterfaceBase
    {
        public ChatInterface(Game game)
        {

        }

        public override void ParseMessage(ITwitchMessage twitchMessage)
        {
            if (twitchMessage.ChatMessage == null)
            {
                return;
            }

            if (twitchMessage.ChatMessage.Bits > 0)
            {
                TriggerController.CheckForBitEvents(twitchMessage.ChatMessage.Bits, twitchMessage.ChatMessage.DisplayName);
            }
        }
    }
}
