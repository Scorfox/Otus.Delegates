// See https://aka.ms/new-console-template for more information

using Otus.Delegates.Extensions;
using Otus.Delegates.Services;

const string filePathForExplore = @"C:\\Users\\user\\Documents";

var maxNumber = new List<string> {"2", "3", "4", "5"}.GetMax(e => int.Parse(e));
Console.WriteLine(maxNumber);


IFileExplorer fileExplorer = new FileExplorer();
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

fileExplorer.FileFound += (_, e) =>
{
    Console.WriteLine($"File found: {e.FilePath}");

    if (e.FilePath.EndsWith("stop!!!.txt", StringComparison.OrdinalIgnoreCase))
        cancellationTokenSource.Cancel();
};

fileExplorer.ExploreDirectory(filePathForExplore, cancellationTokenSource.Token);

Console.WriteLine("Exploration completed");
