using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeutonicBakery.Models.Exceptions.Application
{
    public class PastryNotFoundException : Exception
    {
        public PastryNotFoundException(int pastryId) : base($"Pastry {pastryId} not found")
        {
        }
    }
}
