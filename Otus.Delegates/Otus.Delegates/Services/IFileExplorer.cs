namespace Otus.Delegates.Services;

public interface IFileExplorer
{
    void ExploreDirectory(string directoryPath, CancellationToken token);
    event EventHandler<FileArgs>? FileFound;
}