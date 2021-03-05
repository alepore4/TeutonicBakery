using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeutonicBakery.Models.Exceptions.Infrastructure;

namespace TeutonicBakery.Models.Services.Infrastructure
{
    public class MagickNetImagePersister : IImagePersister
    {
        private readonly IWebHostEnvironment env;

        private readonly SemaphoreSlim semaphore;

        public MagickNetImagePersister(IWebHostEnvironment env)
        {
            ResourceLimits.Height = 4000;
            ResourceLimits.Width = 4000;
            semaphore = new SemaphoreSlim(2);
            this.env = env;
        }

        public async Task<string> SavePastryImageAsync(int pastryId, IFormFile formFile)
        {
            await semaphore.WaitAsync();
            try
            {
                //Salvare il file
                string path = $"/Pastries/{pastryId}.jpg";
                string physicalPath = Path.Combine(env.WebRootPath, "Pastries", $"{pastryId}.jpg");

                using Stream inputStream = formFile.OpenReadStream();
                using MagickImage image = new(inputStream);

                //Manipolare l'immagine
                int width = 300;
                int height = 300;
                MagickGeometry resizeGeometry = new(width, height)
                {
                    FillArea = true
                };
                image.Resize(resizeGeometry);
                image.Crop(width, width, Gravity.Northwest);

                image.Quality = 70;
                image.Write(physicalPath, MagickFormat.Jpg);

                //Restituire il percorso al file
                return path;
            }
            catch (Exception exc)
            {
                throw new ImagePersistenceException(exc);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
