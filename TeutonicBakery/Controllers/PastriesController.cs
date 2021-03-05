using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeutonicBakery.Models.InputModels;
using TeutonicBakery.Models.Services.Application.Pastries;
using TeutonicBakery.Models.ViewModels;
using TeutonicBakery.Models.ViewModels.Pastries;

namespace TeutonicBakery.Controllers
{
    public class PastriesController : Controller
    {
        private readonly IPastryService pastryService;
        public PastriesController(IPastryService pastryService)
        {
            this.pastryService = pastryService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Detail(int id)
        {
            PastryDetailViewModel viewModel = await pastryService.GetPastryAsync(id);
            ViewData["Title"] = viewModel.Name;
            return View(viewModel);
        }

        public async Task<IActionResult> Manage()
        {
            List<PastryViewModel> pastriesList = await pastryService.GetPastriesAsync();
            ViewData["Title"] = "Gestisci i dolci Teutonici della Pasticceria";

            ManageViewModel viewModel = new()
            {
                AllPastriesList = pastriesList
            };

            return View(viewModel);
        }

        //public IActionResult Create()
        //{
        //    ViewData["Title"] = "Nuovo Dolce";
        //    PastryCreateInputModel inputModel = new();
        //    return View(inputModel);
        //}
    }
}
