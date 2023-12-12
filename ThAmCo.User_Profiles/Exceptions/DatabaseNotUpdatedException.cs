using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DatabaseNotUpdatedException : Exception
    {
        public DatabaseNotUpdatedException()
        {
        }

        public DatabaseNotUpdatedException(string message)
            : base(message)
        {
        }

        public DatabaseNotUpdatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
