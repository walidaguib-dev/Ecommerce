using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.ProuctFiles
{
    public class ProductFilesDto
    {
        public int ProductId { get; set; }
        public string Product_name { get; set; }
        public int FileId { get; set; }
        public string fileName { get; set; }
    }
}