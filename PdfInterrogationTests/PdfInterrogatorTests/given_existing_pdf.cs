using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace PdfFileInterrogation.Tests.PdfInterrogatorTests
{
    public class given_existing_pdf
    {
        private PdfInterrogator _subject;

        [SetUp]
        public void SetUp()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Sample.pdf");
            _subject = new PdfInterrogator(filePath);
        }

        [Test]
        public void when_checking_if_file_exists_should_be_true()
        {
            _subject.Exists().Should().BeTrue();
        }

        [Test]
        public void when_checking_for_pdf_should_be_true()
        {
            _subject.IsPdf().Should().BeTrue();
        }

        [Test]
        public void when_checking_for_pdf_with_correct_version_should_be_true()
        {
            _subject.IsPdf(1.0).Should().BeTrue();
        }

        [Test]
        public void when_checking_for_pdf_with_incorrect_version_should_be_false()
        {
            _subject.IsPdf(1.1).Should().BeFalse();
        }

        [Test]
        public void when_checking_for_correct_number_of_pages_should_be_true()
        {
            _subject.Has(1).Pages.Should().BeTrue();
        }

        [Test]
        public void when_checking_for_incorrect_number_of_pages_should_be_false()
        {
            _subject.Has(2).Pages.Should().BeFalse();
        }

        [Test]
        public void when_checking_if_page_contains_text_contained_within_page_should_be_true()
        {
            _subject.Page(1).Contains("MONGE").Should().BeTrue();
        }

        [Test]
        public void when_checking_if_page_contains_text_not_contained_within_page_should_be_false()
        {
            _subject.Page(1).Contains("MONGER").Should().BeFalse();
        }

        [Test]
        public void when_checking_if_page_contains_integer_contained_within_page_should_be_true()
        {
            _subject.Page(1).Contains(48).Should().BeTrue();
        }

        [Test]
        public void when_checking_if_page_contains_integer_not_contained_within_page_should_be_false()
        {
            _subject.Page(1).Contains(49).Should().BeFalse();
        }

        [Test]
        public void when_checking_if_page_contains_double_contained_within_page_should_be_true()
        {
            _subject.Page(1).Contains(48.05).Should().BeTrue();
        }

        [Test]
        public void when_checking_if_page_contains_double_not_contained_within_page_should_be_false()
        {
            _subject.Page(1).Contains(48.04).Should().BeFalse();
        }
    }
}
