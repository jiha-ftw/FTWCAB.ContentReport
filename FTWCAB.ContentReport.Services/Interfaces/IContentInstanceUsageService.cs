using FTWCAB.ContentReport.Models.Models.Api;

namespace FTWCAB.ContentReport.Services.Interfaces;

public interface IContentInstanceUsageService
{
    ContentUsagesModel GetUsages(int contentInstanceId, string languageId, int page, int pageSize);
}
