namespace Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string Name, object Key) : base($"Entidad {Name}, con identificador {Key} no fue encontrado")
        {

        }
    }
}
