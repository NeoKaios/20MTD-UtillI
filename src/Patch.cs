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
        [HarmonyPatch(typeof(GameController), "Start")]
        static void Prefix(GameController __instance)
        {
            GameObject panelObj = new GameObject("UtillI Panel", typeof(RectTransform));
            panelObj.transform.SetParent(__instance.hud.transform.parent);
            panelObj.AddComponent<Watcher>();
        }

        static void DevSetup()
        {
            UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("white"));
            UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("yellow"));
            UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("green"));
            UtillIRegister.Register(PanelPosition.BottomRight, new ColorTextUpdater("red"));
            UtillIRegister.Register(PanelPosition.BottomRight, new ColorTextUpdater("blue"));
        }
    }
}