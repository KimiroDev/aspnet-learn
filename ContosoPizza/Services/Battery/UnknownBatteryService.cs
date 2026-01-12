public class UnknownBatteryService : IBatteryService
{
    public BatteryStatus GetStatus() => new(null, null);
}
