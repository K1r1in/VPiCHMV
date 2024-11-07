## Примеры.

## Фабричный метод

## 1. Фабричный метод + Прототип.

## Сценарий:

Представим, что у нас есть система создания документов, и каждый документ имеет тип (например, договор, отчёт). Вместо создания каждого нового документа с нуля, фабричный метод клонирует уже существующий экземпляр-прототип.

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

## Пояснение к коду

1. Интерфейс IDocumentPrototype определяет метод Clone, который позволяет создавать копию объекта.

2. Классы ReportDocument и ContractDocument реализуют интерфейс IDocumentPrototype и содержат реализацию метода Clone. Каждый из них представляет свой тип документа и имеет специфичное для него поле (например, Content для отчёта и Terms для договора).

3. Фабричный класс DocumentFactory определяет абстрактный метод CreateDocument для создания документов.

4. Классы ReportDocumentFactory и ContractDocumentFactory наследуются от DocumentFactory. Эти фабрики используют прототипы документов и создают новые документы путём клонирования исходного прототипа.

5. В методе Main создаются экземпляры прототипов, фабрики и затем документы путём клонирования. Это позволяет гибко создавать документы на основе уже существующих шаблонов.

## 2. Фабричный метод + Строитель.

## Сценарий:

Предположим, мы создаём систему генерации различных типов компьютерных конфигураций (например, для офисной работы, игр, и работы с графикой). Каждый тип конфигурации включает в себя различные компоненты (процессор, память, диск и т.д.). В данном случае:

Фабричный метод создаёт конкретного строителя в зависимости от типа конфигурации.
Строитель отвечает за сборку каждой конкретной конфигурации, добавляя нужные компоненты.

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

## Пояснение к коду

1. Класс Computer представляет сложный объект — компьютер, имеющий несколько свойств (процессор, память, накопитель и видеокарта).

2. Интерфейс IComputerBuilder определяет методы для установки компонентов компьютера. Каждый конкретный строитель реализует этот интерфейс для конфигурации конкретного типа компьютера.

3. Классы OfficeComputerBuilder, GamingComputerBuilder, GraphicsComputerBuilder реализуют интерфейс IComputerBuilder, настраивая компоненты компьютера для различных типов конфигураций (офисной, игровой, графической).

4. Фабричные классы (OfficeComputerFactory, GamingComputerFactory, GraphicsComputerFactory) наследуются от ComputerFactory. Каждый класс создаёт нужного строителя в зависимости от типа конфигурации компьютера.

5. Класс ComputerDirector управляет процессом сборки и отвечает за последовательность вызова методов строителя. Это позволяет изолировать процесс сборки от основной логики.

6. В методе Main создаются конкретные фабрики для офисного, игрового и графического компьютеров, а затем директора используют созданных строителей для сборки объектов.

## 3. Фабричный метод + компоновщик.

## Сценарий: 

Предположим, что у нас есть структура файловой системы, состоящая из файлов и папок, где папки могут содержать как файлы, так и другие папки. Фабричный метод будет использоваться для создания файлов и папок, а компоновщик обеспечит вложенность и иерархию.

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

## Пояснение к коду

1. Интерфейс IFileSystemComponent — общий интерфейс компоновщика, который предоставляет метод Display для отображения компонентов файловой системы.

2. Класс File — реализует интерфейс IFileSystemComponent и представляет файл (лист). Метод Display выводит имя файла с заданным уровнем вложенности.

3. Класс Directory — также реализует IFileSystemComponent и представляет папку (контейнер). Папка может содержать вложенные компоненты файловой системы (и файлы, и папки), благодаря методу AddComponent.

4. Абстрактный класс FileSystemFactory — определяет фабричный метод для создания файлов и папок.

5. Класс DefaultFileSystemFactory — конкретная реализация фабрики, создающая экземпляры File и Directory.

6. В методе Main — создаётся структура файловой системы, состоящая из папок и файлов. Фабрика упрощает создание различных компонентов, а компоновщик позволяет удобно организовывать и отображать вложенную структуру.

## 4. Фабричный метод + Абстрактная фабрика.

```csharp
```

## 5. Фабричный метод + Декоратор.

## Сценарий:

Предположим, у нас есть система отправки сообщений, где каждое сообщение может быть дополнительно зашифровано или сжато перед отправкой. Фабричный метод используется для создания базового сообщения, а декораторы добавляют дополнительные функциональности.

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

## Пояснение к коду

Пояснение к коду
1. Интерфейс IMessage определяет метод GetContent, который возвращает содержимое сообщения.

2. Класс SimpleMessage реализует интерфейс IMessage и представляет базовое сообщение.

3. Базовый класс MessageDecorator реализует интерфейс IMessage и хранит ссылку на компонент, который он декорирует. Он определяет метод GetContent как абстрактный, оставляя его реализацию дочерним классам.

4. Декоратор EncryptedMessage наследуется от MessageDecorator и добавляет шифрование к содержимому сообщения.

5. Декоратор CompressedMessage также наследуется от MessageDecorator и добавляет сжатие к содержимому сообщения.

6. Абстрактный класс MessageFactory определяет фабричный метод CreateMessage, который создаёт объекты сообщений.

7. Класс SimpleMessageFactory реализует фабричный метод, возвращая экземпляры SimpleMessage.

8. В методе Main создаётся простое сообщение через фабрику, и затем к этому сообщению последовательно добавляются декораторы для шифрования и сжатия.

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

## 7. Фабричный метод + Легковес.

## Сценарий:

Предположим, что у нас есть приложение для рисования различных деревьев в лесу. Каждое дерево имеет тип (например, "Сосна", "Берёза") и общие визуальные данные (например, текстура листьев и коры). Лёгковес помогает создавать деревья с общим состоянием, сохраняя при этом уникальные данные (например, координаты) отдельно.

```csharp
using System;
using System.Collections.Generic;

// Общий интерфейс для дерева
public interface ITree
{
    void Display(int x, int y);
}

// Класс легковеса для конкретного типа дерева
public class TreeType : ITree
{
    private readonly string _name;
    private readonly string _texture;

    public TreeType(string name, string texture)
    {
        _name = name;
        _texture = texture;
    }

    public void Display(int x, int y)
    {
        Console.WriteLine($"Отображение дерева '{_name}' с текстурой '{_texture}' на позиции ({x}, {y}).");
    }
}

// Фабрика для создания и управления объектами TreeType (Легковес)
public class TreeFactory
{
    private readonly Dictionary<string, TreeType> _treeTypes = new Dictionary<string, TreeType>();

    public TreeType GetTreeType(string name, string texture)
    {
        var key = name + texture;
        
        if (!_treeTypes.ContainsKey(key))
        {
            _treeTypes[key] = new TreeType(name, texture);
            Console.WriteLine($"Создан новый тип дерева: {name} с текстурой: {texture}");
        }
        else
        {
            Console.WriteLine($"Использование существующего типа дерева: {name} с текстурой: {texture}");
        }

        return _treeTypes[key];
    }
}

// Класс дерева с координатами
public class Tree
{
    private readonly int _x;
    private readonly int _y;
    private readonly TreeType _type;

    public Tree(int x, int y, TreeType type)
    {
        _x = x;
        _y = y;
        _type = type;
    }

    public void Display()
    {
        _type.Display(_x, _y);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        TreeFactory factory = new TreeFactory();

        // Создаём несколько деревьев
        Tree tree1 = new Tree(10, 20, factory.GetTreeType("Сосна", "Текстура Сосны"));
        Tree tree2 = new Tree(30, 40, factory.GetTreeType("Берёза", "Текстура Берёзы"));
        Tree tree3 = new Tree(50, 60, factory.GetTreeType("Сосна", "Текстура Сосны"));
        Tree tree4 = new Tree(70, 80, factory.GetTreeType("Берёза", "Текстура Берёзы"));

        // Отображаем деревья
        tree1.Display();
        tree2.Display();
        tree3.Display();
        tree4.Display();
    }
}
```

## Пояснение к коду

1. Интерфейс ITree определяет метод Display для отображения дерева на заданных координатах.

2. Класс TreeType реализует ITree и хранит общие данные о дереве (его имя и текстуру). Этот класс реализует поведение лёгкого объекта, который может использоваться совместно.

3. Класс TreeFactory реализует фабричный метод GetTreeType, который возвращает экземпляр TreeType. Если объект с нужными характеристиками уже существует, фабрика возвращает его; если нет — создаёт новый.

4. Класс Tree хранит координаты дерева и ссылку на объект TreeType. Он представляет полное дерево, состоящее из уникального состояния (координаты) и общего состояния (тип дерева).

5. В методе Main создаются несколько деревьев через фабрику. Деревья, которые имеют одинаковый тип (например, "Сосна" с определённой текстурой), будут использовать один и тот же объект TreeType, созданный фабрикой.

## 8. Фабричный метод + Заместитель.

## Сценарий:

Предположим, у нас есть система, которая работает с чувствительными данными — скажем, с базой данных пользователей. Заместитель будет контролировать доступ к базе данных, позволяя, например, получить доступ только после проверки прав. Фабричный метод создаёт объект либо самой базы данных, либо её заместителя.

```csharp
using System;

// Интерфейс, представляющий базу данных
public interface IUserDatabase
{
    void DisplayUserData();
}

// Класс, представляющий настоящую базу данных
public class RealUserDatabase : IUserDatabase
{
    private readonly string _data;

    public RealUserDatabase(string data)
    {
        _data = data;
    }

    public void DisplayUserData()
    {
        Console.WriteLine($"Данные пользователя: {_data}");
    }
}

// Класс заместителя, контролирующий доступ к базе данных
public class UserDatabaseProxy : IUserDatabase
{
    private RealUserDatabase _realDatabase;
    private readonly string _data;
    private bool _hasAccess;

    public UserDatabaseProxy(string data)
    {
        _data = data;
    }

    public void GrantAccess()
    {
        _hasAccess = true;
        Console.WriteLine("Доступ к базе данных предоставлен.");
    }

    public void RevokeAccess()
    {
        _hasAccess = false;
        Console.WriteLine("Доступ к базе данных закрыт.");
    }

    public void DisplayUserData()
    {
        if (_hasAccess)
        {
            if (_realDatabase == null)
            {
                _realDatabase = new RealUserDatabase(_data);
            }
            _realDatabase.DisplayUserData();
        }
        else
        {
            Console.WriteLine("Доступ запрещён. Невозможно показать данные.");
        }
    }
}

// Абстрактная фабрика для создания объектов базы данных
public abstract class DatabaseFactory
{
    public abstract IUserDatabase CreateDatabase(string data);
}

// Конкретная фабрика для создания прокси (заместителя) базы данных
public class ProxyDatabaseFactory : DatabaseFactory
{
    public override IUserDatabase CreateDatabase(string data)
    {
        return new UserDatabaseProxy(data);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрику для базы данных с контролем доступа
        DatabaseFactory factory = new ProxyDatabaseFactory();

        // Создаём прокси-объект через фабрику
        IUserDatabase userDatabase = factory.CreateDatabase("Секретные данные пользователя");

        // Попытка доступа без разрешения
        userDatabase.DisplayUserData();

        // Предоставляем доступ и повторяем попытку
        UserDatabaseProxy proxy = (UserDatabaseProxy)userDatabase;
        proxy.GrantAccess();
        userDatabase.DisplayUserData();

        // Закрываем доступ и снова пытаемся получить данные
        proxy.RevokeAccess();
        userDatabase.DisplayUserData();
    }
}
```

## Пояснение к коду

1. Интерфейс IUserDatabase определяет метод DisplayUserData, который отвечает за отображение данных пользователя.

2. Класс RealUserDatabase реализует IUserDatabase и представляет настоящую базу данных, содержащую чувствительные данные.

3. Класс UserDatabaseProxy реализует IUserDatabase и является заместителем для RealUserDatabase. Он контролирует доступ к настоящей базе данных через методы GrantAccess и RevokeAccess. Если доступ закрыт, данные не отображаются.

4. Абстрактный класс DatabaseFactory определяет фабричный метод CreateDatabase, который создаёт объекты базы данных.

5. Класс ProxyDatabaseFactory реализует фабричный метод и возвращает объект UserDatabaseProxy, что добавляет возможность контролировать доступ к данным.

6. В методе Main создаётся прокси-база данных через фабрику. Затем проверяется поведение программы при попытке получить данные без доступа, при предоставлении доступа и при его закрытии.

## 9. Фабричный метод + Стратегия.

## Сценарий:

Предположим, что у нас есть система для обработки платежей, которая поддерживает разные методы оплаты (например, кредитной картой или через PayPal). Мы применяем Стратегию, чтобы динамически изменять способ обработки платежа в зависимости от выбранной стратегии, а Фабричный метод используется для создания различных стратегий.

```csharp
using System;

// Интерфейс стратегии для обработки платежа
public interface IPaymentStrategy
{
    void ProcessPayment(double amount);
}

// Конкретная стратегия для оплаты через кредитную карту
public class CreditCardPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через кредитную карту на сумму {amount}.");
    }
}

// Конкретная стратегия для оплаты через PayPal
public class PayPalPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через PayPal на сумму {amount}.");
    }
}

// Фабрика для создания стратегии оплаты
public abstract class PaymentFactory
{
    public abstract IPaymentStrategy CreatePaymentMethod();
}

// Конкретная фабрика для создания стратегии оплаты через кредитную карту
public class CreditCardPaymentFactory : PaymentFactory
{
    public override IPaymentStrategy CreatePaymentMethod()
    {
        return new CreditCardPayment();
    }
}

// Конкретная фабрика для создания стратегии оплаты через PayPal
public class PayPalPaymentFactory : PaymentFactory
{
    public override IPaymentStrategy CreatePaymentMethod()
    {
        return new PayPalPayment();
    }
}

// Контекст, использующий стратегию
public class PaymentProcessor
{
    private IPaymentStrategy _paymentStrategy;

    public PaymentProcessor(PaymentFactory paymentFactory)
    {
        _paymentStrategy = paymentFactory.CreatePaymentMethod();
    }

    public void Process(double amount)
    {
        _paymentStrategy.ProcessPayment(amount);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Пользователь выбирает способ оплаты через кредитную карту
        PaymentFactory creditCardFactory = new CreditCardPaymentFactory();
        PaymentProcessor processor1 = new PaymentProcessor(creditCardFactory);
        processor1.Process(150.0);

        // Пользователь выбирает способ оплаты через PayPal
        PaymentFactory payPalFactory = new PayPalPaymentFactory();
        PaymentProcessor processor2 = new PaymentProcessor(payPalFactory);
        processor2.Process(200.0);
    }
}

```

## Пояснение к коду

1. Интерфейс IPaymentStrategy определяет метод ProcessPayment, который реализуют все конкретные стратегии для обработки платежей.

2. Конкретные стратегии:
- CreditCardPayment — стратегия для обработки платежей через кредитную карту.
- PayPalPayment — стратегия для обработки платежей через PayPal.
3. Абстрактный класс PaymentFactory содержит фабричный метод CreatePaymentMethod, который должен быть реализован в конкретных фабриках для создания различных стратегий.

4. Конкретные фабрики:
- CreditCardPaymentFactory — фабрика для создания стратегии обработки через кредитную карту.
- PayPalPaymentFactory — фабрика для создания стратегии обработки через PayPal.
5. Класс PaymentProcessor принимает фабрику в конструкторе и использует её для создания стратегии. Затем вызывается метод Process, который обрабатывает платёж с использованием выбранной стратегии.

6. В методе Main создаются две разные фабрики для оплаты через кредитную карту и PayPal, и в обоих случаях платёж обрабатывается с использованием соответствующей стратегии.

## 10. Фабричный метод + Шаблонный метод.

## Сценарий:

Предположим, у нас есть система, которая генерирует отчеты в разных форматах (например, PDF и Excel). Шаблонный метод задает общий процесс генерации отчета (с извлечением данных, обработкой и выводом), а конкретные шаги могут быть изменены в дочерних классах, например, для разных форматов отчетов. Фабричный метод используется для создания объектов, которые генерируют отчеты в соответствующих форматах.

```csharp
using System;

// Абстрактный класс для отчета с шаблонным методом
public abstract class ReportGenerator
{
    // Шаблонный метод: общий алгоритм генерации отчета
    public void GenerateReport()
    {
        GatherData();
        ProcessData();
        FormatData();
        DisplayReport();
    }

    // Шаги, которые могут быть реализованы по-разному
    protected abstract void GatherData();
    protected abstract void ProcessData();
    protected abstract void FormatData();
    protected abstract void DisplayReport();
}

// Конкретный класс для PDF отчета
public class PdfReportGenerator : ReportGenerator
{
    protected override void GatherData()
    {
        Console.WriteLine("Сбор данных для PDF отчета.");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("Обработка данных для PDF отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для PDF отчета.");
    }

    protected override void DisplayReport()
    {
        Console.WriteLine("Вывод PDF отчета.");
    }
}

// Конкретный класс для Excel отчета
public class ExcelReportGenerator : ReportGenerator
{
    protected override void GatherData()
    {
        Console.WriteLine("Сбор данных для Excel отчета.");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("Обработка данных для Excel отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для Excel отчета.");
    }

    protected override void DisplayReport()
    {
        Console.WriteLine("Вывод Excel отчета.");
    }
}

// Абстрактная фабрика для создания генераторов отчетов
public abstract class ReportFactory
{
    public abstract ReportGenerator CreateReportGenerator();
}

// Фабрика для создания PDF отчета
public class PdfReportFactory : ReportFactory
{
    public override ReportGenerator CreateReportGenerator()
    {
        return new PdfReportGenerator();
    }
}

// Фабрика для создания Excel отчета
public class ExcelReportFactory : ReportFactory
{
    public override ReportGenerator CreateReportGenerator()
    {
        return new ExcelReportGenerator();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрики для генерации отчетов
        ReportFactory pdfFactory = new PdfReportFactory();
        ReportFactory excelFactory = new ExcelReportFactory();

        // Создаём генератор отчета через фабрику
        ReportGenerator pdfReport = pdfFactory.CreateReportGenerator();
        ReportGenerator excelReport = excelFactory.CreateReportGenerator();

        // Генерация отчетов
        Console.WriteLine("Генерация PDF отчета:");
        pdfReport.GenerateReport();

        Console.WriteLine("\nГенерация Excel отчета:");
        excelReport.GenerateReport();
    }
}
```

## Пояснение к коду

1. Абстрактный класс ReportGenerator:
- Этот класс содержит шаблонный метод GenerateReport(), который описывает общий алгоритм создания отчета. Он включает в себя шаги: сбор данных, обработка данных, форматирование и вывод отчета.
- Шаги, которые могут различаться в различных реализациях (например, форматирование данных), определены как абстрактные методы: GatherData(), ProcessData(), FormatData() и DisplayReport().
2. Конкретные классы отчетов:
- PdfReportGenerator и ExcelReportGenerator реализуют абстрактные методы и задают конкретные шаги для генерации отчетов в формате PDF и Excel, соответственно.
3. Абстрактная фабрика ReportFactory:
- Этот класс определяет фабричный метод CreateReportGenerator(), который возвращает объект типа ReportGenerator.
4. Конкретные фабрики:
- PdfReportFactory создает генератор для PDF отчета.
- ExcelReportFactory создает генератор для Excel отчета.
5. В методе Main создаются фабрики для отчетов в формате PDF и Excel. Затем вызывается метод GenerateReport() для каждого типа отчета, который выполняет общий алгоритм генерации, но с различиями в каждом конкретном шаге.

## 11. Фабричный метод + Состояние.

## Сценарий:

Предположим, у нас есть система управления устройством (например, лампочкой), которое может находиться в разных состояниях (включено или выключено). Состояние управляет поведением устройства в зависимости от его текущего состояния, а Фабричный метод используется для создания объектов, представляющих эти состояния.

```csharp
using System;

// Интерфейс состояния, определяющий поведение устройства
public interface IDeviceState
{
    void HandleRequest(Device device);
}

// Конкретное состояние: устройство включено
public class OnState : IDeviceState
{
    public void HandleRequest(Device device)
    {
        Console.WriteLine("Устройство включено. Переключаем на выключено.");
        device.SetState(new OffState());  // Переключаем состояние на Off
    }
}

// Конкретное состояние: устройство выключено
public class OffState : IDeviceState
{
    public void HandleRequest(Device device)
    {
        Console.WriteLine("Устройство выключено. Переключаем на включено.");
        device.SetState(new OnState());  // Переключаем состояние на On
    }
}

// Класс устройства, которое может менять состояния
public class Device
{
    private IDeviceState _currentState;

    public Device(IDeviceState initialState)
    {
        _currentState = initialState;
    }

    public void SetState(IDeviceState state)
    {
        _currentState = state;
    }

    public void Request()
    {
        _currentState.HandleRequest(this);  // Обрабатываем запрос в текущем состоянии
    }
}

// Абстрактная фабрика для создания состояния устройства
public abstract class DeviceStateFactory
{
    public abstract IDeviceState CreateState();
}

// Фабрика для создания состояния "включено"
public class OnStateFactory : DeviceStateFactory
{
    public override IDeviceState CreateState()
    {
        return new OnState();
    }
}

// Фабрика для создания состояния "выключено"
public class OffStateFactory : DeviceStateFactory
{
    public override IDeviceState CreateState()
    {
        return new OffState();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём фабрики для состояний
        DeviceStateFactory onFactory = new OnStateFactory();
        DeviceStateFactory offFactory = new OffStateFactory();

        // Инициализируем устройство с состоянием "выключено"
        Device device = new Device(offFactory.CreateState());

        // Переключаем устройство
        device.Request();  // Устройство включается
        device.Request();  // Устройство выключается
        device.Request();  // Устройство включается снова
    }
}
```

## Пояснение к коду

1. Интерфейс IDeviceState:
- Этот интерфейс определяет метод HandleRequest, который управляет поведением устройства в зависимости от его состояния. Метод принимает объект Device и может изменять его состояние.
2. Конкретные состояния:
- OnState — состояние, в котором устройство включено. При запросе оно переключает устройство в состояние "выключено".
- OffState — состояние, в котором устройство выключено. При запросе оно переключает устройство в состояние "включено".
3. Класс Device:
- Этот класс управляет состоянием устройства. Он хранит текущее состояние (объект, реализующий IDeviceState) и предоставляет метод Request, который делегирует обработку текущему состоянию.
- Метод SetState позволяет изменить состояние устройства.
4. Абстрактная фабрика DeviceStateFactory:
- Этот класс определяет фабричный метод CreateState, который должен быть реализован в конкретных фабриках для создания различных состояний устройства.
5. Конкретные фабрики:
- OnStateFactory создаёт объект состояния "включено".
- OffStateFactory создаёт объект состояния "выключено".
6. В методе Main создаются фабрики для состояний устройства. Инициализируется объект Device с состоянием "выключено". Затем вызывается метод Request, который переключает состояние устройства и выводит информацию о текущем состоянии.

## 12. Фабричный метод + Посетитель.

## Сценарий:

Предположим, у нас есть система, которая управляет коллекцией элементов, представляющих разные виды объектов (например, книги и журналы). Мы применяем Посетителя для выполнения различных операций с этими элементами (например, подсчёт цены или вывод информации), а Фабричный метод используется для создания этих объектов.

```csharp
using System;
using System.Collections.Generic;

// Абстрактный элемент, который принимает посетителя
public abstract class Item
{
    public abstract void Accept(IVisitor visitor);
}

// Конкретный элемент: книга
public class Book : Item
{
    public string Title { get; set; }
    public double Price { get; set; }

    public Book(string title, double price)
    {
        Title = title;
        Price = price;
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitBook(this);
    }
}

// Конкретный элемент: журнал
public class Magazine : Item
{
    public string Title { get; set; }
    public double Price { get; set; }

    public Magazine(string title, double price)
    {
        Title = title;
        Price = price;
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitMagazine(this);
    }
}

// Интерфейс посетителя
public interface IVisitor
{
    void VisitBook(Book book);
    void VisitMagazine(Magazine magazine);
}

// Конкретный посетитель: подсчёт стоимости
public class PriceCalculator : IVisitor
{
    private double _totalPrice = 0;

    public double GetTotalPrice() => _totalPrice;

    public void VisitBook(Book book)
    {
        Console.WriteLine($"Книга: {book.Title}, Цена: {book.Price}");
        _totalPrice += book.Price;
    }

    public void VisitMagazine(Magazine magazine)
    {
        Console.WriteLine($"Журнал: {magazine.Title}, Цена: {magazine.Price}");
        _totalPrice += magazine.Price;
    }
}

// Абстрактная фабрика для создания элементов
public abstract class ItemFactory
{
    public abstract Item CreateItem();
}

// Фабрика для создания книги
public class BookFactory : ItemFactory
{
    private string _title;
    private double _price;

    public BookFactory(string title, double price)
    {
        _title = title;
        _price = price;
    }

    public override Item CreateItem()
    {
        return new Book(_title, _price);
    }
}

// Фабрика для создания журнала
public class MagazineFactory : ItemFactory
{
    private string _title;
    private double _price;

    public MagazineFactory(string title, double price)
    {
        _title = title;
        _price = price;
    }

    public override Item CreateItem()
    {
        return new Magazine(_title, _price);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создание фабрик
        ItemFactory bookFactory = new BookFactory("C# для начинающих", 29.99);
        ItemFactory magazineFactory = new MagazineFactory("Tech Today", 5.99);

        // Создание элементов через фабрики
        Item book = bookFactory.CreateItem();
        Item magazine = magazineFactory.CreateItem();

        // Создание посетителя для подсчета стоимости
        PriceCalculator priceCalculator = new PriceCalculator();

        // Применение посетителя к элементам
        book.Accept(priceCalculator);
        magazine.Accept(priceCalculator);

        // Вывод общей стоимости
        Console.WriteLine($"Общая стоимость: {priceCalculator.GetTotalPrice()}");
    }
}
```

## Пояснение к коду

1. Абстрактный класс Item:
- Этот класс представляет общий интерфейс для всех элементов (например, книги и журналы), которые могут быть посещены посетителем. Метод Accept принимает объект посетителя.
2. Конкретные элементы:
- Book и Magazine — это классы, которые представляют конкретные элементы (книгу и журнал). Каждый из них реализует метод Accept, который вызывает соответствующий метод посетителя.
3. Интерфейс IVisitor:
- Этот интерфейс определяет методы VisitBook и VisitMagazine для выполнения операций с объектами Book и Magazine.
4. Конкретный посетитель PriceCalculator:
- Этот класс реализует интерфейс IVisitor и выполняет операцию подсчета стоимости элементов. Он также хранит общую сумму (_totalPrice) и предоставляет метод GetTotalPrice, чтобы получить итоговую сумму.
5. Абстрактная фабрика ItemFactory:
- Этот класс определяет метод CreateItem, который должен быть реализован в конкретных фабриках для создания различных типов элементов (книг, журналов и т. д.).
6. Конкретные фабрики:
- BookFactory и MagazineFactory — фабрики, которые создают конкретные элементы (книги и журналы).
7. В методе Main:
- Создаются фабрики для создания книги и журнала.
- Создаются элементы через соответствующие фабрики.
- Применяется посетитель PriceCalculator, чтобы подсчитать стоимость элементов.
- Выводится общая стоимость всех элементов.

## 13. Фабричный метод + Цепочка обязанностей.

## Сценарий:

Предположим, у нас есть система, в которой обрабатываются различные типы заявок. Мы будем использовать Цепочку обязанностей для последовательной обработки запросов, а Фабричный метод для создания различных обработчиков заявок.

```csharp
using System;

// Абстрактный класс обработчика в цепочке обязанностей
public abstract class RequestHandler
{
    protected RequestHandler _nextHandler;

    // Устанавливаем следующий обработчик в цепочке
    public void SetNextHandler(RequestHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    // Обрабатываем запрос
    public abstract void HandleRequest(string request);
}

// Конкретный обработчик для заявок на уровень 1
public class Level1RequestHandler : RequestHandler
{
    public override void HandleRequest(string request)
    {
        if (request.Contains("level 1"))
        {
            Console.WriteLine("Обработка запроса на уровне 1.");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

// Конкретный обработчик для заявок на уровень 2
public class Level2RequestHandler : RequestHandler
{
    public override void HandleRequest(string request)
    {
        if (request.Contains("level 2"))
        {
            Console.WriteLine("Обработка запроса на уровне 2.");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

// Конкретный обработчик для заявок на уровень 3
public class Level3RequestHandler : RequestHandler
{
    public override void HandleRequest(string request)
    {
        if (request.Contains("level 3"))
        {
            Console.WriteLine("Обработка запроса на уровне 3.");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

// Абстрактная фабрика для создания обработчиков
public abstract class RequestHandlerFactory
{
    public abstract RequestHandler CreateHandler();
}

// Фабрика для создания обработчика уровня 1
public class Level1RequestHandlerFactory : RequestHandlerFactory
{
    public override RequestHandler CreateHandler()
    {
        return new Level1RequestHandler();
    }
}

// Фабрика для создания обработчика уровня 2
public class Level2RequestHandlerFactory : RequestHandlerFactory
{
    public override RequestHandler CreateHandler()
    {
        return new Level2RequestHandler();
    }
}

// Фабрика для создания обработчика уровня 3
public class Level3RequestHandlerFactory : RequestHandlerFactory
{
    public override RequestHandler CreateHandler()
    {
        return new Level3RequestHandler();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики для обработчиков
        RequestHandlerFactory level1Factory = new Level1RequestHandlerFactory();
        RequestHandlerFactory level2Factory = new Level2RequestHandlerFactory();
        RequestHandlerFactory level3Factory = new Level3RequestHandlerFactory();

        // Создаем обработчиков
        RequestHandler level1Handler = level1Factory.CreateHandler();
        RequestHandler level2Handler = level2Factory.CreateHandler();
        RequestHandler level3Handler = level3Factory.CreateHandler();

        // Настроим цепочку обязанностей
        level1Handler.SetNextHandler(level2Handler);
        level2Handler.SetNextHandler(level3Handler);

        // Создаем запросы
        string request1 = "Request for level 1 processing";
        string request2 = "Request for level 2 processing";
        string request3 = "Request for level 3 processing";
        string request4 = "Request for unhandled level";

        // Обрабатываем запросы
        Console.WriteLine("Обработка запроса 1:");
        level1Handler.HandleRequest(request1);

        Console.WriteLine("\nОбработка запроса 2:");
        level1Handler.HandleRequest(request2);

        Console.WriteLine("\nОбработка запроса 3:");
        level1Handler.HandleRequest(request3);

        Console.WriteLine("\nОбработка запроса 4:");
        level1Handler.HandleRequest(request4);
    }
}
```

## Пояснение к коду

1. Абстрактный класс RequestHandler:
- Это абстрактный класс для всех обработчиков в цепочке обязанностей. Он содержит метод SetNextHandler, который связывает текущий обработчик с следующим, и абстрактный метод HandleRequest, который реализуют конкретные обработчики.
2. Конкретные обработчики:
- Level1RequestHandler, Level2RequestHandler, Level3RequestHandler — это конкретные обработчики, которые обрабатывают запросы разных уровней. Если запрос не подходит для текущего обработчика, он передает его следующему обработчику в цепочке.
3. Абстрактная фабрика RequestHandlerFactory:
- Это абстрактная фабрика, которая создает обработчиков. Каждый конкретный класс фабрики создаёт конкретный обработчик, например, для уровня 1, 2 или 3.
4. Конкретные фабрики:
- Level1RequestHandlerFactory, Level2RequestHandlerFactory, Level3RequestHandlerFactory — фабрики, которые создают обработчики для разных уровней.
5. В методе Main:
- Создаются фабрики для каждого уровня обработки.
- Создаются обработчики через соответствующие фабрики.
- Обработчики связываются в цепочку с помощью метода SetNextHandler.
Запросы обрабатываются через цепочку обязанностей.

## 14. Фабричный метод + Команда.

## Сценарий:

Предположим, у нас есть система управления устройствами. Мы будем использовать Команду для инкапсуляции действия (например, включение и выключение устройства), а Фабричный метод для создания этих команд.

```csharp
using System;

// Интерфейс команды, которая должна быть выполнена
public interface ICommand
{
    void Execute();
}

// Конкретная команда для включения устройства
public class TurnOnCommand : ICommand
{
    private Device _device;

    public TurnOnCommand(Device device)
    {
        _device = device;
    }

    public void Execute()
    {
        _device.TurnOn();
    }
}

// Конкретная команда для выключения устройства
public class TurnOffCommand : ICommand
{
    private Device _device;

    public TurnOffCommand(Device device)
    {
        _device = device;
    }

    public void Execute()
    {
        _device.TurnOff();
    }
}

// Класс устройства, которое может быть включено или выключено
public class Device
{
    public void TurnOn()
    {
        Console.WriteLine("Устройство включено.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Устройство выключено.");
    }
}

// Абстрактная фабрика для создания команд
public abstract class CommandFactory
{
    public abstract ICommand CreateCommand(Device device);
}

// Фабрика для создания команды включения устройства
public class TurnOnCommandFactory : CommandFactory
{
    public override ICommand CreateCommand(Device device)
    {
        return new TurnOnCommand(device);
    }
}

// Фабрика для создания команды выключения устройства
public class TurnOffCommandFactory : CommandFactory
{
    public override ICommand CreateCommand(Device device)
    {
        return new TurnOffCommand(device);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем устройство
        Device device = new Device();

        // Создаем фабрики для команд
        CommandFactory turnOnFactory = new TurnOnCommandFactory();
        CommandFactory turnOffFactory = new TurnOffCommandFactory();

        // Создаем команды через фабрики
        ICommand turnOnCommand = turnOnFactory.CreateCommand(device);
        ICommand turnOffCommand = turnOffFactory.CreateCommand(device);

        // Выполняем команды
        Console.WriteLine("Выполнение команды включения устройства:");
        turnOnCommand.Execute();

        Console.WriteLine("\nВыполнение команды выключения устройства:");
        turnOffCommand.Execute();
    }
}
```

## Пояснение к коду

