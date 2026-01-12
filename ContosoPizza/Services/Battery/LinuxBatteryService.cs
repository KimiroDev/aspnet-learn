public class LinuxBatteryService : IBatteryService
{
    public BatteryStatus GetStatus()
    {
        var batPath = "/sys/class/power_supply/BAT0";

        if (!Directory.Exists(batPath))
            return new BatteryStatus(null, null);

        var capacity = int.Parse(
            File.ReadAllText(Path.Combine(batPath, "capacity")).Trim()
        );

        var status = File.ReadAllText(
            Path.Combine(batPath, "status")
        ).Trim();

        return new BatteryStatus(
            capacity,
            status.Equals("Charging", StringComparison.OrdinalIgnoreCase)
        );
    }
}
