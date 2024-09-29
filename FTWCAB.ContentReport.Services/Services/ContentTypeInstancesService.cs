using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using FTWCAB.ContentReport.Models.Models.Api;
using FTWCAB.ContentReport.Services.Interfaces;

namespace FTWCAB.ContentReport.Services;

public class ContentTypeInstancesService : IContentTypeInstancesService
{
    private readonly IContentLoaderWrapper contentLoaderWrapper;
    private readonly IContentModelUsage contentModelUsage;
    private readonly IContentTypeRepository contentTypeRepository;
    private readonly IContentVersionRepository contentVersionRepository;

    public ContentTypeInstancesService(
        IContentLoaderWrapper contentLoaderWrapper,
        IContentModelUsage contentModelUsage,
        IContentTypeRepository contentTypeRepository,
        IContentVersionRepository contentVersionRepository)
    {
        this.contentLoaderWrapper = contentLoaderWrapper;
        this.contentModelUsage = contentModelUsage;
        this.contentTypeRepository = contentTypeRepository;
        this.contentVersionRepository = contentVersionRepository;
    }

    public ContentInstancesModel GetInstances(int contentTypeId, string languageId, int page, int pageSize)
    {
        var contentTypes = contentTypeRepository.List().ToList().ToDictionary(x => x.ID, x => x);
        var languageSelector = new LanguageSelector(languageId);
        var contentType = contentTypes[contentTypeId];
        var contentItems = contentType is null
            ? Enumerable.Empty<IContent>().ToList()
            : GetLatestContentUsageOfType(contentType, languageSelector)
                .OrderBy(contentUsage => contentUsage.Name)
                .ToList();

        return new ContentInstancesModel
        {
            TotalCount = contentItems.Count,
            Pages = contentItems.Count > 0 ? (int)Math.Ceiling(contentItems.Count / (double)pageSize) : 0,
            Instances = contentItems.Skip(page * pageSize).Take(pageSize).Select(content =>
            {
                var parentLink = GetParentLink(content, languageSelector);
                var parentContent = contentLoaderWrapper.Get<IContent>(parentLink);
                contentTypes.TryGetValue(parentContent?.ContentTypeID ?? 0, out var parentContentType);

                return new ContentInstanceModel
                {
                    Id = content.ContentLink.ID,
                    Name = content.Name,
                    EditLink = PageEditing.GetEditUrl(content.ContentLink),
                    ParentName = parentContent?.Name,
                    ParentContentTypeName = parentContentType?.LocalizedName,
                    ParentEditLink = parentLink is not null ? PageEditing.GetEditUrlForLanguage(parentLink, languageId) : null
                };
            }).ToList(),
        };
    }

    private IEnumerable<IContent> GetLatestContentUsageOfType(ContentType contentType, LanguageSelector languageSelector)
    {
        var allContentReferences = contentModelUsage.ListContentOfContentType(contentType)
            .Where(u => u.LanguageBranch == languageSelector.LanguageBranch)
            .ToList();

        var allContentItems = allContentReferences
            .Select(cr => contentLoaderWrapper.Get<IContent>(cr.ContentLink, languageSelector))
            .Where(content => content != null)
            .ToList();

        var latestPublishedContentItems = allContentItems
            .Where(content =>
            {
                var publishedVersion = contentVersionRepository.LoadPublished(content.ContentLink, languageSelector.LanguageBranch);
                return publishedVersion != null && publishedVersion.ContentLink == content.ContentLink;
            });

        return latestPublishedContentItems!;
    }

    private ContentReference? GetParentLink(IContent content, LanguageSelector languageSelector)
    {
        if (ContentReference.IsNullOrEmpty(content?.ParentLink)) return null;

        var parentContent = contentLoaderWrapper.Get<IContent>(content!.ParentLink, languageSelector);
        if (ContentReference.IsNullOrEmpty(parentContent?.ParentLink) || !typeof(ContentAssetFolder).IsAssignableFrom(parentContent?.GetType()))
        {
            return content!.ParentLink;
        }

        var assetFolder = parentContent as ContentAssetFolder;
        if (assetFolder is not null)
        {
            var assetFolderOwnerContent = contentLoaderWrapper.Get<IContent>(assetFolder.ContentOwnerID, languageSelector);
            if (assetFolderOwnerContent is not null)
            {
                return assetFolderOwnerContent.ContentLink;
            }
            else
            {
                return assetFolder.ContentLink;
            }
        }

        return null;
    }
}
