namespace UnblockableThrust
{
    public class UnblockableThrustConfig
    {
        public static UnblockableThrustConfig Instance { get; private set; } = new UnblockableThrustConfig();
        public bool PlayerOnlyAsAttacker{ get; set; } = false;
        public bool PlayerOnlyAsDefender{ get; set; } = false;
        public bool MountedOnly{ get; set; } = false;
        public float MinRelativeSpeed{ get; set; } = 0;
        public bool CrushThroughShield{ get; set; } = false;
        public bool ShieldMountedOnly{ get; set; } = true;
        public float ShieldMinRelativeSpeed{ get; set; } = 10;
    }
}