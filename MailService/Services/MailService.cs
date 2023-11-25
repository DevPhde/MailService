using MailService.Entities;
using MailService.Persistence.Repository;

namespace MailService.Services
{
	public class MailService : IMailService
	{
		private readonly IMailRepository _mailRepository;
		public MailService(IMailRepository mailRepository)
		{
			_mailRepository = mailRepository;
		}

		public async Task<List<Mail?>> GetAll()
		{
			try
			{
				return await _mailRepository.GetAll();
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<Mail?>> GetMailByUserEmail(string email)
		{
			try
			{
				return await _mailRepository.FindMailByUserEmail(email);
			}
			catch
			{
				throw;
			}
		}

		public void SendMail()
		{

		}

	}
}