1. Интерфейс ICommand:
- Этот интерфейс определяет метод Execute, который должен быть реализован всеми конкретными командами. Он представляет собой абстракцию действия, которое будет выполняться.
2. Конкретные команды:
- TurnOnCommand — команда, которая включает устройство.
- TurnOffCommand — команда, которая выключает устройство.
- Каждая команда хранит ссылку на объект Device и выполняет соответствующее действие при вызове метода Execute.
3. Класс Device:
- Этот класс представляет устройство, которое может быть включено или выключено. Он предоставляет методы TurnOn и TurnOff, которые выполняют соответствующие действия.
4. Абстрактная фабрика CommandFactory:
- Этот класс определяет метод CreateCommand, который должен быть реализован в конкретных фабриках для создания команд.
5. Конкретные фабрики:
- TurnOnCommandFactory — фабрика, которая создаёт команду для включения устройства.
- TurnOffCommandFactory — фабрика, которая создаёт команду для выключения устройства.
6. В методе Main:
- Создается устройство.
- Создаются фабрики для команд включения и выключения.
- Через фабрики создаются команды.
- Выполняются команды с помощью метода Execute.

## 15. Фабричный метод + Посредник.

## Сценарий:

Предположим, у нас есть система, в которой несколько компонентов (например, устройства) должны взаимодействовать друг с другом. Чтобы уменьшить их взаимные зависимости, мы используем Посредника, который будет управлять их взаимодействием. Для создания объектов компонентов используется Фабричный метод.

```csharp
using System;

// Абстрактный класс Компонента
public abstract class Component
{
    protected Mediator _mediator;

    public void SetMediator(Mediator mediator)
    {
        _mediator = mediator;
    }

    public abstract void Send(string message);
    public abstract void Receive(string message);
}

// Конкретный компонент 1
public class Component1 : Component
{
    public override void Send(string message)
    {
        Console.WriteLine("Компонент 1 отправляет сообщение: " + message);
        _mediator.Notify(this, message);
    }

    public override void Receive(string message)
    {
        Console.WriteLine("Компонент 1 получил сообщение: " + message);
    }
}

// Конкретный компонент 2
public class Component2 : Component
{
    public override void Send(string message)
    {
        Console.WriteLine("Компонент 2 отправляет сообщение: " + message);
        _mediator.Notify(this, message);
    }

    public override void Receive(string message)
    {
        Console.WriteLine("Компонент 2 получил сообщение: " + message);
    }
}

// Абстрактный класс Посредника
public abstract class Mediator
{
    public abstract void Notify(Component sender, string message);
}

// Конкретный посредник
public class ConcreteMediator : Mediator
{
    private Component1 _component1;
    private Component2 _component2;

    public void SetComponent1(Component1 component1)
    {
        _component1 = component1;
    }

    public void SetComponent2(Component2 component2)
    {
        _component2 = component2;
    }

    public override void Notify(Component sender, string message)
    {
        if (sender == _component1)
        {
            _component2.Receive(message);
        }
        else if (sender == _component2)
        {
            _component1.Receive(message);
        }
    }
}

// Абстрактная фабрика для создания компонентов
public abstract class ComponentFactory
{
    public abstract Component CreateComponent();
}

// Фабрика для создания компонента 1
public class Component1Factory : ComponentFactory
{
    public override Component CreateComponent()
    {
        return new Component1();
    }
}

// Фабрика для создания компонента 2
public class Component2Factory : ComponentFactory
{
    public override Component CreateComponent()
    {
        return new Component2();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики для компонентов
        ComponentFactory component1Factory = new Component1Factory();
        ComponentFactory component2Factory = new Component2Factory();

        // Создаем компоненты через фабрики
        Component component1 = component1Factory.CreateComponent();
        Component component2 = component2Factory.CreateComponent();

        // Создаем посредника
        ConcreteMediator mediator = new ConcreteMediator();

        // Устанавливаем посредника для компонентов
        component1.SetMediator(mediator);
        component2.SetMediator(mediator);

        // Устанавливаем компоненты в посредник
        mediator.SetComponent1((Component1)component1);
        mediator.SetComponent2((Component2)component2);

        // Компоненты начинают обмениваться сообщениями через посредника
        component1.Send("Привет, компонент 2!");
        component2.Send("Привет, компонент 1!");
    }
}
```

## Пояснение к коду

1. Абстрактный класс Component:
- Это базовый класс для всех компонентов, которые могут отправлять и получать сообщения через посредника. Методы Send и Receive реализуются в конкретных классах компонентов.
2. Конкретные компоненты Component1 и Component2:
- Эти классы реализуют методы Send и Receive, которые используются для отправки и получения сообщений через посредника. При отправке сообщения компонент уведомляет посредника, чтобы тот передал сообщение другому компоненту.
3. Абстрактный класс Mediator:
- Этот класс описывает посредника, который управляет взаимодействием между компонентами. Метод Notify используется для уведомления другого компонента о сообщении.
4. Конкретный посредник ConcreteMediator:
- Это конкретная реализация посредника. Он управляет взаимодействием между компонентами, передавая сообщения от одного компонента к другому.
5. Абстрактная фабрика ComponentFactory:
- Это абстрактная фабрика, которая создает компоненты. Метод CreateComponent должен быть реализован в конкретных фабриках для создания различных компонентов.
6. Конкретные фабрики Component1Factory и Component2Factory:
- Эти фабрики создают конкретные компоненты.
7. В методе Main:
- Создаются фабрики для компонентов.
- Создаются компоненты через соответствующие фабрики.
- Создается посредник, который управляет взаимодействием между компонентами.
- Компоненты начинают отправлять сообщения через посредника.

## 16. Фабричный метод + Снимок.

## Сценарий:

Предположим, у нас есть текстовый редактор, который позволяет отменять изменения текста. Мы будем использовать Снимок для сохранения состояния текста, чтобы потом можно было вернуться к сохранённому состоянию, и Фабричный метод для создания снимков.

```csharp
using System;
using System.Collections.Generic;

// Класс, представляющий текстовый редактор
public class TextEditor
{
    private string _text;

    public void SetText(string text)
    {
        _text = text;
        Console.WriteLine("Текущий текст: " + _text);
    }

    public string GetText()
    {
        return _text;
    }

    // Создаём снимок текущего состояния
    public TextEditorMemento CreateMemento()
    {
        return new TextEditorMemento(_text);
    }

    // Восстанавливаем состояние из снимка
    public void RestoreMemento(TextEditorMemento memento)
    {
        _text = memento.GetState();
        Console.WriteLine("Восстановлено состояние: " + _text);
    }
}

// Класс снимка, хранящий состояние текстового редактора
public class TextEditorMemento
{
    private readonly string _state;

    public TextEditorMemento(string state)
    {
        _state = state;
    }

    public string GetState()
    {
        return _state;
    }
}

// Абстрактная фабрика для создания снимков
public abstract class MementoFactory
{
    public abstract TextEditorMemento CreateMemento(string state);
}

// Конкретная фабрика для создания снимков текстового редактора
public class TextEditorMementoFactory : MementoFactory
{
    public override TextEditorMemento CreateMemento(string state)
    {
        return new TextEditorMemento(state);
    }
}

// Класс, управляющий историей снимков (Caretaker)
public class History
{
    private Stack<TextEditorMemento> _history = new Stack<TextEditorMemento>();

    public void SaveMemento(TextEditorMemento memento)
    {
        _history.Push(memento);
    }

    public TextEditorMemento Undo()
    {
        return _history.Count > 0 ? _history.Pop() : null;
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаём текстовый редактор и фабрику снимков
        TextEditor editor = new TextEditor();
        MementoFactory mementoFactory = new TextEditorMementoFactory();
        History history = new History();

        // Изменяем текст и сохраняем состояние
        editor.SetText("Первый текст.");
        history.SaveMemento(mementoFactory.CreateMemento(editor.GetText()));

        editor.SetText("Второй текст.");
        history.SaveMemento(mementoFactory.CreateMemento(editor.GetText()));

        editor.SetText("Третий текст.");

        // Откат к предыдущему состоянию
        Console.WriteLine("\nОтмена действия:");
        editor.RestoreMemento(history.Undo());

        Console.WriteLine("\nОтмена еще одного действия:");
        editor.RestoreMemento(history.Undo());
    }
}
```

## Пояснение к коду

1. Класс TextEditor:
- Этот класс представляет текстовый редактор с полем _text, которое хранит текущий текст. В нём есть методы для установки текста (SetText), создания снимка состояния (CreateMemento), и восстановления состояния из снимка (RestoreMemento).
2. Класс TextEditorMemento:
- Этот класс представляет снимок, хранящий состояние текста в момент его создания. Он хранит состояние редактора, которое затем может быть восстановлено.
3. Абстрактная фабрика MementoFactory:
- Определяет фабрику для создания снимков, предоставляя метод CreateMemento, который возвращает объект типа TextEditorMemento.
4. Конкретная фабрика TextEditorMementoFactory:
- Реализует метод CreateMemento, который создаёт снимок текущего состояния текста.
5. Класс History (хранитель):
- Этот класс управляет сохранением и откатом состояний. Он содержит стек снимков, чтобы сохранять историю состояний и возвращать к предыдущему состоянию при откате.
6. В методе Main:
- Создается текстовый редактор и фабрика для создания снимков.
- Текст изменяется несколько раз, и для каждого нового состояния создается снимок, который сохраняется в History.
- Затем выполняется откат до предыдущих состояний с помощью метода Undo.

## Абстрактная фабрика

## 1. Абстрактная фабрика + Строитель.

## Сценарий:

Предположим, мы строим разные виды компьютеров (например, игровую и офисную сборки) с использованием Абстрактной фабрики для создания отдельных компонентов (процессор, память и т.д.), а Строитель собирает их в окончательную конфигурацию.

```csharp
using System;

// Абстрактные компоненты компьютера
public interface IProcessor
{
    string GetProcessor();
}

public interface IRAM
{
    string GetRAM();
}

// Конкретные компоненты для игрового компьютера
public class GamingProcessor : IProcessor
{
    public string GetProcessor() => "Процессор для игр";
}

public class GamingRAM : IRAM
{
    public string GetRAM() => "16GB RAM для игр";
}

// Конкретные компоненты для офисного компьютера
public class OfficeProcessor : IProcessor
{
    public string GetProcessor() => "Процессор для офиса";
}

public class OfficeRAM : IRAM
{
    public string GetRAM() => "8GB RAM для офиса";
}

// Абстрактная фабрика для создания компонентов компьютера
public interface IComputerFactory
{
    IProcessor CreateProcessor();
    IRAM CreateRAM();
}

// Фабрика для создания игровых компонентов
public class GamingComputerFactory : IComputerFactory
{
    public IProcessor CreateProcessor() => new GamingProcessor();
    public IRAM CreateRAM() => new GamingRAM();
}

// Фабрика для создания офисных компонентов
public class OfficeComputerFactory : IComputerFactory
{
    public IProcessor CreateProcessor() => new OfficeProcessor();
    public IRAM CreateRAM() => new OfficeRAM();
}

// Класс Строителя для сборки компьютера
public class ComputerBuilder
{
    private IProcessor _processor;
    private IRAM _ram;

    public ComputerBuilder SetProcessor(IProcessor processor)
    {
        _processor = processor;
        return this;
    }

    public ComputerBuilder SetRAM(IRAM ram)
    {
        _ram = ram;
        return this;
    }

    public Computer Build()
    {
        return new Computer(_processor, _ram);
    }
}

// Продукт — собранный компьютер
public class Computer
{
    private readonly IProcessor _processor;
    private readonly IRAM _ram;

    public Computer(IProcessor processor, IRAM ram)
    {
        _processor = processor;
        _ram = ram;
    }

    public void ShowSpecifications()
    {
        Console.WriteLine("Характеристики компьютера:");
        Console.WriteLine("Процессор: " + _processor.GetProcessor());
        Console.WriteLine("Память: " + _ram.GetRAM());
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики для игровых и офисных компонентов
        IComputerFactory gamingFactory = new GamingComputerFactory();
        IComputerFactory officeFactory = new OfficeComputerFactory();

        // Строим игровой компьютер
        ComputerBuilder builder = new ComputerBuilder();
        Computer gamingComputer = builder
            .SetProcessor(gamingFactory.CreateProcessor())
            .SetRAM(gamingFactory.CreateRAM())
            .Build();

        // Строим офисный компьютер
        Computer officeComputer = builder
            .SetProcessor(officeFactory.CreateProcessor())
            .SetRAM(officeFactory.CreateRAM())
            .Build();

        // Показ характеристик
        Console.WriteLine("Игровой компьютер:");
        gamingComputer.ShowSpecifications();

        Console.WriteLine("\nОфисный компьютер:");
        officeComputer.ShowSpecifications();
    }
}
```

## Пояснение к коду

1. Абстрактные интерфейсы компонентов (IProcessor и IRAM): определяют общие методы для процессоров и оперативной памяти.
2. Конкретные компоненты:
- GamingProcessor и GamingRAM представляют игровые компоненты.
- OfficeProcessor и OfficeRAM представляют офисные компоненты.
3. Абстрактная фабрика IComputerFactory: интерфейс для создания компонентов, в котором есть методы для создания процессора и памяти.
4. Конкретные фабрики:
- GamingComputerFactory создает игровые компоненты.
- OfficeComputerFactory создает офисные компоненты.
5. Класс ComputerBuilder: строитель, который поэтапно собирает объект Computer.
6. Класс Computer: конечный продукт, который агрегирует компоненты и выводит их характеристики.

## 2. Абстрактная фабрика + Прототип.

## Сценарий:

Предположим, мы создаём фигуры для графического редактора. У нас есть Абстрактная фабрика, которая создаёт разные виды фигур (например, круги и прямоугольники), используя Прототипы для клонирования базовых экземпляров фигур.

```csharp
using System;

// Интерфейс фигуры с методом клонирования
public interface IShape
{
    IShape Clone();
    void Draw();
}

// Класс Круг, реализующий интерфейс IShape
public class Circle : IShape
{
    public int Radius { get; set; }

    public Circle(int radius)
    {
        Radius = radius;
    }

    public IShape Clone()
    {
        return new Circle(Radius); // Клонирование круга
    }

    public void Draw()
    {
        Console.WriteLine($"Рисуем круг с радиусом {Radius}");
    }
}

// Класс Прямоугольник, реализующий интерфейс IShape
public class Rectangle : IShape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public IShape Clone()
    {
        return new Rectangle(Width, Height); // Клонирование прямоугольника
    }

    public void Draw()
    {
        Console.WriteLine($"Рисуем прямоугольник с шириной {Width} и высотой {Height}");
    }
}

// Абстрактная фабрика для создания фигур
public interface IShapeFactory
{
    IShape CreateShape();
}

// Фабрика для создания кругов
public class CircleFactory : IShapeFactory
{
    private readonly Circle _prototype;

    public CircleFactory(Circle prototype)
    {
        _prototype = prototype;
    }

    public IShape CreateShape()
    {
        return _prototype.Clone(); // Используем клонирование
    }
}

// Фабрика для создания прямоугольников
public class RectangleFactory : IShapeFactory
{
    private readonly Rectangle _prototype;

    public RectangleFactory(Rectangle prototype)
    {
        _prototype = prototype;
    }

    public IShape CreateShape()
    {
        return _prototype.Clone(); // Используем клонирование
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Прототипы для клонирования
        Circle circlePrototype = new Circle(5);
        Rectangle rectanglePrototype = new Rectangle(10, 20);

        // Фабрики для создания объектов на основе прототипов
        IShapeFactory circleFactory = new CircleFactory(circlePrototype);
        IShapeFactory rectangleFactory = new RectangleFactory(rectanglePrototype);

        // Создаём круги
        IShape circle1 = circleFactory.CreateShape();
        IShape circle2 = circleFactory.CreateShape();

        // Создаём прямоугольники
        IShape rectangle1 = rectangleFactory.CreateShape();
        IShape rectangle2 = rectangleFactory.CreateShape();

        // Отображение фигур
        circle1.Draw();
        circle2.Draw();
        rectangle1.Draw();
        rectangle2.Draw();
    }
}
```

## Пояснение к коду

1. Интерфейс IShape: определяет метод Clone() для клонирования и метод Draw() для отображения фигуры.
2. Конкретные классы фигур:
- Circle и Rectangle реализуют интерфейс IShape, предоставляя методы Clone() для создания копий и Draw() для отображения фигуры.
3. Абстрактная фабрика IShapeFactory: определяет интерфейс для фабрик, которые будут создавать экземпляры фигур.
4. Конкретные фабрики:
- CircleFactory и RectangleFactory реализуют интерфейс IShapeFactory и используют переданные прототипы для создания копий с помощью метода Clone().
5. В Main:
- Создаются прототипы circlePrototype и rectanglePrototype.
Фабрики создают новые экземпляры фигур на основе прототипов с помощью Clone().

## 3. Абстрактная фабрика + Одиночка.

## Сценарий:

Предположим, у нас есть система для создания различных видов баз данных (например, MySQL и PostgreSQL). В этом примере Абстрактная фабрика предоставляет интерфейс для создания соединений и команд базы данных, а Одиночка гарантирует, что только одна фабрика будет создана для каждого типа базы данных.

```csharp
using System;

// Интерфейсы для различных компонентов базы данных
public interface IDbConnection
{
    void Connect();
}

public interface IDbCommand
{
    void Execute();
}

// Реализации для MySQL базы данных
public class MySqlConnection : IDbConnection
{
    public void Connect()
    {
        Console.WriteLine("Подключение к MySQL базе данных...");
    }
}

public class MySqlCommand : IDbCommand
{
    public void Execute()
    {
        Console.WriteLine("Выполнение команды MySQL...");
    }
}

// Реализации для PostgreSQL базы данных
public class PostgreSqlConnection : IDbConnection
{
    public void Connect()
    {
        Console.WriteLine("Подключение к PostgreSQL базе данных...");
    }
}

public class PostgreSqlCommand : IDbCommand
{
    public void Execute()
    {
        Console.WriteLine("Выполнение команды PostgreSQL...");
    }
}

// Абстрактная фабрика для создания компонентов базы данных
public interface IDatabaseFactory
{
    IDbConnection CreateConnection();
    IDbCommand CreateCommand();
}

// Фабрика для MySQL базы данных
public class MySqlFactory : IDatabaseFactory
{
    private static MySqlFactory _instance;

    private MySqlFactory() { }

    public static MySqlFactory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new MySqlFactory();
        }
        return _instance;
    }

    public IDbConnection CreateConnection() => new MySqlConnection();
    public IDbCommand CreateCommand() => new MySqlCommand();
}

// Фабрика для PostgreSQL базы данных
public class PostgreSqlFactory : IDatabaseFactory
{
    private static PostgreSqlFactory _instance;

    private PostgreSqlFactory() { }

    public static PostgreSqlFactory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PostgreSqlFactory();
        }
        return _instance;
    }

    public IDbConnection CreateConnection() => new PostgreSqlConnection();
    public IDbCommand CreateCommand() => new PostgreSqlCommand();
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Получаем экземпляры фабрик
        IDatabaseFactory mysqlFactory = MySqlFactory.GetInstance();
        IDatabaseFactory postgresFactory = PostgreSqlFactory.GetInstance();

        // Создаём и используем компоненты MySQL
        IDbConnection mySqlConnection = mysqlFactory.CreateConnection();
        IDbCommand mySqlCommand = mysqlFactory.CreateCommand();
        mySqlConnection.Connect();
        mySqlCommand.Execute();

        // Создаём и используем компоненты PostgreSQL
        IDbConnection postgresConnection = postgresFactory.CreateConnection();
        IDbCommand postgresCommand = postgresFactory.CreateCommand();
        postgresConnection.Connect();
        postgresCommand.Execute();
    }
}
```

## Пояснение к коду

1. Интерфейсы IDbConnection и IDbCommand: определяют общие методы для подключения к базе данных и выполнения команд.
2. Реализации MySQL и PostgreSQL:
- MySqlConnection и MySqlCommand реализуют подключения и команды для MySQL.
- PostgreSqlConnection и PostgreSqlCommand реализуют подключения и команды для PostgreSQL.
3. Абстрактная фабрика IDatabaseFactory: предоставляет интерфейс для создания компонентов базы данных (подключение и команда).
4. Конкретные фабрики с шаблоном Одиночка:
- MySqlFactory и PostgreSqlFactory реализуют IDatabaseFactory, используя Одиночку для гарантии единственного экземпляра каждой фабрики.
- Статический метод GetInstance() контролирует создание единственного экземпляра.
5. Тестовая программа Main:
- Получает экземпляры фабрик и создаёт компоненты для каждой базы данных, затем подключается и выполняет команды.

## 4. Абстрактная фабрика + Декоратор.

## Сценарий:

Предположим, у нас есть система уведомлений. Мы хотим создать разные виды уведомлений (например, Email и SMS) и добавлять к ним дополнительные функции (например, логирование или отслеживание времени). Абстрактная фабрика создаёт базовые уведомления, а Декораторы добавляют дополнительные функции.

```csharp
using System;

// Интерфейс для уведомлений
public interface INotification
{
    void Send(string message);
}

// Реализация Email уведомления
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка Email уведомления: {message}");
    }
}

// Реализация SMS уведомления
public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка SMS уведомления: {message}");
    }
}

// Абстрактная фабрика для создания уведомлений
public interface INotificationFactory
{
    INotification CreateNotification();
}

// Фабрика для создания Email уведомлений
public class EmailNotificationFactory : INotificationFactory
{
    public INotification CreateNotification() => new EmailNotification();
}

// Фабрика для создания SMS уведомлений
public class SmsNotificationFactory : INotificationFactory
{
    public INotification CreateNotification() => new SmsNotification();
}

// Базовый класс Декоратора
public abstract class NotificationDecorator : INotification
{
    protected INotification _notification;

    public NotificationDecorator(INotification notification)
    {
        _notification = notification;
    }

    public virtual void Send(string message)
    {
        _notification.Send(message);
    }
}

// Декоратор для логирования
public class LoggingDecorator : NotificationDecorator
{
    public LoggingDecorator(INotification notification) : base(notification) { }

    public override void Send(string message)
    {
        Console.WriteLine("Логирование уведомления...");
        base.Send(message);
    }
}

// Декоратор для отслеживания времени
public class TimestampDecorator : NotificationDecorator
{
    public TimestampDecorator(INotification notification) : base(notification) { }

    public override void Send(string message)
    {
        Console.WriteLine($"[{DateTime.Now}] Отправка уведомления с отметкой времени...");
        base.Send(message);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики уведомлений
        INotificationFactory emailFactory = new EmailNotificationFactory();
        INotificationFactory smsFactory = new SmsNotificationFactory();

        // Создаем базовое Email уведомление
        INotification emailNotification = emailFactory.CreateNotification();

        // Добавляем декораторы: логирование и отметку времени
        INotification decoratedEmail = new LoggingDecorator(new TimestampDecorator(emailNotification));
        decoratedEmail.Send("Важное сообщение!");

        // Создаем базовое SMS уведомление
        INotification smsNotification = smsFactory.CreateNotification();

        // Добавляем декоратор только для отметки времени
        INotification decoratedSms = new TimestampDecorator(smsNotification);
        decoratedSms.Send("Срочное SMS!");
    }
}
```

## Пояснение к коду

1. Интерфейс INotification: определяет метод Send() для отправки уведомлений.

2. Конкретные классы уведомлений:

EmailNotification и SmsNotification реализуют интерфейс INotification.
3. Абстрактная фабрика INotificationFactory: предоставляет интерфейс для создания уведомлений.

4. Конкретные фабрики:

EmailNotificationFactory и SmsNotificationFactory создают уведомления EmailNotification и SmsNotification соответственно.
5. Базовый класс Декоратора NotificationDecorator:

Реализует INotification и содержит ссылку на базовое уведомление. Это позволяет добавлять поведение к Send().
6. Конкретные декораторы:

LoggingDecorator добавляет логирование.
TimestampDecorator добавляет отметку времени.
7. В Main:

Создаются уведомления с фабрик и оборачиваются в декораторы, чтобы добавить дополнительные функции.

## 5. Абстрактная фабрика + Компоновщик.

## Сценарий:

Предположим, у нас есть система для построения интерфейсов. Она позволяет создавать простые элементы интерфейса, такие как кнопки и текстовые поля, а также составные элементы, например, панели, содержащие несколько других элементов. Абстрактная фабрика создаёт интерфейсные элементы, а Компоновщик позволяет объединять их в сложные структуры.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс для компонентов интерфейса
public interface IUIComponent
{
    void Render();
}

// Реализация кнопки
public class Button : IUIComponent
{
    public void Render()
    {
        Console.WriteLine("Отображение кнопки");
    }
}

// Реализация текстового поля
public class TextField : IUIComponent
{
    public void Render()
    {
        Console.WriteLine("Отображение текстового поля");
    }
}

// Компоновщик для создания контейнеров, которые могут содержать другие компоненты
public class Panel : IUIComponent
{
    private readonly List<IUIComponent> _components = new List<IUIComponent>();

    public void AddComponent(IUIComponent component)
    {
        _components.Add(component);
    }

    public void Render()
    {
        Console.WriteLine("Отображение панели:");
        foreach (var component in _components)
        {
            component.Render();
        }
    }
}

// Абстрактная фабрика для создания компонентов интерфейса
public interface IUIFactory
{
    IUIComponent CreateButton();
    IUIComponent CreateTextField();
    IUIComponent CreatePanel();
}

// Фабрика для создания базовых компонентов интерфейса
public class BasicUIFactory : IUIFactory
{
    public IUIComponent CreateButton() => new Button();
    public IUIComponent CreateTextField() => new TextField();
    public IUIComponent CreatePanel() => new Panel();
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрику интерфейсов
        IUIFactory uiFactory = new BasicUIFactory();

        // Создаем компоненты
        IUIComponent button1 = uiFactory.CreateButton();
        IUIComponent textField1 = uiFactory.CreateTextField();
        IUIComponent button2 = uiFactory.CreateButton();

        // Создаем панель и добавляем в нее компоненты
        Panel mainPanel = (Panel)uiFactory.CreatePanel();
        mainPanel.AddComponent(button1);
        mainPanel.AddComponent(textField1);

        // Создаем вложенную панель и добавляем ее в главную панель
        Panel nestedPanel = (Panel)uiFactory.CreatePanel();
        nestedPanel.AddComponent(button2);

        mainPanel.AddComponent(nestedPanel);

        // Отображаем всю структуру интерфейса
        mainPanel.Render();
    }
}

```

## Пояснение к коду

1. Интерфейс IUIComponent: определяет метод Render(), который будет реализован в каждом компоненте интерфейса.

2. Конкретные классы компонентов интерфейса:

Button и TextField реализуют интерфейс IUIComponent, предоставляя метод Render() для отображения соответствующего компонента.
3. Компоновщик Panel:

Panel реализует IUIComponent и может содержать другие компоненты, включая другие панели, позволяя создавать вложенные структуры.
4. Абстрактная фабрика IUIFactory: предоставляет интерфейс для создания компонентов интерфейса.

5. Конкретная фабрика BasicUIFactory:

Создаёт экземпляры Button, TextField, и Panel.
6. В Main:

Создаётся панель, в которую добавляются кнопки и текстовое поле, а также вложенная панель для создания более сложной структуры интерфейса.

## 6. Абстрактная фабрика + Стратегия.

## Сценарий:

Предположим, у нас есть система для обработки платежей. Платёж может быть проведён разными способами (например, через PayPal или кредитную карту), и для каждого метода оплаты мы можем использовать разные алгоритмы валидации. Абстрактная фабрика создаёт конкретные объекты для каждого типа платежа, а Стратегия позволяет менять алгоритмы валидации для этих объектов.

```csharp
using System;

// Интерфейс для стратегий валидации
public interface IValidationStrategy
{
    bool Validate(string accountInfo);
}

// Конкретная стратегия валидации для PayPal
public class PayPalValidationStrategy : IValidationStrategy
{
    public bool Validate(string accountInfo)
    {
        Console.WriteLine("Валидация PayPal аккаунта...");
        // Логика валидации PayPal
        return accountInfo.StartsWith("paypal_");
    }
}

// Конкретная стратегия валидации для кредитной карты
public class CreditCardValidationStrategy : IValidationStrategy
{
    public bool Validate(string accountInfo)
    {
        Console.WriteLine("Валидация кредитной карты...");
        // Логика валидации кредитной карты
        return accountInfo.StartsWith("cc_");
    }
}

// Интерфейс платежа
public interface IPayment
{
    bool ProcessPayment(string accountInfo);
}

// Реализация платежа через PayPal
public class PayPalPayment : IPayment
{
    private IValidationStrategy _validationStrategy;

    public PayPalPayment(IValidationStrategy validationStrategy)
    {
        _validationStrategy = validationStrategy;
    }

    public bool ProcessPayment(string accountInfo)
    {
        Console.WriteLine("Обработка платежа через PayPal...");
        return _validationStrategy.Validate(accountInfo);
    }
}

// Реализация платежа через кредитную карту
public class CreditCardPayment : IPayment
{
    private IValidationStrategy _validationStrategy;

    public CreditCardPayment(IValidationStrategy validationStrategy)
    {
        _validationStrategy = validationStrategy;
    }

    public bool ProcessPayment(string accountInfo)
    {
        Console.WriteLine("Обработка платежа через кредитную карту...");
        return _validationStrategy.Validate(accountInfo);
    }
}

// Абстрактная фабрика для создания платежей
public interface IPaymentFactory
{
    IPayment CreatePayment(IValidationStrategy validationStrategy);
}

// Фабрика для создания PayPal платежей
public class PayPalPaymentFactory : IPaymentFactory
{
    public IPayment CreatePayment(IValidationStrategy validationStrategy) => new PayPalPayment(validationStrategy);
}

// Фабрика для создания кредитных карт
public class CreditCardPaymentFactory : IPaymentFactory
{
    public IPayment CreatePayment(IValidationStrategy validationStrategy) => new CreditCardPayment(validationStrategy);
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики платежей
        IPaymentFactory paypalFactory = new PayPalPaymentFactory();
        IPaymentFactory creditCardFactory = new CreditCardPaymentFactory();

        // Создаем стратегии валидации
        IValidationStrategy paypalValidation = new PayPalValidationStrategy();
        IValidationStrategy creditCardValidation = new CreditCardValidationStrategy();

        // Создаем платеж PayPal с соответствующей стратегией валидации
        IPayment paypalPayment = paypalFactory.CreatePayment(paypalValidation);
        bool isPayPalPaymentSuccessful = paypalPayment.ProcessPayment("paypal_user123");
        Console.WriteLine($"PayPal платёж успешен: {isPayPalPaymentSuccessful}\n");

        // Создаем платеж кредитной картой с соответствующей стратегией валидации
        IPayment creditCardPayment = creditCardFactory.CreatePayment(creditCardValidation);
        bool isCreditCardPaymentSuccessful = creditCardPayment.ProcessPayment("cc_user456");
        Console.WriteLine($"Платёж кредитной картой успешен: {isCreditCardPaymentSuccessful}");
    }
}
```

## Пояснение к коду

1. Интерфейс IValidationStrategy: определяет метод Validate(), который будет реализован в каждой стратегии валидации.

2. Конкретные стратегии валидации:

PayPalValidationStrategy проверяет учетные данные для PayPal.
CreditCardValidationStrategy проверяет учетные данные для кредитной карты.
3. Интерфейс IPayment: определяет метод ProcessPayment() для обработки платежей.

4. Реализации PayPalPayment и CreditCardPayment:

Используют стратегии валидации, чтобы определить, успешен ли платёж.
5. Абстрактная фабрика IPaymentFactory: определяет метод CreatePayment(), который создаёт объект платежа с определённой стратегией.

6. Конкретные фабрики PayPalPaymentFactory и CreditCardPaymentFactory:

Создают экземпляры PayPalPayment и CreditCardPayment, принимая стратегию валидации.
7. В Main:

Создаются фабрики, стратегии, и платежные объекты, которые используют различные стратегии для валидации перед оплатой.

## 7. Абстрактная фабрика + Посетитель.

## Сценарий:

Предположим, у нас есть система для обработки различных типов документов. Мы хотим выполнять различные операции (например, подсчёт слов или символов) на этих документах. Абстрактная фабрика создаёт объекты документов, а Посетитель реализует различные операции над этими документами.

```csharp
using System;

// Интерфейс для документов
public interface IDocument
{
    void Accept(IDocumentVisitor visitor);
}

// Конкретный класс документа - текстовый документ
public class TextDocument : IDocument
{
    public string Text { get; set; }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Конкретный класс документа - PDF документ
public class PdfDocument : IDocument
{
    public byte[] Content { get; set; }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Интерфейс посетителя
public interface IDocumentVisitor
{
    void Visit(TextDocument textDocument);
    void Visit(PdfDocument pdfDocument);
}

// Реализация посетителя для подсчёта слов
public class WordCountVisitor : IDocumentVisitor
{
    public void Visit(TextDocument textDocument)
    {
        var wordCount = textDocument.Text.Split(' ').Length;
        Console.WriteLine($"Word count in TextDocument: {wordCount}");
    }

    public void Visit(PdfDocument pdfDocument)
    {
        // Для простоты, будем считать количество байтов в PDF как "символы"
        var byteCount = pdfDocument.Content.Length;
        Console.WriteLine($"Byte count in PdfDocument: {byteCount}");
    }
}

// Реализация посетителя для подсчёта символов
public class CharacterCountVisitor : IDocumentVisitor
{
    public void Visit(TextDocument textDocument)
    {
        var characterCount = textDocument.Text.Length;
        Console.WriteLine($"Character count in TextDocument: {characterCount}");
    }

    public void Visit(PdfDocument pdfDocument)
    {
        // Также считаем количество байтов как "символы"
        var byteCount = pdfDocument.Content.Length;
        Console.WriteLine($"Byte count in PdfDocument: {byteCount}");
    }
}

// Абстрактная фабрика для создания документов
public interface IDocumentFactory
{
    IDocument CreateDocument();
}

// Фабрика для создания текстовых документов
public class TextDocumentFactory : IDocumentFactory
{
    public IDocument CreateDocument() => new TextDocument { Text = "This is a simple text document." };
}

// Фабрика для создания PDF документов
public class PdfDocumentFactory : IDocumentFactory
{
    public IDocument CreateDocument() => new PdfDocument { Content = new byte[] { 1, 2, 3, 4, 5 } };
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики документов
        IDocumentFactory textDocumentFactory = new TextDocumentFactory();
        IDocumentFactory pdfDocumentFactory = new PdfDocumentFactory();

