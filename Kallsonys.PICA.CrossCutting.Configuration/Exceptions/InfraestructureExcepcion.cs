using System;

namespace Kallsonys.PICA.CrossCutting.Configuration.Exceptions
{
    public class InfraestructureExcepcion : KallsonysException
    {
        public InfraestructureExcepcion(string mensaje, Exception innerException) : base(mensaje, innerException)
        { }
    }
}