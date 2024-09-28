using EPiServer.DataAbstraction;
using FTWCAB.ContentReport.Models.Models.Api;
using FTWCAB.ContentReport.Services.Interfaces;

namespace FTWCAB.ContentReport.Services.Services;

public class LanguageService(ILanguageBranchRepository languageBranchRepository) : ILanguageService
{
    private readonly ILanguageBranchRepository languageBranchRepository = languageBranchRepository;

    public IEnumerable<LanguageModel> GetLanguages()
    {
        var languages = languageBranchRepository
            .ListEnabled()
            .Select((l, index) => new LanguageModel
            {
                Id = l.LanguageID,
                Name = l.Name,
                Selected = index == 0,
            });

        return languages;
    }
}
