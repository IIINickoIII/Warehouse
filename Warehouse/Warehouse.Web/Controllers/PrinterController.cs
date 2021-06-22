using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Bll.Interfaces;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class PrinterController : Controller
    {
        public PrinterController(IPdfCreatorService pdfCreatorService)
        {
            _pdfCreatorService = pdfCreatorService;
        }

        private readonly IPdfCreatorService _pdfCreatorService;
        private const string FileType = "pdf";
        private const string FileMime = "application/pdf";
        private const string FileName = "Payment Invoice";

        public IActionResult CreatePdfDocument()
        {
            var fileStreamResult = new FileStreamResult(
                _pdfCreatorService.CreateStream("Content"),
                new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(FileMime))
            {
                FileDownloadName = $"{FileName}.{FileType}"
            };
            return fileStreamResult;
        }
    }
}
