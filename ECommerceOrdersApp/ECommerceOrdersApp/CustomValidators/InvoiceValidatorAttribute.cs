using ECommerceOrdersApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerceOrdersApp.CustomValidators
{
	public class InvoiceValidatorAttribute : ValidationAttribute
	{
		public string? OtherPropertyName { get; set; }
		string DefaultErrorMessage = "Invoice does not match";

		public InvoiceValidatorAttribute(string otherPropertyName)
		{
			OtherPropertyName = otherPropertyName;
		}

		public InvoiceValidatorAttribute(string otherPropertyName, string errorMessage)
		{
			OtherPropertyName = otherPropertyName;
			ErrorMessage = errorMessage;
		}


		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			double? CheckInvoicePrice = 0;

			if (value != null)
			{

				PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

				if (otherProperty != null)
				{
					var products = (List<Product>)otherProperty.GetValue(validationContext.ObjectInstance);

					if (products != null)
					{

						var fromAttribute = (double?)value;

						foreach (var item in products)
						{
							var itemPrice = item.Price * item.Quantity;
							CheckInvoicePrice += itemPrice;
						}

						if (CheckInvoicePrice != fromAttribute)
						{
							return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage));
						}
						else
						{
							return ValidationResult.Success;
						}
					}
				}

			}

			return null;
		}
	}
}
