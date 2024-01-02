using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
	public class AccountController : Controller
	{

		[Route("/")]
		public IActionResult Index()
		{
			return Content("<p>Welcome to the Best Bank</p>", "text/html");
		}

		[Route("/account-details")]
		public IActionResult AccountDetails()
		{
			var bankAccount = new
			{
				accountNumber = 1001,
				accountHolderName = "Example Name",
				currentBalance = 5000,
			};
			return Json(bankAccount);
		}

		[Route("/account-statement")]
		public IActionResult AccountStatement()
		{
			return File("/docs.pdf", "application/pdf");
		}

		[Route("/get-current-balance/{accountNumber:int?}")]
		public IActionResult GetCurrentBalace()
		
		{

			//object accountNumberObj;
			//if (HttpContext.Request.RouteValues.TryGetValue("accountNumber", out accountNumberObj) && 
			//accountNumberObj is string accountNumber)
			
				if (string.IsNullOrEmpty(Convert.ToString(Request.RouteValues["accountNumber"])))
			{
				return NotFound("Account Number should be supplied");
			}
			else if (Convert.ToInt32(Request.RouteValues["accountNumber"]) == 1001)
			{
				var bankAccount = new
				{
					accountNumber = 1001,
					accountHolderName = "Example Name",
					currentBalance = 5000,
				};
				return Content($"Current Balance: {bankAccount.currentBalance.ToString()}", "text/plain");

			}
			return BadRequest("Account Number should be 1001");

		}
	}
}
