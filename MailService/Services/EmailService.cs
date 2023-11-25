using MailService.Entities;
using MailService.Exceptions;
using MailService.Model;
using MailService.Persistence.Repository;
using MailService.Providers.MailTrap;

namespace MailService.Services
{
	public class EmailService : IEmailService
	{
		private readonly IMailRepository _mailRepository;
		private readonly IMailTrap _mailtrap;
		public EmailService(IMailRepository mailRepository, IMailTrap mailtrap)
		{
			_mailRepository = mailRepository;
			_mailtrap = mailtrap;
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

		public async void SendMail(Mail mail)
		{

			try
			{
				EmailModel mailModel = new(mail.Email, mail.MailType, mail.Name, mail.Message);
				await _mailtrap.SendAsync(mailModel);
			}
			catch (Exception)
			{
				mail.setSentSuccessfully(false);
			}
			finally
			{
				_mailRepository.Create(mail);
			}
		}
	}
}
