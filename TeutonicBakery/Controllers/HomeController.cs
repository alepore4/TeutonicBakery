using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeutonicBakery.Models.Services.Application.Pastries;
using TeutonicBakery.Models.ViewModels;
using TeutonicBakery.Models.ViewModels.Pastries;

namespace TeutonicBakery.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IPastryService pastryService)
        {
            ViewData["Title"] = "Benvenuto alla Pasticceria Teutonica di Luana e Maria";
            List<PastryViewModel> availablePastries = await pastryService.GetAvailablePastriesAsync();

            HomeViewModel viewModel = new()
            {
                AvailablePastriesList = availablePastries
            };

            return View(viewModel);
        }

    }
}
