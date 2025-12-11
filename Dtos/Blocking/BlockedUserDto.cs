using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Blocking
{
    public class BlockedUserDto
    {
        public string BlockedUserId { get; set; }
        public string? Username { get; set; }
        public string? Image { get; set; }

    }
}