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
            if (crushedThrough)
            {
                // already crushed through
                return;
            }

            if (strikeType == StrikeType.Thrust && attackerAgent != null && defenderAgent != null)
            {
                bool allowCrushThroughShield = false;
                double minRelativeSpeed = 0;
                bool mountedOnly = false;
                EquipmentIndex wieldedOffhandItemIndex = defenderAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
                bool isBlockedByShield = wieldedOffhandItemIndex != EquipmentIndex.None && defenderAgent.Equipment[wieldedOffhandItemIndex].CurrentUsageItem.IsShield;
                
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
                    
                    allowCrushThroughShield = UnblockableThrustConfig.Instance.CrushThroughShield;
                    minRelativeSpeed = isBlockedByShield ? UnblockableThrustConfig.Instance.ShieldMinRelativeSpeed : UnblockableThrustConfig.Instance.MinRelativeSpeed;
                    mountedOnly = isBlockedByShield ? UnblockableThrustConfig.Instance.ShieldMountedOnly : UnblockableThrustConfig.Instance.MountedOnly;
                }
                
                if (mountedOnly && !attackerAgent.HasMount)
                {
                    return;
                }

                if (minRelativeSpeed > 0)
                {
                    Vec2 velocityContribution1 = GetAgentVelocityContribution(attackerAgent);
                    Vec2 velocityContribution2 = GetAgentVelocityContribution(defenderAgent);
                    double relativeSpeed = (velocityContribution1 - velocityContribution2).Length;

                    if (relativeSpeed < minRelativeSpeed)
                    {
                        return;
                    }
                }
                
                if (allowCrushThroughShield)
                {
                    crushedThrough = collisionResult == CombatCollisionResult.Blocked || collisionResult == CombatCollisionResult.Parried;
                }
                else
                {
                    crushedThrough = collisionResult == CombatCollisionResult.Blocked && !isBlockedByShield;
                }
                
                // InformationManager.DisplayMessage(new InformationMessage("thrust attack crushed through"));
                
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