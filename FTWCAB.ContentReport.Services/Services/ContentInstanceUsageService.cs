using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using FTWCAB.ContentReport.Models.Models.Api;
using FTWCAB.ContentReport.Services.Interfaces;

namespace FTWCAB.ContentReport.Services
{
    public class ContentInstanceUsageService : IContentInstanceUsageService
    {
        private readonly IContentLoaderWrapper contentLoader;
        private readonly IContentSoftLinkRepository contentSoftLinkRepository;

        public ContentInstanceUsageService(IContentLoaderWrapper contentLoader, IContentSoftLinkRepository contentSoftLinkRepository)
        {
            this.contentLoader = contentLoader;
            this.contentSoftLinkRepository = contentSoftLinkRepository;
        }

        public ContentUsagesModel GetUsages(int contentInstanceId, int page, int pageSize)
        {
            var content = contentLoader.Get<IContent>(contentInstanceId);
            var usageParentContentItems = contentSoftLinkRepository.Load(content.ContentLink, reversed: true)
                .Where(link =>
                        link.SoftLinkType == ReferenceType.PageLinkReference &&
                        !ContentReference.IsNullOrEmpty(link.OwnerContentLink))
                .Select(link => contentLoader.Get<IContent>(link.OwnerContentLink))
                .DistinctBy(content => content.ContentLink.ID)
                .OrderBy(content => content.Name)
                .ToList();

            return new ContentUsagesModel
            {
                Pages = usageParentContentItems.Count > 0 ? (int)Math.Ceiling(usageParentContentItems.Count / (double)pageSize) : 0,
                TotalCount = usageParentContentItems.Count,
                Usages = usageParentContentItems
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Select(content => { 
                    return new ContentUsageModel
                    { 
                        EditLink = PageEditing.GetEditUrl(content.ContentLink),
                        Id = content.ContentLink.ID,
                        Name = content.Name,
                    };
                })
            };
        }
    }
}
