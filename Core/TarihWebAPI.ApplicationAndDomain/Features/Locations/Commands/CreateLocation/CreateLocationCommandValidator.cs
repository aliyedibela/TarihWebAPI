using FluentValidation;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLocationCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lokasyon adı zorunludur.")
                .MaximumLength(200).WithMessage("Lokasyon adı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Lokasyon tipi zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Enlem -90 ile 90 arasında olmalıdır.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Boylam -180 ile 180 arasında olmalıdır.");

            RuleFor(x => x.ParentLocationId)
                .MustAsync(ParentLocationExists)
                .When(x => x.ParentLocationId.HasValue)
                .WithMessage("Belirtilen üst lokasyon bulunamadı.");

            RuleFor(x => x)
                .MustAsync(BeUniqueLocation)
                .WithMessage("Bu koordinatta aynı isimde bir lokasyon zaten kayıtlı.");
        }

        private async Task<bool> ParentLocationExists(Guid? parentId, CancellationToken token)
        {
            if (!parentId.HasValue) return true;
            var count = await _unitOfWork.GetReadRepository<Location>()
                .CountAsync(l => l.Id == parentId.Value && l.IsActive);
            return count > 0;
        }

        private async Task<bool> BeUniqueLocation(CreateLocationCommandRequest req, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<Location>()
                .CountAsync(l => l.Name == req.Name &&
                                 l.Latitude == req.Latitude &&
                                 l.Longitude == req.Longitude &&
                                 l.IsActive);
            return count == 0;
        }
    }
}