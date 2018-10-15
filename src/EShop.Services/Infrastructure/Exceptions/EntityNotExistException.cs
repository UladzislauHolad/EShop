using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Infrastructure.Exceptions
{
    public class EntityNotExistException : Exception
    {

        public EntityNotExistException(string message) : base(message)
        { }
    }
}
