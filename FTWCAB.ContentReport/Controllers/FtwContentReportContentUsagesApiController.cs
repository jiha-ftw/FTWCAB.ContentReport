using FTWCAB.ContentReport.Models.Api;
using FTWCAB.ContentReport.Models.Models.Api;
using FTWCAB.ContentReport.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTWCAB.ContentReport.Controllers
{
    [Authorize(Roles = "CmsAdmins")]
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
            [FromQuery] int pageSize)
            => contentTypeInstancesService.GetInstances(contentTypeId, page, pageSize);

        [HttpGet]
        public ActionResult GetContentUsages(
            [FromServices] IContentInstanceUsageService contentInstanceUsageService,
            [FromQuery] int contentInstanceId,
            [FromQuery] int page,
            [FromQuery] int pageSize) 
            => Ok(contentInstanceUsageService.GetUsages(contentInstanceId, page, pageSize));
    }
}
