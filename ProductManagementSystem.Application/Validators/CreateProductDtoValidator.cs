using FluentValidation;
using ProductManagementSystem.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Application.Validators
{
    public class CreateProductDtoValidator
    : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.ProductName)
     .NotNull()
     .NotEmpty()
     .Must(x => !string.IsNullOrWhiteSpace(x))
     .WithMessage("Product Name cannot be empty or whitespace.");
        }
    }
}