        // Создаем документы
        IDocument textDocument = textDocumentFactory.CreateDocument();
        IDocument pdfDocument = pdfDocumentFactory.CreateDocument();

        // Создаем посетителей для подсчёта слов и символов
        IDocumentVisitor wordCountVisitor = new WordCountVisitor();
        IDocumentVisitor characterCountVisitor = new CharacterCountVisitor();

        // Применяем посетителей к документам
        Console.WriteLine("Word Count Visitor:");
        textDocument.Accept(wordCountVisitor);
        pdfDocument.Accept(wordCountVisitor);

        Console.WriteLine("\nCharacter Count Visitor:");
        textDocument.Accept(characterCountVisitor);
        pdfDocument.Accept(characterCountVisitor);
    }
}
```

## Пояснение к коду

1. Интерфейс IDocument: определяет метод Accept(), который принимает посетителя. Каждый конкретный документ будет реализовывать этот метод, позволяя посетителям взаимодействовать с ним.

2. Конкретные классы документов:

TextDocument представляет текстовый документ и реализует метод Accept().
PdfDocument представляет PDF документ и также реализует метод Accept().
3. Интерфейс IDocumentVisitor: определяет методы для посещения каждого типа документа. В этом примере есть два метода Visit(): один для текстового документа и один для PDF документа.

4. Конкретные посетители:

WordCountVisitor подсчитывает количество слов в текстовом документе или количество байтов в PDF.
CharacterCountVisitor подсчитывает количество символов в текстовом документе или количество байтов в PDF.
5. Абстрактная фабрика IDocumentFactory: определяет метод CreateDocument(), который создаёт экземпляры документов.

6. Конкретные фабрики:

TextDocumentFactory создаёт экземпляры текстовых документов.
PdfDocumentFactory создаёт экземпляры PDF документов.
7. В Main:

Создаём объекты документов с помощью фабрик.
Создаём посетителей и применяем их к документам для выполнения различных операций (например, подсчёта слов и символов).

## 8. Абстрактная фабрика + Легковес.

## Сценарий:

Предположим, у нас есть система для отображения различных типов графических объектов, таких как прямоугольники и круги. Для каждого типа объекта создаются только те данные, которые изменяются (например, цвет), а общие данные (например, форма) разделяются между объектами с помощью паттерна Легковес.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс для графических объектов
public interface IGraphic
{
    void Draw();
}

// Конкретный класс прямоугольника
public class Rectangle : IGraphic
{
    private string _color;
    private readonly string _shape; // Это часть, которая будет разделяться между всеми объектами

    public Rectangle(string color, string shape)
    {
        _color = color;
        _shape = shape; // В данном случае, форма прямоугольника будет одинаковой для всех
    }

    public void Draw()
    {
        Console.WriteLine($"Рисуем {_color} прямоугольник.");
    }
}

// Конкретный класс круга
public class Circle : IGraphic
{
    private string _color;
    private readonly string _shape; // Эта часть будет общая для всех объектов

    public Circle(string color, string shape)
    {
        _color = color;
        _shape = shape; // Форма круга одинакова для всех
    }

    public void Draw()
    {
        Console.WriteLine($"Рисуем {_color} круг.");
    }
}

// Легковесный объект, который управляет общими данными (формами)
public class ShapeFactory
{
    private readonly Dictionary<string, IGraphic> _shapes = new();

    public IGraphic GetShape(string shapeType)
    {
        if (!_shapes.ContainsKey(shapeType))
        {
            if (shapeType == "Rectangle")
            {
                _shapes[shapeType] = new Rectangle("default", "Rectangle");
            }
            else if (shapeType == "Circle")
            {
                _shapes[shapeType] = new Circle("default", "Circle");
            }
        }

        return _shapes[shapeType];
    }
}

// Абстрактная фабрика для создания объектов
public interface IGraphicFactory
{
    IGraphic CreateGraphic(string color);
}

// Фабрика для создания прямоугольников
public class RectangleFactory : IGraphicFactory
{
    private readonly ShapeFactory _shapeFactory = new ShapeFactory();

    public IGraphic CreateGraphic(string color)
    {
        var rectangle = (Rectangle)_shapeFactory.GetShape("Rectangle");
        // Возвращаем прямоугольник с нужным цветом
        return new Rectangle(color, "Rectangle");
    }
}

// Фабрика для создания кругов
public class CircleFactory : IGraphicFactory
{
    private readonly ShapeFactory _shapeFactory = new ShapeFactory();

    public IGraphic CreateGraphic(string color)
    {
        var circle = (Circle)_shapeFactory.GetShape("Circle");
        // Возвращаем круг с нужным цветом
        return new Circle(color, "Circle");
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрики для прямоугольников и кругов
        IGraphicFactory rectangleFactory = new RectangleFactory();
        IGraphicFactory circleFactory = new CircleFactory();

        // Создаем графические объекты с разными цветами
        IGraphic rectangle1 = rectangleFactory.CreateGraphic("Red");
        IGraphic rectangle2 = rectangleFactory.CreateGraphic("Blue");
        IGraphic circle1 = circleFactory.CreateGraphic("Green");

        // Рисуем объекты
        rectangle1.Draw();
        rectangle2.Draw();
        circle1.Draw();

        // Повторное создание объекта с тем же цветом
        IGraphic rectangle3 = rectangleFactory.CreateGraphic("Red");
        rectangle3.Draw();
    }
}
```

## Пояснение к коду

1. Интерфейс IGraphic: определяет метод Draw(), который будет реализован в конкретных классах графических объектов.

2. Конкретные классы Rectangle и Circle:

Эти классы представляют графические объекты, такие как прямоугольники и круги.
Каждому объекту передаётся цвет, но форма объекта является общей для всех объектов того же типа (например, форма прямоугольника или круга).
3. Класс ShapeFactory:

Это класс, реализующий паттерн Легковес. Он управляет созданием и хранением объектов, которые имеют общие данные (формы), и предоставляет эти объекты для использования.
Например, форма прямоугольника или круга создаётся только один раз и используется многократно.
4. Абстрактные фабрики IGraphicFactory:

Они определяют метод CreateGraphic(), который создаёт графические объекты с заданными параметрами, такими как цвет.
5. Конкретные фабрики:

RectangleFactory и CircleFactory создают объекты прямоугольников и кругов, соответственно, с помощью легковесных объектов.
6. В Main:

Создаются фабрики и графические объекты с разными цветами. Паттерн Легковес используется для того, чтобы общие данные (форма объектов) не создавались заново, а использовались повторно.

## 9. Абстрактная фабрика + Фасад.

## Сценарий:

Предположим, у нас есть система для управления различными типами окон (например, для операционной системы). Мы хотим создать абстракцию для создания окон и взаимодействия с ними, используя фасад, который будет скрывать сложную логику работы с окнами.

```csharp
using System;

// Интерфейсы для окон
public interface IWindow
{
    void Open();
    void Close();
}

// Конкретные классы окон
public class MacOSWindow : IWindow
{
    public void Open() => Console.WriteLine("Окно MacOS открыто.");
    public void Close() => Console.WriteLine("Окно MacOS закрыто.");
}

public class WindowsWindow : IWindow
{
    public void Open() => Console.WriteLine("Окно Windows открыто.");
    public void Close() => Console.WriteLine("Окно Windows закрыто.");
}

public class LinuxWindow : IWindow
{
    public void Open() => Console.WriteLine("Окно Linux открыто.");
    public void Close() => Console.WriteLine("Окно Linux закрыто.");
}

// Абстрактная фабрика для создания окон
public interface IWindowFactory
{
    IWindow CreateWindow();
}

// Конкретные фабрики для создания окон для разных операционных систем
public class MacOSWindowFactory : IWindowFactory
{
    public IWindow CreateWindow() => new MacOSWindow();
}

public class WindowsWindowFactory : IWindowFactory
{
    public IWindow CreateWindow() => new WindowsWindow();
}

public class LinuxWindowFactory : IWindowFactory
{
    public IWindow CreateWindow() => new LinuxWindow();
}

// Фасад, который упрощает работу с окнами
public class WindowFacade
{
    private IWindowFactory _windowFactory;

    public WindowFacade(string os)
    {
        // Выбираем соответствующую фабрику в зависимости от операционной системы
        switch (os)
        {
            case "MacOS":
                _windowFactory = new MacOSWindowFactory();
                break;
            case "Windows":
                _windowFactory = new WindowsWindowFactory();
                break;
            case "Linux":
                _windowFactory = new LinuxWindowFactory();
                break;
            default:
                throw new ArgumentException("Неизвестная операционная система");
        }
    }

    public void OpenWindow()
    {
        var window = _windowFactory.CreateWindow();
        window.Open();
    }

    public void CloseWindow()
    {
        var window = _windowFactory.CreateWindow();
        window.Close();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Используем фасад для создания и управления окнами
        Console.WriteLine("Используем фасад для MacOS:");
        var macOSFacade = new WindowFacade("MacOS");
        macOSFacade.OpenWindow();
        macOSFacade.CloseWindow();

        Console.WriteLine("\nИспользуем фасад для Windows:");
        var windowsFacade = new WindowFacade("Windows");
        windowsFacade.OpenWindow();
        windowsFacade.CloseWindow();

        Console.WriteLine("\nИспользуем фасад для Linux:");
        var linuxFacade = new WindowFacade("Linux");
        linuxFacade.OpenWindow();
        linuxFacade.CloseWindow();
    }
}
```

## Пояснение к коду

1. Интерфейс IWindow:

Определяет методы Open() и Close(), которые должны быть реализованы в конкретных классах окон.
2. Конкретные классы окон:

MacOSWindow, WindowsWindow, LinuxWindow реализуют интерфейс IWindow, предоставляя специфическую реализацию для разных операционных систем.
3. Абстрактная фабрика IWindowFactory:

Определяет метод CreateWindow(), который будет создавать объекты окон для разных операционных систем.
4. Конкретные фабрики:

MacOSWindowFactory, WindowsWindowFactory, LinuxWindowFactory создают объекты окон для соответствующих операционных систем.
5. Фасад WindowFacade:

Скрывает детали работы с фабриками и окнами. В зависимости от операционной системы, фасад создаёт соответствующие окна с помощью фабрики, предоставляя удобный интерфейс для открытия и закрытия окна.
6. В Main:

Создаются фасады для MacOS, Windows и Linux. Каждый фасад упрощает работу с окнами, позволяя пользователю взаимодействовать с системой через один простой интерфейс.

## 10. Абстрактная фабрика + Заместитель.

## Сценарий:

Предположим, у нас есть система для работы с банковскими счетами. Каждый счет имеет свою реализацию, но для контроля доступа и безопасности мы используем Заместителя. Заместитель будет проверять права пользователя перед выполнением операций.

```csharp
using System;

// Интерфейсы для банковского счета
public interface IBankAccount
{
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    decimal GetBalance();
}

// Конкретный класс банковского счета
public class RealBankAccount : IBankAccount
{
    private decimal _balance;

    public RealBankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        _balance += amount;
        Console.WriteLine($"Внесено {amount}. Баланс: {_balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;
            Console.WriteLine($"Снято {amount}. Баланс: {_balance}");
        }
        else
        {
            Console.WriteLine("Недостаточно средств для снятия.");
        }
    }

    public decimal GetBalance()
    {
        return _balance;
    }
}

// Заместитель, который контролирует доступ
public class BankAccountProxy : IBankAccount
{
    private readonly RealBankAccount _realBankAccount;
    private readonly string _userRole;

    public BankAccountProxy(RealBankAccount realBankAccount, string userRole)
    {
        _realBankAccount = realBankAccount;
        _userRole = userRole;
    }

    public void Deposit(decimal amount)
    {
        if (_userRole == "Admin" || _userRole == "User")
        {
            _realBankAccount.Deposit(amount);
        }
        else
        {
            Console.WriteLine("Нет прав для внесения средств.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (_userRole == "Admin" || _userRole == "User")
        {
            _realBankAccount.Withdraw(amount);
        }
        else
        {
            Console.WriteLine("Нет прав для снятия средств.");
        }
    }

    public decimal GetBalance()
    {
        if (_userRole == "Admin" || _userRole == "User")
        {
            return _realBankAccount.GetBalance();
        }
        else
        {
            Console.WriteLine("Нет прав для просмотра баланса.");
            return 0;
        }
    }
}

// Абстрактная фабрика для создания счетов
public interface IBankAccountFactory
{
    IBankAccount CreateAccount(string userRole);
}

// Конкретные фабрики для создания счетов
public class RealBankAccountFactory : IBankAccountFactory
{
    private decimal _initialBalance;

    public RealBankAccountFactory(decimal initialBalance)
    {
        _initialBalance = initialBalance;
    }

    public IBankAccount CreateAccount(string userRole)
    {
        var realAccount = new RealBankAccount(_initialBalance);
        return new BankAccountProxy(realAccount, userRole); // Создаем и возвращаем заместителя
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фабрику для создания реальных банковских счетов
        var bankAccountFactory = new RealBankAccountFactory(1000);

        // Создаем счета для разных пользователей
        var adminAccount = bankAccountFactory.CreateAccount("Admin");
        var userAccount = bankAccountFactory.CreateAccount("User");
        var guestAccount = bankAccountFactory.CreateAccount("Guest");

        // Операции с администраторами
        Console.WriteLine("Администратор:");
        adminAccount.Deposit(500);
        adminAccount.Withdraw(200);
        Console.WriteLine($"Баланс: {adminAccount.GetBalance()}\n");

        // Операции с обычным пользователем
        Console.WriteLine("Пользователь:");
        userAccount.Deposit(300);
        userAccount.Withdraw(100);
        Console.WriteLine($"Баланс: {userAccount.GetBalance()}\n");

        // Операции с гостем
        Console.WriteLine("Гость:");
        guestAccount.Deposit(200); // Нет прав
        guestAccount.Withdraw(100); // Нет прав
        Console.WriteLine($"Баланс: {guestAccount.GetBalance()}\n");
    }
}
```

## Пояснение к коду

1. Интерфейс IBankAccount:

Определяет методы для работы с банковским счетом: Deposit(), Withdraw(), и GetBalance().
2. Конкретный класс RealBankAccount:

Реализует интерфейс IBankAccount и содержит логику работы с реальным счетом, включая внесение, снятие средств и проверку баланса.
3. Заместитель BankAccountProxy:

Этот класс также реализует интерфейс IBankAccount, но перед тем как выполнить операцию на реальном объекте RealBankAccount, он проверяет, есть ли у пользователя соответствующие права (например, роль "Admin" или "User").
Заместитель работает как оболочка, которая контролирует доступ к реальному объекту.
4. Абстрактная фабрика IBankAccountFactory:

Определяет метод CreateAccount(), который будет создавать объекты банковских счетов.
5. Конкретная фабрика RealBankAccountFactory:

Эта фабрика создаёт реальные банковские счета и использует заместителя для контроля доступа. Для каждого счета создаётся экземпляр BankAccountProxy, который управляет доступом.
6. В Main:

Создаются объекты банковских счетов для разных пользователей (админ, пользователь, гость).
Для каждого пользователя выполняются операции с проверкой доступа через заместителя.

## 11. Абстрактная фабрика + Мост.

## Сценарий:

Предположим, у нас есть система для работы с различными типами графических объектов, и нам нужно поддерживать разные способы рендеринга (например, рендеринг на экране и на принтере). Используем Абстрактную фабрику для создания объектов, а Мост для отделения абстракции (например, прямоугольник или круг) от конкретной реализации (например, рендеринг на экране или на принтере).

```csharp
using System;

// Абстракция для рендеринга
public interface IDrawingAPI
{
    void DrawCircle(double x, double y, double radius);
    void DrawRectangle(double x, double y, double width, double height);
}

// Конкретная реализация рендеринга на экране
public class ScreenDrawingAPI : IDrawingAPI
{
    public void DrawCircle(double x, double y, double radius)
    {
        Console.WriteLine($"Рисуем круг на экране с центром ({x}, {y}) и радиусом {radius}");
    }

    public void DrawRectangle(double x, double y, double width, double height)
    {
        Console.WriteLine($"Рисуем прямоугольник на экране с центром ({x}, {y}), шириной {width} и высотой {height}");
    }
}

// Конкретная реализация рендеринга на принтере
public class PrinterDrawingAPI : IDrawingAPI
{
    public void DrawCircle(double x, double y, double radius)
    {
        Console.WriteLine($"Рисуем круг на принтере с центром ({x}, {y}) и радиусом {radius}");
    }

    public void DrawRectangle(double x, double y, double width, double height)
    {
        Console.WriteLine($"Рисуем прямоугольник на принтере с центром ({x}, {y}), шириной {width} и высотой {height}");
    }
}

// Абстракция для графических объектов
public abstract class Shape
{
    protected IDrawingAPI _drawingAPI;

    public Shape(IDrawingAPI drawingAPI)
    {
        _drawingAPI = drawingAPI;
    }

    public abstract void Draw();
    public abstract void Resize(double factor);
}

// Конкретная абстракция для круга
public class Circle : Shape
{
    private double _x, _y, _radius;

    public Circle(double x, double y, double radius, IDrawingAPI drawingAPI) : base(drawingAPI)
    {
        _x = x;
        _y = y;
        _radius = radius;
    }

    public override void Draw()
    {
        _drawingAPI.DrawCircle(_x, _y, _radius);
    }

    public override void Resize(double factor)
    {
        _radius *= factor;
    }
}

// Конкретная абстракция для прямоугольника
public class Rectangle : Shape
{
    private double _x, _y, _width, _height;

    public Rectangle(double x, double y, double width, double height, IDrawingAPI drawingAPI) : base(drawingAPI)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    public override void Draw()
    {
        _drawingAPI.DrawRectangle(_x, _y, _width, _height);
    }

    public override void Resize(double factor)
    {
        _width *= factor;
        _height *= factor;
    }
}

// Абстрактная фабрика для создания объектов
public interface IShapeFactory
{
    Shape CreateCircle(double x, double y, double radius);
    Shape CreateRectangle(double x, double y, double width, double height);
}

// Конкретная фабрика для создания объектов с рендерингом на экране
public class ScreenShapeFactory : IShapeFactory
{
    public Shape CreateCircle(double x, double y, double radius)
    {
        return new Circle(x, y, radius, new ScreenDrawingAPI());
    }

    public Shape CreateRectangle(double x, double y, double width, double height)
    {
        return new Rectangle(x, y, width, height, new ScreenDrawingAPI());
    }
}

// Конкретная фабрика для создания объектов с рендерингом на принтере
public class PrinterShapeFactory : IShapeFactory
{
    public Shape CreateCircle(double x, double y, double radius)
    {
        return new Circle(x, y, radius, new PrinterDrawingAPI());
    }

    public Shape CreateRectangle(double x, double y, double width, double height)
    {
        return new Rectangle(x, y, width, height, new PrinterDrawingAPI());
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Используем фабрику для создания объектов с рендерингом на экране
        IShapeFactory screenFactory = new ScreenShapeFactory();

        Shape screenCircle = screenFactory.CreateCircle(5, 10, 15);
        screenCircle.Draw();
        screenCircle.Resize(1.5);
        screenCircle.Draw();

        Shape screenRectangle = screenFactory.CreateRectangle(10, 20, 30, 40);
        screenRectangle.Draw();
        screenRectangle.Resize(2);
        screenRectangle.Draw();

        // Используем фабрику для создания объектов с рендерингом на принтере
        IShapeFactory printerFactory = new PrinterShapeFactory();

        Shape printerCircle = printerFactory.CreateCircle(5, 10, 15);
        printerCircle.Draw();
        printerCircle.Resize(1.5);
        printerCircle.Draw();

        Shape printerRectangle = printerFactory.CreateRectangle(10, 20, 30, 40);
        printerRectangle.Draw();
        printerRectangle.Resize(2);
        printerRectangle.Draw();
    }
}
```

## Пояснение к коду

1. Интерфейс IDrawingAPI:

Определяет методы для рисования графических объектов (круг и прямоугольник) на различных устройствах (например, на экране или на принтере).
2. Конкретные реализации ScreenDrawingAPI и PrinterDrawingAPI:

Эти классы реализуют интерфейс IDrawingAPI и выполняют рендеринг объектов на экране или на принтере.
3. Абстракция Shape:

Абстрактный класс для графических объектов (например, круг или прямоугольник), который использует интерфейс IDrawingAPI для рендеринга и имеет методы для рисования и изменения размеров.
4. Конкретные абстракции Circle и Rectangle:

Эти классы расширяют Shape и реализуют методы для рисования и изменения размеров круга и прямоугольника соответственно.
5. Абстрактная фабрика IShapeFactory:

Определяет методы для создания объектов Circle и Rectangle.
6. Конкретные фабрики ScreenShapeFactory и PrinterShapeFactory:

Эти фабрики создают объекты с рендерингом на экране или на принтере, используя соответствующие реализации IDrawingAPI.

## Строитель.

## 1. Строитель + Прототип.

## Сценарий:

Предположим, что мы строим объект "Компьютер", который имеет разные компоненты (например, процессор, память, жесткий диск). Мы используем Строителя, чтобы создать такой объект, и Прототип для клонирования уже готовых компьютеров.

```csharp
using System;

// Прототип для создания копий объектов
public abstract class ComputerPrototype
{
    public abstract ComputerPrototype Clone();
}

// Класс, представляющий объект "Компьютер"
public class Computer : ComputerPrototype
{
    public string Processor { get; set; }
    public string Memory { get; set; }
    public string HardDrive { get; set; }

    public Computer(string processor, string memory, string hardDrive)
    {
        Processor = processor;
        Memory = memory;
        HardDrive = hardDrive;
    }

    // Метод клонирования
    public override ComputerPrototype Clone()
    {
        return new Computer(Processor, Memory, HardDrive);
    }

    public void DisplaySpecs()
    {
        Console.WriteLine($"Процессор: {Processor}, Память: {Memory}, Жесткий диск: {HardDrive}");
    }
}

// Строитель для поэтапного создания компьютера
public class ComputerBuilder
{
    private string _processor;
    private string _memory;
    private string _hardDrive;

    public ComputerBuilder SetProcessor(string processor)
    {
        _processor = processor;
        return this;
    }

    public ComputerBuilder SetMemory(string memory)
    {
        _memory = memory;
        return this;
    }

    public ComputerBuilder SetHardDrive(string hardDrive)
    {
        _hardDrive = hardDrive;
        return this;
    }

    public Computer Build()
    {
        return new Computer(_processor, _memory, _hardDrive);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Используем строителя для создания объекта "Компьютер"
        var builder = new ComputerBuilder();
        Computer computer1 = builder.SetProcessor("Intel i7")
                                    .SetMemory("16 GB")
                                    .SetHardDrive("1 TB SSD")
                                    .Build();

        // Отображаем характеристики компьютера
        Console.WriteLine("Первоначальный компьютер:");
        computer1.DisplaySpecs();

        // Клонируем объект с помощью Прототипа
        Computer computer2 = (Computer)computer1.Clone();

        // Отображаем характеристики клонированного компьютера
        Console.WriteLine("\nКлонированный компьютер:");
        computer2.DisplaySpecs();
    }
}
```

## Пояснение к коду

1. Класс ComputerPrototype:

Это абстрактный класс для объектов, которые могут быть клонированы. Он определяет абстрактный метод Clone() для клонирования объектов.
2. Класс Computer:

Это конкретный объект, который мы строим с помощью Строителя. Он наследует от ComputerPrototype и реализует метод Clone(), который создает новый объект с теми же свойствами.
3. Класс ComputerBuilder:

Это строитель для поэтапного создания объекта Компьютер. Он предоставляет методы для установки значений для каждого компонента (процессора, памяти, жесткого диска) и метод Build() для создания окончательного объекта.
4. Тестовая программа:

Мы создаем объект Компьютер с помощью строителя и отображаем его характеристики.
Затем, используя шаблон Прототип, мы клонируем этот объект и выводим характеристики клонированного компьютера.

## 2. Строитель + Компоновщик.

## Сценарий:

Предположим, что мы создаем "Компьютер", который состоит из различных компонентов (например, процессор, память, жесткий диск). Мы будем использовать Строителя для поэтапного создания объекта и Компоновщика, чтобы объединить различные части компьютера в единую структуру.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс компонента (части компьютера)
public interface IComputerComponent
{
    void Display();
}

// Класс, представляющий конкретную деталь компьютера
public class ComputerPart : IComputerComponent
{
    public string Name { get; set; }

    public ComputerPart(string name)
    {
        Name = name;
    }

    public void Display()
    {
        Console.WriteLine(Name);
    }
}

// Компоновщик для объединения частей компьютера
public class ComputerComposite : IComputerComponent
{
    private List<IComputerComponent> _components = new List<IComputerComponent>();

    public void Add(IComputerComponent component)
    {
        _components.Add(component);
    }

    public void Display()
    {
        foreach (var component in _components)
        {
            component.Display();
        }
    }
}

// Строитель для поэтапного создания компьютера
public class ComputerBuilder
{
    private ComputerComposite _computer = new ComputerComposite();

    public ComputerBuilder AddProcessor(string processor)
    {
        _computer.Add(new ComputerPart($"Processor: {processor}"));
        return this;
    }

    public ComputerBuilder AddMemory(string memory)
    {
        _computer.Add(new ComputerPart($"Memory: {memory}"));
        return this;
    }

    public ComputerBuilder AddHardDrive(string hardDrive)
    {
        _computer.Add(new ComputerPart($"Hard Drive: {hardDrive}"));
        return this;
    }

    public ComputerComposite Build()
    {
        return _computer;
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Используем строителя для создания компьютера
        var builder = new ComputerBuilder();
        ComputerComposite computer = builder.AddProcessor("Intel i7")
                                           .AddMemory("16 GB")
                                           .AddHardDrive("1 TB SSD")
                                           .Build();

        // Отображаем части компьютера, используя компоновщик
        Console.WriteLine("Компьютер состоит из следующих частей:");
        computer.Display();
    }
}
```

## Пояснение к коду

1. Интерфейс IComputerComponent:

Это общий интерфейс для всех компонентов компьютера. Он включает метод Display(), который будет отображать информацию о компоненте.
2. Класс ComputerPart:

Это конкретный компонент (например, процессор, память, жесткий диск). Каждый компонент реализует интерфейс IComputerComponent и имеет метод Display(), который выводит его название.
3. Класс ComputerComposite:

Это компоновщик, который может содержать множество объектов, реализующих интерфейс IComputerComponent. Он предоставляет метод Add() для добавления компонентов и метод Display() для отображения всех компонентов в составе.
4. Класс ComputerBuilder:

Строитель, который поэтапно добавляет компоненты (процессор, память, жесткий диск) в объект ComputerComposite. Метод Build() возвращает готовую структуру.
5. Тестовая программа:

Создается объект ComputerComposite с помощью строителя, который добавляет компоненты, а затем выводит состав компьютера с помощью компоновщика.

## 3. Строитель + Декоратор.

## Сценарий:

Предположим, что мы строим объект "Компьютер", который имеет базовые характеристики, такие как процессор, память и жесткий диск. С помощью Декоратора мы можем улучшать характеристики, например, увеличить память или добавить дополнительные функции.

```csharp
using System;

// Базовый интерфейс для компьютера
public interface IComputer
{
    void DisplaySpecs();
}

// Конкретная реализация объекта "Компьютер"
public class Computer : IComputer
{
    public string Processor { get; set; }
    public string Memory { get; set; }
    public string HardDrive { get; set; }

    public Computer(string processor, string memory, string hardDrive)
    {
        Processor = processor;
        Memory = memory;
        HardDrive = hardDrive;
    }

    public void DisplaySpecs()
    {
        Console.WriteLine($"Процессор: {Processor}, Память: {Memory}, Жесткий диск: {HardDrive}");
    }
}

// Строитель для поэтапного создания компьютера
public class ComputerBuilder
{
    private string _processor;
    private string _memory;
    private string _hardDrive;

    public ComputerBuilder SetProcessor(string processor)
    {
        _processor = processor;
        return this;
    }

    public ComputerBuilder SetMemory(string memory)
    {
        _memory = memory;
        return this;
    }

    public ComputerBuilder SetHardDrive(string hardDrive)
    {
        _hardDrive = hardDrive;
        return this;
    }

    public IComputer Build()
    {
        return new Computer(_processor, _memory, _hardDrive);
    }
}

// Декоратор для добавления функциональности
public abstract class ComputerDecorator : IComputer
{
    protected IComputer _computer;

    public ComputerDecorator(IComputer computer)
    {
        _computer = computer;
    }

    public virtual void DisplaySpecs()
    {
        _computer.DisplaySpecs();
    }
}

// Конкретный декоратор для увеличения памяти
public class MemoryUpgradeDecorator : ComputerDecorator
{
    private string _newMemory;

    public MemoryUpgradeDecorator(IComputer computer, string newMemory) : base(computer)
    {
        _newMemory = newMemory;
    }

    public override void DisplaySpecs()
    {
        _computer.DisplaySpecs();
        Console.WriteLine($"Обновленная память: {_newMemory}");
    }
}

// Конкретный декоратор для улучшения процессора
public class ProcessorUpgradeDecorator : ComputerDecorator
{
    private string _newProcessor;

    public ProcessorUpgradeDecorator(IComputer computer, string newProcessor) : base(computer)
    {
        _newProcessor = newProcessor;
    }

    public override void DisplaySpecs()
    {
        _computer.DisplaySpecs();
        Console.WriteLine($"Обновленный процессор: {_newProcessor}");
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим базовый компьютер с помощью строителя
        var builder = new ComputerBuilder();
        IComputer computer = builder.SetProcessor("Intel i5")
                                    .SetMemory("8 GB")
                                    .SetHardDrive("500 GB HDD")
                                    .Build();

        Console.WriteLine("Изначальные характеристики компьютера:");
        computer.DisplaySpecs();

        // Декорируем компьютер (обновляем память и процессор)
        IComputer upgradedComputer = new MemoryUpgradeDecorator(computer, "16 GB");
        upgradedComputer = new ProcessorUpgradeDecorator(upgradedComputer, "Intel i7");

        Console.WriteLine("\nОбновленные характеристики компьютера:");
        upgradedComputer.DisplaySpecs();
    }
}
```

## Пояснение к коду

1. Интерфейс IComputer:

Это общий интерфейс для всех объектов компьютера, который включает метод DisplaySpecs(), отображающий характеристики компьютера.
2. Класс Computer:

Это конкретная реализация объекта Компьютер, который реализует интерфейс IComputer и отображает характеристики компьютера.
3. Класс ComputerBuilder:

Это строитель для поэтапного создания объекта Компьютер. Он позволяет добавлять компоненты (процессор, память, жесткий диск) и собирать объект с помощью метода Build().
4. Абстрактный класс ComputerDecorator:

Это абстрактный декоратор, который реализует интерфейс IComputer и хранит ссылку на исходный объект Компьютер. Этот класс позволяет добавлять дополнительные функциональности.
5. Конкретные декораторы MemoryUpgradeDecorator и ProcessorUpgradeDecorator:

Эти декораторы добавляют улучшения: обновляют память и процессор. Они расширяют функциональность объекта Компьютер, добавляя новые характеристики, не изменяя сам объект.
6. Тестовая программа:

Создается базовый объект Компьютер с помощью Строителя.
Далее, с помощью Декораторов, добавляются улучшения (обновление памяти и процессора).
В конце отображаются как исходные, так и обновленные характеристики компьютера.

## 4. Строитель + Фасад.

## Сценарий:

Предположим, что мы строим "Компьютер", который состоит из нескольких компонентов, таких как процессор, память и жесткий диск. Строитель используется для создания этих компонентов, а Фасад предоставляет клиенту простой интерфейс для создания и использования компьютера.

```csharp
using System;

// Интерфейс компьютера
public interface IComputer
{
    void DisplaySpecs();
}

// Конкретная реализация компьютера
public class Computer : IComputer
{
    public string Processor { get; set; }
    public string Memory { get; set; }
    public string HardDrive { get; set; }

    public Computer(string processor, string memory, string hardDrive)
    {
        Processor = processor;
        Memory = memory;
        HardDrive = hardDrive;
    }

    public void DisplaySpecs()
    {
        Console.WriteLine($"Процессор: {Processor}, Память: {Memory}, Жесткий диск: {HardDrive}");
    }
}

// Строитель для поэтапного создания компьютера
public class ComputerBuilder
{
    private string _processor;
    private string _memory;
    private string _hardDrive;

    public ComputerBuilder SetProcessor(string processor)
    {
        _processor = processor;
        return this;
    }

    public ComputerBuilder SetMemory(string memory)
    {
        _memory = memory;
        return this;
    }

    public ComputerBuilder SetHardDrive(string hardDrive)
    {
        _hardDrive = hardDrive;
        return this;
    }

    public IComputer Build()
    {
        return new Computer(_processor, _memory, _hardDrive);
    }
}

// Фасад для упрощения создания и использования компьютера
public class ComputerFacade
{
    private ComputerBuilder _builder;

    public ComputerFacade()
    {
        _builder = new ComputerBuilder();
    }

    public IComputer CreateBasicComputer()
    {
        return _builder.SetProcessor("Intel i5")
                       .SetMemory("8 GB")
                       .SetHardDrive("500 GB HDD")
                       .Build();
    }

