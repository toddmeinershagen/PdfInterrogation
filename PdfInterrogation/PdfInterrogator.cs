using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PdfFileInterrogation
{
    using IO = System.IO;

    public class PdfInterrogator
    {
        private readonly string _filePath;
        private const string PdfHeaderFormat = "%PDF-{0}";

        public PdfInterrogator(string filePath)
        {
            _filePath = filePath;
        }

        public bool Exists()
        {
            return IO.File.Exists(_filePath);
        }

        public bool IsPdf()
        {
            return ExecuteIfExists(_filePath, () =>
            {
                if (new IO.FileInfo(_filePath).Length < 5)
                    return false;

                var expectedHeader = string.Format(PdfHeaderFormat, string.Empty);
                return HasExpectedHeader(expectedHeader);
            });
        }

        public bool IsPdf(double version)
        {
            return ExecuteIfExists(_filePath, () =>
            {
                var expectedHeader = string.Format(PdfHeaderFormat, version);
                return HasExpectedHeader(expectedHeader);
            });
        }

        private bool HasExpectedHeader(string expectedHeader)
        {
            using (var reader = new IO.BinaryReader(IO.File.OpenRead(_filePath)))
            {
                var buffer = reader.ReadBytes(expectedHeader.Length);
                var header = Encoding.ASCII.GetString(buffer);

                return header.Equals(expectedHeader);
            }
        }

        private static bool ExecuteIfExists(string filePath, Func<bool> function)
        {
            if (IO.File.Exists(filePath))
                return function();

            return false;
        }

        public HasInteger Has(int number)
        {
            return new HasInteger(_filePath, number);
        }

        public PageInterrogator Page(int pageNumber)
        {
            return new PageInterrogator(_filePath, pageNumber);
        }

        public class PageInterrogator
        {
            private readonly string _filePath;
            private readonly int _number;

            internal PageInterrogator(string filePath, int number)
            {
                _filePath = filePath;
                _number = number;
            }

            public bool Contains(string text)
            {
                return ExecuteIfExists(_filePath, () => Text.Contains(text));
            }

            public bool Contains(int number)
            {
                return ExecuteIfExists(_filePath, () => Text.Contains(number.ToString()));
            }

            public bool Contains(double number)
            {
                return ExecuteIfExists(_filePath, () => Text.Contains(number.ToString()));
            }

            private string _pageText;

            public string Text
            {
                get
                {
                    if (_pageText == null)
                    {
                        using (var pdfReader = new PdfReader(_filePath))
                        {
                            var strategy = new SimpleTextExtractionStrategy();
                            var pageText = PdfTextExtractor.GetTextFromPage(pdfReader, _number, strategy);
                            pageText =
                                Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8,
                                    Encoding.Default.GetBytes(pageText)));

                            _pageText = pageText;
                        }
                    }

                    return _pageText;
                }
            }
        }

        public class HasInteger
        {
            private readonly string _filePath;
            private readonly int _number;

            internal HasInteger(string filePath, int number)
            {
                _filePath = filePath;
                _number = number;
            }

            public bool Pages
            {
                get
                {
                    return ExecuteIfExists(_filePath, () =>
                    {
                        using (var reader = new PdfReader(_filePath))
                        {
                            return reader.NumberOfPages == _number;
                        }
                    });
                }
            }
        }
    }
}
