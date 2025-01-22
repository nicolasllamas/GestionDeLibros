using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base("Los datos proporcionados son inválidos.") { }
        public InvalidInputException(string message) : base(message) { }

    }
}
