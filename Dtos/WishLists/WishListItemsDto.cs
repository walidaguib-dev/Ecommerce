using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.WishLists
{
    public class WishListItemsDto
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}