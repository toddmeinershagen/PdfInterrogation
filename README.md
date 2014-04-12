PdfInterrogation
================

A library that allows developers to interrogate a pdf file for testing purposes.

##Sample Syntax
Below are some samples of the methods that the interrogator will provide.

```C#
var pdf = new PdfInterrogator(filePath);
pdf.Exists();
pdf.IsPdf();
pdf.IsPdf(1.0);
pdf.Has(1).Pages;
pdf.Page(1).Contains("Todd Meinershagen");
pdf.Page(1).Contains(48);
pdf.Page(1).Contains(48.05);
pdf.Page(1).Text.StartsWith("Logo");
```

To install PdfInterrogation, run the following command in the Package Manager Console

```
PM> Install-Package PdfInterrogation
```
