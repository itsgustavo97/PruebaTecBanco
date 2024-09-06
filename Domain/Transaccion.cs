using Domain.ModelBase;

namespace Domain
{
    public class Transaccion : BaseModel
    {
        public string Tipo { get; set; } //Compra o pago
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public long IdTarjeta { get; set; }
    }
}
