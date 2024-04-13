using EPiServer.Core;
using EPiServer.DataAbstraction;
using FTWCAB.ContentReport.Services.Interfaces;
using FTWCAB.ContentReport.Models.Api;
using FTWCAB.ContentReport.Services.Extensions;

namespace FTWCAB.ContentReport.Services
{
    public class ContentTypeGroupService : IContentTypeGroupService
    {
        private readonly IContentTypeRepository contentTypeRepository;

        public ContentTypeGroupService(IContentTypeRepository contentTypeRepository)
        {
            this.contentTypeRepository = contentTypeRepository;
        }

        public IEnumerable<ContentTypeGroupViewModel> GetContentTypeGroups()
        {
            var contentTypes = contentTypeRepository.List().ToList();

            var pageContentTypes = FilterByType<PageData>(contentTypes).ToList();
            var blockContentTypes = FilterByType<BlockData>(contentTypes).ToList();
            var mediaContentTypes = FilterByType<MediaData>(contentTypes).ToList();

            var contentGroups = new List<ContentTypeGroupViewModel>
            {
                new("Page", pageContentTypes),
                new("Block", blockContentTypes),
                new("Media",  mediaContentTypes),
                new("Misc", FilterMisc(contentTypes, pageContentTypes, blockContentTypes, mediaContentTypes).ToList()),
            }
            .Where(g => g.Options.Any())
            .ToList();

            return contentGroups;
        }

        private static IEnumerable<ContentTypeViewModel> FilterByType<T>(IEnumerable<ContentType> contentTypes)
        {
            return contentTypes
                .Where(ct => typeof(T).IsAssignableFrom(ct.ModelType))
                .ToContentTypeViewModels();
        }

        private static IEnumerable<ContentTypeViewModel> FilterMisc(
            IEnumerable<ContentType> contentTypes,
            params IEnumerable<ContentTypeViewModel>[] exclude)
        {
            var excludeList = exclude.SelectMany(x => x.Select(y => y.Id)).ToList();

            return contentTypes
                .Where(ct => !excludeList.Any(excludedId => excludedId.Equals(ct.ID)))
                .ToContentTypeViewModels();
        }
    }
}