    public IComputer CreateGamingComputer()
    {
        return _builder.SetProcessor("Intel i9")
                       .SetMemory("32 GB")
                       .SetHardDrive("1 TB SSD")
                       .Build();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем фасад для упрощения создания компьютеров
        var facade = new ComputerFacade();

        // Создаем базовый компьютер с помощью фасада
        IComputer basicComputer = facade.CreateBasicComputer();
        Console.WriteLine("Базовый компьютер:");
        basicComputer.DisplaySpecs();

        // Создаем игровой компьютер с помощью фасада
        IComputer gamingComputer = facade.CreateGamingComputer();
        Console.WriteLine("\nИгровой компьютер:");
        gamingComputer.DisplaySpecs();
    }
}
```

## Пояснение к коду

1. Интерфейс IComputer:

Это общий интерфейс для всех объектов компьютеров, который включает метод DisplaySpecs(), отображающий характеристики компьютера.
2. Класс Computer:

Это конкретная реализация объекта Компьютер, который реализует интерфейс IComputer и отображает характеристики компьютера.
3. Класс ComputerBuilder:

Это строитель для поэтапного создания объекта Компьютер. Он позволяет добавлять компоненты (процессор, память, жесткий диск) и собирать объект с помощью метода Build().
4. Класс ComputerFacade:

Это фасад, который упрощает создание компьютеров. Он использует Строителя для создания базовых или игровых компьютеров и предоставляет клиенту простой интерфейс для работы с этим процессом. Это позволяет клиенту не заботиться о деталях создания объектов.
5. Тестовая программа:

Создается объект ComputerFacade, который упрощает создание базового и игрового компьютеров. Клиент вызывает методы CreateBasicComputer() и CreateGamingComputer() для получения объектов Computer, не беспокоясь о том, как именно они строятся.

## 5. Строитель + Легковес.

## Сценарий:

Предположим, что мы строим объекты "Книга". У каждой книги есть автор и название, но не все книги имеют уникальные авторов или названия, поэтому мы можем использовать Легковес для создания одного экземпляра для одинаковых авторов и названий.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс для книги
public interface IBook
{
    void Display();
}

// Конкретная реализация книги
public class Book : IBook
{
    private string _author;
    private string _title;

    public Book(string author, string title)
    {
        _author = author;
        _title = title;
    }

    public void Display()
    {
        Console.WriteLine($"Автор: {_author}, Название: {_title}");
    }
}

// Фабрика для легковесных объектов (Книг)
public class BookFactory
{
    private Dictionary<string, IBook> _books = new Dictionary<string, IBook>();

    public IBook GetBook(string author, string title)
    {
        string key = $"{author}-{title}";

        if (!_books.ContainsKey(key))
        {
            _books[key] = new Book(author, title);
        }

        return _books[key];
    }
}

// Строитель для создания коллекции книг
public class BookCollectionBuilder
{
    private List<IBook> _books = new List<IBook>();
    private BookFactory _bookFactory = new BookFactory();

    public BookCollectionBuilder AddBook(string author, string title)
    {
        var book = _bookFactory.GetBook(author, title);
        _books.Add(book);
        return this;
    }

    public List<IBook> Build()
    {
        return _books;
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем коллекцию книг с помощью строителя
        var builder = new BookCollectionBuilder();
        builder.AddBook("J.K. Rowling", "Harry Potter and the Sorcerer's Stone")
               .AddBook("J.K. Rowling", "Harry Potter and the Chamber of Secrets")
               .AddBook("J.R.R. Tolkien", "The Hobbit")
               .AddBook("J.K. Rowling", "Harry Potter and the Sorcerer's Stone"); // Дублирование

        var books = builder.Build();

        Console.WriteLine("Коллекция книг:");
        foreach (var book in books)
        {
            book.Display();
        }
    }
}
```

## Пояснение к коду

1. Интерфейс IBook:

Общий интерфейс для всех книг, который имеет метод Display(), отображающий информацию о книге.
2. Класс Book:

Конкретная реализация объекта Книга, которая реализует интерфейс IBook и отображает автора и название книги.
3. Класс BookFactory:

Фабрика, которая управляет созданием объектов Книга. Она использует коллекцию Dictionary для хранения уникальных экземпляров книг. Если книга с таким автором и названием уже существует, фабрика возвращает тот же экземпляр, иначе создает новый объект.
4. Класс BookCollectionBuilder:

Строитель для поэтапного добавления книг в коллекцию. Для каждой книги он использует Фабрику Легковесов для создания или получения уже существующих экземпляров книг.
5. Тестовая программа:

В тестовой программе создается коллекция книг с использованием Строителя. Когда книги добавляются в коллекцию, Фабрика Легковесов гарантирует, что книги с одинаковым автором и названием будут использовать один и тот же экземпляр, а не создавать новые.

## 6. Строитель + Заместитель.

## Сценарий:

Предположим, мы строим "Документ", который может быть представлен в разных форматах (например, текстовый или PDF). Заместитель будет проверять, имеет ли пользователь права на доступ и создание документа, прежде чем разрешить построение документа.

```csharp
using System;

// Интерфейс для документа
public interface IDocument
{
    void Display();
}

// Конкретная реализация документа
public class Document : IDocument
{
    public string Content { get; set; }

    public Document(string content)
    {
        Content = content;
    }

    public void Display()
    {
        Console.WriteLine($"Документ: {Content}");
    }
}

// Строитель для поэтапного создания документа
public class DocumentBuilder
{
    private string _content;

    public DocumentBuilder SetContent(string content)
    {
        _content = content;
        return this;
    }

    public IDocument Build()
    {
        return new Document(_content);
    }
}

// Интерфейс заместителя
public interface IDocumentProxy
{
    void Display();
}

// Заместитель, который контролирует доступ к документу
public class DocumentProxy : IDocumentProxy
{
    private DocumentBuilder _builder;
    private IDocument _document;
    private bool _hasAccess;

    public DocumentProxy(DocumentBuilder builder, bool hasAccess)
    {
        _builder = builder;
        _hasAccess = hasAccess;
    }

    public void Display()
    {
        if (_hasAccess)
        {
            if (_document == null)
            {
                _document = _builder.Build();
            }
            _document.Display();
        }
        else
        {
            Console.WriteLine("Доступ к документу запрещен.");
        }
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим документ с помощью Строителя
        var builder = new DocumentBuilder();
        builder.SetContent("Это текст документа.");

        // Заместитель с доступом
        var proxyWithAccess = new DocumentProxy(builder, true);
        Console.WriteLine("Документ с доступом:");
        proxyWithAccess.Display();

        // Заместитель без доступа
        var proxyWithoutAccess = new DocumentProxy(builder, false);
        Console.WriteLine("\nДокумент без доступа:");
        proxyWithoutAccess.Display();
    }
}
```

## Пояснение к коду

1. Интерфейс IDocument:

Определяет метод Display(), который должен отображать содержание документа.
2. Класс Document:

Конкретная реализация объекта Документ, которая реализует интерфейс IDocument и отображает содержание документа.
3. Класс DocumentBuilder:

Строитель для поэтапного создания документа. Он позволяет задать содержимое документа и построить объект Document.
4. Интерфейс IDocumentProxy:

Интерфейс, который определяет метод Display(), через который будет происходить доступ к документу.
5. Класс DocumentProxy:

Заместитель, который контролирует доступ к объекту Документ. Если у пользователя есть права доступа (_hasAccess), то создается и отображается документ, иначе выводится сообщение о запрете доступа. Это позволяет контролировать доступ к объекту через проверку условий.
6. Тестовая программа:

В тестовой программе создается документ с помощью Строителя. После этого создаются два объекта Заместителя: один с доступом, другой без. В зависимости от прав доступа, заместитель либо покажет документ, либо откажет в доступе.

## 7. Строитель + Стратегия.

## Сценарий:

Предположим, мы строим Документ, который имеет текстовое содержимое. Мы можем применить разные стратегии форматирования текста, такие как заглавные буквы или нижний регистр. Стратегия позволит выбрать способ форматирования, а Строитель поэтапно создаст документ с заданным текстом.

```csharp
using System;

// Интерфейс стратегии для форматирования текста
public interface ITextFormatterStrategy
{
    string Format(string text);
}

// Конкретная стратегия для форматирования в верхний регистр
public class UpperCaseFormatter : ITextFormatterStrategy
{
    public string Format(string text)
    {
        return text.ToUpper();
    }
}

// Конкретная стратегия для форматирования в нижний регистр
public class LowerCaseFormatter : ITextFormatterStrategy
{
    public string Format(string text)
    {
        return text.ToLower();
    }
}

// Класс документа
public class Document
{
    public string Content { get; set; }

    public Document(string content)
    {
        Content = content;
    }

    public void Display()
    {
        Console.WriteLine($"Документ: {Content}");
    }
}

// Строитель документа
public class DocumentBuilder
{
    private string _content;

    public DocumentBuilder SetContent(string content)
    {
        _content = content;
        return this;
    }

    public Document Build()
    {
        return new Document(_content);
    }
}

// Класс для применения стратегии
public class DocumentWithStrategy
{
    private readonly ITextFormatterStrategy _formatterStrategy;
    private readonly Document _document;

    public DocumentWithStrategy(ITextFormatterStrategy formatterStrategy, Document document)
    {
        _formatterStrategy = formatterStrategy;
        _document = document;
    }

    public void Display()
    {
        string formattedContent = _formatterStrategy.Format(_document.Content);
        Console.WriteLine($"Отформатированный документ: {formattedContent}");
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим документ с помощью Строителя
        var builder = new DocumentBuilder();
        builder.SetContent("Это пример документа с разными форматами.");

        var document = builder.Build();

        // Применяем стратегию для форматирования в верхний регистр
        var upperCaseStrategy = new DocumentWithStrategy(new UpperCaseFormatter(), document);
        Console.WriteLine("Документ с верхним регистром:");
        upperCaseStrategy.Display();

        // Применяем стратегию для форматирования в нижний регистр
        var lowerCaseStrategy = new DocumentWithStrategy(new LowerCaseFormatter(), document);
        Console.WriteLine("\nДокумент с нижним регистром:");
        lowerCaseStrategy.Display();
    }
}
```

## Пояснение к коду

1. Интерфейс ITextFormatterStrategy:

Определяет метод Format, который будет реализован для различных стратегий форматирования текста.
2. Классы UpperCaseFormatter и LowerCaseFormatter:

Конкретные реализации стратегий для форматирования текста в верхний и нижний регистр соответственно.
3. Класс Document:

Представляет сам документ с текстовым содержимым. У него есть метод Display(), который выводит содержание документа.
4. Класс DocumentBuilder:

Строитель для поэтапного создания документа. Он позволяет задавать текст документа и собирать его в объект Document.
5. Класс DocumentWithStrategy:

Этот класс принимает стратегию форматирования и применяет её к содержимому документа. В зависимости от выбранной стратегии, содержимое документа форматируется соответствующим образом.
6. Тестовая программа:

В тестовой программе создается объект Document с помощью Строителя, а затем создаются два объекта DocumentWithStrategy, каждый из которых использует разные стратегии форматирования текста (верхний и нижний регистр).

## 8. Строитель + Шаблонный метод.

## Сценарий:

Предположим, мы строим Отчет, который может быть представлен в разных форматах (например, текстовый или HTML). Мы создаем общий шаблонный метод для генерации отчета, но детали вывода (например, заголовок, содержание) будут различаться в зависимости от формата отчета.

```csharp
using System;

// Абстрактный класс для отчета с шаблонным методом
public abstract class Report
{
    // Шаблонный метод, который определяет структуру отчета
    public void GenerateReport()
    {
        AddHeader();
        AddContent();
        AddFooter();
    }

    // Общие шаги отчета, которые всегда выполняются
    private void AddHeader()
    {
        Console.WriteLine("Заголовок отчета:");
    }

    // Абстрактный метод для добавления содержимого отчета (реализуется в подклассах)
    protected abstract void AddContent();

    // Общие шаги отчета, которые всегда выполняются
    private void AddFooter()
    {
        Console.WriteLine("\nКонец отчета.");
    }
}

// Конкретная реализация отчета в текстовом формате
public class TextReport : Report
{
    protected override void AddContent()
    {
        Console.WriteLine("Содержимое отчета в текстовом формате.");
    }
}

// Конкретная реализация отчета в HTML формате
public class HtmlReport : Report
{
    protected override void AddContent()
    {
        Console.WriteLine("Содержимое отчета в HTML формате.");
    }
}

// Строитель для создания отчета
public class ReportBuilder
{
    private Report _report;

    public ReportBuilder SetReportFormat(string format)
    {
        if (format.ToLower() == "text")
        {
            _report = new TextReport();
        }
        else if (format.ToLower() == "html")
        {
            _report = new HtmlReport();
        }
        else
        {
            throw new ArgumentException("Неизвестный формат отчета.");
        }
        return this;
    }

    public void Build()
    {
        _report.GenerateReport();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим и генерируем текстовый отчет
        var builder = new ReportBuilder();
        builder.SetReportFormat("text");
        Console.WriteLine("Генерация текстового отчета:");
        builder.Build();

        // Строим и генерируем HTML отчет
        builder.SetReportFormat("html");
        Console.WriteLine("\nГенерация HTML отчета:");
        builder.Build();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Report:

В этом классе определен Шаблонный метод GenerateReport(), который задает общую структуру отчета. Этот метод вызывает другие методы, такие как AddHeader(), AddContent() и AddFooter(), в предсказуемом порядке.
Метод AddContent() — абстрактный, его реализация оставляется для конкретных классов (например, для текстового или HTML отчета).
2. Конкретные реализации TextReport и HtmlReport:

Эти классы реализуют метод AddContent() по-разному, в зависимости от формата отчета (текстовый или HTML).
3. Класс ReportBuilder:

Этот класс позволяет поэтапно построить отчет, задавая формат отчета через метод SetReportFormat(). Он создает нужный объект отчета и вызывает его метод GenerateReport() для создания отчета.
4. Тестовая программа:

В тестовой программе создаются два разных отчета: текстовый и HTML. Строитель помогает динамически выбрать формат отчета, а шаблонный метод гарантирует, что отчет будет создан в нужной структуре, вне зависимости от конкретной реализации.

## 9. Строитель + Наблюдатель.

## Сценарий:

Предположим, мы строим Документ, и при изменении содержания документа, подписчики (наблюдатели) должны получать уведомления о том, что содержание документа было обновлено.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс наблюдателя, который будет получать уведомления об изменениях
public interface IObserver
{
    void Update(string message);
}

// Класс наблюдателя, который будет получать уведомления
public class DocumentObserver : IObserver
{
    private string _name;

    public DocumentObserver(string name)
    {
        _name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{_name} получил уведомление: {message}");
    }
}

// Класс субъекта, который генерирует уведомления (в данном случае это документ)
public class Document
{
    private string _content;
    private List<IObserver> _observers = new List<IObserver>();

    // Добавление наблюдателя
    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    // Удаление наблюдателя
    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    // Уведомление всех наблюдателей
    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update($"Документ был обновлен: {_content}");
        }
    }

    // Изменение содержания документа
    public void SetContent(string content)
    {
        _content = content;
        Notify();
    }
}

// Строитель для создания документа
public class DocumentBuilder
{
    private string _content;

    public DocumentBuilder SetContent(string content)
    {
        _content = content;
        return this;
    }

    public Document Build()
    {
        return new Document();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем наблюдателей
        var observer1 = new DocumentObserver("Наблюдатель 1");
        var observer2 = new DocumentObserver("Наблюдатель 2");

        // Строим документ с помощью Строителя
        var builder = new DocumentBuilder();
        var document = builder.Build();

        // Подписываем наблюдателей на изменения документа
        document.Attach(observer1);
        document.Attach(observer2);

        // Изменяем содержание документа
        Console.WriteLine("Обновление содержания документа:");
        document.SetContent("Это новое содержание документа.");

        // Изменяем содержание документа снова
        Console.WriteLine("\nОбновление содержания документа снова:");
        document.SetContent("Это еще одно обновленное содержание.");
    }
}
```

## Пояснение к коду

1. Интерфейс IObserver:

Определяет метод Update(), который будет вызываться, когда субъект (в нашем случае документ) уведомляет своих наблюдателей об изменениях.
2. Класс DocumentObserver:

Это конкретный наблюдатель, который реализует интерфейс IObserver. Каждый наблюдатель получает уведомление с сообщением, когда содержимое документа обновляется.
3. Класс Document:

Это субъект, который отслеживает свои изменения и уведомляет всех подписанных наблюдателей. У него есть метод Attach() для подписки наблюдателя, метод Detach() для отписки и метод Notify(), который уведомляет всех наблюдателей о текущем содержимом документа.
4. Класс DocumentBuilder:

Строитель для создания документа. Он может быть расширен для поэтапного добавления данных в объект Document. В данном примере он просто создает пустой объект документа.
5. Тестовая программа:

В программе создаются два наблюдателя. С помощью Строителя создается объект Document, и наблюдатели подписываются на изменения этого объекта. При изменении содержания документа, все подписанные наблюдатели получают уведомления.

## 10. Строитель + Состояние.

## Сценарий:

Предположим, что у нас есть объект Телекоммуникационное устройство, которое может находиться в разных состояниях: включено или выключено. Поведение устройства зависит от его состояния.

```csharp
using System;

// Интерфейс состояния
public interface IDeviceState
{
    void PressButton(Device device);
}

// Конкретное состояние: Устройство включено
public class OnState : IDeviceState
{
    public void PressButton(Device device)
    {
        Console.WriteLine("Устройство выключено.");
        device.SetState(new OffState()); // Переход в состояние выключено
    }
}

// Конкретное состояние: Устройство выключено
public class OffState : IDeviceState
{
    public void PressButton(Device device)
    {
        Console.WriteLine("Устройство включено.");
        device.SetState(new OnState()); // Переход в состояние включено
    }
}

// Класс устройства, который может изменять своё состояние
public class Device
{
    private IDeviceState _state;

    public Device(IDeviceState initialState)
    {
        _state = initialState;
    }

    // Метод для изменения состояния устройства
    public void SetState(IDeviceState newState)
    {
        _state = newState;
    }

    // Метод, который вызывает поведение в зависимости от текущего состояния
    public void PressButton()
    {
        _state.PressButton(this);
    }
}

// Строитель для создания устройства с начальным состоянием
public class DeviceBuilder
{
    private IDeviceState _initialState;

    public DeviceBuilder SetInitialState(string state)
    {
        if (state.ToLower() == "on")
        {
            _initialState = new OnState();
        }
        else if (state.ToLower() == "off")
        {
            _initialState = new OffState();
        }
        else
        {
            throw new ArgumentException("Неверное состояние.");
        }
        return this;
    }

    public Device Build()
    {
        return new Device(_initialState);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим устройство с начальным состоянием "выключено"
        var builder = new DeviceBuilder();
        var device = builder.SetInitialState("off").Build();

        // Тестируем поведение устройства
        Console.WriteLine("Тестирование устройства:");
        device.PressButton(); // Включаем устройство
        device.PressButton(); // Выключаем устройство
    }
}
```

## Пояснение к коду

1. Интерфейс IDeviceState:

Определяет метод PressButton(), который будет вызываться для изменения состояния устройства.
2. Конкретные реализации состояний (OnState и OffState):

Эти классы реализуют поведение устройства в двух состояниях: когда устройство включено (OnState) и когда оно выключено (OffState).
Каждый класс состояния знает, как переключить устройство в следующее состояние при нажатии кнопки.
3. Класс Device:

Это сам объект, который может изменять своё состояние. Он имеет метод SetState() для изменения состояния и метод PressButton(), который вызывает поведение в зависимости от текущего состояния.
4. Класс DeviceBuilder:

Строитель для создания устройства с начальным состоянием. Он позволяет задавать начальное состояние устройства при его создании (включено или выключено).
5. Тестовая программа:

В тестовой программе создается устройство с начальным состоянием "выключено". Затем мы дважды нажимаем кнопку, чтобы увидеть, как устройство меняет свое состояние: сначала оно включается, затем выключается.

## 11. Строитель + Посетитель.

## Сценарий:

Предположим, что у нас есть набор различных типов частей, составляющих Компьютер (например, процессор, память и т.д.). Мы используем Строитель, чтобы поэтапно собрать компьютер, а Посетитель для выполнения различных операций с его компонентами (например, печать информации о компонентах).

```csharp
using System;

// Интерфейс компонента
public interface IComputerComponent
{
    void Accept(IVisitor visitor);
}

// Конкретные компоненты компьютера
public class Processor : IComputerComponent
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Display()
    {
        Console.WriteLine("Процессор: Intel Core i9");
    }
}

public class Memory : IComputerComponent
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Display()
    {
        Console.WriteLine("Оперативная память: 16GB DDR4");
    }
}

public class HardDrive : IComputerComponent
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Display()
    {
        Console.WriteLine("Жесткий диск: 1TB SSD");
    }
}

// Интерфейс посетителя
public interface IVisitor
{
    void Visit(Processor processor);
    void Visit(Memory memory);
    void Visit(HardDrive hardDrive);
}

// Конкретный посетитель, который выполняет действия над компонентами
public class ComputerDisplayVisitor : IVisitor
{
    public void Visit(Processor processor)
    {
        processor.Display();
    }

    public void Visit(Memory memory)
    {
        memory.Display();
    }

    public void Visit(HardDrive hardDrive)
    {
        hardDrive.Display();
    }
}

// Класс, представляющий сборку компьютера (с использованием Строителя)
public class Computer
{
    private IComputerComponent _processor;
    private IComputerComponent _memory;
    private IComputerComponent _hardDrive;

    public void SetProcessor(IComputerComponent processor)
    {
        _processor = processor;
    }

    public void SetMemory(IComputerComponent memory)
    {
        _memory = memory;
    }

    public void SetHardDrive(IComputerComponent hardDrive)
    {
        _hardDrive = hardDrive;
    }

    public void Accept(IVisitor visitor)
    {
        _processor.Accept(visitor);
        _memory.Accept(visitor);
        _hardDrive.Accept(visitor);
    }
}

// Строитель для создания компьютера
public class ComputerBuilder
{
    private Computer _computer = new Computer();

    public ComputerBuilder AddProcessor()
    {
        _computer.SetProcessor(new Processor());
        return this;
    }

    public ComputerBuilder AddMemory()
    {
        _computer.SetMemory(new Memory());
        return this;
    }

    public ComputerBuilder AddHardDrive()
    {
        _computer.SetHardDrive(new HardDrive());
        return this;
    }

    public Computer Build()
    {
        return _computer;
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Создаем компьютер с помощью Строителя
        var builder = new ComputerBuilder();
        var computer = builder.AddProcessor().AddMemory().AddHardDrive().Build();

        // Создаем посетителя, который будет выполнять действия с компонентами
        var visitor = new ComputerDisplayVisitor();

        // Применяем посетителя к компьютеру
        Console.WriteLine("Печать информации о компьютере:");
        computer.Accept(visitor);
    }
}
```

## Пояснение к коду

1. Интерфейс IComputerComponent:

Все компоненты компьютера (процессор, память, жесткий диск) реализуют этот интерфейс и могут принимать посетителей через метод Accept(). Каждый компонент вызывает соответствующий метод Visit() у посетителя.
2. Конкретные компоненты:

Классы Processor, Memory и HardDrive представляют компоненты компьютера. Каждый из них реализует метод Accept() для обработки посетителя.
3. Интерфейс IVisitor:

Это интерфейс для посетителя, который имеет методы для посещения каждого типа компонента (Visit(Processor processor), Visit(Memory memory), и т.д.).
4. Класс ComputerDisplayVisitor:

Это конкретный посетитель, который реализует интерфейс IVisitor. Он выполняет операцию вывода информации о каждом компоненте (например, печатает название компонента и его характеристики).
5. Класс Computer:

Это класс, который собирает компьютер из отдельных компонентов. Он имеет методы для установки процессора, памяти и жесткого диска, а также метод Accept() для того, чтобы передать посетителя компонентам.
6. Класс ComputerBuilder:

Это строитель, который поэтапно создает объект Computer. Он позволяет добавлять компоненты (процессор, память, жесткий диск) и возвращать готовую сборку компьютера.
7. Тестовая программа:

В программе создается компьютер с помощью Строителя, затем используется Посетитель для вывода информации о компонентах компьютера.

## 12. Строитель + Цепочка обязанностей.

## Сценарий:

Предположим, у нас есть система обработки заявок на кредит. Каждый этап обработки заявки может зависеть от определённых условий, и Цепочка обязанностей будет использоваться для последовательной проверки условий, в то время как Строитель будет создавать заявку с необходимыми параметрами.

```csharp
using System;

// Абстрактный класс для обработчиков в цепочке обязанностей
public abstract class RequestHandler
{
    protected RequestHandler _nextHandler;

    public void SetNextHandler(RequestHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public abstract void HandleRequest(CreditRequest request);
}

// Конкретный обработчик: Проверка дохода
public class IncomeHandler : RequestHandler
{
    public override void HandleRequest(CreditRequest request)
    {
        if (request.Income >= 50000)
        {
            Console.WriteLine("Доход проверен: Достаточно для кредита.");
            _nextHandler?.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Доход недостаточен для кредита.");
        }
    }
}

// Конкретный обработчик: Проверка кредитной истории
public class CreditHistoryHandler : RequestHandler
{
    public override void HandleRequest(CreditRequest request)
    {
        if (request.CreditHistoryScore >= 700)
        {
            Console.WriteLine("Кредитная история проверена: Хорошая.");
            _nextHandler?.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Кредитная история плохая.");
        }
    }
}

// Конкретный обработчик: Проверка возраста
public class AgeHandler : RequestHandler
{
    public override void HandleRequest(CreditRequest request)
    {
        if (request.Age >= 21)
        {
            Console.WriteLine("Возраст проверен: Соответствует минимальному требованию.");
            _nextHandler?.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Возраст не соответствует минимальному требованию.");
        }
    }
}

// Класс заявки на кредит
public class CreditRequest
{
    public int Income { get; set; }
    public int CreditHistoryScore { get; set; }
    public int Age { get; set; }

    public CreditRequest(int income, int creditHistoryScore, int age)
    {
        Income = income;
        CreditHistoryScore = creditHistoryScore;
        Age = age;
    }
}

// Строитель для создания заявки на кредит
public class CreditRequestBuilder
{
    private int _income;
    private int _creditHistoryScore;
    private int _age;

    public CreditRequestBuilder SetIncome(int income)
    {
        _income = income;
        return this;
    }

    public CreditRequestBuilder SetCreditHistoryScore(int score)
    {
        _creditHistoryScore = score;
        return this;
    }

    public CreditRequestBuilder SetAge(int age)
    {
        _age = age;
        return this;
    }

    public CreditRequest Build()
    {
        return new CreditRequest(_income, _creditHistoryScore, _age);
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим заявку на кредит
        var builder = new CreditRequestBuilder();
        var request = builder.SetIncome(60000)
                             .SetCreditHistoryScore(750)
                             .SetAge(25)
                             .Build();

        // Создаем цепочку обработчиков
        var incomeHandler = new IncomeHandler();
        var creditHistoryHandler = new CreditHistoryHandler();
        var ageHandler = new AgeHandler();

        // Связываем обработчиков в цепочку
        incomeHandler.SetNextHandler(creditHistoryHandler);
        creditHistoryHandler.SetNextHandler(ageHandler);

        // Обрабатываем заявку
        Console.WriteLine("Обработка заявки на кредит:");
        incomeHandler.HandleRequest(request);
    }
}
```

## Пояснение к коду

1. Абстрактный класс RequestHandler:

Это базовый класс для всех обработчиков в цепочке обязанностей. Он содержит ссылку на следующий обработчик (_nextHandler) и метод SetNextHandler() для настройки этой цепочки. Метод HandleRequest() будет переопределён в каждом конкретном обработчике.
2. Конкретные обработчики:

IncomeHandler: Проверяет, достаточно ли дохода для получения кредита.
CreditHistoryHandler: Проверяет, соответствует ли кредитная история требованиям.
AgeHandler: Проверяет, достаточно ли клиенту лет для получения кредита.
3. Класс CreditRequest:

Этот класс представляет собой заявку на кредит с полями для дохода, кредитной истории и возраста. Он создается с помощью Строителя.
4. Класс CreditRequestBuilder:

Строитель для пошагового создания заявки на кредит. Он позволяет установить значения для каждого поля заявки и собрать её.
5. Тестовая программа:

В программе создается объект заявки с помощью Строителя. Затем создается цепочка обработчиков, и каждый из них проверяет заявку на основе своих критериев. Если все проверки проходят успешно, заявка может быть одобрена.

## 13. Строитель + Команда.

## Сценарий:

Предположим, у нас есть класс Компьютер, который состоит из нескольких компонентов (процессора, памяти, жесткого диска). Мы используем Строитель для поэтапного создания этого компьютера, а затем применяем Команду для выполнения различных операций с ним, например, включение, выключение или перезагрузка.

```csharp
using System;
using System.Collections.Generic;

// Класс, представляющий компоненты компьютера
public class Computer
{
    public string Processor { get; set; }
    public string Memory { get; set; }
    public string HardDrive { get; set; }

    public void ShowDetails()
    {
        Console.WriteLine($"Процессор: {Processor}");
        Console.WriteLine($"Оперативная память: {Memory}");
        Console.WriteLine($"Жесткий диск: {HardDrive}");
    }

    public void Start()
    {
        Console.WriteLine("Компьютер включен.");
    }

    public void Shutdown()
    {
        Console.WriteLine("Компьютер выключен.");
    }

    public void Restart()
    {
        Console.WriteLine("Компьютер перезагружен.");
    }
}

// Строитель для создания компьютера
public class ComputerBuilder
{
    private Computer _computer = new Computer();

    public ComputerBuilder SetProcessor(string processor)
    {
        _computer.Processor = processor;
        return this;
    }

    public ComputerBuilder SetMemory(string memory)
    {
        _computer.Memory = memory;
        return this;
    }

    public ComputerBuilder SetHardDrive(string hardDrive)
    {
        _computer.HardDrive = hardDrive;
        return this;
    }

    public Computer Build()
    {
        return _computer;
    }
}

// Интерфейс команды
public interface ICommand
{
    void Execute();
}

// Конкретные команды для работы с компьютером
public class StartCommand : ICommand
{
    private Computer _computer;

    public StartCommand(Computer computer)
    {
        _computer = computer;
    }

    public void Execute()
    {
        _computer.Start();
    }
}

public class ShutdownCommand : ICommand
{
    private Computer _computer;

    public ShutdownCommand(Computer computer)
    {
        _computer = computer;
    }

    public void Execute()
    {
        _computer.Shutdown();
    }
}

public class RestartCommand : ICommand
{
    private Computer _computer;

    public RestartCommand(Computer computer)
    {
        _computer = computer;
    }

    public void Execute()
    {
        _computer.Restart();
    }
}

// Класс для выполнения команд
public class CommandInvoker
{
    private List<ICommand> _commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим компьютер
        var builder = new ComputerBuilder();
        var computer = builder.SetProcessor("Intel Core i9")
                              .SetMemory("16GB DDR4")
                              .SetHardDrive("1TB SSD")
                              .Build();

        // Создаем команды для управления компьютером
        ICommand startCommand = new StartCommand(computer);
        ICommand shutdownCommand = new ShutdownCommand(computer);
        ICommand restartCommand = new RestartCommand(computer);

        // Создаем инвокер и добавляем команды
        var invoker = new CommandInvoker();
        invoker.AddCommand(startCommand);
        invoker.AddCommand(restartCommand);
        invoker.AddCommand(shutdownCommand);

        // Выполняем команды
        Console.WriteLine("Выполнение команд:");
        invoker.ExecuteCommands();

        // Показываем детали собранного компьютера
        Console.WriteLine("\nДетали компьютера:");
        computer.ShowDetails();
    }
}
```

## Пояснение к коду

1. Класс Computer:

Представляет собой объект компьютера с полями для процессора, памяти и жесткого диска. У него есть методы для включения, выключения и перезагрузки.
2. Строитель ComputerBuilder:

Строит объект Computer. Через методы строителя можно поэтапно задать параметры для компьютера (процессор, память, жесткий диск), а затем собрать его с помощью метода Build().
3. Интерфейс ICommand:

Это интерфейс для всех команд, которые будут выполняться. Команды должны реализовывать метод Execute().
4. Конкретные команды:

StartCommand, ShutdownCommand, RestartCommand: Эти классы реализуют интерфейс ICommand и содержат логику для выполнения соответствующих операций с объектом Computer.
5. Класс CommandInvoker:

Этот класс служит для хранения и выполнения списка команд. Он добавляет команды в очередь и выполняет их через метод ExecuteCommands().
6. Тестовая программа:

В программе сначала создается объект компьютера с помощью Строителя. Затем создаются команды для включения, перезагрузки и выключения компьютера. Все эти команды добавляются в Инвокер, который затем выполняет их.

## 14. Строитель + Посредник.

## Сценарий:

Предположим, у нас есть система, которая строит автомобиль с компонентами, такими как двигатель, колеса и кузов. Посредник будет координировать взаимодействие между этими компонентами, чтобы обеспечить правильную работу автомобиля.

```csharp
using System;

// Интерфейс для всех компонентов автомобиля
public interface ICarComponent
{
    void Initialize();
}

// Конкретные компоненты автомобиля
public class Engine : ICarComponent
{
    public void Initialize()
    {
        Console.WriteLine("Двигатель установлен.");
    }
}

public class Wheels : ICarComponent
{
    public void Initialize()
    {
        Console.WriteLine("Колеса установлены.");
    }
}

public class Body : ICarComponent
{
    public void Initialize()
    {
        Console.WriteLine("Кузов установлен.");
    }
}

// Класс автомобиля
public class Car
{
    public ICarComponent Engine { get; set; }
    public ICarComponent Wheels { get; set; }
    public ICarComponent Body { get; set; }

    public void ShowCar()
    {
        Console.WriteLine("\nАвтомобиль готов:");
        Engine.Initialize();
        Wheels.Initialize();
        Body.Initialize();
    }
}

// Строитель для создания автомобиля
public class CarBuilder
{
    private Car _car = new Car();

    public CarBuilder AddEngine(ICarComponent engine)
    {
        _car.Engine = engine;
        return this;
    }

    public CarBuilder AddWheels(ICarComponent wheels)
    {
        _car.Wheels = wheels;
        return this;
    }

    public CarBuilder AddBody(ICarComponent body)
    {
        _car.Body = body;
        return this;
    }

    public Car Build()
    {
        return _car;
    }
}

// Интерфейс Посредника
public interface ICarMediator
{
    void RegisterEngine(ICarComponent engine);
    void RegisterWheels(ICarComponent wheels);
    void RegisterBody(ICarComponent body);
    void AssembleCar();
}

// Реализация Посредника
public class CarMediator : ICarMediator
{
    private ICarComponent _engine;
    private ICarComponent _wheels;
    private ICarComponent _body;

    public void RegisterEngine(ICarComponent engine)
    {
        _engine = engine;
    }

    public void RegisterWheels(ICarComponent wheels)
    {
        _wheels = wheels;
    }

    public void RegisterBody(ICarComponent body)
    {
        _body = body;
    }

    public void AssembleCar()
    {
        Console.WriteLine("\nМедиатор координирует сборку автомобиля:");
        _engine.Initialize();
        _wheels.Initialize();
        _body.Initialize();
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим автомобиль с помощью Строителя
        var builder = new CarBuilder();
        var car = builder.AddEngine(new Engine())
                         .AddWheels(new Wheels())
                         .AddBody(new Body())
                         .Build();

        // Показываем автомобиль
        car.ShowCar();

        // Используем посредника для координации сборки автомобиля
        var mediator = new CarMediator();
        mediator.RegisterEngine(new Engine());
        mediator.RegisterWheels(new Wheels());
        mediator.RegisterBody(new Body());

        // Сборка автомобиля через посредника
        mediator.AssembleCar();
    }
}
```

## Пояснение к коду

1. Компоненты автомобиля (ICarComponent):

Все компоненты автомобиля (двигатель, колеса, кузов) реализуют интерфейс ICarComponent с методом Initialize(), который используется для их инициализации.
2. Класс Car:

Этот класс представляет собой объект автомобиля. Он содержит ссылки на компоненты (двигатель, колеса, кузов) и метод ShowCar(), который вызывает метод Initialize() для каждого компонента, чтобы показать, что они были установлены.
3. Строитель CarBuilder:

Строитель используется для создания автомобиля, добавляя компоненты по очереди (двигатель, колеса, кузов). После того как все компоненты добавлены, вызывается метод Build(), который создает и возвращает готовый автомобиль.
4. Интерфейс ICarMediator:

Это интерфейс для посредника, который позволяет регистрировать компоненты автомобиля и координировать их сборку.
5. Реализация посредника CarMediator:

Посредник управляет сборкой автомобиля, координируя взаимодействие между компонентами (двигателем, колесами и кузовом). Метод AssembleCar() вызывает метод Initialize() для каждого компонента, показывая, как они работают вместе.
6. Тестовая программа:

Сначала создается автомобиль с помощью Строителя. Затем используется Посредник для координации инициализации компонентов автомобиля и их сборки.

## 15. Строитель + Снимок.

## Сценарий:

Мы строим объект "Автомобиль", который состоит из различных компонентов (двигатель, колеса, кузов), и сохраняем состояние автомобиля на каждом этапе с помощью Снимка. В случае необходимости, мы можем восстановить его состояние на определенном этапе.

```csharp
using System;
using System.Collections.Generic;

// Компоненты автомобиля
public class Car
{
    public string Engine { get; set; }
    public string Wheels { get; set; }
    public string Body { get; set; }

    public void ShowDetails()
    {
        Console.WriteLine($"Двигатель: {Engine}");
        Console.WriteLine($"Колеса: {Wheels}");
        Console.WriteLine($"Кузов: {Body}");
    }
}

// Строитель для создания автомобиля
public class CarBuilder
{
    private Car _car = new Car();

    public CarBuilder SetEngine(string engine)
    {
        _car.Engine = engine;
        return this;
    }

    public CarBuilder SetWheels(string wheels)
    {
        _car.Wheels = wheels;
        return this;
    }

    public CarBuilder SetBody(string body)
    {
        _car.Body = body;
        return this;
    }

    public Car Build()
    {
        return _car;
    }

    // Создание снимка (сохранение состояния)
    public CarMemento Save()
    {
        return new CarMemento(_car.Engine, _car.Wheels, _car.Body);
    }

    // Восстановление состояния из снимка
    public void Restore(CarMemento memento)
    {
        _car.Engine = memento.Engine;
        _car.Wheels = memento.Wheels;
        _car.Body = memento.Body;
    }
}

// Снимок состояния автомобиля
public class CarMemento
{
    public string Engine { get; }
    public string Wheels { get; }
    public string Body { get; }

    public CarMemento(string engine, string wheels, string body)
    {
        Engine = engine;
        Wheels = wheels;
        Body = body;
    }
}

// Хранитель снимков (можно сохранить несколько снимков для восстановления)
public class Caretaker
{
    private List<CarMemento> _mementos = new List<CarMemento>();

    public void SaveState(CarMemento memento)
    {
        _mementos.Add(memento);
    }

    public CarMemento GetState(int index)
    {
        return _mementos[index];
    }
}

// Тестовая программа
public class Program
{
    public static void Main()
    {
        // Строим автомобиль
        var builder = new CarBuilder();
        builder.SetEngine("V8 Engine")
               .SetWheels("18 inch wheels")
               .SetBody("Sedan");

        // Показываем детали автомобиля
        var car = builder.Build();
        Console.WriteLine("Автомобиль после первого этапа сборки:");
        car.ShowDetails();

        // Сохраняем состояние автомобиля
        var caretaker = new Caretaker();
        caretaker.SaveState(builder.Save());

        // Добавляем новые компоненты
        builder.SetBody("Sports Car");

        // Показываем обновленный автомобиль
        car = builder.Build();
        Console.WriteLine("\nАвтомобиль после добавления кузова Sports Car:");
        car.ShowDetails();

        // Восстанавливаем состояние из снимка
        builder.Restore(caretaker.GetState(0));

        // Показываем восстановленный автомобиль
        car = builder.Build();
        Console.WriteLine("\nАвтомобиль после восстановления состояния:");
        car.ShowDetails();
    }
}
```

## Пояснение к коду

1. Класс Car:

Это объект автомобиля с полями для различных компонентов (двигатель, колеса и кузов). У него есть метод ShowDetails(), который выводит информацию о текущем состоянии автомобиля.
2. Строитель CarBuilder:

Этот класс используется для поэтапного создания объекта автомобиля. Мы можем устанавливать компоненты автомобиля (двигатель, колеса, кузов) и собирать готовый объект с помощью метода Build().
Также в Строителе есть методы для создания снимка состояния (Save()) и восстановления состояния из снимка (Restore()).
3. Класс CarMemento:

Этот класс представляет собой снимок состояния автомобиля. Он сохраняет состояние всех компонентов автомобиля (двигатель, колеса, кузов).
4. Класс Caretaker:

Это класс, который хранит снимки состояния автомобиля. Он может сохранять несколько снимков и восстанавливать их по индексу.
5. Тестовая программа:

В программе мы создаем объект Car с помощью Строителя, затем сохраняем его состояние в Caretaker. После этого мы модифицируем автомобиль (изменяем кузов) и восстанавливаем его состояние с помощью Снимка.

## Прототип

## 1. Прототип + Компоновщик.

## Сценарий:

Мы создаем структуру файловой системы, где каждый файл может быть клонирован, и файлы могут быть объединены в папки. Используя Компоновщик, мы строим иерархию, а с помощью Прототипа можем клонировать как отдельные файлы, так и целые папки с их содержимым.

```csharp
using System;
using System.Collections.Generic;

// Компонент (файл или папка), который можно клонировать
public abstract class FileSystemComponent : ICloneable
{
    public string Name { get; set; }

    public FileSystemComponent(string name)
    {
        Name = name;
    }

    public abstract void ShowDetails();

    // Метод клонирования
    public abstract object Clone();
}

// Файл, который наследует от компонента
public class File : FileSystemComponent
{
    public File(string name) : base(name) { }

    public override void ShowDetails()
    {
        Console.WriteLine($"Файл: {Name}");
    }

    // Клонирование файла
    public override object Clone()
    {
        return new File(this.Name);
    }
}

// Папка, которая может содержать другие компоненты (файлы или папки)
public class Folder : FileSystemComponent
{
    private List<FileSystemComponent> _children = new List<FileSystemComponent>();

    public Folder(string name) : base(name) { }

    public void Add(FileSystemComponent component)
    {
        _children.Add(component);
    }

    public override void ShowDetails()
    {
        Console.WriteLine($"Папка: {Name}");
        foreach (var component in _children)
        {
            component.ShowDetails();
        }
    }

    // Клонирование папки (и всех вложенных компонентов)
    public override object Clone()
    {
        var clonedFolder = new Folder(this.Name);
        foreach (var child in _children)
        {
            clonedFolder.Add((FileSystemComponent)child.Clone());
        }
        return clonedFolder;
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем файлы
        var file1 = new File("file1.txt");
        var file2 = new File("file2.txt");

        // Создаем папку и добавляем в нее файлы
        var folder1 = new Folder("Documents");
        folder1.Add(file1);
        folder1.Add(file2);

        // Показываем структуру до клонирования
        Console.WriteLine("До клонирования:");
        folder1.ShowDetails();

        // Клонируем папку
        var clonedFolder = (Folder)folder1.Clone();

        // Показываем структуру после клонирования
        Console.WriteLine("\nПосле клонирования:");
        clonedFolder.ShowDetails();

        // Модифицируем клонированную папку
        clonedFolder.Add(new File("file3.txt"));

        // Показываем структуру клонированной папки после добавления нового файла
        Console.WriteLine("\nПосле добавления нового файла в клонированную папку:");
        clonedFolder.ShowDetails();
    }
}
```

## Пояснение к коду

1. Абстрактный класс FileSystemComponent:

Это абстрактный класс, который представляет файл или папку. Он включает в себя метод Clone() для клонирования и абстрактный метод ShowDetails() для вывода информации.
2. Класс File:

Класс, представляющий файл. Реализует метод Clone(), который создает копию файла.
3. Класс Folder:

Класс, представляющий папку, которая может содержать другие компоненты (файлы или папки). Метод Clone() клонирует папку вместе с содержимым (вложенными файлами и папками).
4. Программа:

В программе создается папка Documents, в которую добавляются два файла. Затем выполняется клонирование этой папки с помощью метода Clone(). После клонирования добавляется новый файл в клонированную папку.

## 2. Прототип + Декоратор.

## Сценарий:

У нас есть базовый объект "Уведомление", который может быть клонирован, и мы применяем к нему декораторы для добавления различных функций, таких как "Оповещение по электронной почте" и "Оповещение через SMS".

```csharp
using System;

// Базовый компонент, который мы будем клонировать и декорировать
public abstract class Notification : ICloneable
{
    public string Message { get; set; }

    public Notification(string message)
    {
        Message = message;
    }

    public abstract void SendNotification();
    
    // Клонирование объекта
    public abstract object Clone();
}

// Конкретный компонент (базовое уведомление)
public class BasicNotification : Notification
{
    public BasicNotification(string message) : base(message) { }

    public override void SendNotification()
    {
        Console.WriteLine($"Отправка уведомления: {Message}");
    }

    public override object Clone()
    {
        return new BasicNotification(this.Message);
    }
}

// Абстрактный декоратор
public abstract class NotificationDecorator : Notification
{
    protected Notification _notification;

    public NotificationDecorator(Notification notification) : base(notification.Message)
    {
        _notification = notification;
    }

    public override void SendNotification()
    {
        _notification.SendNotification();
    }
    
    // Клонирование с учетом декоратора
    public override object Clone()
    {
        return this.MemberwiseClone();
    }
}

// Декоратор для отправки уведомлений по электронной почте
public class EmailNotificationDecorator : NotificationDecorator
{
    public EmailNotificationDecorator(Notification notification) : base(notification) { }

    public override void SendNotification()
    {
        base.SendNotification();
        Console.WriteLine("Отправка уведомления по электронной почте.");
    }

    public override object Clone()
    {
        return new EmailNotificationDecorator((Notification)_notification.Clone());
    }
}

// Декоратор для отправки уведомлений через SMS
public class SMSNotificationDecorator : NotificationDecorator
{
    public SMSNotificationDecorator(Notification notification) : base(notification) { }

    public override void SendNotification()
    {
        base.SendNotification();
        Console.WriteLine("Отправка уведомления через SMS.");
    }

    public override object Clone()
    {
        return new SMSNotificationDecorator((Notification)_notification.Clone());
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем базовое уведомление
        Notification notification = new BasicNotification("Уведомление о новом сообщении.");

        // Применяем декоратор для отправки уведомлений по email
        Notification emailNotification = new EmailNotificationDecorator(notification);
        
        // Применяем декоратор для отправки SMS
        Notification smsNotification = new SMSNotificationDecorator(emailNotification);

        // Отправляем уведомления
        Console.WriteLine("Первоначальное уведомление:");
        notification.SendNotification();

        Console.WriteLine("\nУведомление с декоратором Email и SMS:");
        smsNotification.SendNotification();

        // Клонируем уведомление с декораторами
        Notification clonedNotification = (Notification)smsNotification.Clone();

        // Показываем, что клонированный объект работает так же
        Console.WriteLine("\nКлонированное уведомление:");
        clonedNotification.SendNotification();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Notification:

Это базовый класс для всех уведомлений, который содержит сообщение и абстрактные методы для отправки уведомлений и клонирования объекта.
2. Конкретный класс BasicNotification:

Это базовая реализация уведомления, которое просто выводит сообщение на экран, когда его отправляют.
3. Абстрактный декоратор NotificationDecorator:

Это абстрактный класс декоратора, который добавляет дополнительное поведение к уведомлениям. Он также поддерживает клонирование.
4. Декораторы EmailNotificationDecorator и SMSNotificationDecorator:

Эти декораторы добавляют новое поведение для отправки уведомлений по электронной почте и через SMS.
5. Программа:

В программе мы создаем базовое уведомление, применяем к нему два декоратора (Email и SMS) и отправляем уведомление. Затем мы клонируем объект с декораторами и показываем, что клонированный объект работает так же, как и оригинал.

## 3. Прототип + Фасад.

## Сценарий:

У нас есть система, которая управляет уведомлениями через разные каналы (например, Email и SMS). С помощью Фасада мы будем упрощать взаимодействие с этой системой, а с помощью Прототипа клонировать уведомления.

```csharp
using System;

// Базовый компонент уведомления, который может быть клонирован
public abstract class Notification : ICloneable
{
    public string Message { get; set; }

