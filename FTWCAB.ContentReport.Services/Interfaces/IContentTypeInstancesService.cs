using FTWCAB.ContentReport.Models.Models.Api;

namespace FTWCAB.ContentReport.Services.Interfaces
{
    public interface IContentTypeInstancesService
    {
        ContentInstancesModel GetInstances(int contentTypeId, int page, int pageSize);
    }
}
