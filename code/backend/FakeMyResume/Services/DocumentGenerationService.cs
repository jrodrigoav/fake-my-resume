using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using System.Reflection;
using System.Globalization;

namespace FakeMyResume.Services;

public class DocumentGenerationService : IDocumentGenerationService
{
    private readonly string _assetsPath;
    private readonly Color greyColor = new DeviceCmyk(0, 0, 0, 5);
    private readonly Color blueColor = new DeviceCmyk(99, 79, 0, 0);

    public DocumentGenerationService()
    {
        var assemblyPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        _assetsPath = System.IO.Path.Combine(assemblyPath, "Assets");
    }

    public Stream GenerateResumeInPDF(Resume resume)
    {
        #region Init
        MemoryStream ms = new MemoryStream();
        var pw = new PdfWriter(ms);
        var pdfDocument = new PdfDocument(pw);
        var page = pdfDocument.AddNewPage();
        var document = new Document(pdfDocument);
        document.SetMargins(60, 35, 35, 35);
        #endregion

        #region Styles

        var font = PdfFontFactory.CreateFont(System.IO.Path.Combine(_assetsPath, "Segoe UI.ttf"), PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        // var italicFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(_assetsPath, "Segoe UI Italic.ttf"), PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        var boldFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(_assetsPath, "Segoe UI Bold.ttf"), PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        var boldItalicFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(_assetsPath, "Segoe UI Bold Italic.ttf"), PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

        var textTitleStyle = new Style();
        textTitleStyle.SetFont(boldFont).SetFontColor(ColorConstants.BLACK);
        var sectionTitleStyle = new Style();
        sectionTitleStyle.SetFont(boldFont).SetFontColor(ColorConstants.BLACK).SetFontSize(14);
        var textDescription = new Style();
        textDescription.SetFont(font).SetFontSize(10).SetFontColor(ColorConstants.DARK_GRAY).SetTextAlignment(TextAlignment.JUSTIFIED);
        #endregion

        #region Header
        addRectangleHeader(pdfDocument, page);
        document.Add(addImage());
        addTextHeader(page, boldFont, resume.FullName, resume.CurrentRole);
        #endregion

        #region Profile
        var profile = new Paragraph(new Text("PROFILE").AddStyle(sectionTitleStyle)).SetMarginTop(50);
        document.Add(profile);
        document.Add(addDescription(font, resume.Description));
        #endregion

        #region Technical Skills

        var techenicalSkills = new Paragraph(new Text("TECHNICAL SKILLS").AddStyle(sectionTitleStyle)).SetMarginTop(10);
        document.Add(techenicalSkills);

        var tableTitleStyle = new Style();
        tableTitleStyle.SetFont(font).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER).SetBackgroundColor(greyColor);
        var tableTextStyle = new Style();
        tableTextStyle.SetFont(font).SetFontColor(ColorConstants.BLACK).SetFontSize(9).SetBold().SetCharacterSpacing(1);

        var columnWidths = new float[] { 250, 250 };
        var table = new Table(columnWidths);

        var techologiesCell = new Cell(1, 0).AddStyle(tableTitleStyle).Add(new Paragraph(new Text("TECHNOLOGIES:").AddStyle(tableTextStyle)));
        var certificationsCell = new Cell(1, 1).AddStyle(tableTitleStyle).Add(new Paragraph(new Text("CERTIFICATIONS:").AddStyle(tableTextStyle)));

        // header table
        table.AddHeaderCell(techologiesCell);
        table.AddHeaderCell(certificationsCell);
        /// technologies table
        var technologies = resume?.WorkExperience?.SelectMany(e => e.Technologies).Distinct().Order().ToList() ?? [];
        table.AddCell(addNestedTableWithTwoColumns(technologies, font));
        /// certifications table
        table.AddCell(addNestedTable(resume?.Certifications, font));
        table.SetMargin(10);
        document.Add(table);

        #endregion

        #region Work Experience

        var workExperience = new Paragraph(new Text("WORK EXPERIENCE").AddStyle(sectionTitleStyle)).SetMarginTop(10);
        document.Add(workExperience);

        resume.WorkExperience?.ForEach(we =>
        {
            document.Add(addWorkExperienceTable(textTitleStyle, textDescription, boldFont, boldItalicFont, we));
        });

        #endregion

        #region Education

        var education = new Paragraph(new Text("EDUCATION").AddStyle(sectionTitleStyle)).SetMarginTop(10);
        document.Add(education);

        resume.Education?.ForEach(e =>
        {
            document.Add(addEducationTable(textTitleStyle, e));
        });

        #endregion

        document.Close();

        #region Streams
        byte[] byteStream = ms.ToArray();

        ms = new MemoryStream();
        ms.Write(byteStream, 0, byteStream.Length);
        ms.Position = 0;
        return ms;
        #endregion
    }

