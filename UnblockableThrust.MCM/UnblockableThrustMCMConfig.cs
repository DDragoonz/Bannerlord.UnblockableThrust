using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace UnblockableThrust.MCM
{
    public class UnblockableThrustMCMConfig : AttributeGlobalSettings<UnblockableThrustMCMConfig>
    {
        public override string Id => "UnblockableThrust";
        public override string DisplayName => "Unblockable Thrust";
        public override string FolderName => "UnblockableThrust";
        public override string FormatType => "json2";
        
        [SettingPropertyBool("{=UnbTh_00001}Player Only (attacking)", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00002}Shall crush through only applied if dealt by player (when attacking)?")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public bool PlayerOnlyAsAttacker{ 
            get => UnblockableThrustConfig.Instance.PlayerOnlyAsAttacker;
            set => UnblockableThrustConfig.Instance.PlayerOnlyAsAttacker = value;
        }
        
        [SettingPropertyBool("{=UnbTh_00003}Player Only (defending)", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00004}Shall crush through only applied to player (when defending)? require player only attacking to be false. this means ONLY PLAYER need to parry thrust attack")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public bool PlayerOnlyAsDefender{ 
            get => UnblockableThrustConfig.Instance.PlayerOnlyAsDefender;
            set => UnblockableThrustConfig.Instance.PlayerOnlyAsDefender = value;
        }
        
        [SettingPropertyBool("{=UnbTh_00005}Mounted Only", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00006}Shall crush through only applied when mounted?")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public bool MountedOnly{ 
            get => UnblockableThrustConfig.Instance.MountedOnly;
            set => UnblockableThrustConfig.Instance.MountedOnly = value;
        }
        
        [SettingPropertyFloatingInteger("{=UnbTh_00007}Minimum Relative Speed", 0f, 100f, "0.0 m/s", Order = 1, RequireRestart = false, HintText = "{=UnbTh_00008}Minimum relative speed for crush through to be applied")]
        [SettingPropertyGroup("{=UnbTh_00000}Settings", GroupOrder = 1)]
        public float MinRelativeSpeed{ 
            get => UnblockableThrustConfig.Instance.MinRelativeSpeed;
            set => UnblockableThrustConfig.Instance.MinRelativeSpeed = value;
        }
        
        [SettingPropertyBool("{=UnbTh_00009}Crush Through Shield", Order = 3, RequireRestart = false, HintText = "{=UnbTh_00010}Allow thrust attack to crush through shield. Potentially Over Powered",IsToggle = true)]
        [SettingPropertyGroup("{=UnbTh_00009}Crush Through Shield", GroupOrder = 2)]
        public bool CrushThroughShield{ 
            get => UnblockableThrustConfig.Instance.CrushThroughShield;
            set => UnblockableThrustConfig.Instance.CrushThroughShield = value;
        }
        
        [SettingPropertyBool("{=UnbTh_00005}Mounted Only", Order = 0, RequireRestart = false, HintText = "{=UnbTh_00006}Shall crush through only applied when mounted?")]
        [SettingPropertyGroup("{=UnbTh_00009}Crush Through Shield", GroupOrder = 2)]
        public bool ShieldMountedOnly{ 
            get => UnblockableThrustConfig.Instance.ShieldMountedOnly;
            set => UnblockableThrustConfig.Instance.ShieldMountedOnly = value;
        }
        
        [SettingPropertyFloatingInteger("{=UnbTh_00007}Minimum Relative Speed", 0f, 100f, "0.0 m/s", Order = 1, RequireRestart = false, HintText = "{=UnbTh_00008}Minimum relative speed for crush through to be applied")]
        [SettingPropertyGroup("{=UnbTh_00009}Crush Through Shield", GroupOrder = 2)]
        public float ShieldMinRelativeSpeed{ 
            get => UnblockableThrustConfig.Instance.ShieldMinRelativeSpeed;
            set => UnblockableThrustConfig.Instance.ShieldMinRelativeSpeed = value;
        }
    }
}