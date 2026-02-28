using FluentValidation;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.CreateEra
{
    public class CreateEraCommandValidator : AbstractValidator<CreateEraCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEraCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Çağ adı zorunludur.")
                .MaximumLength(200)
                .MustAsync(BeUniqueName).WithMessage("Bu isimde bir çağ zaten kayıtlı.");

            RuleFor(x => x.StartYear)
                .NotEmpty().WithMessage("Başlangıç yılı zorunludur.");

            RuleFor(x => x.EndYear)
                .GreaterThan(x => x.StartYear)
                .When(x => x.EndYear.HasValue)
                .WithMessage("Bitiş yılı başlangıç yılından büyük olmalıdır.");

            RuleFor(x => x.ColorCode)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorCode))
                .WithMessage("Renk kodu #RRGGBB formatında olmalıdır.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<Era>()
                .CountAsync(e => e.Name == name && e.IsActive);
            return count == 0;
        }
    }
}