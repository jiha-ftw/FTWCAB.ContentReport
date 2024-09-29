using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using FTWCAB.ContentReport.Models.Models.Api;
using FTWCAB.ContentReport.Services.Interfaces;

namespace FTWCAB.ContentReport.Services
{
    public class ContentInstanceUsageService : IContentInstanceUsageService
    {
        private readonly IContentLoaderWrapper contentLoaderWrapper;
        private readonly IContentSoftLinkRepository contentSoftLinkRepository;

        public ContentInstanceUsageService(IContentLoaderWrapper contentLoaderWrapper, IContentSoftLinkRepository contentSoftLinkRepository)
        {
            this.contentLoaderWrapper = contentLoaderWrapper;
            this.contentSoftLinkRepository = contentSoftLinkRepository;
        }

        public ContentUsagesModel GetUsages(int contentInstanceId, string languageId, int page, int pageSize)
        {
            var languageSelector = new LanguageSelector(languageId);
            var content = contentLoaderWrapper.Get<IContent>(contentInstanceId, languageSelector);
            var usageParentContentItems = contentSoftLinkRepository.Load(content.ContentLink, reversed: true)
                .Where(link =>
                        link.SoftLinkType == ReferenceType.PageLinkReference &&
                        !ContentReference.IsNullOrEmpty(link.OwnerContentLink))
                .Select(link => contentLoaderWrapper.Get<IContent>(link.OwnerContentLink, languageSelector))
                .DistinctBy(content => content.ContentLink.ID)
                .OrderBy(content => content.Name)
                .ToList();

            var usages = new ContentUsagesModel
            {
                Pages = usageParentContentItems.Count > 0 ? (int)Math.Ceiling(usageParentContentItems.Count / (double)pageSize) : 0,
                TotalCount = usageParentContentItems.Count,
                Usages = usageParentContentItems
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Select(content => 
                        new ContentUsageModel
                        { 
                            EditLink = PageEditing.GetEditUrlForLanguage(content.ContentLink, languageId),
                            Id = content.ContentLink.ID,
                            Name = content.Name,
                        }
                    )
                    .ToList()
            };

            return usages;
        }
    }
}
