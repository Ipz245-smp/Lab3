using System;
using System.IO;
using System.Text.RegularExpressions;

interface ITextReader
{
    char[][] Read(string filePath);
}

class SmartTextReader : ITextReader
{
    public char[][] Read(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        char[][] result = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
            result[i] = lines[i].ToCharArray();
        return result;
    }
}

class SmartTextChecker : ITextReader
{
    private SmartTextReader _reader;

    public SmartTextChecker(SmartTextReader reader)
    {
        _reader = reader;
    }

    public char[][] Read(string filePath)
    {
        Console.WriteLine($"[Checker] Opening file: {filePath}");
        char[][] result = _reader.Read(filePath);
        Console.WriteLine($"[Checker] File read successfully.");
        Console.WriteLine($"[Checker] Total lines: {result.Length}");

        int totalChars = 0;
        foreach (var line in result)
            totalChars += line.Length;

        Console.WriteLine($"[Checker] Total characters: {totalChars}");
        Console.WriteLine($"[Checker] File closed.");
        return result;
    }
}

class SmartTextReaderLocker : ITextReader
{
    private SmartTextReader _reader;
    private Regex _lockedPattern;

    public SmartTextReaderLocker(SmartTextReader reader, string pattern)
    {
        _reader = reader;
        _lockedPattern = new Regex(pattern);
    }

    public char[][] Read(string filePath)
    {
        if (_lockedPattern.IsMatch(filePath))
        {
            Console.WriteLine("Access denied!");
            return null;
        }
        return _reader.Read(filePath);
    }
}
