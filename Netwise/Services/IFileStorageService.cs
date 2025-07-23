namespace Netwise.Services;

public interface IFileStorageService
{
    public  Task  SaveToFile(string fact);
}