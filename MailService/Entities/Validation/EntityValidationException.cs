namespace MailService.Entities.Validation
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string error) : base(error) { }

        public static void Validation(bool isError, string message)
        {
            if (isError)
            {
                throw new EntityValidationException(message);
            }
        }
    }
}
