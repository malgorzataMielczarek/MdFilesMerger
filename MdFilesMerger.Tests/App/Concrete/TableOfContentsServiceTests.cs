using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class TableOfContentsServiceTests
    {
        [Fact]
        public void CanCreateHyperlinkTOC()
        {
            // Arrange
            MergedFile mergedFile = new MergedFile() { TableOfContents = Domain.Common.TableOfContents.Hyperlink, NewLineStyle = "\n" };
            string[] directories = new string[10] { "", "directory1/", "directory1/directory1a/", "directory1/directory1a/", "directory1/directory1a/", "directory2/", "directory2/directory2a/", "directory2/directory2b/", "directory2/directory2b/", "directory3/" };
            List<ISelectedFile> selectedFiles = new List<ISelectedFile>();
            for (int i = 1; i <= 10; i++)
            {
                selectedFiles.Add(new SelectedFile() { Id = i, Name = directories[i - 1] + "File" + i + ".md", Title = "Title of file " + i });
            }
            selectedFiles.First().Title = null;
            selectedFiles.ElementAt(6).Title = "Title of file 3";

            // Act
            var result = TableOfContentsService.CreateTOC(mergedFile, selectedFiles);

            // Assert
            result.Should().NotBeNull();
            string toc = "### directory1\n#### [Title of file 2]{#title-of-file-2-1}\n#### directory1a\n##### [Title of file 3]{#title-of-file-3-2}\n##### [Title of file 4]{#title-of-file-4-1}\n##### [Title of file 5]{#title-of-file-5-1}\n### directory2\n#### [Title of file 6]{#title-of-file-6-1}\n#### [Title of file 3]{#title-of-file-3-3}\n#### directory2b\n##### [Title of file 8]{#title-of-file-8-1}\n##### [Title of file 9]{title-of-file-9-1}\n### [Title of file 10]{title-of-file-10}\n";
            result.Should().Be(toc);
        }

        [Fact]
        public void CanCreateTextTOC()
        {
            // Arrange
            MergedFile mergedFile = new MergedFile() { TableOfContents = Domain.Common.TableOfContents.Text, NewLineStyle = "\n" };
            string[] directories = new string[10] { "", "directory1/", "directory1/directory1a/", "directory1/directory1a/", "directory1/directory1a/", "directory2/", "directory2/directory2a/", "directory2/directory2b/", "directory2/directory2b/", "directory3/" };
            List<ISelectedFile> selectedFiles = new List<ISelectedFile>();
            for (int i = 1; i <= 10; i++)
            {
                selectedFiles.Add(new SelectedFile() { Id = i, Name = directories[i - 1] + "File" + i + ".md", Title = "Title of file " + i });
            }
            selectedFiles.First().Title = null;
            selectedFiles.ElementAt(6).Title = "Title of file 3";

            // Act
            var result = TableOfContentsService.CreateTOC(mergedFile, selectedFiles);

            // Assert
            result.Should().NotBeNull();
            string toc = "### directory1\n#### Title of file 2\n#### directory1a\n##### Title of file 3\n##### Title of file 4\n##### Title of file 5\n### directory2\n#### Title of file 6\n#### Title of file 3\n#### directory2b\n##### Title of file 8\n##### Title of file 9\n### Title of file 10\n";
            result.Should().Be(toc);
        }

        [Fact]
        public void IsNullForNoneTOC()
        {
            // Arrange
            MergedFile mergedFile = new MergedFile() { TableOfContents = Domain.Common.TableOfContents.None };
            List<ISelectedFile> selectedFiles = new List<ISelectedFile>();
            for (int i = 1; i <= 10; i++)
            {
                selectedFiles.Add(new SelectedFile() { Id = i, Name = "File" + i + ".md", Title = "Title of file " + i });
            }

            // Act
            var toc = TableOfContentsService.CreateTOC(mergedFile, selectedFiles);

            // Assert
            toc.Should().BeNull();
        }
    }
}