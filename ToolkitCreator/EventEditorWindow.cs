using RimWorld;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitCore.Utilities;
using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    public class EventEditorWindow : Window
    {
        public EventEditorWindow(Event evt)
        {
            Event = evt;
            this.doCloseButton = true;
            CacheActionDefLabels();
        }

        public override Vector2 InitialSize => new Vector2(1000, 800);

        public override void DoWindowContents(Rect inRect)
        {
            float columnOneX = 0f;
            float columnTwoX = inRect.width / 2f;

            float columnWidth = inRect.width / 2f;

            Rect eventName = new Rect(0, 0, 300, 24);

            Widgets.Label(eventName, TCText.ColoredText(Event.label, ColorLibrary.BrightPurple));
            Event.label = Widgets.TextField(new Rect(324, 0, 400, 24), Event.label);

            Widgets.DrawLineHorizontal(0, 32, inRect.width);

            // Triggers

            Rect triggerHeader = new Rect(columnOneX, 48f, 200f, 24f);
            Widgets.Label(triggerHeader, "<b>Triggers</b>");

            Rect triggerLabels = new Rect(columnOneX, 48f + 24f + (verticalSpacing * 2), columnWidth / 2f, 24f);
            Rect triggerValues = new Rect(columnWidth / 2f, triggerLabels.y, triggerLabels.width, 24f);

            Widgets.Label(triggerLabels, "Triggered by Bits:");
            Widgets.Checkbox(triggerValues.position, ref Event.bitTrigger);

            if (Event.bitTrigger)
            {
                triggerLabels.y += triggerLabels.height + verticalSpacing;
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Exact Amount");
                Widgets.Checkbox(triggerValues.position, ref Event.exactBitAmount);

                triggerLabels.y += triggerLabels.height + verticalSpacing;
                triggerValues.y = triggerLabels.y;

                if (Event.exactBitAmount)
                {
                    Widgets.Label(triggerLabels, "Bits to Trigger");
                    string exactBitsBuffer = Event.bitsToTrigger.ToString();
                    Widgets.IntEntry(triggerValues, ref Event.bitsToTrigger, ref exactBitsBuffer, 10);
                }
                else
                {
                    Widgets.Label(triggerLabels, "Minimum Bits to Trigger");
                    string minimumBitsBuffer = Event.bitsMinimum.ToString();
                    Widgets.IntEntry(triggerValues, ref Event.bitsMinimum, ref minimumBitsBuffer, 10);

                    triggerLabels.y += triggerLabels.height + verticalSpacing;
                    triggerValues.y = triggerLabels.y;

                    Widgets.Label(triggerLabels, "Infinite Maximum Bits");
                    Widgets.Checkbox(triggerValues.position, ref Event.bitsInfinite);

                    if (!Event.bitsInfinite)
                    {
                        triggerLabels.y += triggerLabels.height + verticalSpacing;
                        triggerValues.y = triggerLabels.y;

                        Widgets.Label(triggerLabels, "Maximum Bits to Trigger");
                        string maximumBitsBuffer = Event.bitsMaximum.ToString();
                        Widgets.IntEntry(triggerValues, ref Event.bitsMaximum, ref maximumBitsBuffer, 10);
                    }
                }
            }

            triggerLabels.y += triggerLabels.height + (verticalSpacing * 4);
            triggerValues.y = triggerLabels.y;

            Widgets.Label(triggerLabels, "Triggered by Donations:");
            Widgets.Checkbox(triggerValues.position, ref Event.tipTrigger);

            if (Event.tipTrigger)
            {
                triggerLabels.y += triggerLabels.height + verticalSpacing;
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Exact Amount");
                Widgets.Checkbox(triggerValues.position, ref Event.exactTipAmount);

                triggerLabels.y += triggerLabels.height + verticalSpacing;
                triggerValues.y = triggerLabels.y;

                if (Event.exactTipAmount)
                {
                    Widgets.Label(triggerLabels, "Donation to Trigger");

                    string exactTipBuffer = Event.tipToTrigger.ToString("C", CultureInfo.CurrentCulture);
                    WidgetExtensions.TextFieldNumericDouble(triggerValues, ref Event.tipToTrigger, ref exactTipBuffer);
                }
                else
                {
                    Widgets.Label(triggerLabels, "Minimum Donation to Trigger");
                    string minimumTipBuffer = Event.tipMinimum.ToString("C", CultureInfo.CurrentCulture);
                    WidgetExtensions.TextFieldNumericDouble(triggerValues, ref Event.tipMinimum, ref minimumTipBuffer);

                    triggerLabels.y += triggerLabels.height + verticalSpacing;
                    triggerValues.y = triggerLabels.y;

                    Widgets.Label(triggerLabels, "Infinite Maximum Donation");
                    Widgets.Checkbox(triggerValues.position, ref Event.tipInfinite);

                    if (!Event.tipInfinite)
                    {
                        triggerLabels.y += triggerLabels.height + verticalSpacing;
                        triggerValues.y = triggerLabels.y;

                        Widgets.Label(triggerLabels, "Maximum Donation to Trigger");
                        string maximumTipBuffer = Event.tipMaximum.ToString("C", CultureInfo.CurrentCulture);
                        WidgetExtensions.TextFieldNumericDouble(triggerValues, ref Event.tipMaximum, ref maximumTipBuffer);
                    }
                }
            }

            triggerLabels.y += triggerLabels.height + (verticalSpacing * 4);
            triggerValues.y = triggerLabels.y;

            Widgets.Label(triggerLabels, "Triggered by Subs:");
            Widgets.Checkbox(triggerValues.position, ref Event.subTrigger);

            if (Event.subTrigger)
            {
                triggerLabels.y += triggerLabels.height + (verticalSpacing);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "First Time Subs");
                Widgets.Checkbox(triggerValues.position, ref Event.firstTimeSubs);

                triggerLabels.y += triggerLabels.height + (verticalSpacing);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Resubs");
                Widgets.Checkbox(triggerValues.position, ref Event.reSubs);

                triggerLabels.y += triggerLabels.height + (verticalSpacing);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Gift Subs");
                Widgets.Checkbox(triggerValues.position, ref Event.giftSubs);

                triggerLabels.y += triggerLabels.height + (verticalSpacing * 2);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Community Subs");
                Widgets.Checkbox(triggerValues.position, ref Event.communitySubs);

                triggerLabels.y += triggerLabels.height + (verticalSpacing * 2);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Tier One Subs");
                Widgets.Checkbox(triggerValues.position, ref Event.tierOneSubs);

                triggerLabels.y += triggerLabels.height + (verticalSpacing);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Tier Two Subs");
                Widgets.Checkbox(triggerValues.position, ref Event.tierTwoSubs);

                triggerLabels.y += triggerLabels.height + (verticalSpacing);
                triggerValues.y = triggerLabels.y;

                Widgets.Label(triggerLabels, "Tier Three Subs");
                Widgets.Checkbox(triggerValues.position, ref Event.tierThreeSubs);
            }

            triggerLabels.y += triggerLabels.height + (verticalSpacing * 4);
            triggerValues.y = triggerLabels.y;

            // Actions

            Rect actionHeader = new Rect(columnTwoX, 48f, 400f + WidgetRow.DefaultGap, 24f);

            GUI.BeginGroup(actionHeader);

            Rect actionHeaderLabel = new Rect(0f, 0f, 200f, 24f);
            Widgets.Label(actionHeaderLabel, "<b>Actions</b>");

            Rect addActionButton = new Rect(actionHeaderLabel.width + WidgetRow.DefaultGap, 0f, 200f, 24f);
            if (Widgets.ButtonText(addActionButton, "Add Action"))
            {
                List<FloatMenuOption> menuOptions = FloatMenuOptions();

                if (menuOptions.Count < 1)
                {
                    return;
                }

                Find.WindowStack.Add(new FloatMenu(FloatMenuOptions()));
            }
            
            GUI.EndGroup();

            Rect actionList = new Rect(columnTwoX, actionHeader.y + actionHeader.height + 8f, columnWidth, 24f);

            foreach (string actionDefLabel in actionDefLabels)
            {
                GUI.BeginGroup(actionList);
                Rect label = new Rect(0f, 0f, 200f, 24f);
                Widgets.Label(label, actionDefLabel);

                Rect remove = new Rect(label.x + label.width + WidgetRow.DefaultGap, 0, 200f, 24f);
                if (Widgets.ButtonText(remove, "Remove"))
                {
                    Action actionToRemove = DefDatabase<Action>.AllDefs.Where((x) => x.LabelCap == actionDefLabel).FirstOrDefault();

                    if (actionToRemove == null)
                    {
                        Log.Error($"Tried to remove {actionDefLabel} but it was null form DefDatabase");
                    }

                    Event.actionDefs = Event.actionDefs.Where((x) => x != actionToRemove.defName).ToList();

                    //Event.actionDefs = Event.actionDefs.Where((x) => x != DefDatabase<Action>.AllDefs.Where((y) => y.LabelCap == x).FirstOrDefault().defName).ToList();
                    CacheActionDefLabels();
                }
                GUI.EndGroup();
            }

        }

        List<FloatMenuOption> FloatMenuOptions()
        {
            List<FloatMenuOption> options = new List<FloatMenuOption>();

            List<Action> actions = DefDatabase<Action>.AllDefs.Where((x) => !Event.actionDefs.Contains(x.defName)).ToList();

            foreach (Action action in actions)
            {
                options.Add(new FloatMenuOption(action.LabelCap, delegate()
                {
                    Event.actionDefs.Add(action.defName);
                    CacheActionDefLabels();
                }));
            }

            return options;
        }

        void CacheActionDefLabels()
        {
            List<string> labels = new List<string>();

            foreach (string str in Event.actionDefs)
            {
                labels.Add(DefDatabase<Action>.GetNamed(str).LabelCap);
            }

            actionDefLabels = labels;
        }

        public override void PostClose()
        {
            SaveConfig.SaveAll();
        }
        private Event Event { get; set; }

        private float verticalSpacing = 8f;

        private List<string> actionDefLabels = new List<string>();
    }
}
