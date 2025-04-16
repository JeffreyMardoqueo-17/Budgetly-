using System.Threading.Tasks;

namespace Budgetly.service.Email
{
    public interface IEmailVerificationService
    {
        Task<string> GenerateVerificationCodeAsync(string email);
        bool ValidateCode(string email, string inputCode);

        // 🚨  email verificado y quitar verificasion
        bool IsEmailVerified(string email);
        void RemoveVerifiedEmail(string email);
    }
}
