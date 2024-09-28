using FTWCAB.ContentReport.Models.Models.Api;

namespace FTWCAB.ContentReport.Services.Interfaces;

public interface ILanguageService
{
    IEnumerable<LanguageModel> GetLanguages();
}
