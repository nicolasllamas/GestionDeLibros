using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AppExceptions : Exception
    {
        public AppExceptions(string mensaje) : base(mensaje) { }
    }
}
