namespace MailService.Model
{
	public enum EmailTypeEnum
	{
		Welcome = 0,
		RecoveryPassword = 1
	}
	public class EmailModel
	{
		public string Subject { get; private set; }
		public string Content { get; private set; }
		public string Receiver { get; private set; }

		public EmailModel(string receiver, EmailTypeEnum type, string name, string? message = null)
		{
			Receiver = receiver;
			switch (type)
			{
				case EmailTypeEnum.Welcome:
					Subject = "Welcome to Our Community!";
					Content = $"Hello {name},<br/><br/>" +
						  "Welcome to our service! We are excited that you've joined us. <br/>" +
						  $"To confirm your account, please click on the following link:<br/><br/>" +
						  $"<a href='{message}'>Confirm Account</a>";
					break;
				case EmailTypeEnum.RecoveryPassword:
					Subject = "Password Recovery";
					Content = $"Hello {name},\n\nYou have requested a password recovery. Your new password is: {message}\n\nPlease make sure to securely store this password and consider changing it after logging in.";
					break;
			}
		}
	}
}