    private void addRectangleHeader(PdfDocument pdfDocument, PdfPage page)
    {
        PdfCanvas canvasHeaderRectangle = new PdfCanvas(pdfDocument.GetFirstPage());
        var rectangle = new Rectangle(0, page.GetPageSize().GetTop() - 80, page.GetPageSize().GetWidth(), 80);
        canvasHeaderRectangle.Rectangle(rectangle)
             .SetFillColor(greyColor)
             .Fill();
    }

    private void addTextHeader(PdfPage page, PdfFont font, string resumeFullName, string resumeCurrentRole)
    {
        var rectangle = new Rectangle(35, page.GetPageSize().GetTop(), 740, 150);
        var canvasTextRectangle = new Canvas(page, rectangle);
        var fullName = new Text(resumeFullName.ToUpperInvariant()).SetFont(font);
        var currentRole = new Text(resumeCurrentRole).SetFont(font);

        canvasTextRectangle.ShowTextAligned(new Paragraph(fullName).SetFontSize(36).SetFontColor(ColorConstants.BLACK), 35, 790, TextAlignment.LEFT)
            .ShowTextAligned(new Paragraph(currentRole.SetCharacterSpacing(1)).SetFontSize(10).SetFontColor(ColorConstants.BLACK), 35, 775, TextAlignment.LEFT)
            .Close();
    }

    private Image addImage()
    {
        string imagePath = System.IO.Path.Combine(_assetsPath, "logo_unosquare_small.PNG");
        var logo = ImageDataFactory.Create(imagePath);

        var image = new Image(logo).ScaleToFit(64, 64).SetFixedPosition(1, 500, 770);
        return image;
    }

    private Paragraph addDescription(PdfFont font, string resumeDescription)
    {
        Paragraph description = new Paragraph(resumeDescription)
                  .SetFont(font).SetFontSize(10).SetFontColor(ColorConstants.DARK_GRAY).SetTextAlignment(TextAlignment.JUSTIFIED);
        description.SetMargin(10);
        return description;
    }

