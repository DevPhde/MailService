using MailService.Entities;

namespace MailService.Services
{
	public interface IMailService
	{
		void SendMail();
		Task<List<Mail?>> GetAll();
		Task<List<Mail?>> GetMailByUserEmail(string email);
	}
}
