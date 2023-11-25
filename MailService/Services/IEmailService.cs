using MailService.Entities;

namespace MailService.Services
{
	public interface IEmailService
	{
		void SendMail(Mail mail);
		Task<List<Mail?>> GetAll();
		Task<List<Mail?>> GetMailByUserEmail(string email);
	}
}
