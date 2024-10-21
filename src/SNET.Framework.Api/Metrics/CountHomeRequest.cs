using System.Diagnostics.Metrics;

namespace SNET.Framework.Api.Metrics;

public static class CountHomeRequest
{
    public const string ServiceName = "CounterHome";

    public static Meter Metrica = new (ServiceName);

    public static Counter<int> Counter = Metrica.CreateCounter<int>("home.request");
}
