## Примеры.

## 1. Фабричный метод + Прототип.

```csharp
using System;

// Интерфейс для прототипов документов
public interface IDocumentPrototype
{
    IDocumentPrototype Clone();
    void Print();
}

// Класс документа для отчёта
public class ReportDocument : IDocumentPrototype
{
    public string Content { get; set; }

    public ReportDocument(string content)
    {
        Content = content;
    }

    // Реализация клонирования (создаётся копия объекта)
    public IDocumentPrototype Clone()
    {
        return new ReportDocument(Content);
    }

    public void Print()
    {
        Console.WriteLine($"Отчёт: {Content}");
    }
}

// Класс документа для договора
public class ContractDocument : IDocumentPrototype
{
    public string Terms { get; set; }

    public ContractDocument(string terms)
    {
        Terms = terms;
    }

    // Реализация клонирования
    public IDocumentPrototype Clone()
    {
        return new ContractDocument(Terms);
    }

    public void Print()
    {
        Console.WriteLine($"Договор: {Terms}");
    }
}

// Абстрактный фабричный класс для создания документов
public abstract class DocumentFactory
{
    public abstract IDocumentPrototype CreateDocument();
}

// Фабрика для создания отчётов путём клонирования
public class ReportDocumentFactory : DocumentFactory
{
    private readonly IDocumentPrototype _prototype;

    public ReportDocumentFactory(IDocumentPrototype prototype)
    {
        _prototype = prototype;
    }

    public override IDocumentPrototype CreateDocument()
    {
        return _prototype.Clone();
    }
}

// Фабрика для создания договоров путём клонирования
public class ContractDocumentFactory : DocumentFactory
{
    private readonly IDocumentPrototype _prototype;

    public ContractDocumentFactory(IDocumentPrototype prototype)
    {
        _prototype = prototype;
    }

    public override IDocumentPrototype CreateDocument()
    {
        return _prototype.Clone();
    }
}

// Тестовая программа
public class Program
{
    public static void Main(string[] args)
    {
        // Создаём прототипы документов
        IDocumentPrototype reportPrototype = new ReportDocument("Ежемесячный отчёт о продажах");
        IDocumentPrototype contractPrototype = new ContractDocument("Условия договора аренды");

        // Создаём фабрики для документов, используя прототипы
        DocumentFactory reportFactory = new ReportDocumentFactory(reportPrototype);
        DocumentFactory contractFactory = new ContractDocumentFactory(contractPrototype);

        // Создаём новые документы на основе прототипов
        IDocumentPrototype report1 = reportFactory.CreateDocument();
        IDocumentPrototype report2 = reportFactory.CreateDocument();
        IDocumentPrototype contract1 = contractFactory.CreateDocument();

        // Выводим документы
        report1.Print();
        report2.Print();
        contract1.Print();
    }
}

```

## 2. Фабричный метод + Строитель.

