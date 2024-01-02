using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Primitives;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
	await next(context);
});

app.UseWhen(context => { return (context.Request.Method == "POST" && context.Request.Path == "/"); },
	app =>
	{
		app.UseCheckLogin();
	});

app.UseWhen(context => { return (context.Request.Method == "GET" && context.Request.Path == "/"); },
	app =>
	{
		// 6: get => 200 + No response
		app.UseMethodEqualsGet();
	});

app.Run(async (HttpContext context) =>
{
	await context.Response.WriteAsync("termination absolution\n");
});

app.Run();

public class MyCustomLogin
{
	private readonly RequestDelegate _next;

	public MyCustomLogin(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		// before logic

		// read body
		StreamReader reader = new StreamReader(context.Request.Body);
		var body = await reader.ReadToEndAsync();

		Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

		// 1: post && valid mail && valid password => 200 OK + Succesful login
		if (queryDict.ContainsKey("email") && queryDict.ContainsKey("password"))
		{
			var email = queryDict["email"][0];
			var password = queryDict["password"][0];

			if (email == "admin@example.com" && password == "admin1234")
			{
				await context.Response.WriteAsync($"Email: {email}\nPassword: {password}\nSuccesful login\n");
			}

			// 2: post + invalid mail || invalid password => 400 + Invalid login
			if (email != "admin@example.com" || password != "admin1234")
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync($"Invalid login\n");
			}
		}
		// 3: post + no mail && no password => 400 + Invalid input for 'email' + Invalid input for 'password'
		else if (!queryDict.ContainsKey("email") && !queryDict.ContainsKey("password"))
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsync("Invalid input for 'email'\nInvalid input for 'password'\n");
		}
		// 4: post + mail && no password => 400 + Invalid input for 'password'
		else if (queryDict.ContainsKey("email") && !queryDict.ContainsKey("password"))
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsync("Invalid input for password\n");
		}
		// 5: post + no mail && password => 400 + Invalid input for 'password'
		else if (!queryDict.ContainsKey("email") && queryDict.ContainsKey("password"))
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsync("Invalid input for 'email'\n");
		}

		await _next(context);
		// after logic
	}
}
public static class MyCustomLoginExtension
{
	public static IApplicationBuilder UseCheckLogin(this IApplicationBuilder app)
	{
		return app.UseMiddleware<MyCustomLogin>();
	}
}


public class MethodEqualsGet
{
	private readonly RequestDelegate _next;

	public MethodEqualsGet(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		await context.Response.WriteAsync("No response\n");
	}
}

public static class MethodEqualsGetExtensions
{
	public static IApplicationBuilder UseMethodEqualsGet(this IApplicationBuilder app)
	{
		return app.UseMiddleware<MethodEqualsGet>();
	}
}



