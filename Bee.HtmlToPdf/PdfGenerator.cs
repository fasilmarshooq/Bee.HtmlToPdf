using DinkToPdf;

namespace Bee.HtmlToPdf
{
    public static class PdfGenerator
    {
        public static byte[] GetBytes(string content)
        {
            try
            {
                CustomAssemblyLoader.Load();

                var converter = ConvertorProvider.GetConvertor();
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = content,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontName = "Arial", FontSize = 5 },
                    FooterSettings = { FontName = "Arial", FontSize = 5, Line = false, Center = "" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = GetGlobalSettings(),
                    Objects = { objectSettings }
                };

                var file = converter.Convert(pdf);
                return file;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        private static GlobalSettings GetGlobalSettings()
        {
            return new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
            };
        }
    }
}
