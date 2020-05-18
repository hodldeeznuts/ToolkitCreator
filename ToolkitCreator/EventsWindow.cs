using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    public class EventsWindow : Window
    {
        public EventsWindow()
        {
            this.doCloseButton = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Rect createButton = new Rect(0, 0, 200, 24);

            if (Widgets.ButtonText(createButton, "New Event"))
            {
                EventEditorWindow window = new EventEditorWindow(new Event());
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            }

            Rect eventScroll = new Rect(0, 30, inRect.width, inRect.height - 80f);
            DrawEventScrollMenu(eventScroll);
        }

        void DrawEventScrollMenu(Rect rect)
        {
            Widgets.DrawMenuSection(rect);
            rect = rect.ContractedBy(17f);

            GUI.BeginGroup(rect);

            Rect rect2 = rect.AtZero();
            Rect outRect = rect2;
            //outRect.yMax -= 65f;
            if (Events.All.Count > 0)
            {
                float height = Events.All.Count * 24f;
                float num = 0f;
                Rect viewRect = new Rect(0f, 0f, outRect.width - 16f, height);
                Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect, true);
                float num2 = this.scrollPosition.y - 24f;
                float num3 = this.scrollPosition.y + outRect.height;

                for (int i = 0; i < Events.All.Count; i++)
                {
                    if (num > num2 && num < num3)
                    {
                        DoRow(new Rect(0f, num, viewRect.width, 24f), Events.All[i]);
                    }
                    num += 24f;
                }
                Widgets.EndScrollView();
            }
            else
            {
                Widgets.NoneLabel(0f, outRect.width, null);
            }
            GUI.EndGroup();
        }

        void DoRow(Rect rect, Event evt)
        {
            Widgets.DrawHighlightIfMouseover(rect);
            GUI.BeginGroup(rect);

            Rect label = new Rect(4f, 0f, 120f, 24f);
            Widgets.Label(label, evt.label);

            Rect edit = new Rect(label);
            edit.x += label.width + WidgetRow.DefaultGap;

            if (Widgets.ButtonText(edit, "Edit"))
            {
                EventEditorWindow window = new EventEditorWindow(evt);
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            }

            Rect delete = new Rect(edit);
            delete.x += edit.width + WidgetRow.DefaultGap;

            if (Widgets.ButtonText(delete, "Delete"))
            {
                Events.All = Events.All.Where((x) => x != evt).ToList();
            }

            Rect fire = new Rect(delete);
            fire.x += delete.width + WidgetRow.DefaultGap;

            if (Widgets.ButtonText(fire, "Fire"))
            {
                evt.FireEvents("playerName");
            }

            GUI.EndGroup();
        }

        public override void PostClose()
        {
            SaveConfig.SaveAll();
        }

        public override Vector2 InitialSize => new Vector2(600, 500);

        Vector2 scrollPosition;
    }
}