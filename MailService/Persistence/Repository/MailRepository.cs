using Dapper;
using MailService.Entities;
using MailService.Exceptions;
using MailService.Persistence.Context;

namespace MailService.Persistence.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly DapperContext _dapperContext;
        public MailRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public void Create(Mail mail)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    string query = "INSERT INTO mail (userId, name, email, mailType, message, createdAt, sentSuccessfully) VALUES (@UserId, @Name, @Email, @MailType, @Message, @CreatedAt, @SentSuccessfully)";
                    connection.Execute(query, mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new InternalErrorException("Internal Error, try again later.");
                }
            }
        }

        public async Task<List<Mail?>> FindMailByUserEmail(string userEmail)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    string query = "SELECT * FROM mail WHERE Email Like @Email";
                    return (List<Mail?>)await connection.QueryAsync<Mail>(query, new { Email = $"{userEmail}%" });
                }
                catch
                {
                    throw new InternalErrorException("Internal Error, try again later.");
                }
            }
        }

        public async Task<List<Mail?>> GetAll()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    string query = "SELECT * FROM mail";
                    return (List<Mail?>)await connection.QueryAsync<Mail?>(query);

                }
                catch
                {
                    throw new InternalErrorException("Internal Error, try again later.");
                }
            }
        }
    }
}
