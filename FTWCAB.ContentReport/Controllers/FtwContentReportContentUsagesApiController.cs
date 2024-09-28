using FTWCAB.ContentReport.Models.Api;
using FTWCAB.ContentReport.Models.Models.Api;
using FTWCAB.ContentReport.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTWCAB.ContentReport.Controllers
{
    [Authorize(Constants.Autorization.PolicyName)]
    public class FtwContentReportContentUsagesApiController : Controller
    {
        [HttpGet]
        public IEnumerable<ContentTypeGroupViewModel> GetContentTypes(
            [FromServices] IContentTypeGroupService contentTypeGroupService)
            => contentTypeGroupService.GetContentTypeGroups();

        [HttpGet]
        public ContentInstancesModel GetContentInstances(
            [FromServices] IContentTypeInstancesService contentTypeInstancesService,
            [FromQuery] int contentTypeId,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string languageId)
            => contentTypeInstancesService.GetInstances(contentTypeId, languageId, page, pageSize);

        [HttpGet]
        public ActionResult GetContentUsages(
            [FromServices] IContentInstanceUsageService contentInstanceUsageService,
            [FromQuery] int contentInstanceId,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string languageId) 
            => Ok(contentInstanceUsageService.GetUsages(contentInstanceId, languageId, page, pageSize));

        [HttpGet]
        public ActionResult GetSiteLanguages(
            [FromServices] ILanguageService languageService) 
            => Ok(languageService.GetLanguages().ToList());
    }
}
