using System;

namespace Kallsonys.PICA.CrossCutting.Configuration.Exceptions
{
    public abstract class KallsonysException : Exception
    {
        public KallsonysException(string mensaje, Exception innerException) : base(mensaje, innerException)
        {
        }
    }
}