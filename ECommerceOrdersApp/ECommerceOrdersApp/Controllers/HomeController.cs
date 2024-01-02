using ECommerceOrdersApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOrdersApp.Controllers
{
	public class HomeController : Controller
	{
		[Route("order")]
		public IActionResult Index([Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))] Order order)
		{

			if (ModelState.IsValid == false)
			{
				var errors = string.Join("\n", ModelState.Values.SelectMany(values => values.Errors).Select(err => err.ErrorMessage));

				return BadRequest(errors);
			}

			return Json(new {OrderNumber = order.OrderNo});

		}
	}
}