```csharp
using System;
using System.Text;

// Продукт — компьютерная конфигурация
public class Computer
{
    public string Processor { get; set; }
    public string Memory { get; set; }
    public string Storage { get; set; }
    public string GraphicsCard { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Процессор: {Processor}");
        sb.AppendLine($"Память: {Memory}");
        sb.AppendLine($"Накопитель: {Storage}");
        sb.AppendLine($"Видеокарта: {GraphicsCard}");
        return sb.ToString();
    }
}

// Интерфейс строителя
public interface IComputerBuilder
{
    void SetProcessor();
    void SetMemory();
    void SetStorage();
    void SetGraphicsCard();
    Computer GetComputer();
}

// Конкретный строитель для офисного компьютера
public class OfficeComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetProcessor() => _computer.Processor = "Intel Core i3";
    public void SetMemory() => _computer.Memory = "8 GB";
    public void SetStorage() => _computer.Storage = "256 GB SSD";
    public void SetGraphicsCard() => _computer.GraphicsCard = "Integrated Graphics";

    public Computer GetComputer() => _computer;
}

// Конкретный строитель для игрового компьютера
public class GamingComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetProcessor() => _computer.Processor = "Intel Core i9";
    public void SetMemory() => _computer.Memory = "32 GB";
    public void SetStorage() => _computer.Storage = "1 TB SSD";
    public void SetGraphicsCard() => _computer.GraphicsCard = "NVIDIA GeForce RTX 3080";

    public Computer GetComputer() => _computer;
}

// Конкретный строитель для графического компьютера
public class GraphicsComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetProcessor() => _computer.Processor = "AMD Ryzen 9";
    public void SetMemory() => _computer.Memory = "64 GB";
    public void SetStorage() => _computer.Storage = "2 TB SSD";
    public void SetGraphicsCard() => _computer.GraphicsCard = "NVIDIA Quadro RTX 6000";

    public Computer GetComputer() => _computer;
}

// Фабричный метод для создания строителей
public abstract class ComputerFactory
{
    public abstract IComputerBuilder CreateBuilder();
}

// Фабрика для создания строителя офисного компьютера
public class OfficeComputerFactory : ComputerFactory
{
    public override IComputerBuilder CreateBuilder()
    {
        return new OfficeComputerBuilder();
    }
}

// Фабрика для создания строителя игрового компьютера
public class GamingComputerFactory : ComputerFactory
{
    public override IComputerBuilder CreateBuilder()
    {
        return new GamingComputerBuilder();
    }
}

// Фабрика для создания строителя графического компьютера
public class GraphicsComputerFactory : ComputerFactory
{
    public override IComputerBuilder CreateBuilder()
    {
        return new GraphicsComputerBuilder();
    }
}

// Директор, управляющий процессом сборки
public class ComputerDirector
{
    private readonly IComputerBuilder _builder;

    public ComputerDirector(IComputerBuilder builder)
    {
        _builder = builder;
    }

    public Computer BuildComputer()
    {
        _builder.SetProcessor();
        _builder.SetMemory();
        _builder.SetStorage();
        _builder.SetGraphicsCard();
        return _builder.GetComputer();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрики для разных конфигураций компьютеров
        ComputerFactory officeFactory = new OfficeComputerFactory();
        ComputerFactory gamingFactory = new GamingComputerFactory();
        ComputerFactory graphicsFactory = new GraphicsComputerFactory();

        // Создаём строителя через фабричный метод
        IComputerBuilder officeBuilder = officeFactory.CreateBuilder();
        IComputerBuilder gamingBuilder = gamingFactory.CreateBuilder();
        IComputerBuilder graphicsBuilder = graphicsFactory.CreateBuilder();

        // Используем директора для сборки компьютеров
        ComputerDirector officeDirector = new ComputerDirector(officeBuilder);
        ComputerDirector gamingDirector = new ComputerDirector(gamingBuilder);
        ComputerDirector graphicsDirector = new ComputerDirector(graphicsBuilder);

        // Строим компьютеры
        Computer officeComputer = officeDirector.BuildComputer();
        Computer gamingComputer = gamingDirector.BuildComputer();
        Computer graphicsComputer = graphicsDirector.BuildComputer();

        // Выводим результаты
        Console.WriteLine("Офисный компьютер:\n" + officeComputer);
        Console.WriteLine("Игровой компьютер:\n" + gamingComputer);
        Console.WriteLine("Компьютер для графики:\n" + graphicsComputer);
    }
}

```

## 3. Фабричный метод + компоновщик.

