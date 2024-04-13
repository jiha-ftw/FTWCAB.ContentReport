using EPiServer.Framework.Web.Resources;
using EPiServer.Shell.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTWCAB.ContentReport.Controllers
{
    [Authorize(Roles = "CmsAdmins")]
    [MenuItem(MenuPaths.Global + "/cms/FtwContentReport", Text = "Content Usages")]
    public class FtwContentReportContentUsagesController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromServices] IRequiredClientResourceList requiredResources)
        {
            requiredResources.Require("FTWCAB.ContentReport.Styles").AtHeader();
            requiredResources.Require("FTWCAB.ContentReport.Scripts").AtFooter();

            return View();
        }
    }
}
