namespace Application.Features.FeatureTransaccion.Vm
{
    public class EstadoCuentaVM
    {
        public long IdTarjeta { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Limite { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoDisponible { get; set; }
        public double PorcentajePagoMinimo { get; set; }
        public double PorcentajeInteres { get; set; }

        public List<TransaccionVM> ListaComprasMes { get; set; }
        public decimal SaldoTotalComprasMes { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinima { get; set; }
        public decimal TotalContadoAPagar { get; set; }
        public decimal TotalContadoMasInteresBonificable { get; set; }
    }
}
