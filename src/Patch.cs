using HarmonyLib;
using flanne.Core;

using UnityEngine;
using UtillI.Internals;
using UtillI.Examples;

namespace UtillI
{
    [HarmonyPatch]
    public class Patch
    {
        public static bool isPaused = false;

        [HarmonyPatch(typeof(GameController), "Start")]
        static void Prefix(GameController __instance)
        {
            RegisterDevSetup();
            GameObject panelObj = new GameObject("UtillI Panel", typeof(RectTransform));
            panelObj.transform.SetParent(__instance.hud.transform.parent);
            panelObj.AddComponent<Watcher>();
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatState), "Enter")]
        private static void CombatStateEnterPost()
        {
            isPaused = false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatState), "Exit")]
        private static void CombatStateExitPost()
        {
            isPaused = true;
        }

        static void RegisterDevSetup()
        {
            UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("white"), DisplayRule.Always);
            UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("yellow"), DisplayRule.PauseOnly);
            UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("green"), DisplayRule.CombatOnly);
            UtillIRegister.Register(PanelPosition.BottomRight, new ColorTextUpdater("red"), DisplayRule.CombatOnly);
            UtillIRegister.Register(PanelPosition.BottomRight, new ColorTextUpdater("blue"), DisplayRule.CombatOnly);
        }
    }
}