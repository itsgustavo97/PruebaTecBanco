using System;

namespace Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string Message) : base(Message)
        {

        }
    }
}
