using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.CreateReligion
{
    public class CreateReligionCommandHandler : BaseHandler, IRequestHandler<CreateReligionCommandRequest, Guid>
    {
        public CreateReligionCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(CreateReligionCommandRequest request, CancellationToken cancellationToken)
        {
    
            var religion = new Religion
            {
                Name = request.Name,
                NameInOriginalLanguage = request.NameInOriginalLanguage,
                ShortName = request.ShortName,
                Description = request.Description,
                MainBranch = request.MainBranch,
                FoundedYear = request.FoundedYear,
                FounderPersonId = request.FounderPersonId,
                FoundedBy = request.FoundedBy,
                FoundedLocationId = request.FoundedLocationId,
                FoundedLocationName = request.FoundedLocationName,
                ApproximateFollowers = request.ApproximateFollowers,
                IconUrl = request.IconUrl,
                ColorCode = request.ColorCode,
                WikipediaUrl = request.WikipediaUrl,
                IsActive = true
            };

            await unitOfWork.GetWriteRepository<Religion>().AddAsync(religion);
            await unitOfWork.SaveAsync(); 

          
            if (request.HolyBooks?.Any() == true)
            {
                var holyBooks = request.HolyBooks.Select(hb => new ReligionHolyBook
                {
                    ReligionId = religion.Id,
                    Name = hb.Name,
                    OriginalName = hb.OriginalName,
                    Language = hb.Language,
                    Description = hb.Description,
                    WikipediaUrl = hb.WikipediaUrl,
                    IsActive = true
                }).ToList();

                await unitOfWork.GetWriteRepository<ReligionHolyBook>().AddRangeAsync(holyBooks);
            }

   
            if (request.HolyCities?.Any() == true)
            {
                var holyCities = request.HolyCities.Select(hc => new ReligionHolyCity
                {
                    ReligionId = religion.Id,
                    CityName = hc.CityName,
                    Significance = hc.Significance,
                    Description = hc.Description,
                    LocationId = hc.LocationId,
                    IsActive = true
                }).ToList();

                await unitOfWork.GetWriteRepository<ReligionHolyCity>().AddRangeAsync(holyCities);
            }

            if (request.SubBranches?.Any() == true)
            {
                var subBranches = request.SubBranches.Select(sb => new ReligionSubBranch
                {
                    ReligionId = religion.Id,
                    Name = sb.Name,
                    NameInOriginalLanguage = sb.NameInOriginalLanguage,
                    Description = sb.Description,
                    FoundedYear = sb.FoundedYear,
                    WikipediaUrl = sb.WikipediaUrl,
                    IsActive = true
                }).ToList();

                await unitOfWork.GetWriteRepository<ReligionSubBranch>().AddRangeAsync(subBranches);
            }

            await unitOfWork.SaveAsync();

            return religion.Id;
        }
    }
}