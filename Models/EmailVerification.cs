using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class EmailVerification
    {
        public int Id { get; set; }
        public int code { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
    }
}