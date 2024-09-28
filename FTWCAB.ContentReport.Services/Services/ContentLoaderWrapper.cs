using EPiServer;
using EPiServer.Core;
using FTWCAB.ContentReport.Services.Interfaces;

namespace FTWCAB.ContentReport.Services;

public class ContentLoaderWrapper : IContentLoaderWrapper
{
    private readonly IContentLoader contentLoader;

    public ContentLoaderWrapper(IContentLoader contentLoader)
    {
        this.contentLoader = contentLoader;
    }

    public T? Get<T>(int contentId, LanguageSelector languageSelector) where T : IContentData
     => Get<T>(new ContentReference(contentId), languageSelector);

    public T? Get<T>(ContentReference? c) where T : IContentData
    {
        if (c is null || ContentReference.IsNullOrEmpty(c)) return default;

        return contentLoader.Get<T>(c);
    }

    public T? Get<T>(ContentReference? c, LanguageSelector languageSelector) where T : IContentData
    {
        if (c is null || ContentReference.IsNullOrEmpty(c)) return default;

        return contentLoader.Get<T>(c, languageSelector);
    }

    public T? Get<T>(Guid guid, LanguageSelector languageSelector) where T : IContentData
    {
        try
        {
            return contentLoader.Get<T>(guid, languageSelector);
        }
        catch (ContentNotFoundException ex)
        {
            return default;
        }
    }
}
