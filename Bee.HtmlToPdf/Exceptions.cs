using System;

namespace Bee.HtmlToPdf
{
    public class NativeDllLoadException : Exception
    {
        public NativeDllLoadException() : base("Unable Load Native Convertor Dll, Check inner exception for more info")
        {

        }
    }

    public class PdfGenerationException : Exception
    {
        public PdfGenerationException() : base("Unable Generate Pdf, Check inner exception for more info")
        {

        }
    }
}