    private Table addWorkExperienceTable(Style textTitleStyle, Style textDescription, PdfFont boldFont, PdfFont boldItalicFont, WorkExperience workExperience)
    {
        float[] workExperienceWidth = [800f];
        var workExperienceTable = new Table(workExperienceWidth);

        var description = workExperience.Description;

        var culture = CultureInfo.CreateSpecificCulture("en-US");
        string fullDate  = $"({workExperience.DateBegin.ToString("MMM", culture).ToUpperInvariant()} {workExperience.DateBegin.Year} - {workExperience.DateEnd.ToString("MMM", culture).ToUpperInvariant()} {workExperience.DateEnd.Year})";
        var fullTitle = new Paragraph(new Text(workExperience.CompanyName).AddStyle(textTitleStyle).SetFontColor(blueColor));
        fullTitle.Add(new Text($" {workExperience.ProjectName} ").AddStyle(textTitleStyle).SetFontColor(ColorConstants.BLACK));
        fullTitle.Add(new Text(fullDate).SetFontColor(ColorConstants.BLACK).SetBold().SetCharacterSpacing(1).SetFontSize(11));


        var title = new Cell(1, 0).Add(fullTitle).SetBorder(Border.NO_BORDER);
        workExperienceTable.AddCell(title);

        var roleCell = new Cell(1, 0).Add(new Paragraph(new Text(workExperience.Role).SetFont(boldItalicFont).SetFontColor(ColorConstants.BLACK).SetFontSize(9).SetCharacterSpacing(1))).SetBorder(Border.NO_BORDER);
        workExperienceTable.AddCell(roleCell);

        var descriptionCell = new Cell(1, 0).Add(new Paragraph(new Text(description).AddStyle(textDescription))).SetBorder(Border.NO_BORDER);
        workExperienceTable.AddCell(descriptionCell);

        if(workExperience.Technologies.Count > 0)
        {
            var experienceTechnologies = string.Join(", ", workExperience.Technologies) + ".";
            var technologiesCell = new Cell(1, 0).Add(
                new Paragraph(new Text("Technologies & Tools: ").SetFont(boldFont).SetFontColor(ColorConstants.BLACK).SetFontSize(9))
                .Add(new Text(experienceTechnologies)).AddStyle(textDescription)
            ).SetBorder(Border.NO_BORDER);
            workExperienceTable.AddCell(technologiesCell);
        }

        var line = new SolidLine(2f);
        line.SetColor(greyColor);

        workExperienceTable.SetMargin(10);

        return workExperienceTable;
    }

    private Table addEducationTable(Style textTitleStyle, Education education)
    {
        float[] educationWidth = [800];
        var educationTable = new Table(educationWidth);

        string educationTitleStart = $"{education.UniversityName}. {education.State}, {education.Country}";
        string educationTitleEnd = $" {education.Degree} ({education.YearOfCompletion})";
        var pFullEducation = new Paragraph(new Text(educationTitleStart).AddStyle(textTitleStyle).SetFontColor(blueColor));
        pFullEducation.Add(new Text(educationTitleEnd).SetFontColor(ColorConstants.BLACK).SetBold().SetCharacterSpacing(1));
        var titleEducation = new Cell(1, 0).Add(pFullEducation).SetBorder(Border.NO_BORDER);

        educationTable.AddCell(titleEducation);
        educationTable.SetMargin(10);

        return educationTable;
    }

    private Cell addNestedTableWithTwoColumns(List<string> list, PdfFont font)
    {
        float[] cellTechnologiesWidth = [7, 90, 7, 90];

        var techologiesCellTable = new Table(cellTechnologiesWidth);
        for (int i = 0; i < list.Count; i++)
        {
            if (list.Count % 2 == 0)
            {
                techologiesCellTable.AddCell(addNestedCell(true));
                techologiesCellTable.AddCell(addNestedCell(false, list[i]));
            } 
            else
            {
                techologiesCellTable.AddCell(addNestedCell(true));
                techologiesCellTable.AddCell(addNestedCell(false, list[i]));
            }
        }
        Cell technologiesCell = new Cell().SetBorder(Border.NO_BORDER).Add(techologiesCellTable);
        return technologiesCell;
    }

    private Cell addNestedTable(List<string> list, PdfFont font)
    {
        float[] columns = { 7, 400 };
        Table table = new Table(columns);

        for (int i = 0; i < list.Count; i++)
        {
            table.AddCell(addNestedCell(true));
            table.AddCell(addNestedCell(false, list[i]));

        }
        Cell result = new Cell().SetBorder(Border.NO_BORDER).Add(table);
        return result;
    }

    private Cell addNestedCell(bool isBullet, string value = "\u2022")
    {
        if (isBullet)
        {
            var bullet = new Paragraph(new Text(value).SetBold().SetFontColor(blueColor));
            Cell columnBullet = new Cell().SetBorder(Border.NO_BORDER).Add(bullet);
            return columnBullet;
        } 
        else
        {
            var message = new Paragraph(new Text(value).SetFontSize(10).SetFontColor(ColorConstants.BLACK));
            Cell columnMessage = new Cell().SetBorder(Border.NO_BORDER).Add(message.SetVerticalAlignment(VerticalAlignment.BOTTOM));
            return columnMessage;
        }
    }
}
