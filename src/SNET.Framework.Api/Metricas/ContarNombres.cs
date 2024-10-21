using System.Diagnostics.Metrics;

namespace SNET.Framework.Api.Metricas;

public static class ContarNombres
{
    public  const string ServiceName = "ContarNombres";

    public static Meter Metrica = new Meter(ServiceName);

    public static Counter<int> Contar = Metrica.CreateCounter<int>("tota.nombres");
}
