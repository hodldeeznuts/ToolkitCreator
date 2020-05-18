using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitCore;
using TwitchLib.Client.Events;
using Verse;

namespace ToolkitCreator
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("com.rimworld.mod.hodlhodl.toolkit.creator");

            Harmony.DEBUG = true;

            harmony.Patch(
                    original: AccessTools.Method(type: typeof(ToolkitCore.Database.DatabaseController), name: "SaveToolkit"),
                    postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(SaveGame_PostFix))
                );

            harmony.Patch(
                    original: AccessTools.Method(type: typeof(ToolkitCore.Database.DatabaseController), name: "LoadToolkit"),
                    postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(LoadGame_PostFix))
                );
            harmony.Patch(
                    original: AccessTools.Method(type: typeof(TwitchWrapper), name: nameof(TwitchWrapper.OnCommunitySubscription), new Type[] { typeof(object), typeof(OnCommunitySubscriptionArgs) }),
                    postfix: new HarmonyMethod(typeof(TriggerController), nameof(TriggerController.CheckForCommunitySubEvents))
                );
            harmony.Patch(
                original: AccessTools.Method(type: typeof(TwitchWrapper), name: nameof(TwitchWrapper.OnGiftedSubscription), new Type[] { typeof(object), typeof(OnGiftedSubscriptionArgs) }),
                postfix: new HarmonyMethod(typeof(TriggerController), nameof(TriggerController.CheckForGiftedSubEvents))
            );
            harmony.Patch(
                original: AccessTools.Method(type: typeof(TwitchWrapper), name: nameof(TwitchWrapper.OnNewSubscriber), new Type[] { typeof(object), typeof(OnNewSubscriberArgs) }),
                postfix: new HarmonyMethod(typeof(TriggerController), nameof(TriggerController.CheckForNewSubscriberEvents))
            );
            harmony.Patch(
                original: AccessTools.Method(type: typeof(TwitchWrapper), name: nameof(TwitchWrapper.OnReSubscriber), new Type[] { typeof(object), typeof(OnReSubscriberArgs) }),
                postfix: new HarmonyMethod(typeof(TriggerController), nameof(TriggerController.CheckForReSubscriberEvents))
            );
        }

        static void SaveGame_PostFix()
        {
            SaveConfig.SaveAll();
        }

        static void LoadGame_PostFix()
        {
            SaveConfig.LoadAll();
        }
    }
}
