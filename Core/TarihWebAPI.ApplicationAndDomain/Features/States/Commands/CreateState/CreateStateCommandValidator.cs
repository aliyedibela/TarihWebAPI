using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.CreateState
{
    public class CreateStateCommandValidator : AbstractValidator<CreateStateCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStateCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Devlet adı zorunludur.")
                .MaximumLength(200).WithMessage("Devlet adı en fazla 200 karakter olabilir.")
                .MustAsync(BeUniqueName).WithMessage("Bu isimde bir devlet zaten kayıtlı.");

            RuleFor(x => x.ColorCode)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorCode))
                .WithMessage("Renk kodu #RRGGBB formatında olmalıdır.");

            RuleFor(x => x.EndYear)
                .GreaterThan(x => x.StartYear)
                .When(x => x.StartYear.HasValue && x.EndYear.HasValue)
                .WithMessage("Bitiş yılı başlangıç yılından büyük olmalıdır.");

            RuleFor(x => x.CapitalLocationId)
                .MustAsync(CapitalLocationExists)
                .When(x => x.CapitalLocationId.HasValue)
                .WithMessage("Belirtilen başkent lokasyonu bulunamadı.");

            RuleFor(x => x.OfficialReligionId)
                .MustAsync(ReligionExists)
                .When(x => x.OfficialReligionId.HasValue)
                .WithMessage("Belirtilen din bulunamadı.");

            RuleFor(x => x.OfficialLanguages)
                .Must(langs => langs == null || langs.Count == 0 || langs.Any(l => l.IsPrimary))
                .WithMessage("En az bir dil 'birincil' olarak işaretlenmelidir.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<State>()
                .CountAsync(s => s.Name == name && s.IsActive);
            return count == 0;
        }

        private async Task<bool> CapitalLocationExists(Guid? locationId, CancellationToken token)
        {
            if (!locationId.HasValue) return true;
            var count = await _unitOfWork.GetReadRepository<Location>()
                .CountAsync(l => l.Id == locationId.Value && l.IsActive);
            return count > 0;
        }

        private async Task<bool> ReligionExists(Guid? religionId, CancellationToken token)
        {
            if (!religionId.HasValue) return true;
            var count = await _unitOfWork.GetReadRepository<Religion>()
                .CountAsync(r => r.Id == religionId.Value && r.IsActive);
            return count > 0;
        }
    }
}