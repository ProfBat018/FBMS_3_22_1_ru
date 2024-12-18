namespace ApiGateway.Middlewares;

public class CookieMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Cookies["accessToken"];
        
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        return next(context);
    }
}





