using Microsoft.AspNetCore.Mvc;
using RoundTheCodeYouTubePdf.Models;
using SelectPdf;
using System.Diagnostics;

namespace RoundTheCodeYouTubePdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string html)
        {
            return View();
           
           
        }

        public IActionResult GeneratePdf(string html)
        {
            HtmlToPdf convert = new HtmlToPdf();
            convert.Options.PdfPageSize = PdfPageSize.A4;
            convert.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            convert.Options.MarginLeft = 10;
            convert.Options.MarginRight = 10;
            convert.Options.MarginTop = 50;
            convert.Options.MarginBottom = 20;
            html = html.Replace("start", "<").Replace("end", ">");
            PdfDocument doc = convert.ConvertHtmlString(html);
            byte[] pdfFile = doc.Save();
            doc.Close();
            return File(pdfFile, "application/pdf");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}