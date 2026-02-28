using FluentValidation;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.UpdateState
{
    public class UpdateStateCommandValidator : AbstractValidator<UpdateStateCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStateCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Güncellenecek devlet ID'si zorunludur.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Devlet adı zorunludur.")
                .MaximumLength(200)
                .MustAsync(BeUniqueNameExcludingSelf).WithMessage("Bu isimde başka bir devlet zaten kayıtlı.");

            RuleFor(x => x.ColorCode)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorCode))
                .WithMessage("Renk kodu #RRGGBB formatında olmalıdır.");

            RuleFor(x => x.EndYear)
                .GreaterThan(x => x.StartYear)
                .When(x => x.StartYear.HasValue && x.EndYear.HasValue)
                .WithMessage("Bitiş yılı başlangıç yılından büyük olmalıdır.");
        }

        private async Task<bool> BeUniqueNameExcludingSelf(UpdateStateCommandRequest req, string name, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<State>()
                .CountAsync(s => s.Name == name && s.Id != req.Id && s.IsActive);
            return count == 0;
        }
    }
}