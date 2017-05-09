using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nab.Services
{
    /// <summary>
    /// E-mail service interface
    /// </summary>
    public interface IEmailService
    {
        Task SendEmailAsync(string senderName, string email, string subject, string message);
    }
}
