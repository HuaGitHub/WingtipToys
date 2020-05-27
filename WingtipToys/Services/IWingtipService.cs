using System.Collections.Generic;
using WingtipToys.Data.Entities;

namespace WingtipToys.Services
{
    public interface IWingtipService
    {
        int GetCategoryID(string name);
        IEnumerable<Product> GetProductsByCategoryId(int id);
    }
}