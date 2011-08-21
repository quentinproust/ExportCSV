using System;
using System.IO;
using System.Text;
using Exporter.Builder;
using Exporter.Helpers;
using Exporter.Mapping;
using NUnit.Framework;

namespace Exporter.Tests
{
    [TestFixture]
    public class ExporterTest
    {
        [Test]
        public void CanExport()
        {
            var stringBuilder = new StringBuilder();
            var output = new StringWriter(stringBuilder);
            SampleCollections.Students.Export(m => 
                m.ImplicitMap(x => x.FirstName)
                .ImplicitMap(x => x.Surname))
                .Output(output);

            var result = stringBuilder.ToString();
            Assert.That(result, Is.Not.Empty);
            Console.WriteLine(result);
        }

        [Test]
        public void CanExportWithMoreComplexInformations()
        {
            var stringBuilder = new StringBuilder();
            var output = new StringWriter(stringBuilder);
            SampleCollections.Students.Export(m =>
                m.Map(k => k.Title("FullName").Compute(x => x.FirstName + " " + x.Surname))
                .Map(k => k.Title("Has a scholarship ?").Compute(x => x.IsScholar ? "Yes" : "No"))
                .ImplicitMap(x => x.Age)
                .Map(k => k.Title("Birthday").Value(x => x.Birthday).Format("d MMM yyyy")))
                .Output(output);

            var result = stringBuilder.ToString();
            Assert.That(result, Is.Not.Empty);
            Console.WriteLine(result);
        }

        [Test]
        public void ShouldNotBeBotheredByComma()
        {
            var stringBuilder = new StringBuilder();
            var output = new StringWriter(stringBuilder);
            SampleCollections.StudentsWithComma.Export(m =>
                m.Map(k => k.Title("FullName").Compute(x => x.FirstName + " " + x.Surname))
                .ImplicitMap(x => x.IsScholar)
                .ImplicitMap(x => x.Age))
                .Output(output);

            var result = stringBuilder.ToString();
            Assert.That(result, Is.Not.Empty);
            Console.WriteLine(result);
        }
    }
}
