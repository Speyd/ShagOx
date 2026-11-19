namespace ShagOxServer.Infrastructure.ExternalServices.Payments;
public static class MoneyConverter
{
    public static long ToMinorUnits(
        decimal amount,
        int DecimalPlaces)
    {
        var multiplier = (decimal)Math.Pow(10, DecimalPlaces);

        return decimal.ToInt64(
            decimal.Round(
                amount * multiplier,
                0,
                MidpointRounding.AwayFromZero));
    }
}
