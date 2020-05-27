using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingtipToys.Data.Entities;

namespace WingtipToys.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }

    }
}
