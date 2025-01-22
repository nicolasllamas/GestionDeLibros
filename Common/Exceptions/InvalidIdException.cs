using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("El ID proporcionado es inválido.") { }
        public InvalidIdException(string message) : base(message) { }
    }
}
