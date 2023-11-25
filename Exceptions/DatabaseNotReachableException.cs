using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DatabaseNotReachableException : Exception
    {
        public DatabaseNotReachableException()
        {
        }

        public DatabaseNotReachableException(string message)
            : base(message)
        {
        }

        public DatabaseNotReachableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
