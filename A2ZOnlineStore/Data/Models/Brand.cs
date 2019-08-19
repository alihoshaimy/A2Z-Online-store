using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
