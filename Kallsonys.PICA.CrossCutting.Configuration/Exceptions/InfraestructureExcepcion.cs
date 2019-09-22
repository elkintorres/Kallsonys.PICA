using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.CrossCutting.Configuration.Exceptions
{
    public class InfraestructureExcepcion : KallsonysException
    {
        public InfraestructureExcepcion(string mensaje, Exception innerException) : base(mensaje, innerException)
        { }
    }
}
