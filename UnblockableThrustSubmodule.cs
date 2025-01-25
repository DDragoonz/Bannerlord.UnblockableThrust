using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace UnblockableThrust
{
    public class UnblockableThrustSubmodule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("mod.bannerlord.unblockablethrust").PatchAll();
        }


    }
}