using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IVerification
    {
        public Task SendVerificationEmail(string email, string subject, string code);
        public int GenerateCode();
        public Task<EmailVerification> CreateVerification(EmailVerification verification);
    }
}