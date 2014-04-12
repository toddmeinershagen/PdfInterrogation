using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace PdfFileInterrogation.Tests.PdfInterrogatorTests
{
    public class given_nonexisting_pdf
    {
        private PdfInterrogator _subject;

        [SetUp]
        public void SetUp()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Bogus.pdf");
            _subject = new PdfInterrogator(filePath);
        }

        [Test]
        public void when_checking_if_file_exists_should_be_false()
        {
            _subject.Exists().Should().BeFalse();
        }

        [Test]
        public void when_checking_for_pdf_should_be_false()
        {
            _subject.IsPdf().Should().BeFalse();
        }

        [Test]
        public void when_checking_for_pdf_with_version_should_be_false()
        {
            _subject.IsPdf(1.0).Should().BeFalse();
        }

        [Test]
        public void when_checking_for_number_of_pages_shoudld_be_false()
        {
            _subject.Has(1).Pages.Should().BeFalse();
        }

        [Test]
        public void when_checking_if_page_contains_text_should_be_false()
        {
            _subject.Page(1).Contains("MONGE").Should().BeFalse();
        }

        [Test]
        public void when_checking_if_page_contains_integer_should_be_False()
        {
            _subject.Page(1).Contains(48).Should().BeFalse();
        }

        [Test]
        public void when_checking_if_page_contains_double_should_be_false()
        {
            _subject.Page(1).Contains(48.05).Should().BeFalse();
        }
    }
}