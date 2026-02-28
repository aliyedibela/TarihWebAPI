using FluentValidation;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLocationCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Güncellenecek lokasyon ID'si zorunludur.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lokasyon adı zorunludur.")
                .MaximumLength(200);

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Lokasyon tipi zorunludur.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Enlem -90 ile 90 arasında olmalıdır.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Boylam -180 ile 180 arasında olmalıdır.");
            RuleFor(x => x)
                .Must(x => x.ParentLocationId != x.Id)
                .WithMessage("Bir lokasyon kendisinin üst lokasyonu olamaz.");

            RuleFor(x => x.ParentLocationId)
                .MustAsync(ParentLocationExists)
                .When(x => x.ParentLocationId.HasValue)
                .WithMessage("Belirtilen üst lokasyon bulunamadı.");
        }

        private async Task<bool> ParentLocationExists(Guid? parentId, CancellationToken token)
        {
            if (!parentId.HasValue) return true;
            var count = await _unitOfWork.GetReadRepository<Location>()
                .CountAsync(l => l.Id == parentId.Value && l.IsActive);
            return count > 0;
        }
    }
}