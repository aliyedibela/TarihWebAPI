using FluentValidation;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.UpdateEra
{
    public class UpdateEraCommandValidator : AbstractValidator<UpdateEraCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEraCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Güncellenecek çağ ID'si zorunludur.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Çağ adı zorunludur.")
                .MaximumLength(200)
                .MustAsync(BeUniqueNameExcludingSelf).WithMessage("Bu isimde başka bir çağ zaten kayıtlı.");

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

        private async Task<bool> BeUniqueNameExcludingSelf(UpdateEraCommandRequest req, string name, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<Era>()
                .CountAsync(e => e.Name == name && e.Id != req.Id && e.IsActive);
            return count == 0;
        }
    }
}