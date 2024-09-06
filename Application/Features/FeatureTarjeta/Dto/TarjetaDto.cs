namespace Application.Features.FeatureTarjeta.Dto
{
    public class TarjetaDto
    {
        public long Id { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Limite { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal InteresBonificable { get; set; }
        public double PorcentajePagoMinimo { get; set; }
        public double PorcentajeInteres { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime? FechaModificado { get; set; }
    }
}
