using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.CreateTradeRoute
{
    public class CreateTradeRouteCommandValidator : AbstractValidator<CreateTradeRouteCommandRequest>
    {
        public CreateTradeRouteCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(300).WithMessage("Name cannot exceed 300 characters.");
            RuleFor(x => x.AlternativeName)
                .MaximumLength(300).WithMessage("Alternative Name cannot exceed 300 characters.");
            RuleFor(x => x.StartYear)
                .NotEmpty().WithMessage("Start Year is required.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Start Year cannot be in the future.");
            RuleFor(x => x.EndYear)
                .GreaterThanOrEqualTo(x => x.StartYear).When(x => x.EndYear.HasValue)
                .WithMessage("End Year must be greater than or equal to Start Year.");
            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");
            RuleFor(x => x.MainGoods)
                .MaximumLength(1000).WithMessage("Main Goods cannot exceed 1000 characters.");
            RuleFor(x => x.MapUrl)
                .MaximumLength(500).WithMessage("Map URL cannot exceed 500 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).When(x => !string.IsNullOrEmpty(x.MapUrl))
                .WithMessage("Map URL must be a valid URL.");
            RuleFor(x => x.WikipediaUrl)
                .MaximumLength(500).WithMessage("Wikipedia URL cannot exceed 500 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).When(x => !string.IsNullOrEmpty(x.WikipediaUrl))
                .WithMessage("Wikipedia URL must be a valid URL.");
        }
    }
}
