using MailService.Entities.Validation;
using MailService.Model;
using System.Text.RegularExpressions;

namespace MailService.Entities
{
    public sealed class Mail
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public EmailTypeEnum MailType { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public bool SentSuccessfully { get; private set; } = true;

        public void setSentSuccessfully(bool isSuccess)
        {
			SentSuccessfully = isSuccess;
        }

        public Mail(int userId, string name, string email, EmailTypeEnum mailType, string message)
        {
            PropertiesValidation(userId, name, email, message);
            MailType = mailType;
            CreatedAt = DateTime.Now;
			SentSuccessfully = true;
        }
        public Mail()
        {

        }
        private void PropertiesValidation(int userId, string name, string email, string message)
        {
            EntityValidationException.Validation(userId < 0, "Invalid User Id");
            EntityValidationException.Validation(string.IsNullOrWhiteSpace(name) || name.Length < 3, "Invalid Name. Property Name he must have 3 or more letters.");
            EntityValidationException.Validation(string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"), "Invalid Email. Valid email is required.");
            EntityValidationException.Validation(string.IsNullOrWhiteSpace(message) || message.Length < 3, "Invalid Message. Property Message need 3 or more letters.");

            UserId = userId;
            Name = name;
            Email = email;
            Message = message;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
