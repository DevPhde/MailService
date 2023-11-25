using MailService.Exceptions;
using MailService.Providers.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MailService.Middlewares
{
	public class AuthorizationMiddleware : TypeFilterAttribute
	{
		public AuthorizationMiddleware() : base(typeof(AuthorizationMiddlewareFilter))
		{
		}

		public class AuthorizationMiddlewareFilter : IAsyncActionFilter
		{
			private readonly IJwtProvider _jwtProvider;

			public AuthorizationMiddlewareFilter(IJwtProvider jwtProvider)
			{
				_jwtProvider = jwtProvider;
			}
			public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
			{
				try
				{
					string authorization = context.HttpContext.Request.Headers["Authorization"];
					if (string.IsNullOrWhiteSpace(authorization) || !_jwtProvider.IsTokenValid(authorization))
					{
						var unauthorizedResult = new ObjectResult("Invalid or missing JWT token")
						{
							StatusCode = 401
						};
						context.Result = unauthorizedResult;
						return;
					}
					else
					{
						await next();
					}
				}
				catch (InternalErrorException)
				{
					var internalError = new ObjectResult("Internal Error, contact the support.")
					{
						StatusCode = 500,
					};
					context.Result = internalError;
					return;

				}
			}
		}
	}
}
