namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
public interface IPictureCache<TPicture>
{
    Task InvalidateCache(
        TPicture image);
}
