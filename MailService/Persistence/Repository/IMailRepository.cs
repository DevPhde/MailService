using MailService.Entities;

namespace MailService.Persistence.Repository
{
    public interface IMailRepository
    {
        void Create(Mail mail);
        Task<List<Mail?>> FindMailByUserEmail(string userEmail);
        Task<List<Mail>> GetAll();
    }
}
