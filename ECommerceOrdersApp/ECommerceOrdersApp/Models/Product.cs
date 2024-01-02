using System.ComponentModel.DataAnnotations;

namespace ECommerceOrdersApp.Models
{
	public class Product
	{
		[Required(ErrorMessage = "{0} cannot be blank")]
		[Display(Name = "Product Code")]
		[Range(1, int.MaxValue)]
		public int? ProductCode { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank")]
		[Display(Name = "Product Price")]
		[Range (1, double.MaxValue)]
		public double? Price { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank")]
		[Display(Name = "Product Quantity")]
		[Range(1, int.MaxValue)]
		public int? Quantity { get; set; }

    }
}
