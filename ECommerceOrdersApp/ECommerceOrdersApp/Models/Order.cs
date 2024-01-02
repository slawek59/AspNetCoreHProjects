using ECommerceOrdersApp.CustomValidators;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ECommerceOrdersApp.Models
{
	public class Order 
	{
		//[Required(ErrorMessage = "{0} cannot be blank")]
		[Display(Name = "Order Number")]
		public int? OrderNo { get; set; }

		[Required(ErrorMessage = "OrderDate cannot be blank")]
		[MinimumYearValidator]
		public DateTime? OrderDate { get; set; }

		[ListContentValidator] 
		public List<Product>? Products { get; set; } = new List<Product>();

		[Required(ErrorMessage = "{0} cannot be blank")]
		[Display(Name = "Invoice Price")]
		[InvoiceValidator("Products")]
		[Range(1, double.MaxValue)]
		public double InvoicePrice { get; set; }

        public Order()
        {
			OrderNo = RandomNumberGenerator.GetInt32(int.MaxValue);
		}

	}
}
