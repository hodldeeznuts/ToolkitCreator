using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using Verse;

namespace ToolkitCreator
{
    public static class TriggerController
    {
        public static void CheckForBitEvents(int bits, string displayName)
        {
            List<Event> eventsWithBitTriggers = Events.All.Where((x) => x.bitTrigger).ToList();

            List<Event> eventsToTrigger = new List<Event>();

            foreach (Event evt in eventsWithBitTriggers)
            {
                if (evt.exactBitAmount && bits == evt.bitsToTrigger)
                {
                    eventsToTrigger.Add(evt);
                }
                else if (bits >= evt.bitsMinimum && (evt.bitsInfinite || bits <= evt.bitsMaximum))
                {
                    eventsToTrigger.Add(evt);
                }
            }

            if (eventsToTrigger.Count > 0)
            {
                foreach (Event evt in eventsToTrigger)
                {
                    evt.FireEvents(displayName);
                }
            }
        }

        public static void CheckForCommunitySubEvents(object sender, OnCommunitySubscriptionArgs e)
        {
            CommunitySubscription g = e.GiftedSubscription;
            Log.Message($"CommunitySubscription = Anonymous: {g.IsAnonymous} - MassGiftCount: {g.MsgParamMassGiftCount} - SenderCounter: {g.MsgParamSenderCount} SubPlan: {g.MsgParamSubPlan}");

            List<Event> eventsWithCommunitySubTriggers = Events.All.Where((x) => x.communitySubs).ToList();

            foreach (Event evt in eventsWithCommunitySubTriggers)
            {
                bool fire = false;

                if (e.GiftedSubscription.MsgParamSubPlan.ToString() == "Tier1" && evt.tierOneSubs)
                {
                    fire = true;
                }
                else if (e.GiftedSubscription.MsgParamSubPlan.ToString() == "Tier2" && evt.tierTwoSubs)
                {
                    fire = true;
                }
                else if (e.GiftedSubscription.MsgParamSubPlan.ToString() == "Tier3" && evt.tierThreeSubs)
                {
                    fire = true;
                }

                if (fire)
                {
                    evt.FireEvents(e.GiftedSubscription.DisplayName);
                }
            }
        }

        public static void CheckForGiftedSubEvents(object sender, OnGiftedSubscriptionArgs e)
        {
            GiftedSubscription g = e.GiftedSubscription;
            Log.Message($"GiftedSubscription = Anonymous: {g.IsAnonymous} - Months: {g.MsgParamMonths} - Recipient: {g.MsgParamRecipientDisplayName} SubPlan: {g.MsgParamSubPlan} - SubPlanName: {g.MsgParamSubPlanName}");

            List<Event> eventsWithGiftedSubTriggers = Events.All.Where((x) => x.giftSubs).ToList();

            foreach (Event evt in eventsWithGiftedSubTriggers)
            {
                bool fire = false;

                if (e.GiftedSubscription.MsgParamSubPlan.ToString() == "Tier1" && evt.tierOneSubs)
                {
                    fire = true;
                }
                else if (e.GiftedSubscription.MsgParamSubPlan.ToString() == "Tier2" && evt.tierTwoSubs)
                {
                    fire = true;
                }
                else if (e.GiftedSubscription.MsgParamSubPlan.ToString() == "Tier3" && evt.tierThreeSubs)
                {
                    fire = true;
                }

                if (fire)
                {
                    evt.FireEvents(e.GiftedSubscription.DisplayName);
                }
            }
        }

        public static void CheckForNewSubscriberEvents(object sender, OnNewSubscriberArgs e)
        {
            Subscriber g = e.Subscriber;
            Log.Message($"NewSubscriber = CumulativeMonths - {g.MsgParamCumulativeMonths} - ShareStreak: {g.MsgParamShouldShareStreak} - StreakMonths: {g.MsgParamStreakMonths} - ResubMessage: {g.ResubMessage} - SubPlan: {g.SubscriptionPlan} - PlanName: {g.SubscriptionPlanName}");

            List<Event> eventsWithNewSubscriberTriggers = Events.All.Where((x) => x.firstTimeSubs).ToList();

            foreach (Event evt in eventsWithNewSubscriberTriggers)
            {
                bool fire = false;

                if (e.Subscriber.SubscriptionPlan.ToString() == "Tier1" && evt.tierOneSubs)
                {
                    fire = true;
                }
                else if (e.Subscriber.SubscriptionPlan.ToString() == "Tier2" && evt.tierTwoSubs)
                {
                    fire = true;
                }
                else if (e.Subscriber.SubscriptionPlan.ToString() == "Tier3" && evt.tierThreeSubs)
                {
                    fire = true;
                }

                if (fire)
                {
                    evt.FireEvents(e.Subscriber.DisplayName);
                }
            }
        }

        public static void CheckForReSubscriberEvents(object sender, OnReSubscriberArgs e)
        {
            ReSubscriber g = e.ReSubscriber;
            Log.Message($"Resubscriber = CumulativeMonths - {g.MsgParamCumulativeMonths} - ShareStreak: {g.MsgParamShouldShareStreak} - StreakMonths: {g.MsgParamStreakMonths} - ResubMessage: {g.ResubMessage} - SubPlan: {g.SubscriptionPlan} - PlanName: {g.SubscriptionPlanName}");

            List<Event> eventsWithResubscriberTriggers = new List<Event>();

            foreach (Event evt in eventsWithResubscriberTriggers)
            {
                bool fire = false;

                if (e.ReSubscriber.SubscriptionPlan.ToString() == "Tier1" && evt.tierOneSubs)
                {
                    fire = true;
                }
                else if (e.ReSubscriber.SubscriptionPlan.ToString() == "Tier2" && evt.tierTwoSubs)
                {
                    fire = true;
                }
                else if (e.ReSubscriber.SubscriptionPlan.ToString() == "Tier3" && evt.tierThreeSubs)
                {
                    fire = true;
                }

                if (fire)
                {
                    evt.FireEvents(e.ReSubscriber.DisplayName);
                }
            }
        }
    }
}
