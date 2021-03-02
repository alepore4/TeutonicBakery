using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TeutonicBakery.Models.Services.Infrastructure
{
    public interface IImagePersister
    {
        Task<string> SavePastryImageAsync(int pastryId, IFormFile formFile);
    }
}
