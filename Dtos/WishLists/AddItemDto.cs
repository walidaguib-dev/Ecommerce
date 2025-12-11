using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.WishLists
{
    public class AddItemDto
    {
        public string userId { get; set; }
        public int ProductId { get; set; }
    }
}