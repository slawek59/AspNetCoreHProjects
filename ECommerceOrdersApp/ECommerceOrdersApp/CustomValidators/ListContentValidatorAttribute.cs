using ECommerceOrdersApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceOrdersApp.CustomValidators
{
	public class ListContentValidatorAttribute : ValidationAttribute
	{
		public int MinimalNumberOfItems { get; set; } = 1;
		public string DefaultErrorMessage { get; set; } = "At least one Product required";

		public ListContentValidatorAttribute()
		{

		}


		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{

			if (value != null)
			{
				var products = (List<Product>)value;

				if (products.Count == 0)
				{
					return new ValidationResult(DefaultErrorMessage);
				}
				else
				{
					return ValidationResult.Success;
				}

			}
			return null;

		}
	}
}