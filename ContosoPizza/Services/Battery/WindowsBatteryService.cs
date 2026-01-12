using System.Runtime.InteropServices;

public class WindowsBatteryService : IBatteryService
{
    [DllImport("kernel32.dll")]
    private static extern bool GetSystemPowerStatus(out SYSTEM_POWER_STATUS sps);

    public BatteryStatus GetStatus()
    {
        if (!GetSystemPowerStatus(out var sps))
            return new BatteryStatus(null, null);

        return new BatteryStatus(
            sps.BatteryLifePercent == 255 ? null : sps.BatteryLifePercent,
            sps.ACLineStatus == 1
        );
    }

    private struct SYSTEM_POWER_STATUS
    {
        public byte ACLineStatus;
        public byte BatteryFlag;
        public byte BatteryLifePercent;
        public byte Reserved1;
        public int BatteryLifeTime;
        public int BatteryFullLifeTime;
    }
}
