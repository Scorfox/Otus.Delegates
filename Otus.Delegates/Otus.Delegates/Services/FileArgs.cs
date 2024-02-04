namespace Otus.Delegates.Services;

public class FileArgs : EventArgs
{
    public string FilePath { get; }

    public FileArgs(string filePath)
    {
        FilePath = filePath;
    }
}