using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace UnblockableThrust
{

    [HarmonyPatch(typeof(MissionCombatMechanicsHelper))]
    [HarmonyPatch("GetDefendCollisionResults")]
    class MissionCombatMechanicsHelperGetDefendCollisionResultsPatch
    {
        static void Postfix(Agent attackerAgent,
            Agent defenderAgent,
            CombatCollisionResult collisionResult,
            int attackerWeaponSlotIndex,
            bool isAlternativeAttack,
            StrikeType strikeType,
            Agent.UsageDirection attackDirection,
            float collisionDistanceOnWeapon,
            float attackProgress,
            bool attackIsParried,
            bool isPassiveUsageHit,
            bool isHeavyAttack,
            ref float defenderStunPeriod,
            ref float attackerStunPeriod,
            ref bool crushedThrough,
            ref bool chamber)
        {

            if (strikeType == StrikeType.Thrust && collisionResult == CombatCollisionResult.Blocked && defenderAgent != null)
            {
                EquipmentIndex wieldedOffhandItemIndex = defenderAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
                if (wieldedOffhandItemIndex != EquipmentIndex.None && defenderAgent.Equipment[wieldedOffhandItemIndex].CurrentUsageItem.IsShield)
                {
                    // InformationManager.DisplayMessage(new InformationMessage("thrust attack blocked by shield"));
                    return;
                }
                // InformationManager.DisplayMessage(new InformationMessage("thrust attack crushed through"));
                crushedThrough = true;
            }
        }
    }

     
    
}