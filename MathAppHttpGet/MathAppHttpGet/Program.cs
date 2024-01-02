using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
	if (context.Request.Method == "GET" && context.Request.Path == "/")
	{

		var reader = context.Request.QueryString.ToString();

		Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(reader);

		if (queryDict.ContainsKey("firstnumber") && queryDict.ContainsKey("secondnumber") && queryDict.ContainsKey("operation"))
		{

			var firstNumber = int.Parse(queryDict["firstnumber"]);
			var secondNumber = int.Parse(queryDict["secondnumber"]);
			var operation = queryDict["operation"];

			switch (operation)
			{
				case "add":
					await context.Response.WriteAsync($"\n{firstNumber}+{secondNumber}={firstNumber + secondNumber}");
					break;

				case "subtract":
					await context.Response.WriteAsync($"\n{firstNumber}-{secondNumber}={firstNumber - secondNumber}");
					break;

				case "multiply":
					await context.Response.WriteAsync($"\n{firstNumber}*{secondNumber}={firstNumber * secondNumber}");
					break;


				case "divide":
					await context.Response.WriteAsync($"\n{firstNumber}/{secondNumber}={firstNumber / secondNumber}");
					break;

				case "modulo":
					await context.Response.WriteAsync($"\n{firstNumber}%{secondNumber}={firstNumber % secondNumber}");
					break;

				default:
					context.Response.StatusCode = 400;
					await context.Response.WriteAsync("Invalid input for 'operation'\n");
					break;
			}
		}
		else if(!queryDict.ContainsKey("firstnumber") && !queryDict.ContainsKey("secondnumber") && !queryDict.ContainsKey("operation"))
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
			await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
			await context.Response.WriteAsync("Invalid input for 'operation'\n");
		}
		else
		{
			context.Response.StatusCode = 400;

			if (!queryDict.ContainsKey("firstnumber"))
			{
				await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
			}
			if (!queryDict.ContainsKey("secondnumber"))
			{
				await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
			}
			if (!queryDict.ContainsKey("operation"))
			{
				await context.Response.WriteAsync("Invalid input for 'operation'\n");
			}
		}
	}
});

app.Run();
