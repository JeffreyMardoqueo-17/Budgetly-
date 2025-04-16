using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgetly.service.Auth
{
     public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}