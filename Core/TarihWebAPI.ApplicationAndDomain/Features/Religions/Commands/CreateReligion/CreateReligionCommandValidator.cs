using FluentValidation;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.CreateReligion
{
    public class CreateReligionCommandValidator : AbstractValidator<CreateReligionCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReligionCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Din adı zorunludur.")
                .MaximumLength(200)
                .MustAsync(BeUniqueName).WithMessage("Bu isimde bir din zaten kayıtlı.");

            RuleFor(x => x.ColorCode)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorCode))
                .WithMessage("Renk kodu #RRGGBB formatında olmalıdır.");

       
            RuleFor(x => x.FounderPersonId)
                .MustAsync(PersonExists)
                .When(x => x.FounderPersonId.HasValue)
                .WithMessage("Belirtilen kurucu kişi bulunamadı.");

    
            RuleFor(x => x.FoundedLocationId)
                .MustAsync(LocationExists)
                .When(x => x.FoundedLocationId.HasValue)
                .WithMessage("Belirtilen kuruluş lokasyonu bulunamadı.");

            RuleFor(x => x)
                .Must(x => x.FounderPersonId.HasValue || !string.IsNullOrEmpty(x.FoundedBy) || x.FoundedYear == null)
                .WithMessage("Kurucu bilgisi girilmişse FounderPersonId veya FoundedBy alanlarından biri dolu olmalıdır.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<Religion>()
                .CountAsync(r => r.Name == name && r.IsActive);
            return count == 0;
        }

        private async Task<bool> PersonExists(Guid? personId, CancellationToken token)
        {
            if (!personId.HasValue) return true;
            var count = await _unitOfWork.GetReadRepository<Person>()
                .CountAsync(p => p.Id == personId.Value && p.IsActive);
            return count > 0;
        }

        private async Task<bool> LocationExists(Guid? locationId, CancellationToken token)
        {
            if (!locationId.HasValue) return true;
            var count = await _unitOfWork.GetReadRepository<Location>()
                .CountAsync(l => l.Id == locationId.Value && l.IsActive);
            return count > 0;
        }
    }
}