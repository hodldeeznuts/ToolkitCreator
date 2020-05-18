using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Models;
using TwitchLib.Client.Models.Internal;
using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    public class DevWindow : Window
    {
        public DevWindow()
        {
            this.doCloseButton = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Rect bitMessage = new Rect(0, 0, inRect.width, 24f);
            GUI.BeginGroup(bitMessage);

            Rect label = new Rect(0, 0, 200f, 24f);
            Widgets.Label(label, "ChatMessage w/ Bits:");

            Rect bitInput = new Rect(label.width + WidgetRow.DefaultGap, 0, 200f, 24f);
            string bitBuffer = bits.ToString();
            Widgets.TextFieldNumeric<int>(bitInput, ref bits, ref bitBuffer);

            Rect submitChatMessage = new Rect(bitInput.x + bitInput.width + WidgetRow.DefaultGap, 0, 100f, 24f);
            if (Widgets.ButtonText(submitChatMessage, "Submit"))
            {
                TriggerController.CheckForBitEvents(bits, "hodlhodl");
            }

            GUI.EndGroup();
        }

        public override Vector2 InitialSize => new Vector2(600, 500);

        static int bits = 0;
    }
}