    public Notification(string message)
    {
        Message = message;
    }

    public abstract void SendNotification();
    
    // Метод клонирования
    public abstract object Clone();
}

// Конкретное уведомление, которое отправляется
public class BasicNotification : Notification
{
    public BasicNotification(string message) : base(message) { }

    public override void SendNotification()
    {
        Console.WriteLine($"Отправка уведомления: {Message}");
    }

    public override object Clone()
    {
        return new BasicNotification(this.Message);
    }
}

// Система, которая управляет отправкой уведомлений через Email и SMS
public class NotificationSystem
{
    public void SendEmail(string message)
    {
        Console.WriteLine($"Отправка уведомления по электронной почте: {message}");
    }

    public void SendSMS(string message)
    {
        Console.WriteLine($"Отправка уведомления через SMS: {message}");
    }
}

// Фасад, который упрощает взаимодействие с системой уведомлений
public class NotificationFacade
{
    private NotificationSystem _notificationSystem;

    public NotificationFacade()
    {
        _notificationSystem = new NotificationSystem();
    }

    public void SendEmailNotification(Notification notification)
    {
        _notificationSystem.SendEmail(notification.Message);
    }

    public void SendSMSNotification(Notification notification)
    {
        _notificationSystem.SendSMS(notification.Message);
    }

    // Клонирование уведомления с помощью прототипа
    public Notification CloneNotification(Notification notification)
    {
        return (Notification)notification.Clone();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем базовое уведомление
        Notification notification = new BasicNotification("Уведомление о новом сообщении.");

        // Создаем фасад для работы с уведомлениями
        NotificationFacade facade = new NotificationFacade();

        // Отправляем уведомление по email и через SMS
        Console.WriteLine("Отправка уведомлений с использованием фасада:");
        facade.SendEmailNotification(notification);
        facade.SendSMSNotification(notification);

        // Клонируем уведомление
        Notification clonedNotification = facade.CloneNotification(notification);

        // Показываем, что клонированное уведомление работает точно так же
        Console.WriteLine("\nКлонированное уведомление:");
        facade.SendEmailNotification(clonedNotification);
        facade.SendSMSNotification(clonedNotification);
    }
}

```

## Пояснение к коду

1. Абстрактный класс Notification:

Базовый класс для всех уведомлений, который содержит сообщение и абстрактные методы для отправки уведомлений и клонирования объекта.
2. Класс BasicNotification:

Конкретная реализация уведомления, которое просто выводит сообщение на экран при отправке.
3. Класс NotificationSystem:

Это класс, который содержит сложную логику для отправки уведомлений через разные каналы (Email и SMS). Он предоставляет методы SendEmail и SendSMS.
4. Класс NotificationFacade:

Это фасад, который упрощает взаимодействие с системой уведомлений. Он скрывает сложные вызовы и предоставляет упрощенные методы для отправки уведомлений по email и через SMS. Также предоставляет метод для клонирования уведомлений.
5. Программа:

В программе создается уведомление, затем используется фасад для отправки уведомлений по email и через SMS. Далее создается клон уведомления и отправляется снова с использованием фасада.

## 4. Прототип + Легковес.

## Сценарий:

Предположим, у нас есть система, которая управляет различными типами уведомлений. Эти уведомления могут быть повторно использованы, а при необходимости клонированы для отправки.

```csharp
using System;
using System.Collections.Generic;

// Абстрактный класс уведомлений, который можно клонировать
public abstract class Notification : ICloneable
{
    public string Message { get; set; }

    public Notification(string message)
    {
        Message = message;
    }

    public abstract void SendNotification();

    // Метод клонирования
    public abstract object Clone();
}

// Конкретное уведомление, которое отправляется
public class BasicNotification : Notification
{
    public BasicNotification(string message) : base(message) { }

    public override void SendNotification()
    {
        Console.WriteLine($"Отправка уведомления: {Message}");
    }

    public override object Clone()
    {
        return new BasicNotification(this.Message);
    }
}

// Класс, управляющий экземплярами уведомлений (Легковес)
public class NotificationFactory
{
    private Dictionary<string, Notification> _notificationPool = new Dictionary<string, Notification>();

    public Notification GetNotification(string message)
    {
        // Если уведомление с таким сообщением уже существует, возвращаем его
        if (!_notificationPool.ContainsKey(message))
        {
            _notificationPool[message] = new BasicNotification(message);
            Console.WriteLine($"Создано новое уведомление с сообщением: {message}");
        }
        else
        {
            Console.WriteLine($"Использовано существующее уведомление с сообщением: {message}");
        }

        return _notificationPool[message];
    }

    // Метод для клонирования уведомлений
    public Notification CloneNotification(Notification notification)
    {
        return (Notification)notification.Clone();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        NotificationFactory factory = new NotificationFactory();

        // Получаем уведомления, используя Легковес
        Notification notification1 = factory.GetNotification("Уведомление о новом сообщении.");
        Notification notification2 = factory.GetNotification("Уведомление о новом сообщении."); // Это будет использовано из пула
        Notification notification3 = factory.GetNotification("Уведомление о системной ошибке.");

        // Отправляем уведомления
        notification1.SendNotification();
        notification2.SendNotification();
        notification3.SendNotification();

        // Клонируем уведомления
        Notification clonedNotification = factory.CloneNotification(notification1);

        // Показываем, что клонированное уведомление работает так же
        Console.WriteLine("\nКлонированное уведомление:");
        clonedNotification.SendNotification();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Notification:

Это базовый класс для всех уведомлений, который содержит сообщение и абстрактные методы для отправки уведомлений и клонирования объекта.
2. Класс BasicNotification:

Это конкретная реализация уведомления, которое просто выводит сообщение на экран при отправке. Реализует метод Clone для клонирования объектов.
3. Класс NotificationFactory:

Это фабрика, которая управляет объектами уведомлений. Она реализует шаблон Легковес, позволяя повторно использовать экземпляры уведомлений с одинаковыми сообщениями. Если уведомление с таким сообщением уже существует, возвращается его копия. Если нет — создается новый объект.
Также предоставляет метод CloneNotification, который создает клон существующего уведомления с использованием Прототипа.
4.Программа:

В программе создаются несколько уведомлений с одинаковыми и различными сообщениями. После этого уведомления отправляются, и одно из них клонируется для демонстрации использования Прототипа.

## 5. Прототип + Заместитель.

## Сценарий:

Предположим, у нас есть система, в которой храним уведомления. Эти уведомления могут быть клонированы для повторного использования, а Заместитель будет контролировать доступ к реальной операции (например, отправке уведомлений), выполняя дополнительные проверки или предоставляя ленивая инициализацию объекта уведомления.

```csharp
using System;

// Абстрактный класс уведомлений, который можно клонировать
public abstract class Notification : ICloneable
{
    public string Message { get; set; }

    public Notification(string message)
    {
        Message = message;
    }

    public abstract void SendNotification();

    // Метод клонирования
    public abstract object Clone();
}

// Конкретное уведомление, которое отправляется
public class BasicNotification : Notification
{
    public BasicNotification(string message) : base(message) { }

    public override void SendNotification()
    {
        Console.WriteLine($"Отправка уведомления: {Message}");
    }

    public override object Clone()
    {
        return new BasicNotification(this.Message);
    }
}

// Заместитель уведомления, который выполняет ленивую инициализацию и контроль доступа
public class NotificationProxy : Notification
{
    private BasicNotification _realNotification;

    public NotificationProxy(string message) : base(message) { }

    public override void SendNotification()
    {
        // Ленивая инициализация
        if (_realNotification == null)
        {
            _realNotification = new BasicNotification(Message);
            Console.WriteLine("Инициализация реального уведомления...");
        }

        // Контролируем доступ к реальному объекту
        Console.WriteLine("Прокси проверяет доступ к уведомлению...");
        _realNotification.SendNotification();
    }

    public override object Clone()
    {
        // Создание клона через прокси
        return new NotificationProxy(this.Message);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем прокси для уведомления
        Notification proxyNotification = new NotificationProxy("Уведомление о новом сообщении.");

        // Отправляем уведомление через прокси
        proxyNotification.SendNotification();

        // Клонируем прокси
        Notification clonedNotification = (Notification)proxyNotification.Clone();

        // Отправляем клонированное уведомление
        Console.WriteLine("\nОтправка клонированного уведомления:");
        clonedNotification.SendNotification();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Notification:

Это базовый класс для всех уведомлений, который содержит сообщение и абстрактные методы для отправки уведомлений и клонирования объекта.
2. Класс BasicNotification:

Это конкретная реализация уведомления, которое выводит сообщение на экран при отправке. Реализует метод Clone для клонирования объектов.
3. Класс NotificationProxy:

Это заместитель, который лениво инициализирует реальное уведомление (класс BasicNotification) только при необходимости. Заместитель также контролирует доступ к реальному объекту и может выполнять дополнительные действия, такие как проверка прав доступа или логирование.
Метод Clone создает новый экземпляр прокси.
4. Программа:

В программе создается уведомление через прокси, которое будет инициализировано только при отправке уведомления. Затем это уведомление клонируется и отправляется снова.

## 6. Прототип + Стратегия.

## Сценарий:

Предположим, что у нас есть система уведомлений, и мы хотим использовать разные стратегии отправки уведомлений (например, через электронную почту или через SMS). Также мы будем клонировать объекты уведомлений для повторного использования, применяя шаблон Прототип.

```csharp
using System;

// Абстрактный класс уведомлений, который можно клонировать
public abstract class Notification : ICloneable
{
    public string Message { get; set; }
    public INotificationStrategy Strategy { get; set; }

    public Notification(string message, INotificationStrategy strategy)
    {
        Message = message;
        Strategy = strategy;
    }

    public abstract void SendNotification();

    // Метод клонирования
    public abstract object Clone();
}

// Интерфейс для стратегии отправки уведомлений
public interface INotificationStrategy
{
    void Send(string message);
}

// Конкретная стратегия отправки уведомлений через Email
public class EmailNotificationStrategy : INotificationStrategy
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка уведомления по Email: {message}");
    }
}

// Конкретная стратегия отправки уведомлений через SMS
public class SmsNotificationStrategy : INotificationStrategy
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка уведомления по SMS: {message}");
    }
}

// Конкретное уведомление, которое использует стратегию отправки
public class BasicNotification : Notification
{
    public BasicNotification(string message, INotificationStrategy strategy)
        : base(message, strategy) { }

    public override void SendNotification()
    {
        Strategy.Send(Message);
    }

