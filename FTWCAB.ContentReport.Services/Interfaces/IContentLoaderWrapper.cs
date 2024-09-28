using EPiServer.Core;

namespace FTWCAB.ContentReport.Services.Interfaces;

public interface IContentLoaderWrapper
{
    T? Get<T>(int contentId, LanguageSelector languageSelector) where T : IContentData;
    T? Get<T>(ContentReference? contentReference) where T : IContentData;
    T? Get<T>(ContentReference? c, LanguageSelector languageSelector) where T : IContentData;
    T? Get<T>(Guid guid, LanguageSelector languageSelector) where T : IContentData;
}
