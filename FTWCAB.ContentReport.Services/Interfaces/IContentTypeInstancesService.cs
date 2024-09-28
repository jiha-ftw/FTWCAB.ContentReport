using FTWCAB.ContentReport.Models.Models.Api;

namespace FTWCAB.ContentReport.Services.Interfaces
{
    public interface IContentTypeInstancesService
    {
        ContentInstancesModel GetInstances(int contentTypeId, string languageId, int page, int pageSize);
    }
}
