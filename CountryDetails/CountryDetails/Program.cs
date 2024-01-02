var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseRouting();

Dictionary<int, string> countries = new Dictionary<int, string>() 
{ 
	{ 1, "USA" }, 
	{2, "Canada" },
	{3, "United Kingdom" },
	{4, "India" }, 
	{5, "Japan" } 
};

app.UseEndpoints(endpoints =>
{
	endpoints.MapGet("/countries", async context =>
	{
		foreach (var country in countries)
		{
			await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
		}
	});

	endpoints.MapGet("/countries/{countryID:int:range(1,100)}", async context =>
	{
		var countryId = Convert.ToInt32(context.Request.RouteValues["countryID"]);

		if (countries.ContainsKey(countryId))
		{
			await context.Response.WriteAsync(countries[countryId]);
		}
		else
		{
			context.Response.StatusCode = 404;
			await context.Response.WriteAsync("[No country]");
		}
	});

	endpoints.MapGet("/countries/{countryID:int:min(101)}", async context =>
	{
		context.Response.StatusCode = 400;
		await context.Response.WriteAsync("The CountryID should be between 1 and 100\n");
	});
});

app.Run(async context =>
{
	await context.Response.WriteAsync("No response\n");
});

app.Run();
