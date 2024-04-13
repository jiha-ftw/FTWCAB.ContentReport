using FTWCAB.ContentReport.Models.Models.Api;

namespace FTWCAB.ContentReport.Services.Interfaces
{
    public interface IContentInstanceUsageService
    {
        ContentUsagesModel GetUsages(int contentInstanceId, int page, int pageSize);
    }
}
