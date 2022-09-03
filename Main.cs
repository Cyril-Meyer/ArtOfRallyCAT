using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace ArtOfRallyCATransmission
{
    public static class Main
    {
        public static UnityModManager.ModEntry mod;
        public static bool enabled;
        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;
            mod.Logger.Log("Loading...");

            modEntry.OnToggle = OnToggle;

            mod.Logger.Log("Loaded");
            return true;
        }
        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            var harmony = new Harmony(modEntry.Info.Id);

            if (value)
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony.UnpatchAll();
            }
            return true;
        }
    }


    [HarmonyPatch(typeof(AxisCarController), "GetInput")]
    public static class AxisCarController_GetInput_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);
            var index = -1;

            for (var i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Brtrue)
                {
                    index = i;
                }
            }

            if (index > -1)
            {
                code.RemoveRange(index, 4);
            }

            return code;
        }
    }
}
