using ShagOxServer.Application.DTOs.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
public interface IImageQueryService
    : IQueryService<ImageDto, Image, ImageSearchFilter>
{
}