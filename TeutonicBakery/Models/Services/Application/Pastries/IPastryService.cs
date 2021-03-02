using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeutonicBakery.Models.ViewModels.Pastries;

namespace TeutonicBakery.Models.Services.Application.Pastries
{
    public interface IPastryService
    {
        Task<List<PastryViewModel>> GetAvailablePastriesAsync();
        //Task<ListViewModel<PastryViewModel>> GetCoursesAsync(PastryListInputModel model);
        //Task<PastryDetailViewModel> GetCourseAsync(int id);
        //Task<PastryEditInputModel> GetCourseForEditingAsync(int id);
        //Task<PastryDetailViewModel> CreateCourseAsync(PastryCreateInputModel inputModel);
        //Task<PastryDetailViewModel> EditCourseAsync(PastryEditInputModel inputModel);
        //Task DeletePastryAsync(PastryDeleteInputModel inputModel);
        //Task<bool> IsTitleAvailableAsync(string title, int excludeId);
    }
}
