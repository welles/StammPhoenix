using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using StammPhoenix.Util.Interfaces;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Extensions;
using StammPhoenix.Web.Models.Downloads;

namespace StammPhoenix.Web.Controllers
{
    public class DownloadsController : Controller
    {
        private IDownloadFilesService DownloadFilesService { get; }

        private ITempCookieService TempCookieService { get; }

        private IContentTypeProvider ContentTypeProvider { get; }

        public DownloadsController(IDownloadFilesService downloadFilesService, ITempCookieService tempCookieService, IContentTypeProvider contentTypeProvider)
        {
            this.DownloadFilesService = downloadFilesService;
            this.TempCookieService = tempCookieService;
            this.ContentTypeProvider = contentTypeProvider;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string? key)
        {
            if (key != null)
            {
                var file = this.DownloadFilesService.GetFile(key);

                if (file == null)
                {
                    this.TempCookieService.SetTempCookie("DownloadErrorMessage", "Die angegebene Datei konnte nicht gefunden werden.");

                    return this.RedirectTo("Index", "Downloads");
                }

                if(!this.ContentTypeProvider.TryGetContentType(file.Value.FilePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                return this.PhysicalFile(file.Value.FilePath, contentType, file.Value.NameWithExtension);
            }

            var files = this.DownloadFilesService.GetFiles().ToList();

            var viewModel = new DownloadsViewModel {Files = files};

            return this.View(viewModel);
        }
    }
}
