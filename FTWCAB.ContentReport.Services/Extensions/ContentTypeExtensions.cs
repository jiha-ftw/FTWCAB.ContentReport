using EPiServer.DataAbstraction;
using FTWCAB.ContentReport.Models.Api;

namespace FTWCAB.ContentReport.Services.Extensions
{
    public static class ContentTypeExtensions
    {
        public static IEnumerable<ContentTypeViewModel> ToContentTypeViewModels(this IEnumerable<ContentType> contentTypes)
        {
            return contentTypes
                .OrderBy(ct => ct.Name)
                .Select(ct => new ContentTypeViewModel(ct.ID, ct.Name, ct.LocalizedFullName));
        }
    }
}
