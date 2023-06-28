using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using System.Text;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class TableOfContentsServiceTests
    {
        [Theory]
        [InlineData(TableOfContents.Hyperlink)]
        [InlineData(TableOfContents.Text)]
        public void CreateTOC_Empty_ReturnsNewLineString(TableOfContents tableOfContents)
        {
            // Arrange
            MergedFile mergedFile = new MergedFile() { TableOfContents = tableOfContents, NewLineStyle = "\n" };
            List<ISelectedFile> selectedFiles = new List<ISelectedFile>();

            // Act
            var result = TableOfContentsService.CreateTOC(mergedFile, selectedFiles);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(mergedFile.NewLineStyle);
        }

        [Theory]
        [InlineData(TableOfContents.Hyperlink)]
        [InlineData(TableOfContents.None)]
        [InlineData(TableOfContents.Text)]
        public void CreateTOC_ListOfFiles_ReturnsTableOfContentsContent(TableOfContents tableOfContents)
        {
            // Arrange
            MergedFile mergedFile = new MergedFile() { TableOfContents = tableOfContents, NewLineStyle = "\n" };
            List<ISelectedFile> selectedFiles = CreateExampleList();
            string? correctToc = CreateCorrectTableOfContents(tableOfContents, mergedFile.NewLineStyle);

            // Act
            var result = TableOfContentsService.CreateTOC(mergedFile, selectedFiles);

            // Assert
            result.Should().Be(correctToc);
        }

        [Theory]
        [InlineData(TableOfContents.Hyperlink)]
        [InlineData(TableOfContents.None)]
        [InlineData(TableOfContents.Text)]
        public void CreateTOC_Null_ReturnsNull(TableOfContents tableOfContents)
        {
            // Arrange
            MergedFile mergedFile = new MergedFile() { TableOfContents = tableOfContents, NewLineStyle = "\n" };

            // Act
            var result = TableOfContentsService.CreateTOC(mergedFile, null);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void CreateTOC_NullMergedFile_ReturnsNull()
        {
            // Arrange
            List<ISelectedFile> selectedFiles = new List<ISelectedFile>();

            // Act
            var result = TableOfContentsService.CreateTOC(null, selectedFiles);

            // Assert
            result.Should().BeNull();
        }

        private string CreateCorrectHyperlinksToc(string newLine)
        {
            StringBuilder builder = new StringBuilder("### d");
            builder.Append(newLine);
            builder.Append("#### [Title of file 2](#title-of-file-2-1)");
            builder.Append(newLine);
            builder.Append("#### sd");
            builder.Append(newLine);
            builder.Append("##### [Repeated title](#repeated-title-2)");
            builder.Append(newLine);
            builder.Append("##### [Title of file 4](#title-of-file-4-1)");
            builder.Append(newLine);
            builder.Append("##### [Title of file 5](#title-of-file-5-1)");
            builder.Append(newLine);
            builder.Append("### d2");
            builder.Append(newLine);
            builder.Append("#### [Title of file 6](#title-of-file-6-1)");
            builder.Append(newLine);
            builder.Append("#### [Repeated title](#repeated-title-3)");
            builder.Append(newLine);
            builder.Append("#### d2b");
            builder.Append(newLine);
            builder.Append("##### [Title of file 8](#title-of-file-8-1)");
            builder.Append(newLine);
            builder.Append("##### [Title of file 9](#title-of-file-9-1)");
            builder.Append(newLine);
            builder.Append("### [Title of file 10](#title-of-file-10-1)");
            builder.Append(newLine);

            return builder.ToString();
        }

        private string? CreateCorrectTableOfContents(TableOfContents tableOfContents, string newLineStyle) => tableOfContents switch
        {
            TableOfContents.None => null,
            TableOfContents.Text => CreateCorrectTextToc(newLineStyle),
            TableOfContents.Hyperlink => CreateCorrectHyperlinksToc(newLineStyle)
        };

        private string CreateCorrectTextToc(string newLine)
        {
            StringBuilder builder = new StringBuilder("### d");
            builder.Append(newLine);
            builder.Append("#### Title of file 2");
            builder.Append(newLine);
            builder.Append("#### sd");
            builder.Append(newLine);
            builder.Append("##### Repeated title");
            builder.Append(newLine);
            builder.Append("##### Title of file 4");
            builder.Append(newLine);
            builder.Append("##### Title of file 5");
            builder.Append(newLine);
            builder.Append("### d2");
            builder.Append(newLine);
            builder.Append("#### Title of file 6");
            builder.Append(newLine);
            builder.Append("#### Repeated title");
            builder.Append(newLine);
            builder.Append("#### d2b");
            builder.Append(newLine);
            builder.Append("##### Title of file 8");
            builder.Append(newLine);
            builder.Append("##### Title of file 9");
            builder.Append(newLine);
            builder.Append("### Title of file 10");
            builder.Append(newLine);

            return builder.ToString();
        }

        private List<ISelectedFile> CreateExampleList()
        {
            List<ISelectedFile> selectedFiles = new List<ISelectedFile>();
            selectedFiles.Add(new SelectedFile() { Id = 1, Name = "File.md", Title = null });
            selectedFiles.Add(new SelectedFile() { Id = 2, Name = "d/File2.md", Title = "Title of file 2" });
            selectedFiles.Add(new SelectedFile() { Id = 3, Name = "d/sd/File3.md", Title = "Repeated title" });
            selectedFiles.Add(new SelectedFile() { Id = 4, Name = "d/sd/File4.md", Title = "Title of file 4" });
            selectedFiles.Add(new SelectedFile() { Id = 5, Name = "d/sd/File5.md", Title = "Title of file 5" });
            selectedFiles.Add(new SelectedFile() { Id = 6, Name = "d2/File6.md", Title = "Title of file 6" });
            selectedFiles.Add(new SelectedFile() { Id = 7, Name = "d2/d2a/File7.md", Title = "Repeated title" });
            selectedFiles.Add(new SelectedFile() { Id = 8, Name = "d2/d2b/File8.md", Title = "Title of file 8" });
            selectedFiles.Add(new SelectedFile() { Id = 9, Name = "d2/d2b/File9.md", Title = "Title of file 9" });
            selectedFiles.Add(new SelectedFile() { Id = 10, Name = "d3/File10.md", Title = "Title of file 10" });

            return selectedFiles;
        }
    }
}