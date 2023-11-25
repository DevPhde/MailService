using dotenv.net;
using MailService.Model;
using System.Net;
using System.Net.Mail;


namespace MailService.Providers.MailTrap
{
	public class MailTrap : IMailTrap
	{
		public MailTrap()
		{
			DotEnv.Load();
		}

		private SmtpClient SmtpClient { get; set; }

		public async Task SendAsync(EmailModel model)
		{
			SmtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
			{
				Credentials = new NetworkCredential("33f82f1b6226e5", "a7de3a13e76eb7"),
				EnableSsl = true,
			};

			await Task.Run(() =>
			{
				MailMessage mailMessage = new()
				{
					From = new MailAddress("noreply.teste@teste.com"),
					To = { model.Receiver },
					Subject = model.Subject,
					Body = model.Content,
					IsBodyHtml = true
				};

				try
				{
					Console.WriteLine(Environment.GetEnvironmentVariable("MAILTRAP_USERNAME"));
					SmtpClient.Send(mailMessage);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					throw;
				}
			});
		}
	}
}
