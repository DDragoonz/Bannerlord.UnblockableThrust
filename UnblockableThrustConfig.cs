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
        
        [SettingPropertyBool("{=UnbTh_00001}Player Only (attacking)", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00002}Shall crush through only applied if dealt by player (when attacking)?")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public bool PlayerOnlyAsAttacker{ get; set; } = false;
        
        [SettingPropertyBool("{=UnbTh_00003}Player Only (defending)", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00004}Shall crush through only applied to player (when defending)? require player only attacking to be false. this means ONLY PLAYER need to parry thrust attack")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public bool PlayerOnlyAsDefender{ get; set; } = false;
        
        [SettingPropertyBool("{=UnbTh_00005}Mounted Only", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00006}Shall crush through only applied when mounted?")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public bool MountedOnly{ get; set; } = false;
        
        [SettingPropertyFloatingInteger("{=UnbTh_00007}Minimum Relative Speed", 0f, 100f, "0.0", Order = 1, RequireRestart = false, HintText = "{=UnbTh_00008}Minimum relative speed for crush through to be applied")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public float MinRelativeSpeed{ get; set; } = 0;
    }
}