    public override object Clone()
    {
        // Клонируем уведомление, но сохраняем стратегию
        return new BasicNotification(this.Message, this.Strategy);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Стратегия отправки через Email
        INotificationStrategy emailStrategy = new EmailNotificationStrategy();
        // Стратегия отправки через SMS
        INotificationStrategy smsStrategy = new SmsNotificationStrategy();

        // Создаем уведомления с разными стратегиями
        Notification emailNotification = new BasicNotification("Уведомление о новом сообщении", emailStrategy);
        Notification smsNotification = new BasicNotification("Уведомление о новом сообщении", smsStrategy);

        // Отправляем уведомления
        emailNotification.SendNotification();
        smsNotification.SendNotification();

        // Клонируем уведомления
        Notification clonedEmailNotification = (Notification)emailNotification.Clone();
        Notification clonedSmsNotification = (Notification)smsNotification.Clone();

        // Отправляем клонированные уведомления
        Console.WriteLine("\nОтправка клонированных уведомлений:");
        clonedEmailNotification.SendNotification();
        clonedSmsNotification.SendNotification();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Notification:

Этот класс описывает общие свойства и поведение для всех уведомлений. Включает ссылку на стратегию отправки уведомлений (INotificationStrategy), а также абстрактный метод SendNotification, который использует эту стратегию.
Метод Clone создает клон текущего объекта.
2. Интерфейс INotificationStrategy:

Это интерфейс для стратегии отправки уведомлений. Он определяет метод Send, который будет использоваться для отправки сообщений.
3. Конкретные стратегии:

EmailNotificationStrategy: реализует отправку уведомлений по электронной почте.
SmsNotificationStrategy: реализует отправку уведомлений по SMS.
4. Класс BasicNotification:

Это конкретная реализация уведомления, которое использует стратегию для отправки сообщений. Он может быть клонирован, при этом сохраняется выбранная стратегия.
5. Программа:

В программе создаются уведомления с разными стратегиями отправки (через email и SMS), а затем эти уведомления отправляются. После этого уведомления клонируются, и отправляются клонированные объекты.

## 7. Прототип + Шаблонный метод.

## Сценарий:

Предположим, что у нас есть система уведомлений, где процесс отправки уведомлений определяется общим шаблонным методом. Однако в зависимости от типа уведомления (например, текстовое или с изображением) могут быть дополнительные шаги, которые нужно определить в подклассе. Также мы будем использовать Прототип для клонирования уведомлений, чтобы уменьшить накладные расходы на создание новых объектов.

```csharp
using System;

// Абстрактный класс уведомлений, который можно клонировать и использующий шаблонный метод
public abstract class Notification : ICloneable
{
    public string Message { get; set; }

    public Notification(string message)
    {
        Message = message;
    }

    // Шаблонный метод
    public void SendNotification()
    {
        PrepareMessage();
        Send();
        FinalizeMessage();
    }

    // Шаблонные шаги
    protected abstract void PrepareMessage();
    protected abstract void Send();
    protected virtual void FinalizeMessage()
    {
        Console.WriteLine("Завершаем процесс отправки уведомления.");
    }

    // Метод клонирования
    public abstract object Clone();
}

// Конкретное текстовое уведомление, использующее шаблонный метод
public class TextNotification : Notification
{
    public TextNotification(string message) : base(message) { }

    // Конкретизация шагов шаблонного метода
    protected override void PrepareMessage()
    {
        Console.WriteLine($"Подготовка текстового сообщения: {Message}");
    }

    protected override void Send()
    {
        Console.WriteLine($"Отправка текстового уведомления: {Message}");
    }

    public override object Clone()
    {
        return new TextNotification(this.Message);
    }
}

// Конкретное уведомление с изображением, использующее шаблонный метод
public class ImageNotification : Notification
{
    public string ImageUrl { get; set; }

    public ImageNotification(string message, string imageUrl) : base(message)
    {
        ImageUrl = imageUrl;
    }

    // Конкретизация шагов шаблонного метода
    protected override void PrepareMessage()
    {
        Console.WriteLine($"Подготовка уведомления с изображением: {Message}, URL изображения: {ImageUrl}");
    }

    protected override void Send()
    {
        Console.WriteLine($"Отправка уведомления с изображением: {Message}, {ImageUrl}");
    }

    public override object Clone()
    {
        return new ImageNotification(this.Message, this.ImageUrl);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем текстовое уведомление
        Notification textNotification = new TextNotification("Новое текстовое уведомление!");
        // Создаем уведомление с изображением
        Notification imageNotification = new ImageNotification("Новое уведомление с изображением!", "http://example.com/image.png");

        // Отправляем уведомления
        Console.WriteLine("Отправка текстового уведомления:");
        textNotification.SendNotification();

        Console.WriteLine("\nОтправка уведомления с изображением:");
        imageNotification.SendNotification();

        // Клонируем уведомления
        Notification clonedTextNotification = (Notification)textNotification.Clone();
        Notification clonedImageNotification = (Notification)imageNotification.Clone();

        // Отправляем клонированные уведомления
        Console.WriteLine("\nОтправка клонированных уведомлений:");
        clonedTextNotification.SendNotification();
        clonedImageNotification.SendNotification();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Notification:

Этот класс описывает общие свойства и поведение для всех уведомлений.
Метод SendNotification является шаблонным методом и вызывает несколько шагов, которые могут быть переопределены в подклассах: PrepareMessage, Send, и FinalizeMessage.
2. Конкретные уведомления:

TextNotification — это уведомление, которое отправляется в виде текста. Реализует шаги шаблонного метода, такие как подготовка текста и его отправка.
ImageNotification — это уведомление, которое содержит изображение. Оно также переопределяет шаги шаблонного метода для подготовки сообщения и отправки.
3. Метод Clone:

Для каждого уведомления реализован метод клонирования, который создает новый экземпляр того же типа уведомления с таким же сообщением (и, в случае изображения, URL изображения).
4. Программа:

В программе создаются два уведомления (текстовое и с изображением), отправляются, затем эти уведомления клонируются, и клонированные уведомления отправляются снова.

## 8. Прототип + Наблюдатель.

## Сценарий:

Предположим, у нас есть объект "Система", который отслеживает какие-то изменения. Этот объект будет клонирован (используя Прототип), а все подписанные на него наблюдатели будут уведомлены об изменениях.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс Наблюдателя
public interface IObserver
{
    void Update(string message);
}

// Абстрактный класс для объекта, который можно клонировать
public abstract class Subject : ICloneable
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
    }

    // Метод клонирования
    public abstract object Clone();
}

// Конкретный класс системы, который может быть клонирован и уведомляет наблюдателей
public class SystemStatus : Subject
{
    public string Status { get; set; }

    public SystemStatus(string status)
    {
        Status = status;
    }

    // Конкретизация клонирования
    public override object Clone()
    {
        return new SystemStatus(this.Status);
    }

    // Метод для обновления статуса и уведомления наблюдателей
    public void ChangeStatus(string newStatus)
    {
        Status = newStatus;
        Console.WriteLine($"Статус изменен на: {Status}");
        NotifyObservers($"Статус изменен на: {Status}");
    }
}

// Конкретный наблюдатель, который будет получать уведомления
public class SystemObserver : IObserver
{
    public string ObserverName { get; }

    public SystemObserver(string name)
    {
        ObserverName = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{ObserverName} получил уведомление: {message}");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем объект системы и наблюдателей
        SystemStatus systemStatus = new SystemStatus("Запуск...");
        SystemObserver observer1 = new SystemObserver("Наблюдатель 1");
        SystemObserver observer2 = new SystemObserver("Наблюдатель 2");

        // Добавляем наблюдателей
        systemStatus.AddObserver(observer1);
        systemStatus.AddObserver(observer2);

        // Меняем статус системы и уведомляем наблюдателей
        systemStatus.ChangeStatus("Работает нормально");

        // Клонируем систему и наблюдателей
        SystemStatus clonedSystemStatus = (SystemStatus)systemStatus.Clone();
        SystemObserver clonedObserver1 = new SystemObserver("Клонированный наблюдатель 1");
        clonedSystemStatus.AddObserver(clonedObserver1);

        // Меняем статус в клонированной системе
        clonedSystemStatus.ChangeStatus("Ошибка!");

        // Вывод:
        // Статус изменен на: Работает нормально
        // Наблюдатель 1 получил уведомление: Статус изменен на: Работает нормально
        // Наблюдатель 2 получил уведомление: Статус изменен на: Работает нормально
        // Статус изменен на: Ошибка!
        // Клонированный наблюдатель 1 получил уведомление: Статус изменен на: Ошибка!
    }
}
```

## Пояснение к коду

1. Интерфейс IObserver:

Это интерфейс для наблюдателей, который требует реализации метода Update, принимающего сообщение об изменении.
2. Абстрактный класс Subject:

Этот класс реализует логику добавления, удаления и уведомления наблюдателей. Он также содержит абстрактный метод Clone для клонирования.
3. Конкретный класс SystemStatus:

Это класс, который представляет объект, отслеживающий какой-то статус. Он наследует от Subject и реализует метод Clone для клонирования самого себя.
Метод ChangeStatus изменяет статус и уведомляет всех подписанных наблюдателей о изменении.
4. Конкретный наблюдатель SystemObserver:

Это наблюдатель, который получает уведомления от системы о статусе. Каждый наблюдатель имеет имя, которое выводится при получении уведомлений.
5. Программа:

В программе создаются объекты SystemStatus и несколько наблюдателей. Когда статус системы изменяется, все наблюдатели получают уведомления.
Затем объект системы клонируется, и изменения статуса также отправляются новому наблюдателю.

## 9. Прототип + Состояние.

## Сценарий:

Предположим, у нас есть объект "Игровой персонаж", который может быть в разных состояниях (например, "Здоров", "Поврежден", "Мертв"). Каждый раз, когда персонаж изменяет состояние, он может быть клонирован (с помощью шаблона Прототип), и его поведение будет зависеть от текущего состояния.

```csharp
using System;

// Интерфейс состояния
public interface IState
{
    void HandleRequest(Character character);
}

// Конкретное состояние "Здоров"
public class HealthyState : IState
{
    public void HandleRequest(Character character)
    {
        Console.WriteLine("Персонаж здоров. Готов к бою.");
        // Переводим в состояние "Поврежден"
        character.SetState(new DamagedState());
    }
}

// Конкретное состояние "Поврежден"
public class DamagedState : IState
{
    public void HandleRequest(Character character)
    {
        Console.WriteLine("Персонаж поврежден. Требуется отдых.");
        // Переводим в состояние "Мертв"
        character.SetState(new DeadState());
    }
}

// Конкретное состояние "Мертв"
public class DeadState : IState
{
    public void HandleRequest(Character character)
    {
        Console.WriteLine("Персонаж мертв. Не может действовать.");
        // Игра окончена, состояние не меняется
    }
}

// Класс персонажа, который изменяет состояния и может быть клонирован
public class Character : ICloneable
{
    private IState _state;

    public Character(IState state)
    {
        _state = state;
    }

    public void SetState(IState state)
    {
        _state = state;
    }

    public void Request()
    {
        _state.HandleRequest(this);
    }

    // Метод для клонирования персонажа
    public object Clone()
    {
        return new Character(_state); // Создаем новый объект с тем же состоянием
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем персонажа с начальным состоянием "Здоров"
        Character character = new Character(new HealthyState());

        // Персонаж действует, меняет состояния
        character.Request(); // Здоров -> Поврежден
        character.Request(); // Поврежден -> Мертв

        // Клонируем персонажа
        Character clonedCharacter = (Character)character.Clone();
        Console.WriteLine("Клонированный персонаж:");

        // Изменяем состояние клонированного персонажа
        clonedCharacter.Request(); // Мертв, не может действовать

        // Вывод:
        // Персонаж здоров. Готов к бою.
        // Персонаж поврежден. Требуется отдых.
        // Персонаж мертв. Не может действовать.
        // Клонированный персонаж:
        // Персонаж мертв. Не может действовать.
    }
}
```

## Пояснение к коду

1. Интерфейс IState:

Этот интерфейс определяет метод HandleRequest, который будет вызываться для изменения состояния объекта и изменения его поведения в зависимости от текущего состояния.
2. Конкретные классы состояний:

HealthyState: Состояние персонажа, когда он здоров и готов к действию.
DamagedState: Состояние, когда персонаж поврежден и нуждается в отдыхе.
DeadState: Состояние, когда персонаж мертв и не может действовать.
3. Класс Character:

Этот класс представляет игрового персонажа, который может менять свои состояния. В нем также реализован метод клонирования (с помощью Прототипа), который создает новый объект персонажа с текущим состоянием.
4. Программа:

Мы создаем персонажа с состоянием "Здоров" и несколько раз вызываем метод Request, чтобы он менял состояния. Затем мы клонируем персонажа и выводим результат, чтобы показать, как поведение изменяется в зависимости от состояния.

## 10. Прототип + Посетитель.

## Сценарий:

Предположим, у нас есть объект "Товар", который может быть различных типов (например, "Продукт" или "Услуга"). Мы используем шаблон Посетитель для выполнения различных операций с товарами, таких как расчет цены. Также объекты могут быть клонированы с помощью Прототипа, чтобы использовать их в дальнейшем.

```csharp
using System;

// Интерфейс Посетителя
public interface IVisitor
{
    void Visit(Product product);
    void Visit(Service service);
}

// Интерфейс для объектов, которые принимают посетителей
public interface IElement
{
    void Accept(IVisitor visitor);
}

// Класс Продукта
public class Product : IElement, ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // Метод для принятия посетителя
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    // Метод клонирования
    public object Clone()
    {
        return new Product(this.Name, this.Price);
    }
}

// Класс Услуги
public class Service : IElement, ICloneable
{
    public string Name { get; set; }
    public double HourlyRate { get; set; }

    public Service(string name, double hourlyRate)
    {
        Name = name;
        HourlyRate = hourlyRate;
    }

    // Метод для принятия посетителя
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    // Метод клонирования
    public object Clone()
    {
        return new Service(this.Name, this.HourlyRate);
    }
}

// Конкретный Посетитель для расчета цены
public class PriceCalculatorVisitor : IVisitor
{
    public void Visit(Product product)
    {
        Console.WriteLine($"Цена продукта '{product.Name}': {product.Price} рублей.");
    }

    public void Visit(Service service)
    {
        Console.WriteLine($"Цена услуги '{service.Name}': {service.HourlyRate} рублей/час.");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем объекты товара и услуги
        Product product = new Product("Ноутбук", 50000);
        Service service = new Service("Консультация", 1500);

        // Создаем посетителя для расчета цены
        PriceCalculatorVisitor priceCalculator = new PriceCalculatorVisitor();

        // Применяем посетителя к объектам
        product.Accept(priceCalculator);
        service.Accept(priceCalculator);

        // Клонируем объекты
        Product clonedProduct = (Product)product.Clone();
        Service clonedService = (Service)service.Clone();

        Console.WriteLine("\nКлонированные объекты:");

        // Применяем посетителя к клонированным объектам
        clonedProduct.Accept(priceCalculator);
        clonedService.Accept(priceCalculator);

        // Вывод:
        // Цена продукта 'Ноутбук': 50000 рублей.
        // Цена услуги 'Консультация': 1500 рублей/час.
        // Клонированные объекты:
        // Цена продукта 'Ноутбук': 50000 рублей.
        // Цена услуги 'Консультация': 1500 рублей/час.
    }
}
```

## Пояснение к коду

1. Интерфейс IVisitor:

Этот интерфейс содержит методы Visit, которые принимают объекты разных типов (в данном случае, Product и Service). Это позволяет добавлять различные поведения к объектам без изменения их исходного кода.
2. Интерфейс IElement:

Все объекты, которые могут быть посещены, должны реализовать этот интерфейс, чтобы позволить посетителям применять к ним операции (метод Accept).
3. Классы Product и Service:

Эти классы представляют объекты, с которыми будет работать посетитель. Они реализуют интерфейс IElement и предоставляют метод Accept, чтобы принять посетителя.
Также реализован метод клонирования Clone для того, чтобы создавать копии объектов.
4. Конкретный посетитель PriceCalculatorVisitor:

Это класс, который выполняет конкретную операцию — расчет цены для объектов Product и Service. Каждый объект передает себя посетителю через метод Accept.
5. Программа:

В программе создаются объекты Product и Service, затем применяется посетитель для расчета их цен.
Далее объекты клонируются с помощью метода Clone, и посетитель снова применяет свои операции к клонированным объектам.

## 11. Прототип + Цепочка обязанностей.

## Сценарий:

Предположим, что у нас есть система, в которой обработчики выполняют разные действия с запросами. Также мы хотим, чтобы запросы могли быть клонированы с помощью шаблона Прототип. Например, запрос может быть обработан сначала одним обработчиком, затем другим, и если запрос не может быть обработан, он может быть клонирован и передан в новую цепочку.

```csharp
using System;

// Интерфейс Обработчика в цепочке обязанностей
public interface IHandler
{
    void SetNext(IHandler handler);
    void HandleRequest(Request request);
}

// Класс Запроса
public class Request : ICloneable
{
    public string Message { get; set; }
    public int Priority { get; set; }

    public Request(string message, int priority)
    {
        Message = message;
        Priority = priority;
    }

    // Метод клонирования
    public object Clone()
    {
        return new Request(this.Message, this.Priority);
    }
}

// Конкретный Обработчик 1
public class Handler1 : IHandler
{
    private IHandler _nextHandler;

    public void SetNext(IHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleRequest(Request request)
    {
        if (request.Priority < 5)
        {
            Console.WriteLine($"Обработчик 1 обработал запрос: {request.Message}");
        }
        else
        {
            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }
}

// Конкретный Обработчик 2
public class Handler2 : IHandler
{
    private IHandler _nextHandler;

    public void SetNext(IHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleRequest(Request request)
    {
        if (request.Priority >= 5 && request.Priority < 10)
        {
            Console.WriteLine($"Обработчик 2 обработал запрос: {request.Message}");
        }
        else
        {
            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }
}

// Конкретный Обработчик 3
public class Handler3 : IHandler
{
    private IHandler _nextHandler;

    public void SetNext(IHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleRequest(Request request)
    {
        if (request.Priority >= 10)
        {
            Console.WriteLine($"Обработчик 3 обработал запрос: {request.Message}");
        }
        else
        {
            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем запрос
        Request request = new Request("Запрос на обработку", 7);

        // Создаем обработчиков
        IHandler handler1 = new Handler1();
        IHandler handler2 = new Handler2();
        IHandler handler3 = new Handler3();

        // Устанавливаем цепочку обработчиков
        handler1.SetNext(handler2);
        handler2.SetNext(handler3);

        // Обрабатываем запрос
        handler1.HandleRequest(request);

        // Клонируем запрос
        Request clonedRequest = (Request)request.Clone();
        clonedRequest.Priority = 12; // Изменяем приоритет

        Console.WriteLine("\nОбработка клонированного запроса:");
        // Обрабатываем клонированный запрос
        handler1.HandleRequest(clonedRequest);

        // Вывод:
        // Обработчик 2 обработал запрос: Запрос на обработку
        // Обработка клонированного запроса:
        // Обработчик 3 обработал запрос: Запрос на обработку
    }
}
```

## Пояснение к коду

1. Интерфейс IHandler:

Определяет метод SetNext, который позволяет устанавливать следующий обработчик в цепочке, и метод HandleRequest, который будет обрабатывать запрос.
2. Класс Request:

Этот класс представляет запрос, который содержит сообщение и приоритет. Он реализует метод Clone, который создает копию запроса с текущими значениями.
3. Конкретные обработчики (Handler1, Handler2, Handler3):

Эти классы реализуют обработку запросов с различным приоритетом. Каждый обработчик пытается обработать запрос в зависимости от его приоритета. Если обработчик не может обработать запрос, он передает его следующему обработчику в цепочке.
4. Программа:

В программе создаются объекты Request, Handler1, Handler2, и Handler3. Запрос обрабатывается через цепочку обязанностей. Затем запрос клонируется, и приоритет клонированного запроса изменяется. Клонированный запрос обрабатывается заново с измененным приоритетом.

## 12. Прототип + Команда.

## Сценарий:

Предположим, у нас есть несколько команд, каждая из которых выполняет операцию с объектом. Мы используем шаблон Прототип для клонирования объекта перед выполнением команды. Например, перед тем как применить команду, объект может быть клонирован для тестирования или отката изменений.

```csharp
using System;

// Интерфейс Команды
public interface ICommand
{
    void Execute();
}

// Класс Прототипа, который реализует клонирование
public class Product : ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // Метод клонирования
    public object Clone()
    {
        return new Product(this.Name, this.Price);
    }
}

// Конкретная Команда для изменения цены продукта
public class ChangePriceCommand : ICommand
{
    private Product _product;
    private double _newPrice;

    public ChangePriceCommand(Product product, double newPrice)
    {
        _product = product;
        _newPrice = newPrice;
    }

    public void Execute()
    {
        Console.WriteLine($"Цена продукта '{_product.Name}' изменена с {_product.Price} на {_newPrice}");
        _product.Price = _newPrice;
    }
}

// Конкретная Команда для клонирования продукта
public class CloneProductCommand : ICommand
{
    private Product _product;

    public CloneProductCommand(Product product)
    {
        _product = product;
    }

    public void Execute()
    {
        Product clonedProduct = (Product)_product.Clone();
        Console.WriteLine($"Продукт клонирован. Новый продукт: {clonedProduct.Name}, Цена: {clonedProduct.Price}");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем продукт
        Product product = new Product("Ноутбук", 50000);

        // Создаем команды
        ICommand changePriceCommand = new ChangePriceCommand(product, 45000);
        ICommand cloneProductCommand = new CloneProductCommand(product);

        // Выполняем команды
        changePriceCommand.Execute();  // Изменяем цену
        cloneProductCommand.Execute(); // Клонируем продукт

        // Вывод:
        // Цена продукта 'Ноутбук' изменена с 50000 на 45000
        // Продукт клонирован. Новый продукт: Ноутбук, Цена: 45000
    }
}
```

## Пояснение к коду

1. Интерфейс ICommand:

Это общий интерфейс для всех команд. Каждая команда реализует метод Execute, который выполняет необходимую операцию.
2. Класс Product:

Этот класс представляет продукт с полями Name и Price. Также реализуется метод Clone, который позволяет клонировать объект.
3. Конкретные команды (ChangePriceCommand и CloneProductCommand):

ChangePriceCommand изменяет цену продукта.
CloneProductCommand клонирует продукт и выводит информацию о новом клонированном объекте.
4. Программа:

Создаются объекты продукта и команды. Затем команды выполняются, сначала изменяется цена, затем продукт клонируется. Обе операции выводят информацию о своих действиях.

## 13. Прототип + Посредник.

## Сценарий:

Предположим, у нас есть несколько объектов, которые взаимодействуют друг с другом, и нам нужно, чтобы эти объекты могли быть клонированы. Посредник будет координировать их взаимодействие, а объекты будут клонироваться с помощью шаблона Прототип перед передачей посреднику.

```csharp
using System;

// Интерфейс для посредника
public interface IMediator
{
    void SendMessage(string message, Colleague colleague);
}

// Абстрактный класс для коллег
public abstract class Colleague
{
    protected IMediator _mediator;

    public Colleague(IMediator mediator)
    {
        _mediator = mediator;
    }

    public abstract void ReceiveMessage(string message);
    public abstract void SendMessage(string message);
}

// Класс Прототипа, который будет клонироваться
public class Product : ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // Метод клонирования
    public object Clone()
    {
        return new Product(this.Name, this.Price);
    }
}

// Конкретный коллега 1
public class ProductColleague : Colleague
{
    private Product _product;

    public ProductColleague(IMediator mediator, Product product) : base(mediator)
    {
        _product = product;
    }

    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"Продукт {this._product.Name} получил сообщение: {message}");
    }

    public override void SendMessage(string message)
    {
        Console.WriteLine($"Продукт {this._product.Name} отправляет сообщение: {message}");
        _mediator.SendMessage(message, this);
    }

    // Метод клонирования объекта
    public Product CloneProduct()
    {
        return (Product)_product.Clone();
    }
}

// Конкретный посредник
public class ProductMediator : IMediator
{
    private ProductColleague _productColleague1;
    private ProductColleague _productColleague2;

    public void RegisterColleague(ProductColleague colleague1, ProductColleague colleague2)
    {
        _productColleague1 = colleague1;
        _productColleague2 = colleague2;
    }

    public void SendMessage(string message, Colleague colleague)
    {
        if (colleague == _productColleague1)
        {
            _productColleague2.ReceiveMessage(message);
        }
        else
        {
            _productColleague1.ReceiveMessage(message);
        }
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем объект продукта
        Product product1 = new Product("Телефон", 20000);
        Product product2 = new Product("Ноутбук", 50000);

        // Создаем посредника
        ProductMediator mediator = new ProductMediator();

        // Создаем коллег (продукты)
        ProductColleague colleague1 = new ProductColleague(mediator, product1);
        ProductColleague colleague2 = new ProductColleague(mediator, product2);

        // Регистрируем коллег в посреднике
        mediator.RegisterColleague(colleague1, colleague2);

        // Отправляем сообщения
        colleague1.SendMessage("Привет от телефона!");
        colleague2.SendMessage("Привет от ноутбука!");

        // Клонируем продукт
        Product clonedProduct = colleague1.CloneProduct();
        Console.WriteLine($"Продукт клонирован: {clonedProduct.Name}, Цена: {clonedProduct.Price}");

        // Вывод:
        // Продукт Телефон отправляет сообщение: Привет от телефона!
        // Продукт Ноутбук получает сообщение: Привет от телефона!
        // Продукт Ноутбук отправляет сообщение: Привет от ноутбука!
        // Продукт Телефон получает сообщение: Привет от ноутбука!
        // Продукт клонирован: Телефон, Цена: 20000
    }
}
```

## Пояснение к коду

1. Интерфейс IMediator:

Это интерфейс для посредника, который координирует взаимодействие между объектами (коллегами).
2. Абстрактный класс Colleague:

Это базовый класс для коллег, который содержит ссылку на посредника и методы для отправки и получения сообщений.
3. Класс Product:

Это класс продукта с полями Name и Price. Он реализует метод Clone, который позволяет клонировать объект.
4. Конкретный коллега ProductColleague:

Этот класс реализует отправку и получение сообщений. Также добавлен метод CloneProduct, который клонирует объект продукта.
5. Конкретный посредник ProductMediator:

Посредник координирует взаимодействие между коллегами. Он отправляет сообщения от одного коллеги другому.
6. Программа:

В программе создаются объекты продуктов и посредника. Продукты могут отправлять друг другу сообщения через посредника. Также продукты могут быть клонированы с помощью шаблона Прототип.

## 14. Прототип + Снимок.

## Сценарий:

Предположим, у нас есть объект, который изменяет своё состояние. Мы будем использовать Прототип для клонирования объекта и Снимок для сохранения его состояния. Позже объект сможет восстановить состояние с помощью клонирования.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс для снимка
public interface ISnapshot
{
    object GetState();
    void SetState(object state);
}

// Класс Прототипа, который будет клонироваться
public class Product : ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // Метод клонирования
    public object Clone()
    {
        return new Product(this.Name, this.Price);
    }

    // Метод для отображения состояния
    public void DisplayState()
    {
        Console.WriteLine($"Продукт: {Name}, Цена: {Price}");
    }
}

// Класс Снимка для хранения состояния объекта
public class ProductSnapshot : ISnapshot
{
    private Product _productState;

    public ProductSnapshot(Product product)
    {
        _productState = (Product)product.Clone(); // Сохраняем состояние объекта через клонирование
    }

    public object GetState()
    {
        return _productState;
    }

    public void SetState(object state)
    {
        _productState = (Product)state;
    }
}

// Класс для управления объектом и его состоянием
public class ProductStateManager
{
    private Stack<ISnapshot> _snapshots = new Stack<ISnapshot>();
    private Product _currentProduct;

    public ProductStateManager(Product product)
    {
        _currentProduct = product;
    }

    public void ChangeProductState(string name, double price)
    {
        // Сохраняем снимок текущего состояния перед изменением
        _snapshots.Push(new ProductSnapshot(_currentProduct));
        _currentProduct.Name = name;
        _currentProduct.Price = price;
        Console.WriteLine("Изменено состояние продукта:");
        _currentProduct.DisplayState();
    }

    public void Undo()
    {
        if (_snapshots.Count > 0)
        {
            ProductSnapshot snapshot = (ProductSnapshot)_snapshots.Pop();
            _currentProduct = (Product)snapshot.GetState(); // Восстанавливаем состояние
            Console.WriteLine("Состояние продукта восстановлено:");
            _currentProduct.DisplayState();
        }
        else
        {
            Console.WriteLine("Нет состояния для восстановления.");
        }
    }

    public void DisplayCurrentState()
    {
        Console.WriteLine("Текущее состояние продукта:");
        _currentProduct.DisplayState();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем начальный объект продукта
        Product product = new Product("Телефон", 20000);
        ProductStateManager stateManager = new ProductStateManager(product);

        // Отображаем текущее состояние
        stateManager.DisplayCurrentState();

        // Изменяем состояние и сохраняем снимок
        stateManager.ChangeProductState("Ноутбук", 50000);

        // Изменяем состояние и сохраняем снимок
        stateManager.ChangeProductState("Планшет", 30000);

        // Отменяем изменения (восстанавливаем состояние)
        stateManager.Undo();

        // Отменяем изменения (восстанавливаем состояние)
        stateManager.Undo();

        // Вывод:
        // Текущее состояние продукта:
        // Продукт: Телефон, Цена: 20000
        // Изменено состояние продукта:
        // Продукт: Ноутбук, Цена: 50000
        // Изменено состояние продукта:
        // Продукт: Планшет, Цена: 30000
        // Состояние продукта восстановлено:
        // Продукт: Ноутбук, Цена: 50000
        // Состояние продукта восстановлено:
        // Продукт: Телефон, Цена: 20000
    }
}
```

## Пояснение к коду

1. Интерфейс ISnapshot:

Этот интерфейс представляет снимок состояния объекта. Он имеет два метода: GetState для получения состояния и SetState для восстановления состояния.
2. Класс Product:

Класс продукта с полями Name и Price, а также методами для отображения состояния и клонирования объекта (метод Clone).
3. Класс ProductSnapshot:

Этот класс реализует интерфейс ISnapshot. Он сохраняет состояние объекта Product и предоставляет методы для получения и восстановления состояния.
4. Класс ProductStateManager:

Этот класс управляет состоянием объекта продукта. Он может изменять состояние объекта, сохранять его снимки и восстанавливать предыдущее состояние с помощью снимков.
5. Программа:

В программе создается объект Product, который управляется через ProductStateManager. Состояние продукта изменяется несколько раз, и каждое изменение сохраняется в снимок. Мы также можем отменить изменения и восстановить состояние из снимков.

## Компоновщик

## 1. Компоновщик + Одиночка.

## Сценарий:

Предположим, что у нас есть система управления папками и файлами. Папки могут содержать другие папки или файлы. Мы будем использовать Компоновщик для представления этих объектов, а Одиночку — для управления экземпляром 

```csharp
using System;
using System.Collections.Generic;

// Интерфейс компонента для Компоновщика
public interface IComponent
{
    void Display();
}

// Класс Файл
public class File : IComponent
{
    public string Name { get; set; }

    public File(string name)
    {
        Name = name;
    }

    public void Display()
    {
        Console.WriteLine($"Файл: {Name}");
    }
}

// Класс Папка (Компоновщик)
public class Folder : IComponent
{
    public string Name { get; set; }
    private List<IComponent> _components = new List<IComponent>();

    public Folder(string name)
    {
        Name = name;
    }

    public void AddComponent(IComponent component)
    {
        _components.Add(component);
    }

    public void Display()
    {
        Console.WriteLine($"Папка: {Name}");
        foreach (var component in _components)
        {
            component.Display();
        }
    }
}

// Класс Одиночки для управления всеми папками и файлами
public class FileManager
{
    private static FileManager _instance;

    private FileManager() { }

    public static FileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FileManager();
            }
            return _instance;
        }
    }

    public void ManageFileSystem(IComponent component)
    {
        component.Display();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создание папок и файлов
        var file1 = new File("file1.txt");
        var file2 = new File("file2.txt");
        var file3 = new File("file3.txt");

        var folder1 = new Folder("Folder1");
        folder1.AddComponent(file1);
        folder1.AddComponent(file2);

        var folder2 = new Folder("Folder2");
        folder2.AddComponent(file3);

        var rootFolder = new Folder("Root");
        rootFolder.AddComponent(folder1);
        rootFolder.AddComponent(folder2);

        // Использование одиночки для управления файловой системой
        FileManager fileManager = FileManager.Instance;
        fileManager.ManageFileSystem(rootFolder);
    }
}
```

## Пояснение к коду

1. Интерфейс IComponent:
Этот интерфейс определяет метод Display, который будет использоваться для отображения файлов и папок.

2. Класс File:
Реализует интерфейс IComponent и представляет файл с именем. Метод Display выводит имя файла.

3. Класс Folder:
Это Компоновщик, который может содержать другие компоненты (папки или файлы). Он реализует метод Display, который выводит имя папки и вызывает метод Display для каждого дочернего компонента.

4. Класс FileManager:
Это Одиночка, который управляет файловой системой. Мы используем свойство Instance для получения единственного экземпляра этого класса. Метод ManageFileSystem используется для отображения иерархии папок и файлов.

5. Программа:
В Main создаются папки и файлы. Используя Одиночку, мы получаем доступ к единственному экземпляру FileManager и вызываем метод для отображения всей структуры файлов и папок.

## 2. Компоновщик + Декоратор.

## Сценарий:

Предположим, у нас есть базовый объект "Кофе". Мы будем использовать Прототип для клонирования объекта "Кофе" с разными вариантами добавок (например, молоко и сахар), добавляя их с помощью Декоратора.

```csharp
using System;

// Интерфейс напитка (Базовый компонент)
public interface ICoffee
{
    string GetDescription();
    double Cost();
    ICoffee Clone();  // Метод для клонирования объекта
}

// Конкретный напиток (Кофе)
public class Coffee : ICoffee
{
    public string GetDescription()
    {
        return "Кофе";
    }

    public double Cost()
    {
        return 5.0;  // Стоимость базового кофе
    }

    public ICoffee Clone()
    {
        return (ICoffee)this.MemberwiseClone();  // Клонирование текущего объекта
    }
}

// Декоратор — добавляет молоко
public class MilkDecorator : ICoffee
{
    private ICoffee _coffee;

    public MilkDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public string GetDescription()
    {
        return _coffee.GetDescription() + ", с молоком";
    }

    public double Cost()
    {
        return _coffee.Cost() + 1.0;  // Молоко добавляет 1.0 к стоимости
    }

    public ICoffee Clone()
    {
        return (ICoffee)this.MemberwiseClone();  // Клонирование текущего объекта
    }
}

// Декоратор — добавляет сахар
public class SugarDecorator : ICoffee
{
    private ICoffee _coffee;

    public SugarDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public string GetDescription()
    {
        return _coffee.GetDescription() + ", с сахаром";
    }

    public double Cost()
    {
        return _coffee.Cost() + 0.5;  // Сахар добавляет 0.5 к стоимости
    }

    public ICoffee Clone()
    {
        return (ICoffee)this.MemberwiseClone();  // Клонирование текущего объекта
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создание базового кофе
        ICoffee coffee1 = new Coffee();

        // Декорирование кофе с молоком и сахаром
        ICoffee coffeeWithMilk = new MilkDecorator(coffee1);
        ICoffee coffeeWithMilkAndSugar = new SugarDecorator(coffeeWithMilk);

        // Клонирование кофе с добавками
        ICoffee clonedCoffee = coffeeWithMilkAndSugar.Clone();

        // Отображение описания и стоимости
        Console.WriteLine($"Кофе с молоком и сахаром: {coffeeWithMilkAndSugar.GetDescription()}");
        Console.WriteLine($"Стоимость: {coffeeWithMilkAndSugar.Cost()}");

        Console.WriteLine($"Клонированный кофе: {clonedCoffee.GetDescription()}");
        Console.WriteLine($"Стоимость клонированного кофе: {clonedCoffee.Cost()}");
    }
}
```

## Пояснение к коду

1. Интерфейс ICoffee:
Это базовый интерфейс для напитков, который включает методы для получения описания, стоимости и клонирования объекта.

2. Класс Coffee:
Реализует интерфейс ICoffee и представляет базовый объект кофе. В методе Clone мы используем MemberwiseClone, чтобы создать копию объекта.

3. Декораторы (MilkDecorator и SugarDecorator):
Эти классы добавляют функциональность (молоко и сахар) к базовому объекту кофе. Они реализуют методы интерфейса ICoffee и добавляют дополнительное описание и стоимость.

4. Программа:
В методе Main мы создаём объект Coffee, добавляем к нему молоко и сахар с помощью декораторов, затем клонируем этот объект и выводим описание и стоимость как оригинального, так и клонированного объекта.

## 3. Компоновщик + Мост.

## Сценарий:

Предположим, у нас есть система, которая управляет различными типами форм (например, круг, квадрат). Каждый тип формы может быть окрашен в разные цвета. Шаблон Мост поможет разделить абстракцию (форма) и реализацию (цвет), а Прототип позволит клонировать объекты.

```csharp
using System;

// Интерфейс для реализации (Цвет)
public interface IColor
{
    void ApplyColor();
}

// Конкретные реализации (Цвета)
public class RedColor : IColor
{
    public void ApplyColor()
    {
        Console.WriteLine("Применяется красный цвет.");
    }
}

public class BlueColor : IColor
{
    public void ApplyColor()
    {
        Console.WriteLine("Применяется синий цвет.");
    }
}

// Абстракция (Форма)
public abstract class Shape
{
    protected IColor _color;

    protected Shape(IColor color)
    {
        _color = color;
    }

    public abstract void Draw(); // Абстрактный метод для рисования
    public abstract Shape Clone(); // Метод для клонирования
}

// Конкретная абстракция (Круг)
public class Circle : Shape
{
    public Circle(IColor color) : base(color) { }

    public override void Draw()
    {
        Console.WriteLine("Рисуется круг.");
        _color.ApplyColor();
    }

    public override Shape Clone()
    {
        return (Shape)this.MemberwiseClone(); // Клонирование объекта
    }
}

// Конкретная абстракция (Квадрат)
public class Square : Shape
{
    public Square(IColor color) : base(color) { }

    public override void Draw()
    {
        Console.WriteLine("Рисуется квадрат.");
        _color.ApplyColor();
    }

    public override Shape Clone()
    {
        return (Shape)this.MemberwiseClone(); // Клонирование объекта
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создание объектов форм с различными цветами
        IColor red = new RedColor();
        IColor blue = new BlueColor();

        Shape circle = new Circle(red);
        Shape square = new Square(blue);

        // Рисуем формы
        circle.Draw();
        square.Draw();

        // Клонируем формы
        Shape clonedCircle = circle.Clone();
        Shape clonedSquare = square.Clone();

        // Рисуем клонированные формы
        Console.WriteLine("\nКлонированные формы:");
        clonedCircle.Draw();
        clonedSquare.Draw();
    }
}
```

## Пояснение к коду

1. Интерфейс IColor:
Определяет метод ApplyColor, который будет использоваться для применения цвета к форме.

2. Конкретные реализации RedColor и BlueColor:
Реализуют интерфейс IColor и определяют, как применяется красный и синий цвет.

3. Абстракция Shape:
Абстрактный класс, который принимает объект IColor через конструктор. Он определяет абстрактный метод Draw для рисования формы и метод Clone, который используется для клонирования объектов.

4. Конкретные абстракции Circle и Square:
Эти классы представляют конкретные формы (круг и квадрат), которые используют метод Draw для отображения формы и применяют цвет через объект IColor. Также они реализуют метод Clone, позволяя клонировать объекты.

5. Программа:
В методе Main создаются формы с разными цветами и рисуются. Затем создаются их клоны с помощью метода Clone, и отображается результат рисования клонированных объектов.

## 4. Компоновщик + Легковес.

## Сценарий:

Предположим, у нас есть система, которая управляет игровыми персонажами в ролевой игре. Каждый персонаж может иметь общие характеристики, такие как класс и оружие, которые можно разделить среди нескольких персонажей, используя шаблон Легковес. При этом, мы будем использовать шаблон Прототип для клонирования персонажей с уникальными данными, такими как имя.

```csharp
using System;
using System.Collections.Generic;

// Легковес (Общие данные для персонажей)
public class CharacterType
{
    public string ClassName { get; }
    public string Weapon { get; }

    public CharacterType(string className, string weapon)
    {
        ClassName = className;
        Weapon = weapon;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Класс: {ClassName}, Оружие: {Weapon}");
    }
}

// Прототип (Персонаж)
public class Character : ICloneable
{
    private string _name;
    private CharacterType _characterType;  // Ссылка на общие данные

    public Character(string name, CharacterType characterType)
    {
        _name = name;
        _characterType = characterType;
    }

    public void ShowCharacterInfo()
    {
        Console.WriteLine($"Персонаж: {_name}");
        _characterType.ShowDetails();
    }

    public object Clone()
    {
        // Клонируем только уникальные данные (имя персонажа)
        return new Character(_name, _characterType);
    }
}

// Фабрика для создания и клонирования персонажей
public class CharacterFactory
{
    private Dictionary<string, CharacterType> _characterTypes = new Dictionary<string, CharacterType>();

    // Метод для получения или создания типа персонажа
    public CharacterType GetCharacterType(string className, string weapon)
    {
        string key = className + weapon;
        if (!_characterTypes.ContainsKey(key))
        {
            _characterTypes[key] = new CharacterType(className, weapon);
            Console.WriteLine("Создан новый тип персонажа.");
        }
        else
        {
            Console.WriteLine("Используется существующий тип персонажа.");
        }
        return _characterTypes[key];
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        CharacterFactory factory = new CharacterFactory();

        // Получаем общие данные для персонажей
        CharacterType warriorType = factory.GetCharacterType("Воин", "Меч");
        CharacterType mageType = factory.GetCharacterType("Маг", "Посох");

        // Создаем персонажей с использованием прототипа
        Character character1 = new Character("Артур", warriorType);
        Character character2 = (Character)character1.Clone();  // Клонирование персонажа

        // Изменяем имя для клонированного персонажа
        character2.ShowCharacterInfo(); // Персонаж: Артур

        Character character3 = new Character("Гендальф", mageType);
        Character character4 = (Character)character3.Clone();  // Клонирование персонажа
        character4.ShowCharacterInfo(); // Персонаж: Гендальф

        // Повторное создание того же типа персонажа
        CharacterType warriorType2 = factory.GetCharacterType("Воин", "Меч");

        Console.WriteLine("\nПерсонажи:");
        character1.ShowCharacterInfo(); // Персонаж: Артур
        character2.ShowCharacterInfo(); // Персонаж: Артур
        character3.ShowCharacterInfo(); // Персонаж: Гендальф
        character4.ShowCharacterInfo(); // Персонаж: Гендальф
    }
}
```

## Пояснение к коду

1. Класс CharacterType (Легковес):
Содержит общие данные для персонажей, такие как класс и оружие. Эти данные могут быть разделены между всеми персонажами с одинаковыми характеристиками.

2. Класс Character (Прототип):
Каждый объект Character имеет уникальные данные, такие как имя, но использует общий тип (CharacterType) для хранения общих характеристик. Метод Clone клонирует объект, создавая новый объект с теми же общими характеристиками.

3. Класс CharacterFactory:
Хранит и управляет общими типами персонажей, обеспечивая, чтобы одинаковые типы персонажей создавались только один раз. Если тип уже существует, он возвращает существующий, иначе создаёт новый.

4. Программа:
В методе Main создаются персонажи с использованием фабрики типов и клонируются с помощью метода Clone из шаблона Прототип. Также показывается, что одинаковые типы персонажей (например, "Воин с Мечом") создаются один раз, что демонстрирует принцип Легковеса.

## 5. Компоновщик + Заместитель.

## Сценарий:

Предположим, у нас есть система для управления персонажами в игре. Каждый персонаж может быть клонирован с помощью Прототипа, а для контроля доступа к реальному объекту персонажа используется Заместитель. Заместитель может добавлять дополнительную логику, например, логировать действия перед или после вызова методов персонажа.

```csharp
using System;

