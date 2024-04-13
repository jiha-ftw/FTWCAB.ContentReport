using EPiServer.Core;

namespace FTWCAB.ContentReport.Services.Interfaces
{
    public interface IContentLoaderWrapper
    {
        T? Get<T>(int contentID) where T : IContentData;
        T? Get<T>(ContentReference? contentReference) where T : IContentData;
        T? Get<T>(Guid guid) where T : IContentData;
    }
}
