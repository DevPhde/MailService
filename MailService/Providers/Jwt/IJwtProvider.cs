namespace MailService.Providers.Jwt
{
	public interface IJwtProvider
	{
		bool IsTokenValid(string token);
	}
}
