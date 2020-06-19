using System;
using System.Collections.Generic;
using System.Text;

namespace MVC2AT.Dominio.Model.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message)
            : base(message)
        {
        }

        public RepositoryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
