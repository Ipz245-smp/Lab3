using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Завдання 1: Адаптер");
        string logFile = "log.txt";
        if (File.Exists(logFile)) File.Delete(logFile);

        var fileLogger = new FileLogger(logFile);
        fileLogger.Log("Application started");
        fileLogger.Warn("Low memory warning");
        fileLogger.Error("Something went wrong!");
        Console.WriteLine($"(Також записано у файл: {logFile})");
        Console.WriteLine();

        Console.WriteLine("Завдання 2: Декоратор");
        IHero warrior = new Warrior();
        warrior = new SwordDecorator(warrior);
        warrior = new ShieldDecorator(warrior);
        warrior = new MagicRingDecorator(warrior);
        Console.WriteLine(warrior.GetDescription());

        IHero mage = new Mage();
        mage = new MagicRingDecorator(mage);
        mage = new SwordDecorator(mage);
        Console.WriteLine(mage.GetDescription());

        IHero palladin = new Palladin();
        palladin = new ArmorDecorator(palladin);
        palladin = new ShieldDecorator(palladin);
        Console.WriteLine(palladin.GetDescription());
        Console.WriteLine();

        Console.WriteLine("Завдання 3: Міст");
        var vector = new VectorRenderer();
        var raster = new RasterRenderer();

        Shape circle    = new Circle(vector);
        Shape square    = new Square(raster);
        Shape triangle  = new Triangle(vector);
        Shape triangle2 = new Triangle(raster);

        circle.Draw();
        square.Draw();
        triangle.Draw();
        triangle2.Draw();
        Console.WriteLine();

        Console.WriteLine("Завдання 4: Проксі");
        string testFile = "test.txt";
        File.WriteAllLines(testFile, new[]
        {
            "Hello World",
            "This is line two",
            "Third line here"
        });

        var reader  = new SmartTextReader();
        var checker = new SmartTextChecker(reader);
        checker.Read(testFile);
        Console.WriteLine();

        var locker = new SmartTextReaderLocker(reader, @"secret.*\.txt");
        Console.Write("Reading test.txt via locker: ");
        var res = locker.Read(testFile);
        if (res != null) Console.WriteLine("Read OK");

        Console.Write("Reading secret_data.txt via locker: ");
        locker.Read("secret_data.txt");
        Console.WriteLine();

        File.Delete(testFile);

        Console.WriteLine("Завдання 5: Компонувальник (LightHTML)");
        var ul = new LightElementNode("ul", DisplayType.Block, ClosingType.WithClosing, "menu");

        var li1 = new LightElementNode("li", DisplayType.Block, ClosingType.WithClosing);
        li1.AddChild(new LightTextNode("Home"));

        var li2 = new LightElementNode("li", DisplayType.Block, ClosingType.WithClosing);
        li2.AddChild(new LightTextNode("About"));

        var li3 = new LightElementNode("li", DisplayType.Block, ClosingType.WithClosing);
        li3.AddChild(new LightTextNode("Contact"));

        ul.AddChild(li1);
        ul.AddChild(li2);
        ul.AddChild(li3);

        Console.WriteLine("OuterHTML:");
        Console.WriteLine(ul.OuterHTML);
        Console.WriteLine($"Children count: {ul.ChildCount}");
        Console.WriteLine();

        Console.WriteLine("Завдання 6: Легковаговик");

        string bookFile = "romeo_and_juliet.txt";
        if (!File.Exists(bookFile))
        {
            Console.WriteLine("Завантаження книги з Project Gutenberg...");
            using (var client = new WebClient())
                client.DownloadFile("https://www.gutenberg.org/cache/epub/1513/pg1513.txt", bookFile);
            Console.WriteLine("Завантажено!");
        }

        string[] bookLines = File.ReadAllLines(bookFile);
        Console.WriteLine($"Всього рядків у книзі: {bookLines.Length}");

        long memWithout = 0;
        var uniqueTags = new HashSet<string>();
        for (int i = 0; i < bookLines.Length; i++)
        {
            string line = bookLines[i];
            string tag  = GetTag(i, line);
            uniqueTags.Add(tag);

            memWithout += 48;
            memWithout += 20 + tag.Length * 2;
            memWithout += 24;
            memWithout += 20 + line.Trim().Length * 2;
        }

        long memWith = 0;
        foreach (var tag in uniqueTags)
            memWith += 48 + 20 + tag.Length * 2;

        for (int i = 0; i < bookLines.Length; i++)
        {
            string line = bookLines[i];
            memWith += 32;
            memWith += 20 + line.Trim().Length * 2;
        }

        Console.WriteLine($"Пам'ять без Легковаговика: ~{memWithout} bytes");
        Console.WriteLine($"Пам'ять з Легковаговиком:  ~{memWith} bytes");
        Console.WriteLine($"Економія пам'яті: ~{memWithout - memWith} bytes");
        Console.WriteLine($"Унікальних flyweight об'єктів у кеші: {uniqueTags.Count}");

        var factory        = new FlyweightFactory();
        var flyweightNodes = new List<FlyweightNode>();
        for (int i = 0; i < bookLines.Length; i++)
        {
            string line = bookLines[i];
            string tag  = GetTag(i, line);
            var fw = factory.GetFlyweight(tag, DisplayType.Block, ClosingType.WithClosing, new List<string>());
            flyweightNodes.Add(new FlyweightNode(fw, line.Trim()));
        }

        Console.WriteLine("\nПерші 5 елементів:");
        for (int i = 0; i < Math.Min(5, flyweightNodes.Count); i++)
            Console.WriteLine(flyweightNodes[i].OuterHTML);
    }

    static string GetTag(int index, string line)
    {
        if (index == 0)                                    return "h1";
        if (line.Length < 20)                              return "h2";
        if (line.StartsWith(" ") || line.StartsWith("\t")) return "blockquote";
        return "p";
    }
}
