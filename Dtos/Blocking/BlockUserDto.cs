using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Blocking
{
    public class BlockUserDto
    {
        public string UserId { get; set; }
        public string BlockedUserId { get; set; }
    }
}