```csharp
using System;
using System.Collections.Generic;

// Общий интерфейс для компонентов файловой системы (Компоновщик)
public interface IFileSystemComponent
{
    void Display(int depth = 0);
}

// Класс для представления файла (Лист)
public class File : IFileSystemComponent
{
    private readonly string _name;

    public File(string name)
    {
        _name = name;
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + _name);
    }
}

// Класс для представления папки (Контейнер)
public class Directory : IFileSystemComponent
{
    private readonly string _name;
    private readonly List<IFileSystemComponent> _components = new List<IFileSystemComponent>();

    public Directory(string name)
    {
        _name = name;
    }

    public void AddComponent(IFileSystemComponent component)
    {
        _components.Add(component);
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + _name);
        foreach (var component in _components)
        {
            component.Display(depth + 2);
        }
    }
}

// Абстрактный класс фабрики
public abstract class FileSystemFactory
{
    public abstract IFileSystemComponent CreateFile(string name);
    public abstract IFileSystemComponent CreateDirectory(string name);
}

// Конкретная фабрика для создания файлов и папок
public class DefaultFileSystemFactory : FileSystemFactory
{
    public override IFileSystemComponent CreateFile(string name)
    {
        return new File(name);
    }

    public override IFileSystemComponent CreateDirectory(string name)
    {
        return new Directory(name);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрику файловой системы
        FileSystemFactory factory = new DefaultFileSystemFactory();

        // Создаём корневую папку
        var rootDirectory = factory.CreateDirectory("Root");

        // Создаём файлы и папки в корневой папке
        var file1 = factory.CreateFile("File1.txt");
        var file2 = factory.CreateFile("File2.txt");
        var subDirectory1 = factory.CreateDirectory("SubDirectory1");
        var subDirectory2 = factory.CreateDirectory("SubDirectory2");

        // Добавляем файлы и папки в корневую папку
        ((Directory)rootDirectory).AddComponent(file1);
        ((Directory)rootDirectory).AddComponent(file2);
        ((Directory)rootDirectory).AddComponent(subDirectory1);
        ((Directory)rootDirectory).AddComponent(subDirectory2);

        // Добавляем вложенные файлы и папки
        var subFile1 = factory.CreateFile("SubFile1.txt");
        var subFile2 = factory.CreateFile("SubFile2.txt");
        ((Directory)subDirectory1).AddComponent(subFile1);
        ((Directory)subDirectory2).AddComponent(subFile2);

        // Отображаем структуру файловой системы
        rootDirectory.Display();
    }
}
```

## 4. Фабричный метод + Абстрактная фабрика.

```csharp
```

## 5. Фабричный метод + Декоратор.

```csharp
using System;

// Интерфейс для сообщения
public interface IMessage
{
    string GetContent();
}

// Класс базового сообщения
public class SimpleMessage : IMessage
{
    private readonly string _content;

    public SimpleMessage(string content)
    {
        _content = content;
    }

    public string GetContent()
    {
        return _content;
    }
}

// Базовый класс декоратора
public abstract class MessageDecorator : IMessage
{
    protected IMessage _message;

    protected MessageDecorator(IMessage message)
    {
        _message = message;
    }

    public abstract string GetContent();
}

// Декоратор для шифрования сообщения
public class EncryptedMessage : MessageDecorator
{
    public EncryptedMessage(IMessage message) : base(message) { }

    public override string GetContent()
    {
        return $"[Encrypted: {_message.GetContent()}]";
    }
}

// Декоратор для сжатия сообщения
public class CompressedMessage : MessageDecorator
{
    public CompressedMessage(IMessage message) : base(message) { }

    public override string GetContent()
    {
        return $"[Compressed: {_message.GetContent()}]";
    }
}

// Абстрактный класс фабрики для создания сообщений
public abstract class MessageFactory
{
    public abstract IMessage CreateMessage(string content);
}

// Фабрика для создания простого сообщения
public class SimpleMessageFactory : MessageFactory
{
    public override IMessage CreateMessage(string content)
    {
        return new SimpleMessage(content);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрику для простого сообщения
        MessageFactory factory = new SimpleMessageFactory();

        // Создаём базовое сообщение через фабричный метод
        IMessage message = factory.CreateMessage("Hello, World!");

        // Декорируем сообщение: добавляем шифрование и сжатие
        IMessage encryptedMessage = new EncryptedMessage(message);
        IMessage compressedAndEncryptedMessage = new CompressedMessage(encryptedMessage);

        // Выводим результаты
        Console.WriteLine("Оригинальное сообщение: " + message.GetContent());
        Console.WriteLine("Зашифрованное сообщение: " + encryptedMessage.GetContent());
        Console.WriteLine("Сжатое и зашифрованное сообщение: " + compressedAndEncryptedMessage.GetContent());
    }
}
```

## 6. Фабричный метод + фасад.

## Сценарий:
Предположим, у нас есть система управления домашним кинотеатром, которая состоит из различных устройств. Фабричный метод используется для создания компонентов домашнего кинотеатра, таких как проектор, звуковая система и проигрыватель. Фасад предоставляет единый интерфейс для управления всем процессом, позволяя, например, запустить всю систему для просмотра фильма одной командой.

