using DinkToPdf;
using System;

namespace Bee.HtmlToPdf
{
    public static class PdfGenerator
    {
        /// <summary>
        /// Document Settings if not set default value will be set
        /// Default Value : Mode: Color, Orientation: Potrait, Size: A4, Doc Title: pdf report
        /// </summary>
        public static GlobalSettings GlobalSettings { get; set; }

        /// <summary>
        /// Page Settings if not set default value will be set
        /// Default Value : PageNumber: True, Encoding: UTF-8, Header/Footer Font and size: Aerial - 5
        /// </summary>
        public static ObjectSettings ObjectSettings { get; set; }
        /// <summary>
        /// Gets Pdf object as byte[] for provided string of html content.
        /// </summary>
        /// <param name="content" > Html content for pdf generation </param>
        /// <returns>
        /// byte[] of pdf blob.
        /// </returns>
        /// <exception cref="PdfGenerationException"></exception>
        /// <exception cref="NativeDllLoadException"></exception>
        public static byte[] GetBytes(string content)
        {
            try
            {
                CustomAssemblyLoader.Load();
                ConfigureSettings();

                var converter = ConvertorProvider.GetConvertor();
                ObjectSettings.HtmlContent = content;

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = GlobalSettings,
                    Objects = { ObjectSettings }
                };

                return converter.Convert(pdf);
            }
            catch (Exception)
            {
                throw new PdfGenerationException();
            }
        }

        private static void ConfigureSettings()
        {
            if (GlobalSettings == null)
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                };
            }
            else
            {
                GlobalSettings.ColorMode ??= ColorMode.Color;
                GlobalSettings.Orientation ??= Orientation.Portrait;
                GlobalSettings.PaperSize ??= PaperKind.A4;
                GlobalSettings.Margins ??= new MarginSettings { Top = 10 };
                GlobalSettings.DocumentTitle ??= "PDF Report";
            }

            if (ObjectSettings == null)
            {
                ObjectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontName = "Arial", FontSize = 5 },
                    FooterSettings = { FontName = "Arial", FontSize = 5, Line = false, Center = "" }
                };
            }
            else
            {
                ObjectSettings.PagesCount ??= true;
                ObjectSettings.WebSettings ??= new WebSettings { DefaultEncoding = "utf-8" };
                ObjectSettings.HeaderSettings ??= new HeaderSettings { FontName = "Arial", FontSize = 5 };
                ObjectSettings.FooterSettings ??= new FooterSettings { FontName = "Arial", FontSize = 5, Line = false, Center = "" };
            }
        }
    }
}
