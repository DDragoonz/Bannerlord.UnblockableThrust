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

            if (strikeType == StrikeType.Thrust && collisionResult == CombatCollisionResult.Blocked && attackerAgent != null && defenderAgent != null)
            {
                if (UnblockableThrustConfig.Instance != null)
                {
                    if (UnblockableThrustConfig.Instance.PlayerOnlyAsAttacker && !attackerAgent.IsPlayerControlled)
                    {
                        return;
                    }
                
                    if (UnblockableThrustConfig.Instance.PlayerOnlyAsDefender && !defenderAgent.IsPlayerControlled)
                    {
                        return;
                    }

                    if (UnblockableThrustConfig.Instance.MountedOnly && !attackerAgent.HasMount)
                    {
                        return;
                    }

                    if (UnblockableThrustConfig.Instance.MinRelativeSpeed > 0)
                    {
                        Vec2 velocityContribution1 = GetAgentVelocityContribution(attackerAgent);
                        Vec2 velocityContribution2 = GetAgentVelocityContribution(defenderAgent);
                        double relativeSpeed = (velocityContribution1 - velocityContribution2).Length;

                        if (relativeSpeed < UnblockableThrustConfig.Instance.MinRelativeSpeed)
                        {
                            return;
                        }
                    }   
                }
                
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
        
        private static Vec2 GetAgentVelocityContribution(Agent agent)
        {
            bool hasAgentMountAgent = agent.HasMount; 
            Vec2 agentMovementVelocity = agent.MovementVelocity;
            Vec2 agentMountMovementDirection = hasAgentMountAgent ? agent.MountAgent.GetMovementDirection() : Vec2.Zero;
            float agentMovementDirectionAsAngle = agent.MovementDirectionAsAngle;
            Vec2 velocityContribution;
            if (hasAgentMountAgent)
            {
                velocityContribution = agentMovementVelocity.y * agentMountMovementDirection;
            }
            else
            {
                velocityContribution = agentMovementVelocity;
                velocityContribution.RotateCCW(agentMovementDirectionAsAngle);
            }

            return velocityContribution;
        }
    }



}