// Прототип — Персонаж
public class Character : ICloneable
{
    private string _name;
    private string _role;

    public Character(string name, string role)
    {
        _name = name;
        _role = role;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Персонаж: {_name}, Роль: {_role}");
    }

    // Клонирование персонажа
    public object Clone()
    {
        return new Character(_name, _role);
    }
}

// Заместитель — для управления доступом к персонажу
public class CharacterProxy : Character
{
    private Character _realCharacter;

    public CharacterProxy(string name, string role) : base(name, role) { }

    public void LazyLoad()
    {
        // Ленивая инициализация реального персонажа
        if (_realCharacter == null)
        {
            _realCharacter = new Character(base.GetType().Name, base.GetType().Name); 
        }
    }

    // Переопределение метода ShowInfo с добавлением дополнительной логики
    public new void ShowInfo()
    {
        // Прежде чем показать информацию, можно выполнить дополнительную логику
        Console.WriteLine("Доступ к персонажу через заместителя.");
        LazyLoad();
        _realCharacter.ShowInfo();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем персонажа
        Character character1 = new Character("Артур", "Воин");
        
        // Клонируем персонажа
        Character character2 = (Character)character1.Clone();
        character2.ShowInfo();  // Персонаж: Артур, Роль: Воин

        // Создаем заместителя
        CharacterProxy proxy1 = new CharacterProxy("Гендальф", "Маг");
        proxy1.ShowInfo();  // Доступ к персонажу через заместителя. Персонаж: Гендальф, Роль: Маг

        // Клонируем персонажа с заместителем
        CharacterProxy proxy2 = (CharacterProxy)proxy1.Clone();
        proxy2.ShowInfo();  // Доступ к персонажу через заместителя. Персонаж: Гендальф, Роль: Маг
    }
}
```

## Пояснение к коду

1. Класс Character (Прототип):
Представляет собой персонажа с уникальными данными, такими как имя и роль. Этот класс реализует интерфейс ICloneable для клонирования объектов. Метод Clone позволяет создавать копии персонажа.

2. Класс CharacterProxy (Заместитель):
Используется для управления доступом к реальному объекту Character. В этом классе реализована дополнительная логика, такая как ленивая инициализация (LazyLoad) реального объекта персонажа, а также добавление дополнительной логики до или после вызова метода реального объекта. Когда мы вызываем метод ShowInfo у прокси-объекта, он сначала загружает реального персонажа, если это необходимо, и затем вызывает его метод.

3. Программа:
В методе Main мы создаем персонажей, клонируем их и создаем объекты-прокси, которые управляют доступом к реальному объекту Character. Прокси-объект демонстрирует, как можно добавить дополнительную логику (например, ленивая инициализация или логирование) при работе с реальным объектом.

## 6. Компоновщик + Стратегия.

## Сценарий:

Предположим, у нас есть система для управления персонажами в игре, и персонажи могут атаковать разными способами. Мы используем Стратегию для выбора способа атаки, а Прототип для клонирования персонажей с разными стратегиями атаки.

```csharp
using System;

// Интерфейс стратегии атаки
public interface IAttackStrategy
{
    void Attack();
}

// Конкретная стратегия атаки — Меч
public class SwordAttack : IAttackStrategy
{
    public void Attack()
    {
        Console.WriteLine("Атака мечом!");
    }
}

// Конкретная стратегия атаки — Лук
public class BowAttack : IAttackStrategy
{
    public void Attack()
    {
        Console.WriteLine("Атака луком!");
    }
}

// Персонаж — использует стратегию атаки
public class Character : ICloneable
{
    private string _name;
    private IAttackStrategy _attackStrategy;

    public Character(string name, IAttackStrategy attackStrategy)
    {
        _name = name;
        _attackStrategy = attackStrategy;
    }

    public void PerformAttack()
    {
        Console.WriteLine($"{_name} выполняет атаку:");
        _attackStrategy.Attack();
    }

    // Клонирование персонажа
    public object Clone()
    {
        // Создаем новый персонаж с текущей стратегией
        return new Character(_name, _attackStrategy);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем персонажей с разными стратегиями атаки
        Character warriorWithSword = new Character("Рыцарь", new SwordAttack());
        Character archerWithBow = new Character("Лучник", new BowAttack());

        // Персонажи выполняют атаки
        warriorWithSword.PerformAttack();  // Рыцарь выполняет атаку: Атака мечом!
        archerWithBow.PerformAttack();  // Лучник выполняет атаку: Атака луком!

        // Клонируем персонажей
        Character clonedWarrior = (Character)warriorWithSword.Clone();
        Character clonedArcher = (Character)archerWithBow.Clone();

        // Клонированные персонажи выполняют атаки
        clonedWarrior.PerformAttack();  // Рыцарь выполняет атаку: Атака мечом!
        clonedArcher.PerformAttack();  // Лучник выполняет атаку: Атака луком!
    }
}
```

## Пояснение к коду

1. Интерфейс IAttackStrategy:
Определяет метод Attack, который должен реализовать любой класс стратегии. Стратегии будут реализовывать разные способы атаки (например, мечом или луком).

2. Конкретные стратегии атаки (SwordAttack, BowAttack):
Эти классы реализуют интерфейс IAttackStrategy и предоставляют конкретную реализацию атаки (мечом и луком соответственно).

3. Класс Character:
Этот класс представляет персонажа с именем и стратегией атаки. Персонаж может выполнить атаку, вызвав метод PerformAttack, который использует стратегию, переданную в конструкторе. Класс также реализует метод Clone, позволяющий клонировать персонажа с его текущей стратегией.

4. Программа:
В методе Main создаются два персонажа с разными стратегиями атаки (мечом и луком), каждый выполняет свою атаку. Затем эти персонажи клонируются, и их клонированные экземпляры также выполняют те же самые атаки.

## 7. Компоновщик + Шаблонный метод.

## Сценарий:

Предположим, у нас есть система для создания различных типов напитков. Шаблонный метод определяет общий алгоритм приготовления напитка, а конкретные шаги, такие как добавление сахара или молока, могут быть изменены в подклассах. Мы используем Прототип для клонирования рецептов напитков.

```csharp
using System;

// Абстрактный класс с шаблонным методом
public abstract class Beverage
{
    // Шаблонный метод, который определяет общий алгоритм
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }

    // Конкретные шаги, которые не изменяются
    private void BoilWater()
    {
        Console.WriteLine("Кипятим воду");
    }

    private void PourInCup()
    {
        Console.WriteLine("Наливаем напиток в чашку");
    }

    // Абстрактные шаги, которые будут изменяться в подклассах
    protected abstract void Brew();
    protected abstract void AddCondiments();

    // Клонирование рецепта
    public object Clone()
    {
        return MemberwiseClone();  // Клонирование объекта
    }
}

// Конкретный класс для приготовления чая
public class Tea : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Завариваем чай");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавляем лимон");
    }
}

// Конкретный класс для приготовления кофе
public class Coffee : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Завариваем кофе");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавляем сахар и молоко");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем объекты напитков
        Beverage tea = new Tea();
        Beverage coffee = new Coffee();

        // Готовим напитки
        Console.WriteLine("Приготовление чая:");
        tea.PrepareRecipe();
        Console.WriteLine();

        Console.WriteLine("Приготовление кофе:");
        coffee.PrepareRecipe();
        Console.WriteLine();

        // Клонируем рецепты
        Beverage clonedTea = (Beverage)tea.Clone();
        Beverage clonedCoffee = (Beverage)coffee.Clone();

        // Готовим клонированные напитки
        Console.WriteLine("Приготовление клонированного чая:");
        clonedTea.PrepareRecipe();
        Console.WriteLine();

        Console.WriteLine("Приготовление клонированного кофе:");
        clonedCoffee.PrepareRecipe();
    }
}
```

## Пояснение к коду

1. Абстрактный класс Beverage (Шаблонный метод):
Это абстрактный класс, который содержит шаблонный метод PrepareRecipe. Шаблонный метод определяет общий алгоритм приготовления напитка: сначала кипятится вода, затем заваривается напиток, наливается в чашку и добавляются специи или добавки. Конкретные шаги, такие как заварывание и добавление добавок, реализуются в подклассах.

2. Классы Tea и Coffee (Конкретные шаги):
Эти классы реализуют конкретные шаги для приготовления чая и кофе. Каждый класс определяет, как заваривать напиток и какие добавки использовать (лимон для чая, сахар и молоко для кофе).

3. Метод Clone:
В абстрактном классе Beverage реализован метод Clone, который использует MemberwiseClone для создания копии объекта. Это позволяет клонировать рецепты напитков.

4. Программа:
В методе Main создаются объекты для чая и кофе, и они готовятся с помощью шаблонного метода. Также создаются клонированные версии этих объектов и готовятся заново.

## 8. Компоновщик + Наблюдатель.

## Сценарий:

Предположим, у нас есть система для управления пользователями. Каждый пользователь может подписаться на получение уведомлений об изменениях в аккаунте. Когда изменяется информация о пользователе (например, баланс), все подписчики получают уведомления. Мы будем использовать Прототип для клонирования пользователей и Наблюдатель для уведомлений.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс наблюдателя
public interface IObserver
{
    void Update(string message);
}

// Интерфейс субъекта
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

// Класс пользователя (с использованием прототипа)
public class User : ISubject, ICloneable
{
    private string _name;
    private decimal _balance;
    private List<IObserver> _observers = new List<IObserver>();

    public User(string name, decimal balance)
    {
        _name = name;
        _balance = balance;
    }

    public string Name => _name;
    public decimal Balance => _balance;

    public void ChangeBalance(decimal newBalance)
    {
        _balance = newBalance;
        Notify();  // Уведомляем всех наблюдателей об изменении
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update($"Баланс пользователя {_name} изменен на {_balance}");
        }
    }

    // Метод клонирования пользователя
    public object Clone()
    {
        return new User(_name, _balance);
    }
}

// Конкретный наблюдатель
public class EmailNotifier : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем пользователя
        User user1 = new User("Иван", 1000);
        
        // Создаем наблюдателей
        IObserver emailNotifier = new EmailNotifier();
        
        // Подписываем наблюдателя на изменения
        user1.Attach(emailNotifier);

        // Изменяем баланс пользователя и уведомляем наблюдателей
        user1.ChangeBalance(1500);
        Console.WriteLine();

        // Клонируем пользователя
        User clonedUser = (User)user1.Clone();
        
        // Подписываем клонированного пользователя на уведомления
        clonedUser.Attach(emailNotifier);

        // Изменяем баланс клонированного пользователя и уведомляем
        clonedUser.ChangeBalance(2000);
    }
}
```

## Пояснение к коду

1. Интерфейс IObserver:
Этот интерфейс определяет метод Update, который будет вызываться, когда изменяется состояние субъекта, чтобы уведомить наблюдателей о изменениях.

2. Интерфейс ISubject:
Субъект (в данном случае, пользователь) реализует этот интерфейс для управления подписчиками (наблюдателями). Субъект может добавлять, удалять и уведомлять наблюдателей.

3. Класс User (Субъект и Прототип):
Класс User представляет пользователя с именем и балансом. Этот класс реализует интерфейс ISubject и поддерживает список наблюдателей. Когда баланс пользователя изменяется, метод ChangeBalance вызывает Notify, чтобы оповестить всех наблюдателей. Также реализован метод Clone, который создает копию пользователя.

4. Класс EmailNotifier (Наблюдатель):
Этот класс реализует интерфейс IObserver и уведомляет о изменениях, выводя сообщение на экран (вместо отправки настоящего email).

5. Программа:
В методе Main создается пользователь, добавляется наблюдатель, и происходит изменение баланса, что вызывает уведомление. Затем создается клонированный пользователь, который также подписывается на уведомления и получает обновления при изменении баланса.

## 9. Компоновщик + Состояние.

## Сценарий:

Предположим, у нас есть система для управления состоянием аккаунта пользователя. Каждый пользователь может быть в одном из нескольких состояний (например, "Активный", "Заблокирован", "Ожидает проверки"). В зависимости от состояния аккаунта выполняются разные действия. Мы будем использовать Прототип для клонирования пользователя и Состояние для управления состоянием.

```csharp
using System;

// Интерфейс состояния
public interface IAccountState
{
    void HandleRequest(User user);
}

// Конкретные состояния
public class ActiveState : IAccountState
{
    public void HandleRequest(User user)
    {
        Console.WriteLine($"Пользователь {user.Name} активен. Доступ разрешен.");
    }
}

public class BlockedState : IAccountState
{
    public void HandleRequest(User user)
    {
        Console.WriteLine($"Пользователь {user.Name} заблокирован. Доступ запрещен.");
    }
}

public class PendingState : IAccountState
{
    public void HandleRequest(User user)
    {
        Console.WriteLine($"Пользователь {user.Name} в состоянии ожидания. Доступ ограничен.");
    }
}

// Класс пользователя с поддержкой состояния и прототипа
public class User : ICloneable
{
    public string Name { get; set; }
    private IAccountState _state;

    public User(string name, IAccountState initialState)
    {
        Name = name;
        _state = initialState;
    }

    public void SetState(IAccountState newState)
    {
        _state = newState;
    }

    public void Request()
    {
        _state.HandleRequest(this);
    }

    // Метод клонирования пользователя
    public object Clone()
    {
        // Создание нового объекта с тем же состоянием
        return new User(Name, _state);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем пользователя в активном состоянии
        User user1 = new User("Иван", new ActiveState());
        
        // Проверяем состояние пользователя
        user1.Request();

        // Клонируем пользователя
        User clonedUser = (User)user1.Clone();
        
        // Изменяем состояние клонированного пользователя на "Заблокирован"
        clonedUser.SetState(new BlockedState());
        
        // Проверяем новое состояние клонированного пользователя
        clonedUser.Request();

        // Создаем нового пользователя в состоянии ожидания
        User user2 = new User("Мария", new PendingState());
        user2.Request();
    }
}
```

## Пояснение к коду

1. Интерфейс IAccountState:
Этот интерфейс определяет метод HandleRequest, который будет вызываться для выполнения действия в зависимости от текущего состояния пользователя.

2. Конкретные состояния:
Классы ActiveState, BlockedState и PendingState реализуют интерфейс IAccountState и описывают поведение пользователя в разных состояниях. Например, в активном состоянии доступ разрешен, в заблокированном — доступ запрещен, а в состоянии ожидания — доступ ограничен.

3. Класс User:
Класс пользователя хранит ссылку на текущее состояние. Метод SetState позволяет изменять состояние пользователя. Метод Request вызывает метод HandleRequest для текущего состояния. Также реализован метод Clone, который создает копию пользователя, сохраняя его текущее состояние.

4. Программа:
В методе Main создаются пользователи с различными состояниями. Один из пользователей клонируется, и его состояние меняется на заблокированное. Все пользователи выполняют запросы для проверки состояния.

## 10. Компоновщик + Посетитель.

## Сценарий:

Предположим, у нас есть система для управления пользователями. Каждый пользователь имеет разные типы данных (например, имя и возраст), и мы хотим выполнять над ними различные операции, такие как вывод информации или проверка возрастных ограничений. Используем Прототип для клонирования пользователей и Посетитель для выполнения операции над ними.

```csharp
using System;

// Интерфейс посетителя
public interface IUserVisitor
{
    void Visit(User user);
}

// Конкретный посетитель, который выводит информацию о пользователе
public class InfoVisitor : IUserVisitor
{
    public void Visit(User user)
    {
        Console.WriteLine($"Пользователь: {user.Name}, Возраст: {user.Age}");
    }
}

// Конкретный посетитель, который проверяет возраст пользователя
public class AgeCheckVisitor : IUserVisitor
{
    public void Visit(User user)
    {
        if (user.Age >= 18)
        {
            Console.WriteLine($"Пользователь {user.Name} старше 18 лет.");
        }
        else
        {
            Console.WriteLine($"Пользователь {user.Name} младше 18 лет.");
        }
    }
}

// Класс пользователя с поддержкой прототипа
public class User : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Метод клонирования пользователя
    public object Clone()
    {
        return new User(Name, Age);
    }

    // Метод для применения посетителя
    public void Accept(IUserVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем пользователя
        User user1 = new User("Иван", 25);
        
        // Создаем посетителей
        IUserVisitor infoVisitor = new InfoVisitor();
        IUserVisitor ageCheckVisitor = new AgeCheckVisitor();
        
        // Применяем посетителей к пользователю
        user1.Accept(infoVisitor);
        user1.Accept(ageCheckVisitor);

        // Клонируем пользователя
        User clonedUser = (User)user1.Clone();

        // Применяем посетителей к клонированному пользователю
        Console.WriteLine("\nКлонированный пользователь:");
        clonedUser.Accept(infoVisitor);
        clonedUser.Accept(ageCheckVisitor);
    }
}
```

## Пояснение к коду

1. Интерфейс IUserVisitor:
Этот интерфейс определяет метод Visit, который будет вызываться для каждого пользователя. Конкретные классы посетителей реализуют этот интерфейс и выполняют соответствующие операции.

2. Конкретные посетители:

InfoVisitor: Выводит информацию о пользователе, включая его имя и возраст.
AgeCheckVisitor: Проверяет, старше ли пользователь 18 лет, и выводит соответствующее сообщение.
3. Класс User:

Содержит имя и возраст пользователя.
Реализует метод Clone, который создает и возвращает новый объект User с теми же значениями.
Метод Accept вызывает метод Visit у переданного посетителя, тем самым предоставляя возможность выполнять операции над объектом.
4. Программа:

Создается пользователь, и к нему применяются два разных посетителя для различных операций.
Пользователь клонируется, и те же посетители применяются к клонированному объекту.

## 11. Компоновщик + Цепочка обязанностей.

## Сценарий:

Предположим, что у нас есть система обработки запросов на покупку товаров. Каждый запрос проверяется через несколько этапов, например, проверка наличия товара, проверка баланса пользователя и проверка возраста. Все эти этапы обрабатываются в цепочке. При этом, мы будем использовать Прототип для клонирования объектов запросов.

```csharp
using System;

// Интерфейс обработчика (Handler)
public interface IRequestHandler
{
    void SetNext(IRequestHandler handler);
    void HandleRequest(Request request);
}

// Абстрактный обработчик, который реализует цепочку
public abstract class RequestHandler : IRequestHandler
{
    private IRequestHandler _nextHandler;

    public void SetNext(IRequestHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleRequest(Request request)
    {
        if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

// Конкретные обработчики

public class ProductAvailabilityHandler : RequestHandler
{
    public override void HandleRequest(Request request)
    {
        if (request.ProductAvailable)
        {
            Console.WriteLine("Товар в наличии.");
            base.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Товар недоступен.");
        }
    }
}

public class UserBalanceHandler : RequestHandler
{
    public override void HandleRequest(Request request)
    {
        if (request.UserBalance >= request.ProductPrice)
        {
            Console.WriteLine("Баланс пользователя достаточно для покупки.");
            base.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Недостаточно средств на счете.");
        }
    }
}

public class AgeVerificationHandler : RequestHandler
{
    public override void HandleRequest(Request request)
    {
        if (request.UserAge >= 18)
        {
            Console.WriteLine("Пользователь прошел возрастную проверку.");
            base.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Пользователь не достиг 18 лет.");
        }
    }
}

// Класс запроса с поддержкой прототипа
public class Request : ICloneable
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
    public double UserBalance { get; set; }
    public double ProductPrice { get; set; }
    public bool ProductAvailable { get; set; }

    public Request(string userName, int userAge, double userBalance, double productPrice, bool productAvailable)
    {
        UserName = userName;
        UserAge = userAge;
        UserBalance = userBalance;
        ProductPrice = productPrice;
        ProductAvailable = productAvailable;
    }

    // Метод клонирования запроса
    public object Clone()
    {
        return new Request(UserName, UserAge, UserBalance, ProductPrice, ProductAvailable);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем запрос
        Request request1 = new Request("Иван", 25, 1500, 1200, true);
        
        // Создаем цепочку обработчиков
        IRequestHandler handler1 = new ProductAvailabilityHandler();
        IRequestHandler handler2 = new UserBalanceHandler();
        IRequestHandler handler3 = new AgeVerificationHandler();
        
        handler1.SetNext(handler2);
        handler2.SetNext(handler3);
        
        // Обрабатываем запрос
        Console.WriteLine("Обработка запроса 1:");
        handler1.HandleRequest(request1);
        
        // Клонируем запрос
        Request clonedRequest = (Request)request1.Clone();
        
        // Изменяем данные клонированного запроса
        clonedRequest.UserBalance = 500; // Недостаточно средств
        clonedRequest.ProductAvailable = false; // Товар недоступен
        
        // Обрабатываем клонированный запрос
        Console.WriteLine("\nОбработка клонированного запроса:");
        handler1.HandleRequest(clonedRequest);
    }
}
```

## Пояснение к коду

1. Интерфейс IRequestHandler:
Определяет метод SetNext для добавления следующего обработчика в цепочку и метод HandleRequest для обработки запроса.

2. Абстрактный класс RequestHandler:
Реализует общую логику для цепочки обязанностей. Каждый конкретный обработчик должен переопределить метод HandleRequest для выполнения своей задачи, а затем вызвать base.HandleRequest(request) для передачи запроса дальше по цепочке.

3. Конкретные обработчики:

ProductAvailabilityHandler проверяет, доступен ли товар.
UserBalanceHandler проверяет, достаточно ли средств у пользователя для покупки.
AgeVerificationHandler проверяет, достиг ли пользователь 18 лет.
4. Класс Request: Класс запроса содержит информацию о пользователе, товаре и данных, которые используются в обработке запроса. Метод Clone используется для клонирования запроса с теми же данными.

5. Программа:

Вначале создается запрос, который обрабатывается через цепочку обработчиков.
После этого запрос клонируется, и его данные изменяются, чтобы проверить, как цепочка будет работать с новым запросом.

## 12. Компоновщик + Команда.

## Сценарий:

Предположим, что у нас есть система для управления заказами. Когда приходит запрос на обработку заказа, мы используем Команду для выполнения действия, а Прототип для клонирования объектов запроса перед выполнением, чтобы мы могли сохранить исходные данные.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс команды
public interface ICommand
{
    void Execute();
}

// Класс заказов (Прототип)
public class Order : ICloneable
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string CustomerName { get; set; }

    public Order(string product, int quantity, double price, string customerName)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
        CustomerName = customerName;
    }

    // Метод клонирования заказа
    public object Clone()
    {
        return new Order(Product, Quantity, Price, CustomerName);
    }
}

// Конкретная команда для обработки заказа
public class ProcessOrderCommand : ICommand
{
    private readonly Order _order;

    public ProcessOrderCommand(Order order)
    {
        _order = order;
    }

    public void Execute()
    {
        Console.WriteLine($"Обработка заказа: { _order.Product }, Количество: {_order.Quantity}, Цена: {_order.Price}, Клиент: {_order.CustomerName}");
    }
}

// Конкретная команда для отмены заказа
public class CancelOrderCommand : ICommand
{
    private readonly Order _order;

    public CancelOrderCommand(Order order)
    {
        _order = order;
    }

    public void Execute()
    {
        Console.WriteLine($"Отмена заказа: { _order.Product }, Количество: {_order.Quantity}, Цена: {_order.Price}, Клиент: {_order.CustomerName}");
    }
}

// Invoker, который хранит и вызывает команды
public class OrderInvoker
{
    private readonly List<ICommand> _commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем оригинальный заказ
        Order originalOrder = new Order("Laptop", 1, 1500, "Иван");
        
        // Клонируем заказ для создания нового экземпляра
        Order clonedOrder = (Order)originalOrder.Clone();
        clonedOrder.CustomerName = "Петр"; // Изменяем имя клиента в клонированном заказе
        
        // Создаем команды для обработки заказов
        ICommand processOriginalOrder = new ProcessOrderCommand(originalOrder);
        ICommand processClonedOrder = new ProcessOrderCommand(clonedOrder);
        
        ICommand cancelOriginalOrder = new CancelOrderCommand(originalOrder);
        ICommand cancelClonedOrder = new CancelOrderCommand(clonedOrder);
        
        // Создаем инвокер для выполнения команд
        OrderInvoker invoker = new OrderInvoker();
        
        // Добавляем команды в инвокер
        invoker.AddCommand(processOriginalOrder);
        invoker.AddCommand(processClonedOrder);
        invoker.AddCommand(cancelOriginalOrder);
        invoker.AddCommand(cancelClonedOrder);
        
        // Выполняем все команды
        Console.WriteLine("Выполнение команд:");
        invoker.ExecuteCommands();
    }
}
```

## Пояснение к коду

1. Интерфейс ICommand: Определяет метод Execute, который будет реализован в конкретных командах.

2. Класс Order (Прототип): Класс заказов содержит информацию о товаре, количестве, цене и имени клиента. Метод Clone используется для клонирования объекта заказа.

3. Команды:

ProcessOrderCommand выполняет обработку заказа, выводя информацию о заказе.
CancelOrderCommand отменяет заказ, также выводя информацию о заказе.
4. Класс OrderInvoker: Класс инвокера хранит список команд и выполняет их по очереди через метод ExecuteCommands.

5. Программа:

Создаем оригинальный заказ и клонируем его.
Создаем команды для обработки и отмены обоих заказов.
Добавляем эти команды в инвокер и выполняем их.

## 13. Компоновщик + Посредник.

## Сценарий:

Предположим, что у нас есть система обмена сообщениями, и когда пользователь отправляет сообщение, система может клонировать объект сообщения перед отправкой, чтобы обеспечить возможность использования оригинала и его копии. Посредник будет координировать отправку сообщений между пользователями.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс посредника
public interface IMediator
{
    void SendMessage(string message, User sender);
}

// Абстракция пользователя
public class User
{
    public string Name { get; set; }
    private IMediator _mediator;

    public User(string name, IMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{Name} отправляет сообщение: {message}");
        _mediator.SendMessage(message, this);
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"{Name} получил сообщение: {message}");
    }
}

// Конкретный посредник
public class ChatMediator : IMediator
{
    private List<User> _users = new List<User>();

    public void RegisterUser(User user)
    {
        _users.Add(user);
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in _users)
        {
            if (user != sender)
            {
                user.ReceiveMessage(message);
            }
        }
    }
}

// Сообщение (Прототип)
public class Message : ICloneable
{
    public string Text { get; set; }
    public string SenderName { get; set; }

    public Message(string text, string senderName)
    {
        Text = text;
        SenderName = senderName;
    }

    // Метод клонирования
    public object Clone()
    {
        return new Message(Text, SenderName);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем посредника
        ChatMediator mediator = new ChatMediator();

        // Создаем пользователей
        User user1 = new User("Иван", mediator);
        User user2 = new User("Петр", mediator);
        User user3 = new User("Мария", mediator);

        // Регистрируем пользователей в посреднике
        mediator.RegisterUser(user1);
        mediator.RegisterUser(user2);
        mediator.RegisterUser(user3);

        // Создаем сообщение и клонируем его
        Message originalMessage = new Message("Привет всем!", "Иван");
        Message clonedMessage = (Message)originalMessage.Clone();

        // Изменяем сообщение в клонированном объекте
        clonedMessage.Text = "Привет, Петр!";

        // Отправляем оригинальное и клонированное сообщение
        user1.SendMessage(originalMessage.Text);  // Оригинальное сообщение
        user2.SendMessage(clonedMessage.Text);    // Клонированное сообщение
    }
}
```

## Пояснение к коду

1. Интерфейс IMediator: Определяет метод SendMessage, который будет использоваться для отправки сообщений между пользователями.

2. Класс User: Представляет пользователя, который может отправлять и получать сообщения через посредника.

3. Класс ChatMediator: Конкретный посредник, который управляет пользователями и перенаправляет их сообщения друг другу. Он знает о всех пользователях, зарегистрированных в системе.

4. Класс Message (Прототип): Представляет сообщение. Класс реализует метод Clone, который позволяет клонировать объект сообщения. Это позволяет создавать копии сообщения перед отправкой.

5. Программа:

Создаем пользователей и регистрируем их у посредника.
Создаем сообщение и клонируем его.
Оригинальное сообщение и клонированное отправляются через посредника.

## 14. Компоновщик + Снимок.

## Сценарий:

Предположим, у нас есть объект, представляющий пользователя с некоторыми настройками. Мы можем клонировать объект с помощью Прототипа и сохранять его состояние с помощью Снимка, чтобы позднее восстановить его.

```csharp
using System;
using System.Collections.Generic;

// Прототип
public class UserProfile : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }

    public UserProfile(string name, int age, string country)
    {
        Name = name;
        Age = age;
        Country = country;
    }

    // Клонирование объекта (метод прототипа)
    public object Clone()
    {
        return new UserProfile(Name, Age, Country);
    }

    // Метод для отображения информации о пользователе
    public void DisplayProfile()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Country: {Country}");
    }
}

// Снимок
public class ProfileSnapshot
{
    public UserProfile UserProfile { get; private set; }
    public string Date { get; private set; }

    public ProfileSnapshot(UserProfile userProfile)
    {
        // Сохраняем состояние профиля
        UserProfile = (UserProfile)userProfile.Clone();
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // Метод для восстановления состояния
    public void Restore()
    {
        Console.WriteLine($"Restoring profile from snapshot taken at {Date}:");
        UserProfile.DisplayProfile();
    }
}

// Менеджер снимков, который хранит историю изменений
public class SnapshotManager
{
    private List<ProfileSnapshot> _snapshots = new List<ProfileSnapshot>();

    public void AddSnapshot(ProfileSnapshot snapshot)
    {
        _snapshots.Add(snapshot);
    }

    public void ShowHistory()
    {
        Console.WriteLine("Snapshot history:");
        foreach (var snapshot in _snapshots)
        {
            Console.WriteLine($"Snapshot taken at {snapshot.Date}");
        }
    }

    public void RestoreFromSnapshot(int index)
    {
        if (index >= 0 && index < _snapshots.Count)
        {
            _snapshots[index].Restore();
        }
        else
        {
            Console.WriteLine("Invalid snapshot index.");
        }
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Создаем начальный объект профиля
        UserProfile userProfile = new UserProfile("John Doe", 30, "USA");

        // Отображаем текущий профиль
        userProfile.DisplayProfile();

        // Сохраняем снимок текущего состояния
        SnapshotManager snapshotManager = new SnapshotManager();
        ProfileSnapshot snapshot1 = new ProfileSnapshot(userProfile);
        snapshotManager.AddSnapshot(snapshot1);

        // Изменяем профиль
        userProfile.Name = "Jane Smith";
        userProfile.Age = 25;
        userProfile.Country = "Canada";

        // Отображаем измененный профиль
        userProfile.DisplayProfile();

        // Сохраняем еще один снимок
        ProfileSnapshot snapshot2 = new ProfileSnapshot(userProfile);
        snapshotManager.AddSnapshot(snapshot2);

        // Показываем историю снимков
        snapshotManager.ShowHistory();

        // Восстанавливаем профиль из первого снимка
        snapshotManager.RestoreFromSnapshot(0);

        // Восстанавливаем профиль из второго снимка
        snapshotManager.RestoreFromSnapshot(1);
    }
}
```

## Пояснение к коду

1. Класс UserProfile:

Представляет профиль пользователя с его данными: имя, возраст и страна.
Реализует метод Clone для клонирования объекта. Это необходимо для шаблона Прототип, чтобы мы могли создавать точные копии объекта.
Метод DisplayProfile выводит информацию о профиле пользователя.
2. Класс ProfileSnapshot:

Хранит снимок состояния объекта UserProfile.
При создании сохраняет копию профиля (используя метод Clone).
Метод Restore восстанавливает сохраненное состояние профиля и отображает его.
3. Класс SnapshotManager:

Управляет историей снимков. Он может хранить несколько снимков состояний объектов и восстанавливать состояние из любого из них.
4. Программа:

Создается объект профиля UserProfile, и сохраняются его состояния с помощью Снимка.
Затем профиль изменяется, и снова сохраняется снимок.
Программа выводит историю снимков и позволяет восстанавливать состояние профиля из выбранного снимка.

## Одиночка.

## 1. Одиночка + Адаптер.

## Сценарий:

Предположим, у нас есть класс, который реализует Одиночку, и этот класс предоставляет функциональность для работы с каким-то старым интерфейсом. Мы используем Адаптер для того, чтобы этот класс соответствовал новому интерфейсу, при этом оставляя возможность работать с существующим функционалом.

```csharp
using System;

// Одиночка (Singleton)
public class Singleton
{
    private static Singleton _instance;

    // Приватный конструктор, чтобы предотвратить создание экземпляров
    private Singleton() { }

    // Статический метод для получения единственного экземпляра
    public static Singleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Singleton();
        }
        return _instance;
    }

    // Простой метод, который демонстрирует работу класса
    public void ShowMessage()
    {
        Console.WriteLine("Singleton instance: ShowMessage called");
    }
}

// Старый интерфейс (необходимо адаптировать)
public interface IOldSystem
{
    void DisplayInfo();
}

// Новый интерфейс
public interface INewSystem
{
    void ShowDetails();
}

// Адаптер, который преобразует старый интерфейс в новый
public class Adapter : INewSystem
{
    private readonly IOldSystem _oldSystem;

    public Adapter(IOldSystem oldSystem)
    {
        _oldSystem = oldSystem;
    }

    // Реализует новый интерфейс, используя старый
    public void ShowDetails()
    {
        _oldSystem.DisplayInfo();
    }
}

// Класс, который реализует старый интерфейс
public class OldSystem : IOldSystem
{
    private readonly Singleton _singleton;

    public OldSystem()
    {
        _singleton = Singleton.GetInstance(); // Получаем единственный экземпляр
    }

    // Реализуем метод старого интерфейса
    public void DisplayInfo()
    {
        _singleton.ShowMessage();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр Singleton
        Singleton singleton = Singleton.GetInstance();

        // Показываем сообщение
        singleton.ShowMessage();

        // Используем старый интерфейс с адаптером
        IOldSystem oldSystem = new OldSystem();

        // Адаптируем старый интерфейс к новому
        INewSystem newSystem = new Adapter(oldSystem);

        // Отображаем информацию через новый интерфейс
        Console.WriteLine("\nUsing Adapter to call ShowDetails:");
        newSystem.ShowDetails();
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс Singleton реализует шаблон Одиночка и обеспечивает, чтобы только один экземпляр этого класса мог быть создан. Он также предоставляет метод ShowMessage, который просто выводит сообщение.
Старый интерфейс IOldSystem и новый интерфейс INewSystem:

Старый интерфейс IOldSystem имеет метод DisplayInfo, который выводит информацию о продукте или состоянии.
Новый интерфейс INewSystem имеет метод ShowDetails, который также выводит информацию, но в новом формате.
Адаптер Adapter:

Преобразует старый интерфейс IOldSystem в новый интерфейс INewSystem. Внутри класса адаптера мы используем старый объект для вызова его метода, но через новый интерфейс.
Класс OldSystem:

Реализует старый интерфейс IOldSystem и вызывает метод из класса Одиночка, чтобы отобразить сообщение.
Программа:

Создается экземпляр Одиночки, который используется для вывода сообщения.
Мы также создаем объект OldSystem, который реализует старый интерфейс, и используем Адаптер, чтобы преобразовать старый интерфейс в новый, вызывая его через адаптированный интерфейс.

## 2. Одиночка + Мост.

## Сценарий:

Предположим, у нас есть система, которая использует различные способы логирования. Мы можем использовать шаблон Мост для разделения абстракции (логирование) и различных реализаций (например, логирование в файл или в базу данных). Одиночка гарантирует, что у нас есть только один экземпляр логирования, независимо от типа реализации.

```csharp
using System;

