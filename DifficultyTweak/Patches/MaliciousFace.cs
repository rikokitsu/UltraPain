﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DifficultyTweak.Patches
{
    [HarmonyPatch(typeof(SpiderBody))]
    [HarmonyPatch("Start")]
    class MaliciousFace_Start_Patch
    {
        static void Postfix(SpiderBody __instance, ref GameObject ___proj)
        {
            if (!Plugin.ultrapainDifficulty || !ConfigManager.enemyTweakToggle.value || !ConfigManager.maliciousFaceHomingProjectileToggle.value)
                return;

            if (Plugin.homingProjectile.gameObject != null)
                ___proj = Plugin.maliciousFaceBullet;
        }
    }

    [HarmonyPatch(typeof(SpiderBody))]
    [HarmonyPatch("ShootProj")]
    class MaliciousFace_ShootProj_Patch
    {
        static void Postfix(SpiderBody __instance, ref GameObject ___currentProj)
        {
            if (!Plugin.ultrapainDifficulty || !ConfigManager.enemyTweakToggle.value || !ConfigManager.maliciousFaceHomingProjectileToggle.value)
                return;

            Projectile proj = ___currentProj.GetComponent<Projectile>();
            proj.target = MonoSingleton<PlayerTracker>.Instance.GetTarget();
            proj.speed = ConfigManager.maliciousFaceHomingProjectileSpeed.value;
            proj.turningSpeedMultiplier = ConfigManager.maliciousFaceHomingProjectileTurnSpeed.value;
            proj.damage = ConfigManager.maliciousFaceHomingProjectileDamage.value;
            ___currentProj.SetActive(true);
        }
    }

    [HarmonyPatch(typeof(SpiderBody))]
    [HarmonyPatch("Enrage")]
    class MaliciousFace_Enrage_Patch
    {
        static void Postfix(SpiderBody __instance)
        {
            if (!Plugin.ultrapainDifficulty || !ConfigManager.enemyTweakToggle.value || !ConfigManager.maliciousFaceRadianceOnEnrage.value)
                return;

            EnemyIdentifier comp = __instance.GetComponent<EnemyIdentifier>();
            for(int i = 0; i < ConfigManager.maliciousFaceRadianceAmount.value; i++)
                comp.BuffAll();

            __instance.spark = new GameObject();
        }
    }

    /*[HarmonyPatch(typeof(SpiderBody))]
    [HarmonyPatch("BeamChargeEnd")]
    class MaliciousFace_BeamChargeEnd_Patch
    {
        static void Postfix(SpiderBody __instance, ref bool ___parryable)
        {
            if (__instance.currentEnrageEffect == null)
                return;
            ___parryable = false;
        }
    }*/

    /*[HarmonyPatch(typeof(HookArm))]
    [HarmonyPatch("FixedUpdate")]
    class HookArm_FixedUpdate_MaliciousFacePatch
    {
        static void Postfix(HookArm __instance, ref EnemyIdentifier ___caughtEid)
        {
            if (__instance.state == HookState.Caught && ___caughtEid.enemyType == EnemyType.MaliciousFace)
            {
                if (___caughtEid.GetComponent<SpiderBody>().currentEnrageEffect == null)
                    return;

                //__instance.state = HookState.Pulling;
                ___caughtEid = null;
                __instance.StopThrow();
            }
        }
    }*/
}
