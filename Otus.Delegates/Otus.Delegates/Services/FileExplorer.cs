namespace Otus.Delegates.Services;

public class FileExplorer : IFileExplorer
{
    public event EventHandler<FileArgs>? FileFound;
    
    /// <summary>
    /// Метод, обходящий каталог файлов и выдающий событие при нахождении каждого файла
    /// </summary>
    /// <param name="directoryPath">Путь начальной директории</param>
    /// <param name="token">Токен для отмены</param>
    /// <exception cref="ArgumentException"></exception>
    public void ExploreDirectory(string directoryPath, CancellationToken token)
    {
        if (string.IsNullOrEmpty(directoryPath))
            throw new ArgumentException("Directory path cannot be null or empty.", nameof(directoryPath));

        ExploreDirectoryInternal(directoryPath, token);
    }

    private void ExploreDirectoryInternal(string directoryPath, CancellationToken token)
    {
        try
        {
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                if (token.IsCancellationRequested)
                    return;
                
                OnFileFound(filePath);
            }

            foreach (var subdirectory in Directory.GetDirectories(directoryPath))
            {
                if (token.IsCancellationRequested)
                    return;
                
                ExploreDirectoryInternal(subdirectory, token);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exploring directory {directoryPath}: {ex.Message}");
        }
    }
    
    protected virtual void OnFileFound(string filePath)
    {
        FileFound?.Invoke(this, new FileArgs(filePath));
    }
}