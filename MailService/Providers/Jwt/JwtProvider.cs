using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MailService.Providers.Jwt
{
	public class JwtProvider : IJwtProvider
	{
		private readonly string _issuer = "AuthService";
		private readonly string _audience = "User";
		private readonly TokenValidationParameters _validationParameters;
		private readonly SymmetricSecurityKey _symmetricSecurityKey = new(Encoding.ASCII.GetBytes("chave-secreta-com-mais-de-256-bits"));

		public JwtProvider()
		{

			_validationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _symmetricSecurityKey,
				ValidIssuer = _issuer,
				ValidAudience = _audience
			};

		}
		public bool IsTokenValid(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				tokenHandler.ValidateToken(token, _validationParameters, out _);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
