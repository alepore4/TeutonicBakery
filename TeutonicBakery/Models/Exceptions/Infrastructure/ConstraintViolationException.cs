using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeutonicBakery.Models.Exceptions.Infrastructure
{
    public class ConstraintViolationException : Exception
    {
        public ConstraintViolationException(Exception innerException) : base($"A violation occurred for a database constraint", innerException)
        {
        }
    }
}
