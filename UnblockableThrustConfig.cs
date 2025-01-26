using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace UnblockableThrust
{
    public class UnblockableThrustConfig : AttributeGlobalSettings<UnblockableThrustConfig>
    {
        public override string Id => "UnblockableThrust";
        public override string DisplayName => "Unblockable Thrust";
        public override string FolderName => "UnblockableThrust";
        public override string FormatType => "json2";
        
        [SettingPropertyBool("Player Only (attacking)", Order = 0, RequireRestart = false, HintText = "shall crush through only applied if dealt by player (when attacking)?")]
        [SettingPropertyGroup("Settings", GroupOrder = 1)]
        public bool PlayerOnlyAsAttacker{ get; set; } = false;
        
        [SettingPropertyBool("Player Only (defending)", Order = 0, RequireRestart = false, HintText = "shall crush through only applied to player (when defending)? require player only attacking to be false. this means ONLY PLAYER need to parry thrust attack")]
        [SettingPropertyGroup("Settings", GroupOrder = 1)]
        public bool PlayerOnlyAsDefender{ get; set; } = false;
        
        [SettingPropertyBool("Mounted Only", Order = 0, RequireRestart = false, HintText = "Shall crush through only applied when mounted?")]
        [SettingPropertyGroup("Settings", GroupOrder = 1)]
        public bool MountedOnly{ get; set; } = false;
        
        [SettingPropertyFloatingInteger("Minimum Relative Speed", 0f, 100f, "0.0", Order = 1, RequireRestart = false, HintText = "Minimum relative speed for crush through to be applied")]
        [SettingPropertyGroup("Settings", GroupOrder = 1)]
        public float MinRelativeSpeed{ get; set; } = 0;
    }
}