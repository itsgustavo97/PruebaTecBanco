using Domain.ModelBase;

namespace Domain
{
    public class EstadoCuenta : BaseModel
    {
        public decimal SaldoTotalAnterior { get; set; }
        public decimal SaldoTotalActual { get; set; }
        public decimal PagoMinimo { get; set; }
        public decimal PagoContado { get; set; }
        public decimal InteresBonificable { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public long IdTarjeta { get; set; }
    }
}
