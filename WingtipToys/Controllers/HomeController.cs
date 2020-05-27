using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WingtipToys.Models;
using WingtipToys.Data;
using WingtipToys.Services;
using WingtipToys.Data.Entities;

namespace WingtipToys.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWingtipService _wingtipService;

        public const string CategoryType_Cars = "Cars";
        public HomeController(ILogger<HomeController> logger, IWingtipService wingtipService)
        {
            _logger = logger;
            _wingtipService = wingtipService;
        }

        public IActionResult Index()
        {
            try
            {
                _logger.LogDebug("Index() page called...");
                //Get the category id
                int categoryID = _wingtipService.GetCategoryID(CategoryType_Cars);
                if (categoryID != -1)
                {
                    //Get the list of products
                    var products = _wingtipService.GetProductsByCategoryId(categoryID);

                    if (products != null && products.Any())
                    {
                        var viewModel = new ProductViewModel()
                        {
                            Products = products
                        };
                        return View(viewModel);
                    }
                }
                _logger.LogWarning("No products were found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Index() page failed - {ex}");
            }
            return View(new ProductViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
