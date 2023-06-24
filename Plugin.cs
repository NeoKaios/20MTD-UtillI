using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;

namespace UtillI
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {

        private void Awake()
        {
            try
            {
                Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message + '\n' + e.StackTrace);
                Logger.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods.");
            }
        }
    }
}
