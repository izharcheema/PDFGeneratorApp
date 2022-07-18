using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDFGeneratorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly ILogger<PDFController> _logger;
        private readonly IConverter _converter;

        public PDFController(ILogger<PDFController> logger,IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }
        [HttpGet("GeneratePDF")]
        public IActionResult GeneratePDF()
        {
            var html = $@"
             <!DOCTYPE html>
           <html lang=""en"">
           <head>
               Heading Here.
           </head>
          <body>
          <h1>Welcome to MY PDFGeneratorApp</h1>
          <p>Hi This Is Izhar Ul Haq Learning To Create PDF Through WebApi</p>
          </body>
          </html>   
             ";
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            ObjectSettings objectSettings=new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;
            WebSettings webSettings=new WebSettings();
            webSettings.DefaultEncoding="UTF-8";
            HeaderSettings headerSettings=new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [topage]";
            headerSettings.Line = true;
            FooterSettings footerSettings=new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Prepared By Izhar Ul Haq";
            footerSettings.Line = true;
            objectSettings.HeaderSettings=headerSettings;
            objectSettings.FooterSettings=footerSettings;
            objectSettings.WebSettings=webSettings;
            HtmlToPdfDocument htmlToPdfDocument=new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                    Objects ={ objectSettings},
            };
            var pdfFile = _converter.Convert(htmlToPdfDocument); ;
            return File(pdfFile, "application/octet-stream", "DemoPdf.pdf");

        }
    }
}
