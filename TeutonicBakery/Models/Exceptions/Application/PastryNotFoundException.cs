using System;

namespace TeutonicBakery.Models.Exceptions.Application
{
    public class PastryNotFoundException : Exception
    {
        public PastryNotFoundException(int pastryId) : base($"Pastry {pastryId} not found")
        {
        }
    }
}
