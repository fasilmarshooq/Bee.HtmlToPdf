using NUnit.Framework;

namespace Bee.HtmlToPdf.Tests
{
    public class PdfGeneratorTests
    {
        [Test]
        public void ShouldGenerateBytesForPdf()
        {
            const string content = @"<!DOCTYPE html><html><body><h1 style=""background - color: red; "">Hello World!</h1>< p > This is a paragraph.</ p ></ body ></ html > ";

            var result = PdfGenerator.GetBytes(content);
            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldNotThrowThreadingErrorWhenGeneratedMultipleTimes()
        {
            const string content = @"<!DOCTYPE html><html><body><h1 style=""background - color: red; "">Hello World!</h1>< p > This is a paragraph.</ p ></ body ></ html > ";

            PdfGenerator.GetBytes(content);
            PdfGenerator.GetBytes(content);
            PdfGenerator.GetBytes(content);
            PdfGenerator.GetBytes(content);
            PdfGenerator.GetBytes(content);
            Assert.Pass();
        }
    }
}