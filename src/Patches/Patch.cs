using HarmonyLib;
using flanne.Core;

using UnityEngine;
using UtillI.Internals;
using UtillI.Examples;

namespace UtillI.Patches
{
    [HarmonyPatch]
    class Patch
    {
        private static Watcher watcher;

        [HarmonyPatch(typeof(GameController), "Start")]
        static void Prefix(GameController __instance)
        {
            GameObject panelObj = new GameObject("UtillI Panel", typeof(RectTransform));
            panelObj.transform.SetParent(__instance.hud.transform.parent);
            watcher = panelObj.AddComponent<Watcher>();
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatState), "Enter")]
        private static void CombatStateEnterPost()
        {
            watcher.SetPauseStatus(false);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatState), "Exit")]
        private static void CombatStateExitPost()
        {
            watcher.SetPauseStatus(true);
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerSurvivedState), "Enter")]
        [HarmonyPatch(typeof(PlayerDeadState), "Enter")]
        private static void GameEndEnterPost()
        {
            watcher.HideAndStopWatcher();
        }

        static void RegisterDevSetup()
        {
            UtillIRegister.Register(new ColoredRegistration(PanelPosition.BottomLeft, DisplayRule.Always, "white"));
            UtillIRegister.Register(new FixedColoredRegistration("yellow"));
            UtillIRegister.Register(new ColoredRegistration(PanelPosition.BottomLeft, DisplayRule.CombatOnly, "green"));
            UtillIRegister.Register(new ColoredRegistration(PanelPosition.BottomRight, DisplayRule.CombatOnly, "red"));
            UtillIRegister.Register(new ColoredRegistration(PanelPosition.BottomRight, DisplayRule.Always, "blue"));
        }
    }
}