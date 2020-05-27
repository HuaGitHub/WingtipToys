using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WingtipToys.Data;
using WingtipToys.Data.Entities;

namespace WingtipToys.Services
{
    public class WingtipService : IWingtipService
    {
        private readonly WingtipContext _context;
        private readonly ILogger<WingtipService> _logger;

        public WingtipService(WingtipContext context, ILogger<WingtipService> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        /// <summary>
        /// Get Products by Category ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>List of Products</returns>
        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            try
            {
                _logger.LogDebug($"GetProductsByCategoryId() called - CategoryID:{id}");
                return _context.Products.Where(x => x.CategoryID == id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProductsByCategoryId() failed - {ex}");
                return null;
            }
        }

        /// <summary>
        /// Get Category ID
        /// </summary>
        /// <param name="name">Category Name</param>
        /// <returns>Category ID</returns>
        public int GetCategoryID(string name)
        {
            try
            {
                _logger.LogDebug($"GetCategoryID() called - CategoryName:{name}");
                var category = _context.Categories.FirstOrDefault(x => x.CategoryName == name);
                if (category != null) return category.CategoryID;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCategoryID() failed - {ex}");
            }
            return -1;
        }
    }
}
