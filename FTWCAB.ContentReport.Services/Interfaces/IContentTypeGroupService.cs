using FTWCAB.ContentReport.Models.Api;

namespace FTWCAB.ContentReport.Services.Interfaces
{
    public interface IContentTypeGroupService
    {
        IEnumerable<ContentTypeGroupViewModel> GetContentTypeGroups();
    }
}