```csharp
using System;

// Компоненты домашнего кинотеатра
public class Projector
{
    public void On() => Console.WriteLine("Проектор включен.");
    public void Off() => Console.WriteLine("Проектор выключен.");
}

public class SoundSystem
{
    public void On() => Console.WriteLine("Звуковая система включена.");
    public void Off() => Console.WriteLine("Звуковая система выключена.");
    public void SetVolume(int level) => Console.WriteLine($"Громкость установлена на уровень {level}.");
}

public class MediaPlayer
{
    public void On() => Console.WriteLine("Проигрыватель включен.");
    public void Off() => Console.WriteLine("Проигрыватель выключен.");
    public void Play(string movie) => Console.WriteLine($"Проигрывание фильма: {movie}");
}

// Фасад для управления домашним кинотеатром
public class HomeTheaterFacade
{
    private readonly Projector _projector;
    private readonly SoundSystem _soundSystem;
    private readonly MediaPlayer _mediaPlayer;

    public HomeTheaterFacade(Projector projector, SoundSystem soundSystem, MediaPlayer mediaPlayer)
    {
        _projector = projector;
        _soundSystem = soundSystem;
        _mediaPlayer = mediaPlayer;
    }

    public void WatchMovie(string movie)
    {
        Console.WriteLine("Подготовка к просмотру фильма...");
        _projector.On();
        _soundSystem.On();
        _soundSystem.SetVolume(5);
        _mediaPlayer.On();
        _mediaPlayer.Play(movie);
    }

    public void EndMovie()
    {
        Console.WriteLine("Выключение системы кинотеатра...");
        _projector.Off();
        _soundSystem.Off();
        _mediaPlayer.Off();
    }
}

// Абстрактная фабрика для создания компонентов домашнего кинотеатра
public abstract class HomeTheaterFactory
{
    public abstract Projector CreateProjector();
    public abstract SoundSystem CreateSoundSystem();
    public abstract MediaPlayer CreateMediaPlayer();
}

// Конкретная фабрика для создания стандартных компонентов домашнего кинотеатра
public class StandardHomeTheaterFactory : HomeTheaterFactory
{
    public override Projector CreateProjector()
    {
        return new Projector();
    }

    public override SoundSystem CreateSoundSystem()
    {
        return new SoundSystem();
    }

    public override MediaPlayer CreateMediaPlayer()
    {
        return new MediaPlayer();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрику компонентов домашнего кинотеатра
        HomeTheaterFactory factory = new StandardHomeTheaterFactory();

        // Создаём компоненты через фабричный метод
        Projector projector = factory.CreateProjector();
        SoundSystem soundSystem = factory.CreateSoundSystem();
        MediaPlayer mediaPlayer = factory.CreateMediaPlayer();

        // Создаём фасад для домашнего кинотеатра
        HomeTheaterFacade homeTheater = new HomeTheaterFacade(projector, soundSystem, mediaPlayer);

        // Используем фасад для запуска системы
        homeTheater.WatchMovie("Inception");

        Console.WriteLine();

        // Останавливаем систему
        homeTheater.EndMovie();
    }
}
```

## Пояснение к коду.

1. Классы Projector, SoundSystem, MediaPlayer представляют компоненты домашнего кинотеатра. У каждого из них есть методы для включения и выключения устройства, а также дополнительный метод, например, SetVolume для звуковой системы и Play для проигрывателя.

2. Класс HomeTheaterFacade реализует фасад, предоставляя методы WatchMovie и EndMovie, которые упрощают управление всеми компонентами. В методе WatchMovie включаются все устройства, устанавливается громкость и запускается фильм. В методе EndMovie выключаются все устройства.

3. Абстрактный класс HomeTheaterFactory определяет фабричные методы CreateProjector, CreateSoundSystem и CreateMediaPlayer, которые используются для создания экземпляров компонентов.

4. Класс StandardHomeTheaterFactory реализует абстрактные методы, создавая стандартные компоненты домашнего кинотеатра.

5. В методе Main создаются компоненты через фабрику и передаются в фасад. После этого с помощью фасада запускается и останавливается система домашнего кинотеатра.