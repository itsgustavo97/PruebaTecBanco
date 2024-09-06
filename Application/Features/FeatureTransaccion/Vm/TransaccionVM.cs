namespace Application.Features.FeatureTransaccion.Vm
{
    public class TransaccionVM
    {
        public long Id { get; set; }
        public string Tipo { get; set; } //Compra o pago
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public long IdTarjeta { get; set; }
        public bool Borrado { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaModificado { get; set; }
    }
}
