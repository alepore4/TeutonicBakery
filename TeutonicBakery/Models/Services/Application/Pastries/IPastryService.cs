using System.Collections.Generic;
using System.Threading.Tasks;
using TeutonicBakery.Models.ViewModels.Pastries;

namespace TeutonicBakery.Models.Services.Application.Pastries
{
    public interface IPastryService
    {
        Task<List<PastryViewModel>> GetAvailablePastriesAsync();
        Task<PastryDetailViewModel> GetPastryAsync(int id);
        Task<List<PastryViewModel>> GetPastriesAsync();

    }
}
