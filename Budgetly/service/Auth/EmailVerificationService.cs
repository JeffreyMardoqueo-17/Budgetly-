using Budgetly.service.Auth;
using Budgetly.service.Email;
using Budgetly.Services.Auth;
using System.Collections.Concurrent;

namespace Budgetly.Services.Auth
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly IEmailService _emailService;

        // Guarda códigos y expiraciones por correo
        private readonly ConcurrentDictionary<string, (string Code, DateTime Expiration)> _verifications = new();
        private readonly HashSet<string> _verifiedEmails = new();

        public EmailVerificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<string> GenerateVerificationCodeAsync(string email)
        {
            var code = new Random().Next(100000, 999999).ToString();
            var expiration = DateTime.UtcNow.AddMinutes(3);
            _verifications[email] = (code, expiration);

            var subject = "Código de verificación";
            var body = $"Tu código de verificación es: {code}. Expira en 3 minutos.";
            await _emailService.SendEmailAsync(email, subject, body);

            return code;
        }

        public bool ValidateCode(string email, string inputCode)
        {
            if (_verifications.TryGetValue(email, out var data))
            {
                if (DateTime.UtcNow > data.Expiration) return false;
                if (data.Code == inputCode)
                {
                    _verifiedEmails.Add(email);
                    _verifications.TryRemove(email, out _);
                    return true;
                }
            }
            return false;
        }

        public bool IsEmailVerified(string email)
        {
            return _verifiedEmails.Contains(email);
        }

        public void RemoveVerifiedEmail(string email)
        {
            _verifiedEmails.Remove(email);
        }
    }
}
