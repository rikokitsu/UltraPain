﻿using Discord;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace DifficultyTweak.Patches
{
    [HarmonyPatch(typeof(DiscordController), "SendActivity")]
    class DiscordController_SendActivity_Patch
    {
        static bool Prefix(DiscordController __instance, ref Activity ___cachedActivity)
        {
            if(___cachedActivity.State != null && ___cachedActivity.State == "DIFFICULTY: VIOLENT")
                ___cachedActivity.State = "DIFFICULTY: ULTRAKILL MUST DIE";
            return true;
        }
    }
}