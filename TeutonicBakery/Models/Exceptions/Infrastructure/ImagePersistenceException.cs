using System;

namespace TeutonicBakery.Models.Exceptions.Infrastructure
{
    public class ImagePersistenceException : Exception
    {
        public ImagePersistenceException(Exception innerException) : base("Couldn't persist the image", innerException)
        {
        }
    }
}
