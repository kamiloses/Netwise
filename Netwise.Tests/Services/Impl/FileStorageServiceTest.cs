using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Netwise.Dto;
using Netwise.Services.Impl;
using Xunit;

namespace Netwise.Tests.Services.Impl;

[TestSubject(typeof(FileStorageService))]
public class FileStorageServiceTest
{
    private FileStorageService CreateService(string filePath)
    {
        var mockLogger = new Mock<ILogger<FileStorageService>>();
        var options = Options.Create(new FilePathSettings { FilePath = filePath });
        return new FileStorageService(mockLogger.Object, options);
    }

    [Fact]
    public async Task SaveToFile_CreatesFile_WhenFileDoesNotExist()
    {
        var filePath = "catfacts.txt";
        File.Delete(filePath); 

        var service = CreateService(filePath);
        var fact = "Test cat fact";

        await service.SaveToFile(fact);

        Assert.True(File.Exists(filePath));
        var content = await File.ReadAllTextAsync(filePath);
        Assert.Equal(fact + System.Environment.NewLine, content);

        File.Delete(filePath);
    }

    [Fact]
    public async Task SaveToFile_AppendsToFile_WhenFileExists()
    {
        var filePath = "catfacts.txt";

        var service = CreateService(filePath);
        var fact1 = "First cat fact";
        var fact2 = "Second cat fact";

        await service.SaveToFile(fact1);
        await service.SaveToFile(fact2);

        var lines = await File.ReadAllLinesAsync(filePath);
        Assert.Equal(2, lines.Length);
        Assert.Equal(fact1, lines[0]);
        Assert.Equal(fact2, lines[1]);

        File.Delete(filePath);
    }

}