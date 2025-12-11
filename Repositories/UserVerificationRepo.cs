using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;

namespace Ecommerce.Repositories
{
    public class UserVerificationRepo
    (
        ApplicationDBContext _context,
        IConfiguration _configuration
        ) : IVerification
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IConfiguration configuration = _configuration;
        public async Task<EmailVerification> CreateVerification(EmailVerification verification)
        {
            await context.emailVerifications.AddAsync(verification);
            await context.SaveChangesAsync();
            return verification;
        }

        public int GenerateCode()
        {
            int random = new Random().Next(1000, 9999);
            return random;
        }

        public async Task SendVerificationEmail(string email, string subject, string code)
        {
            var client = new SmtpClient(configuration.GetSection("Smtp").GetValue<string>("Host"), configuration.GetSection("Smtp").GetValue<int>("Port"))
            {
                Credentials = new NetworkCredential(configuration.GetSection("Smtp").GetValue<string>("UserName"), configuration.GetSection("Smtp").GetValue<string>("Password")),
                EnableSsl = configuration.GetSection("Smtp").GetValue<bool>("EnableSsl")
            };
            var mailMessage = new MailMessage("noreply@yourapp.com", email, subject, code);
            await client.SendMailAsync(mailMessage);
        }
    }
}