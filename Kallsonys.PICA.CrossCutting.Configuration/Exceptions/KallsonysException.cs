using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.CrossCutting.Configuration.Exceptions
{
    public abstract class KallsonysException: Exception
    {
        public KallsonysException(string mensaje,Exception innerException) :base(mensaje, innerException)
        {
        }
    }
}
