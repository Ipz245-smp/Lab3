using System;
using System.IO;

class Logger
{
    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[LOG] {message}");
        Console.ResetColor();
    }

    public void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ResetColor();
    }

    public void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARN] {message}");
        Console.ResetColor();
    }
}

class FileWriter
{
    private string _path;

    public FileWriter(string path)
    {
        _path = path;
    }

    public void Write(string text)
    {
        File.AppendAllText(_path, text);
    }

    public void WriteLine(string text)
    {
        File.AppendAllText(_path, text + Environment.NewLine);
    }
}

class FileLogger : Logger
{
    private FileWriter _fileWriter;

    public FileLogger(string filePath)
    {
        _fileWriter = new FileWriter(filePath);
    }

    public new void Log(string message)
    {
        base.Log(message);
        _fileWriter.WriteLine($"[LOG] {message}");
    }

    public new void Error(string message)
    {
        base.Error(message);
        _fileWriter.WriteLine($"[ERROR] {message}");
    }

    public new void Warn(string message)
    {
        base.Warn(message);
        _fileWriter.WriteLine($"[WARN] {message}");
    }
}
