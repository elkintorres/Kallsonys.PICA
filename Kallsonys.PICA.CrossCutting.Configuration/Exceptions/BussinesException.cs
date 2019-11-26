using System;

namespace Kallsonys.PICA.CrossCutting.Configuration.Exceptions
{
    public class BussinesException : KallsonysException
    {
        public BussinesException(string mensaje, Exception innerException) : base(mensaje, innerException)
        { }

    }
}
