public record BatteryStatus(
    int? Percentage,
    bool? IsCharging
);

public interface IBatteryService
{
    BatteryStatus GetStatus();
}