// Одиночка (Singleton)
public class Logger
{
    private static Logger _instance;

    // Приватный конструктор, чтобы предотвратить создание экземпляров извне
    private Logger() { }

    // Метод для получения единственного экземпляра Logger
    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    // Метод для логирования сообщений
    public void Log(string message)
    {
        Console.WriteLine("Log: " + message);
    }
}

// Абстракция (логирование)
public abstract class LoggerBridge
{
    protected Logger _logger;

    // Конструктор принимает экземпляр Logger
    public LoggerBridge(Logger logger)
    {
        _logger = logger;
    }

    // Метод логирования, реализуемый в наследуемых классах
    public abstract void LogMessage(string message);
}

// Реализация логирования в файл
public class FileLogger : LoggerBridge
{
    public FileLogger(Logger logger) : base(logger) { }

    public override void LogMessage(string message)
    {
        _logger.Log("Log to file: " + message);
    }
}

// Реализация логирования в базу данных
public class DatabaseLogger : LoggerBridge
{
    public DatabaseLogger(Logger logger) : base(logger) { }

    public override void LogMessage(string message)
    {
        _logger.Log("Log to database: " + message);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр Logger
        Logger logger = Logger.GetInstance();

        // Используем мост для логирования в файл
        LoggerBridge fileLogger = new FileLogger(logger);
        fileLogger.LogMessage("File log message");

        // Используем мост для логирования в базу данных
        LoggerBridge dbLogger = new DatabaseLogger(logger);
        dbLogger.LogMessage("Database log message");
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс Logger реализует шаблон Одиночка. Это гарантирует, что в приложении будет только один экземпляр Logger. Метод GetInstance проверяет, был ли уже создан экземпляр, и возвращает его. Метод Log используется для записи сообщения.
Абстракция и реализации с использованием Моста:

Абстракция LoggerBridge задает общий интерфейс для логирования. Это позволяет переключаться между разными реализациями логирования.
Реализация FileLogger и DatabaseLogger — это конкретные реализации логирования, которые могут изменяться независимо от абстракции. Каждая из них использует экземпляр Logger, чтобы записывать сообщения в файл или базу данных.
Программа:

В Main создается один экземпляр Одиночки Logger. Затем создаются два объекта FileLogger и DatabaseLogger, которые используют Мост для логирования в разные места (файл или базу данных).

## 3. Одиночка + Декоратор.

## Сценарий:

Предположим, у нас есть класс логирования, который записывает сообщения в консоль. Мы можем использовать шаблон Декоратор, чтобы добавить дополнительные возможности, такие как запись сообщений с меткой времени или уровнем логирования, без изменения исходного класса. Одиночка гарантирует, что существует только один экземпляр логера.

```csharp
using System;

// Одиночка (Singleton)
public class Logger
{
    private static Logger _instance;

    // Приватный конструктор, чтобы предотвратить создание экземпляров извне
    private Logger() { }

    // Метод для получения единственного экземпляра Logger
    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    // Метод для записи лог-сообщений
    public virtual void Log(string message)
    {
        Console.WriteLine("Log: " + message);
    }
}

// Абстрактный класс Декоратора
public abstract class LoggerDecorator : Logger
{
    protected Logger _logger;

    public LoggerDecorator(Logger logger)
    {
        _logger = logger;
    }

    public override void Log(string message)
    {
        _logger.Log(message);
    }
}

// Декоратор для добавления метки времени
public class TimestampLogger : LoggerDecorator
{
    public TimestampLogger(Logger logger) : base(logger) { }

    public override void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        base.Log($"[{timestamp}] {message}");
    }
}

// Декоратор для добавления уровня логирования
public class LogLevelLogger : LoggerDecorator
{
    private string _level;

    public LogLevelLogger(Logger logger, string level) : base(logger)
    {
        _level = level;
    }

    public override void Log(string message)
    {
        base.Log($"[{_level}] {message}");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр Logger
        Logger logger = Logger.GetInstance();

        // Логирование без декоратора
        logger.Log("This is a simple log message.");

        // Используем декоратор для добавления метки времени
        Logger timestampLogger = new TimestampLogger(logger);
        timestampLogger.Log("This is a log message with timestamp.");

        // Используем декоратор для добавления уровня логирования
        Logger logLevelLogger = new LogLevelLogger(logger, "ERROR");
        logLevelLogger.Log("This is an error log message.");

        // Сочетание декораторов: метка времени и уровень логирования
        Logger decoratedLogger = new TimestampLogger(new LogLevelLogger(logger, "INFO"));
        decoratedLogger.Log("This is a log message with timestamp and level.");
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс Logger реализует шаблон Одиночка. Он гарантирует, что существует только один экземпляр класса, который можно получить через метод GetInstance.
Декоратор (Decorator):

Абстрактный класс LoggerDecorator расширяет функциональность класса Logger. Он может изменять или добавлять новые функциональности для логирования.
TimestampLogger — это конкретный декоратор, который добавляет метку времени к сообщению.
LogLevelLogger — это еще один декоратор, который добавляет уровень логирования (например, ERROR или INFO) к сообщению.
Программа:

В методе Main создается единственный экземпляр Одиночки Logger.
Затем создаются несколько объектов с применением декораторов: один для добавления метки времени, другой — для уровня логирования, и наконец — комбинация этих декораторов.

## 4. Одиночка + Фасад.

## Сценарий:

Предположим, у нас есть система для обработки различных типов платежей (кредитные карты, PayPal и банковские переводы), и мы хотим предоставить простой интерфейс для пользователей, скрывая всю сложность взаимодействия с этими системами. Фасад упрощает взаимодействие, а Одиночка гарантирует, что существует только один экземпляр фасада.

```csharp
using System;

// Одиночка (Singleton)
public class PaymentFacade
{
    private static PaymentFacade _instance;

    // Приватные компоненты системы (эмулируем сложную подсистему)
    private CreditCardPayment _creditCardPayment;
    private PayPalPayment _payPalPayment;
    private BankTransferPayment _bankTransferPayment;

    // Приватный конструктор для ограничения создания экземпляров
    private PaymentFacade()
    {
        _creditCardPayment = new CreditCardPayment();
        _payPalPayment = new PayPalPayment();
        _bankTransferPayment = new BankTransferPayment();
    }

    // Метод для получения единственного экземпляра PaymentFacade
    public static PaymentFacade GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PaymentFacade();
        }
        return _instance;
    }

    // Упрощенный метод для выполнения платежа
    public void MakePayment(string method)
    {
        switch (method.ToLower())
        {
            case "creditcard":
                _creditCardPayment.ProcessPayment();
                break;
            case "paypal":
                _payPalPayment.ProcessPayment();
                break;
            case "banktransfer":
                _bankTransferPayment.ProcessPayment();
                break;
            default:
                Console.WriteLine("Invalid payment method.");
                break;
        }
    }
}

// Компоненты подсистемы
public class CreditCardPayment
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing payment through Credit Card.");
    }
}

public class PayPalPayment
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing payment through PayPal.");
    }
}

public class BankTransferPayment
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing payment through Bank Transfer.");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр PaymentFacade
        PaymentFacade paymentFacade = PaymentFacade.GetInstance();

        // Используем фасад для выполнения различных платежей
        paymentFacade.MakePayment("creditcard");
        paymentFacade.MakePayment("paypal");
        paymentFacade.MakePayment("banktransfer");
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс PaymentFacade реализует шаблон Одиночка. Это гарантирует, что существует только один экземпляр фасада в системе. Экземпляр можно получить через метод GetInstance.
Фасад имеет три компонента подсистемы: CreditCardPayment, PayPalPayment и BankTransferPayment. Все они обрабатывают различные типы платежей.
Фасад (Facade):

Класс PaymentFacade предоставляет упрощённый интерфейс для выполнения платежей. Внутри фасада происходит взаимодействие с компонентами подсистемы.
Метод MakePayment позволяет пользователю выбрать способ оплаты (кредитная карта, PayPal или банковский перевод) без необходимости взаимодействовать с каждым компонентом напрямую.
Программа:

В методе Main мы создаём единственный экземпляр Одиночки PaymentFacade и вызываем метод MakePayment для различных типов платежей.

## 5. Одиночка + Легковес.

## Сценарий:

Предположим, что в нашей системе нужно управлять большим количеством объектов с одинаковыми данными, например, клиентов с общими данными о стране. Мы будем использовать Легковес, чтобы сохранить информацию о стране только один раз для всех клиентов из одной страны. Кроме того, Одиночка будет обеспечивать, что мы используем единственный экземпляр фабрики для создания клиентов.

```csharp
using System;
using System.Collections.Generic;

// Одиночка (Singleton) для управления фабрикой клиентов
public class ClientFactory
{
    private static ClientFactory _instance;
    private Dictionary<string, Country> _countryCache;

    // Приватный конструктор
    private ClientFactory()
    {
        _countryCache = new Dictionary<string, Country>();
    }

    // Получить единственный экземпляр фабрики
    public static ClientFactory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ClientFactory();
        }
        return _instance;
    }

    // Получить страну из кеша или создать новую
    public Country GetCountry(string countryName)
    {
        if (!_countryCache.ContainsKey(countryName))
        {
            _countryCache[countryName] = new Country(countryName);
        }
        return _countryCache[countryName];
    }

    // Создание клиента с использованием легковесной страны
    public Client CreateClient(string clientName, string countryName)
    {
        Country country = GetCountry(countryName);
        return new Client(clientName, country);
    }
}

// Легковес (Flyweight) для хранения данных о стране
public class Country
{
    public string Name { get; private set; }

    public Country(string name)
    {
        Name = name;
        Console.WriteLine($"Creating new Country: {name}");
    }
}

// Класс клиента, который использует Легковес
public class Client
{
    public string Name { get; private set; }
    public Country Country { get; private set; }

    public Client(string name, Country country)
    {
        Name = name;
        Country = country;
    }

    public void DisplayClientInfo()
    {
        Console.WriteLine($"Client: {Name}, Country: {Country.Name}");
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр фабрики
        ClientFactory factory = ClientFactory.GetInstance();

        // Создаем клиентов с одинаковыми странами (Легковес)
        Client client1 = factory.CreateClient("Alice", "USA");
        Client client2 = factory.CreateClient("Bob", "USA");
        Client client3 = factory.CreateClient("Charlie", "Canada");

        // Отображаем информацию о клиентах
        client1.DisplayClientInfo();
        client2.DisplayClientInfo();
        client3.DisplayClientInfo();
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс ClientFactory реализует шаблон Одиночка и управляет созданием клиентов. Метод GetInstance обеспечивает создание только одного экземпляра фабрики.
В фабрике хранится кеш для стран, чтобы не создавать несколько экземпляров одной и той же страны (это делает систему более эффективной).
Легковес (Flyweight):

Класс Country представляет данные о стране. Это объект Легковес, потому что одинаковая информация о стране (например, название) используется многими клиентами. В конструкторе создается только один экземпляр каждой страны.
Все клиенты, принадлежащие одной стране, используют один и тот же объект Country, чтобы сэкономить память.
Client:

Класс Client использует объект Легковес Country. Клиент хранит информацию о своем имени и стране, но сам объект страны не создается заново для каждого клиента, если страна уже существует в системе.
Программа:

В методе Main мы создаем клиентов с одинаковыми странами. Например, два клиента имеют страну "USA", и оба будут использовать один и тот же объект Country, благодаря паттерну Легковес.

## 6. Одиночка + Заместитель.

## Сценарий:

Предположим, что у нас есть сервис, который представляет собой ресурс, требующий некоторых вычислительных затрат, и доступ к нему должен быть ограничен через заместителя. Заместитель будет контролировать доступ, а сам сервис будет единственным экземпляром в системе, управляемым через Одиночку.

```csharp
using System;

// Одиночка (Singleton) для управления доступом к сервису
public class ResourceService
{
    private static ResourceService _instance;

    // Приватный конструктор
    private ResourceService() { }

    // Метод для получения единственного экземпляра ресурса
    public static ResourceService GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ResourceService();
        }
        return _instance;
    }

    // Метод, который выполняет ресурсозатратные операции
    public void PerformHeavyOperation()
    {
        Console.WriteLine("Performing a heavy operation in ResourceService...");
    }
}

// Заместитель (Proxy), который управляет доступом к ResourceService
public class ResourceServiceProxy
{
    private ResourceService _resourceService;

    public ResourceServiceProxy()
    {
        _resourceService = ResourceService.GetInstance(); // Получаем единственный экземпляр
    }

    // Метод для выполнения операции через заместителя
    public void PerformOperation()
    {
        Console.WriteLine("Checking access control...");
        if (CheckAccess())  // Проверка доступа
        {
            _resourceService.PerformHeavyOperation();
        }
        else
        {
            Console.WriteLine("Access denied. You do not have permission to perform this operation.");
        }
    }

    // Метод для проверки доступа
    private bool CheckAccess()
    {
        // Здесь можно добавить логику проверки прав доступа
        return true;  // В данном примере всегда доступ разрешен
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Заместитель будет использовать единственный экземпляр ResourceService
        ResourceServiceProxy proxy = new ResourceServiceProxy();

        // Попытка выполнить операцию через заместителя
        proxy.PerformOperation();

        // Попытка выполнить операцию через заместителя снова, чтобы продемонстрировать повторное использование одного экземпляра
        proxy.PerformOperation();
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс ResourceService реализует шаблон Одиночка. Он гарантирует, что будет создан только один экземпляр сервиса, и предоставляет метод GetInstance для доступа к этому экземпляру.
Метод PerformHeavyOperation имитирует ресурсозатратную операцию, которая будет использоваться через заместителя.
Заместитель (Proxy):

Класс ResourceServiceProxy представляет собой Заместителя для ResourceService. Он проверяет доступ перед выполнением операции и скрывает саму реализацию проверки доступа.
В методе PerformOperation заместитель выполняет проверку доступа перед вызовом PerformHeavyOperation через экземпляр Одиночки.
Программа:

В методе Main создается объект ResourceServiceProxy. Заместитель управляет доступом к ресурсу и выполняет тяжелую операцию через единственный экземпляр ResourceService.

## 7. Одиночка + Стратегия.

## Сценарий:

Предположим, у нас есть система для обработки платежей. Стратегии оплаты могут изменяться в зависимости от типа пользователя или способа оплаты. При этом мы будем использовать Одиночку для управления состоянием платежной системы (например, для ведения журнала или управления конфигурацией), которая будет иметь одну единственную точку доступа.

```csharp
using System;

// Стратегия (Strategy) для обработки разных типов платежей
public interface IPaymentStrategy
{
    void ProcessPayment();
}

// Стратегия для обработки кредитных карт
public class CreditCardPayment : IPaymentStrategy
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing payment through Credit Card...");
    }
}

// Стратегия для обработки PayPal
public class PayPalPayment : IPaymentStrategy
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing payment through PayPal...");
    }
}

// Одиночка (Singleton) для управления платежной системой
public class PaymentSystem
{
    private static PaymentSystem _instance;
    public IPaymentStrategy PaymentStrategy { get; set; }

    // Приватный конструктор
    private PaymentSystem() { }

    // Метод для получения единственного экземпляра системы
    public static PaymentSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PaymentSystem();
        }
        return _instance;
    }

    // Метод для выполнения платежа с использованием стратегии
    public void ProcessPayment()
    {
        if (PaymentStrategy != null)
        {
            PaymentStrategy.ProcessPayment();
        }
        else
        {
            Console.WriteLine("No payment strategy set.");
        }
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр PaymentSystem
        PaymentSystem paymentSystem = PaymentSystem.GetInstance();

        // Устанавливаем стратегию оплаты через кредитную карту
        paymentSystem.PaymentStrategy = new CreditCardPayment();
        paymentSystem.ProcessPayment();  // Вывод: Processing payment through Credit Card...

        // Меняем стратегию на PayPal
        paymentSystem.PaymentStrategy = new PayPalPayment();
        paymentSystem.ProcessPayment();  // Вывод: Processing payment through PayPal...
    }
}
```

## Пояснение к коду

Стратегия (Strategy):

Интерфейс IPaymentStrategy задает контракт для всех стратегий обработки платежей, а конкретные стратегии, такие как CreditCardPayment и PayPalPayment, реализуют метод ProcessPayment, который выполняет соответствующую логику обработки платежа.
Одиночка (Singleton):

Класс PaymentSystem реализует шаблон Одиночка. Он гарантирует, что в системе будет только один экземпляр этого объекта.
В классе PaymentSystem также есть свойство PaymentStrategy, которое позволяет установить конкретную стратегию обработки платежа.
Метод ProcessPayment выполняет действие, связанное с оплатой, в зависимости от установленной стратегии.
Программа:

В методе Main создается единственный экземпляр PaymentSystem, и устанавливается сначала стратегия для оплаты через кредитную карту, а затем стратегия для оплаты через PayPal.
Программа демонстрирует, как можно менять стратегию во время выполнения, не изменяя сам класс PaymentSystem.

## 8. Одиночка + Шаблонный метод.

## Сценарий:

Предположим, у нас есть система для обработки разных типов отчетов (например, отчетов о продажах и отчетов о клиентах). Каждый тип отчета имеет общие этапы (сбор данных, форматирование, вывод), но каждый тип имеет свои особенности в реализации этих шагов. Мы будем использовать Одиночку для управления журналом или конфигурацией отчета.

```csharp
using System;

// Шаблонный метод (Template Method) — базовый класс для отчета
public abstract class Report
{
    // Шаблонный метод, задающий общий алгоритм для создания отчета
    public void GenerateReport()
    {
        CollectData();
        FormatReport();
        DisplayReport();
    }

    // Абстрактные методы, которые будут реализованы в подклассах
    protected abstract void CollectData();
    protected abstract void FormatReport();

    // Конкретный метод, который реализован в базовом классе
    protected void DisplayReport()
    {
        Console.WriteLine("Displaying report...");
    }
}

// Конкретная реализация отчета о продажах
public class SalesReport : Report
{
    protected override void CollectData()
    {
        Console.WriteLine("Collecting sales data...");
    }

    protected override void FormatReport()
    {
        Console.WriteLine("Formatting sales data...");
    }
}

// Конкретная реализация отчета о клиентах
public class CustomerReport : Report
{
    protected override void CollectData()
    {
        Console.WriteLine("Collecting customer data...");
    }

    protected override void FormatReport()
    {
        Console.WriteLine("Formatting customer data...");
    }
}

// Одиночка (Singleton) для управления отчетами
public class ReportManager
{
    private static ReportManager _instance;

    // Приватный конструктор
    private ReportManager() { }

    // Метод для получения единственного экземпляра
    public static ReportManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ReportManager();
        }
        return _instance;
    }

    // Метод для генерации отчета с использованием шаблонного метода
    public void GenerateReport(Report report)
    {
        report.GenerateReport();
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр ReportManager
        ReportManager reportManager = ReportManager.GetInstance();

        // Создаем и генерируем отчет о продажах
        Report salesReport = new SalesReport();
        reportManager.GenerateReport(salesReport);

        // Создаем и генерируем отчет о клиентах
        Report customerReport = new CustomerReport();
        reportManager.GenerateReport(customerReport);
    }
}
```

## Пояснение к коду

Шаблонный метод (Template Method):

Базовый абстрактный класс Report содержит общий алгоритм для генерации отчета в методе GenerateReport, который вызывает другие методы, такие как CollectData, FormatReport и DisplayReport.
Конкретные реализации отчетов, такие как SalesReport и CustomerReport, реализуют шаги алгоритма (CollectData и FormatReport), которые могут различаться в зависимости от типа отчета.
Одиночка (Singleton):

Класс ReportManager реализует шаблон Одиночка. Он гарантирует, что в системе существует только один экземпляр для управления отчетами.
Метод GenerateReport используется для генерации отчета с использованием шаблонного метода.
Программа:

В методе Main создается единственный экземпляр ReportManager и используется для генерации разных типов отчетов.
Мы создаем два типа отчетов (SalesReport и CustomerReport) и вызываем их метод GenerateReport, который выполняет алгоритм отчета с конкретной реализацией для каждого типа.

## 9. Одиночка + Наблюдатель.

## Сценарий:

Предположим, у нас есть система управления акциями на бирже, где один объект (например, биржа) изменяет цену акций, а несколько наблюдателей (например, инвесторы) получают уведомления об изменениях.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс Наблюдателя
public interface IObserver
{
    void Update(string stockSymbol, double price);
}

// Абстрактный класс для Наблюдаемого объекта
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

// Конкретный наблюдаемый объект (Биржа)
public class StockMarket : ISubject
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _stockSymbol;
    private double _price;

    public string StockSymbol
    {
        get { return _stockSymbol; }
        set
        {
            _stockSymbol = value;
            Notify();  // Уведомление наблюдателей при изменении
        }
    }

    public double Price
    {
        get { return _price; }
        set
        {
            _price = value;
            Notify();  // Уведомление наблюдателей при изменении
        }
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_stockSymbol, _price);
        }
    }
}

// Конкретный наблюдатель (Инвестор)
public class Investor : IObserver
{
    private string _name;

    public Investor(string name)
    {
        _name = name;
    }

    public void Update(string stockSymbol, double price)
    {
        Console.WriteLine($"Investor {_name} has been notified: {stockSymbol} is now {price}");
    }
}

// Одиночка (Singleton) для управления состоянием акций
public class StockManager
{
    private static StockManager _instance;

    public StockMarket StockMarket { get; }

    private StockManager()
    {
        StockMarket = new StockMarket();
    }

    // Метод для получения единственного экземпляра
    public static StockManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new StockManager();
        }
        return _instance;
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр StockManager
        StockManager stockManager = StockManager.GetInstance();

        // Создаем инвесторов
        Investor investor1 = new Investor("John");
        Investor investor2 = new Investor("Alice");

        // Подписываем инвесторов на изменения
        stockManager.StockMarket.Attach(investor1);
        stockManager.StockMarket.Attach(investor2);

        // Изменяем цену акции и уведомляем инвесторов
        stockManager.StockMarket.StockSymbol = "AAPL";
        stockManager.StockMarket.Price = 145.30;

        // Изменяем цену другой акции
        stockManager.StockMarket.StockSymbol = "GOOG";
        stockManager.StockMarket.Price = 2750.50;
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс StockManager реализует шаблон Одиночка. Он гарантирует, что существует только один экземпляр, который управляет объектом StockMarket (биржей). Это позволяет централизованно управлять состоянием рынка и его наблюдателями.
Наблюдатель (Observer):

Интерфейс IObserver и его реализация в классе Investor описывают наблюдателей, которые получают уведомления об изменениях.
Класс StockMarket реализует интерфейс ISubject и поддерживает список наблюдателей, уведомляя их об изменениях цены и символа акции.
Программа:

В методе Main мы создаем экземпляр StockManager (через Одиночку), а затем подписываем два объекта наблюдателей (Investor) на изменения в StockMarket.
Когда изменяется цена акции, все подписанные наблюдатели получают уведомления.

## 10. Одиночка + Состояние.

## Сценарий:

Предположим, что мы создаем систему для работы с банковским счетом, который может находиться в разных состояниях (например, "Активен", "Заморожен", "Заблокирован"). Мы хотим, чтобы система управления состоянием счета была единой (Одиночка), и чтобы поведение счета изменялось в зависимости от его состояния.

```csharp
using System;

// Интерфейс для состояния
public interface IAccountState
{
    void HandleRequest(BankAccount account);
}

// Конкретные состояния счета
public class ActiveState : IAccountState
{
    public void HandleRequest(BankAccount account)
    {
        Console.WriteLine("Account is active. You can withdraw and deposit money.");
        // Переход в другое состояние (например, заморожен)
        account.SetState(new FrozenState());
    }
}

public class FrozenState : IAccountState
{
    public void HandleRequest(BankAccount account)
    {
        Console.WriteLine("Account is frozen. You cannot perform any operations.");
        // Переход в другое состояние (например, активен)
        account.SetState(new ActiveState());
    }
}

public class BlockedState : IAccountState
{
    public void HandleRequest(BankAccount account)
    {
        Console.WriteLine("Account is blocked. No operations are allowed.");
    }
}

// Класс банковского счета
public class BankAccount
{
    private IAccountState _state;

    // Конструктор, который устанавливает начальное состояние
    public BankAccount()
    {
        _state = new ActiveState();
    }

    // Установка состояния
    public void SetState(IAccountState state)
    {
        _state = state;
    }

    // Обработчик запроса
    public void HandleRequest()
    {
        _state.HandleRequest(this);
    }
}

// Одиночка для управления банковским счетом
public class AccountManager
{
    private static AccountManager _instance;

    public BankAccount Account { get; }

    private AccountManager()
    {
        Account = new BankAccount();
    }

    // Метод для получения единственного экземпляра
    public static AccountManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new AccountManager();
        }
        return _instance;
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр AccountManager
        AccountManager accountManager = AccountManager.GetInstance();

        // Выполняем операции с банковским счетом
        Console.WriteLine("Initial state:");
        accountManager.Account.HandleRequest();

        Console.WriteLine("\nState after first transition:");
        accountManager.Account.HandleRequest();

        Console.WriteLine("\nState after second transition:");
        accountManager.Account.HandleRequest();

        // Здесь можно продолжить переходы между состояниями, используя тот же AccountManager.
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс AccountManager реализует шаблон Одиночка. Он гарантирует, что существует только один экземпляр менеджера, который управляет объектом BankAccount (банковский счет).
Метод GetInstance() предоставляет доступ к этому единственному экземпляру.
Состояние (State):

Интерфейс IAccountState определяет метод HandleRequest, который реализуется в конкретных состояниях счета: ActiveState, FrozenState, BlockedState.
В зависимости от состояния счета, его поведение меняется. Например, в состоянии "Активен" разрешены операции, а в "Замороженном" — нет.
Программа:

В методе Main мы получаем единственный экземпляр AccountManager и выполняем операции с банковским счетом. Каждый вызов HandleRequest изменяет состояние счета, и счет переходит в новое состояние (активен → заморожен → заблокирован и т. д.).


## 11. Одиночка + Посетитель.

## Сценарий:

Предположим, что у нас есть система для обработки различных типов документов. Мы используем Одиночку, чтобы гарантировать, что только один объект будет управлять всеми документами. Также с помощью Посетителя добавим возможность обработки различных типов документов без изменения их классов.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс для посетителя
public interface IDocumentVisitor
{
    void Visit(Report document);
    void Visit(Invoice document);
}

// Абстрактный класс документа
public abstract class Document
{
    public string Name { get; set; }

    public Document(string name)
    {
        Name = name;
    }

    public abstract void Accept(IDocumentVisitor visitor);
}

// Конкретные типы документов
public class Report : Document
{
    public Report(string name) : base(name) { }

    public override void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Invoice : Document
{
    public Invoice(string name) : base(name) { }

    public override void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Конкретный посетитель, который выполняет операции с документами
public class DocumentPrinter : IDocumentVisitor
{
    public void Visit(Report document)
    {
        Console.WriteLine($"Printing report: {document.Name}");
    }

    public void Visit(Invoice document)
    {
        Console.WriteLine($"Printing invoice: {document.Name}");
    }
}

// Одиночка для управления коллекцией документов
public class DocumentManager
{
    private static DocumentManager _instance;

    private List<Document> _documents;

    // Конструктор инициализирует список документов
    private DocumentManager()
    {
        _documents = new List<Document>();
    }

    // Метод для получения единственного экземпляра
    public static DocumentManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DocumentManager();
        }
        return _instance;
    }

    // Метод для добавления документа
    public void AddDocument(Document document)
    {
        _documents.Add(document);
    }

    // Метод для применения посетителя ко всем документам
    public void AcceptVisitor(IDocumentVisitor visitor)
    {
        foreach (var document in _documents)
        {
            document.Accept(visitor);
        }
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр DocumentManager
        DocumentManager documentManager = DocumentManager.GetInstance();

        // Добавляем документы
        documentManager.AddDocument(new Report("Annual Report"));
        documentManager.AddDocument(new Invoice("Invoice #1234"));

        // Создаем посетителя для печати документов
        IDocumentVisitor printer = new DocumentPrinter();

        // Применяем посетителя ко всем документам
        documentManager.AcceptVisitor(printer);
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс DocumentManager реализует шаблон Одиночка, чтобы гарантировать, что существует только один экземпляр менеджера документов.
Метод GetInstance() предоставляет доступ к этому единственному экземпляру.
Посетитель (Visitor):

Интерфейс IDocumentVisitor определяет методы Visit для разных типов документов (Report и Invoice).
Конкретный класс DocumentPrinter реализует метод Visit для каждого типа документа, выполняя операции (в данном случае — печать документа).
Классы Report и Invoice реализуют метод Accept, чтобы вызвать метод посетителя для себя.
Программа:

В методе Main мы создаем единственный экземпляр DocumentManager, добавляем в него два типа документов (отчёт и счёт-фактуру) и применяем к ним посетителя для печати.

## 12. Одиночка + Цепочка обязанностей.

## Сценарий:

Предположим, что у нас есть система, которая обрабатывает запросы на поддержку. Каждый запрос имеет приоритет, и его должен обработать один из агентов в цепочке. Мы будем использовать Одиночку, чтобы гарантировать, что существует только один объект, управляющий этой цепочкой.

```csharp
using System;

// Интерфейс обработчика запроса
public abstract class SupportHandler
{
    protected SupportHandler _nextHandler;

    public void SetNextHandler(SupportHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public abstract void HandleRequest(string request, int priority);
}

// Конкретные обработчики запросов
public class LowPriorityHandler : SupportHandler
{
    public override void HandleRequest(string request, int priority)
    {
        if (priority == 1)
        {
            Console.WriteLine($"LowPriorityHandler: Handling request - {request}");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request, priority);
        }
    }
}

public class MediumPriorityHandler : SupportHandler
{
    public override void HandleRequest(string request, int priority)
    {
        if (priority == 2)
        {
            Console.WriteLine($"MediumPriorityHandler: Handling request - {request}");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request, priority);
        }
    }
}

public class HighPriorityHandler : SupportHandler
{
    public override void HandleRequest(string request, int priority)
    {
        if (priority == 3)
        {
            Console.WriteLine($"HighPriorityHandler: Handling request - {request}");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request, priority);
        }
    }
}

// Одиночка для управления цепочкой обработчиков
public class SupportRequestManager
{
    private static SupportRequestManager _instance;
    private SupportHandler _firstHandler;

    private SupportRequestManager()
    {
        // Создаем цепочку обработчиков
        var lowPriority = new LowPriorityHandler();
        var mediumPriority = new MediumPriorityHandler();
        var highPriority = new HighPriorityHandler();

        // Настроим цепочку
        lowPriority.SetNextHandler(mediumPriority);
        mediumPriority.SetNextHandler(highPriority);

        _firstHandler = lowPriority;
    }

    public static SupportRequestManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SupportRequestManager();
        }
        return _instance;
    }

    // Метод для обработки запроса
    public void HandleSupportRequest(string request, int priority)
    {
        _firstHandler.HandleRequest(request, priority);
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        // Получаем единственный экземпляр SupportRequestManager
        var supportRequestManager = SupportRequestManager.GetInstance();

        // Обрабатываем запросы с разными приоритетами
        supportRequestManager.HandleSupportRequest("Password reset", 1); // Low priority
        supportRequestManager.HandleSupportRequest("System crash", 3);   // High priority
        supportRequestManager.HandleSupportRequest("Software installation", 2); // Medium priority
    }
}
```

## Пояснение к коду

Одиночка (Singleton):

Класс SupportRequestManager реализует шаблон Одиночка, чтобы гарантировать, что существует только один экземпляр менеджера запросов на поддержку.
Метод GetInstance() предоставляет доступ к этому единственному экземпляру.
Цепочка обязанностей (Chain of Responsibility):

Абстрактный класс SupportHandler представляет обработчик запроса. У него есть ссылка на следующий обработчик в цепочке и метод HandleRequest, который выполняет обработку запроса или передает его следующему обработчику.
Конкретные обработчики LowPriorityHandler, MediumPriorityHandler и HighPriorityHandler обрабатывают запросы в зависимости от их приоритета.
Каждый обработчик решает, может ли он обработать запрос, или передает его следующему обработчику.
Программа:

В методе Main мы получаем единственный экземпляр SupportRequestManager, который управляет цепочкой обработчиков.
Мы создаем запросы с разными приоритетами и передаем их менеджеру для обработки.

## 13. Одиночка + Команда.

## Сценарий:



```csharp

```

## Пояснение к коду



## 14. Одиночка + Посредник.

## Сценарий:



```csharp

```

## Пояснение к коду



## 15. Одиночка + Снимок.

## Сценарий:



```csharp

```

## Пояснение к коду



## 1. Одиночка + .

## Сценарий:



```csharp

```

## Пояснение к коду



## 1. Одиночка + .

## Сценарий:



```csharp

```

## Пояснение к коду



