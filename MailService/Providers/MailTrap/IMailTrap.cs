using MailService.Model;

namespace MailService.Providers.MailTrap
{
	public interface IMailTrap
	{
		Task SendAsync(EmailModel model);
	}
}
