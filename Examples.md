## Строитель - порождающий

Предоставляет способ создания составного объекта. Он отделяет конструирование сложного объекта от его представления так, что в результате одного и того же конструирования могут получаться разные представления.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDeveloper androidDeveloper = new AndroidDeveloper();

            Director director = new Director(androidDeveloper);

            Phone samsung = director.MountFullPhone();
            Console.WriteLine(samsung.AboutPhone());

            IDeveloper iphoneDeveloper = new IphoneDeveloper();

            director.SetDeveloper(iphoneDeveloper);

            Phone iphone = director.MountOnlyPhone();
            Console.WriteLine(iphone.AboutPhone());


        }
    }

    class Phone
    {
        string data;
        public Phone() => data = "";
        public string AboutPhone() => data;

        public void AppendData(string str) => data += str;
    }

    interface IDeveloper
    {
        void CreateDisplay();
        void CreateBox();
        void SystemInstall();

        Phone GetPhone();
    }

    class AndroidDeveloper : IDeveloper
    {
        private Phone phone;
        public AndroidDeveloper() => phone = new Phone();

        public void CreateDisplay() => phone.AppendData("Произведен дисплей Samsung");
        public void CreateBox() => phone.AppendData("Произведен корпус Samsung");
        public void SystemInstall() => phone.AppendData("Установлена система Android");

        public Phone GetPhone() => phone;
    }

    class IphoneDeveloper : IDeveloper
    {
        private Phone phone;

        public IphoneDeveloper() => phone = new Phone();

        public void CreateDisplay() => phone.AppendData("Произведен дисплей Iphone");
        public void CreateBox() => phone.AppendData("Произведен корпус Iphone");
        public void SystemInstall() => phone.AppendData("Установлена система Iphone");

        public Phone GetPhone() => phone;

    }

    class Director
    {
        private IDeveloper developer;

        public Director(IDeveloper developer) => this.developer = developer;

        public void SetDeveloper(IDeveloper developer) => this.developer = developer;

        public Phone MountOnlyPhone()
        {
            developer.CreateBox();
            developer.CreateDisplay();
            return developer.GetPhone();
        }

        public Phone MountFullPhone()
        {
            developer.CreateBox();
            developer.CreateDisplay();
            developer.SystemInstall();
            return developer.GetPhone();
        }

    }
}
```

## Фабричный метод - порождающий шаблон проектирования.

Этот шаблон предоставляет дочерним классам интерфейс для создания экземпляров некоторого класса. В момент создания наследники могут определить какой класс создавать. Данный шаблон делегирует создание объектов наследникам родительского класса.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWorkShop creator = new CarWorkShop();
            IProduction car = creator.Create();

            creator = new TruckWorkShop();
            IProduction truck = creator.Create();

            car.Release();
            truck.Release();
        }
    }

    interface IProduction
    {
        void Release();
    }

    class Car : IProduction
    {
        public void Release() => Console.WriteLine("Выпущена новая машина");
    }

    class Truck : IProduction
    {
        public void Release() => Console.WriteLine("Выпущен новый трак");
    }

    interface IWorkShop
    {
        IProduction Create();
    }

    class CarWorkShop : IWorkShop
    {
        public IProduction Create() => new Car();
    }

    class TruckWorkShop : IWorkShop
    {
        public IProduction Create() => new Truck();
    }
}
```

## Абстрактная фабрика - порождающий шаблон.

Он предоставляет интерфейс для создания семейств взаимосвязанных или взаимозависимых объектов не специфицируя их конкретных классов. Шаблон применяется тогда, когда программа должна быть не зависима от процесса и типов создаваемых новых объектов, а также когда необходимо создать семейство или группу взаимосвязанных объектов.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFactory jFactory = new JapaneseFactory();

            IEngine jengine = jFactory.CreateEngine();
            ICar jCar = jFactory.CreateCar();

            jCar.ReleaseCar(jengine);

            IFactory rFactory = new RussianFactory();
            IEngine rengine = rFactory.CreateEngine();
            ICar rCar = rFactory.CreateCar();

            rCar.ReleaseCar(rengine);
        }
    }

    interface IEngine
    {
        void ReleaseEngine();
    }

    class JapaneseEngine : IEngine
    {
        public void ReleaseEngine() => Console.WriteLine("японский двигатель");
    }

    class RussianEngine : IEngine
    {
        public void ReleaseEngine() => Console.WriteLine("русский двигатель");
    }

    interface ICar
    {
        void ReleaseCar(IEngine engine);
    }

    class JapaneseCar : ICar
    {
        public void ReleaseCar(IEngine engine)
        {
            Console.WriteLine("Собрали японский автомобиль");
            engine.ReleaseEngine();
        }
    }

    class RussianCar : ICar
    {
        public void ReleaseCar(IEngine engine)
        {
            Console.WriteLine("Собрали русский автомобиль");
            engine.ReleaseEngine();
        }
    }

    interface IFactory
    {
        IEngine CreateEngine();
        ICar CreateCar();
    }

    class JapaneseFactory : IFactory
    {
        public ICar CreateCar() => new JapaneseCar();
        public IEngine CreateEngine() => new JapaneseEngine();
    }

    class RussianFactory : IFactory
    {
        public ICar CreateCar() => new RussianCar();
        public IEngine CreateEngine() => new RussianEngine();
    }
}
```

## Одиночка - порождающий

Который гарантирует что в приложении будет единственный экземпляр некоторого класса и предоставляет глобальную точку доступа к этому экземпляру. Однако глобальные объекты могут быть вредны в некоторых случаях, что приводит к созданию немасшабируемого проекта.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DataBaseHelper connecntion = new DataBaseHelper();
            DataBaseHelper.GetConnecntion().InsertData("123");
            Console.WriteLine("Выборка данных из БД " + DataBaseHelper.GetConnecntion().SelectData());
        }
    }

    class DataBaseHelper
    {
        private string _data;
        private static DataBaseHelper dataBaseConnecntion;

        private DataBaseHelper() => Console.WriteLine("Подключение к БД");

        public static DataBaseHelper GetConnecntion()
        {
            if (dataBaseConnecntion == null)
                dataBaseConnecntion = new DataBaseHelper();
            return dataBaseConnecntion;
        }

        public string SelectData() => _data;

        public void InsertData(string data)
        {
            Console.WriteLine($"Новые данные: {data}, внесены в БД");
            _data = data;
        }

    }
}
```

## Прототип - порождающий

Позволяет копировать объекты, не вдаваясь в подробнисти его реализации. Позволяет клонировать объекты не привязываюсь к их конкретным классам. Уменьшает повторяющийся код при инициализации объекты. Однако составные объекты, имеющие ссылки на другие классы, клонировать сложнее.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAnimal sheepDonor = new Sheep();
            sheepDonor.SetName("Долли");

            IAnimal sheepClone = sheepDonor.Clone();

            Console.WriteLine(sheepDonor.GetName());
            Console.WriteLine(sheepClone.GetName());
        }
    }

    interface IAnimal
    {
        void SetName(string name);
        string GetName();
        IAnimal Clone();
    }

    class Sheep : IAnimal
    {
        private string name;

        public Sheep() { }
        private Sheep(Sheep donor) => this.name = donor.name;

        public void SetName(String name) => this.name = name;

        public string GetName() => name;

        public IAnimal Clone() => new Sheep(this);
    }

}
```

## Адаптер (Реализация на уровне объектов) - структурный шаблон проектирования.

Позволяет объектам с несовместимыми интерфейсами работать вместе.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float kg = 55.0f;
            float lb = 55.0f;

            IScales rscales = new RussianScales(kg);
            IScales bscales = new AdapterForBritishScales(new BritishScales(lb));

            Console.WriteLine(rscales.GetWeight());
            Console.WriteLine(bscales.GetWeight());
        }
    }

    interface IScales
    {
        float GetWeight();
    }

    class RussianScales: IScales
    {
        private float _currentWeight;
        public RussianScales(float cw) => this._currentWeight = cw;
        public float GetWeight() => _currentWeight;
    }

    class BritishScales
    {
        private float _currentWeight;
        public BritishScales(float cw) => this._currentWeight = cw;
        public float GetWeight() => _currentWeight;
    }

    class AdapterForBritishScales : IScales
    {
        BritishScales _britishScales;

        public AdapterForBritishScales(BritishScales britishScales) => this._britishScales = britishScales;
        public float GetWeight() => _britishScales.GetWeight() * 0.453f;
    }
}
```

## Адаптер (Реализация на уровне класса) - структурный.

(тоже самое что и в реализации на уровне объектов), но и предназначен для организации использования функции объекта, недоступного для модификации, через специально созданный интерфейс.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            IScales rscales = new RussianScales(55.0f);
            IScales bscales = new AdapterForBritishScales(55.0f);

            Console.WriteLine(rscales.GetWeight());
            Console.WriteLine(bscales.GetWeight());

            rscales.Adjust();
            bscales.Adjust();
        }
    }

    interface IScales
    {
        float GetWeight();
        void Adjust();
    }

    class RussianScales : IScales
    {
        private float _currentWeight;
        public RussianScales(float cw) => this._currentWeight = cw;
        public void Adjust() => Console.WriteLine("Регулировка российских весов");
        public float GetWeight() { return _currentWeight; }

    }

    class BritishScales
    {
        private float _currentWeight;
        public BritishScales(float cw) => this._currentWeight = cw;
        public float GetWeight() => _currentWeight;

        protected void Adjust() => Console.WriteLine("Регулировка британских весов");
    }

    class AdapterForBritishScales : BritishScales, IScales
    {
        public AdapterForBritishScales(float cw) : base(cw) { }
        float IScales.GetWeight()
        {
            return base.GetWeight() * 0.453f;
        }

        void IScales.Adjust()
        {
            base.Adjust();
            Console.WriteLine("В методе ругулировки Adjust() адаптера");
        }
    }
}
```

## Мост - структурный

Используется в проектировании ПО, разделяя абстракцию и реализацию так, чтобы они могли изменяться независимо. Мост использует инкапсуляцию, агрегирование и может использовать наследования для того, чтобы разделить ответственность между классами.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sender sender = new EmailSender(new DataBaseReader());
            sender.Send();

            sender.SetDataReader(new FileReader());
            sender.Send();

            sender = new TelegramBotSender(new DataBaseReader());
            sender.Send();
            sender.SetDataReader(new FileReader());
            sender.Send();
        }
    }

    interface IDataReader
    {
        void Read();
    }

    class DataBaseReader : IDataReader
    {
        public void Read() => Console.WriteLine("Данные из БД ");
    }

    class FileReader: IDataReader
    {
        public void Read() => Console.WriteLine("Данные из файла ");
    }

    abstract class Sender
    {
        protected IDataReader _reader;

        public Sender(IDataReader dr) => _reader = dr;

        public void SetDataReader(IDataReader dr) => _reader = dr;

        public abstract void Send();
    }

    class EmailSender : Sender
    {
        public EmailSender(IDataReader dr) : base(dr) { }

        public override void Send()
        {
            _reader.Read();
            Console.WriteLine("Отправлены при помощи EMail");
        }
    }

    class TelegramBotSender : Sender
    {
        public TelegramBotSender(IDataReader dr) : base(dr) { }

        public override void Send()
        {
            _reader.Read();
            Console.WriteLine("Отправлены при помощи TelegramBot");
        }
    }
}
```

## Компоновщик - структурный

Объединяет объекты в древовидную структуры для представления иерархии. Позволяет клиента обращаться к отдельным объектам и группам объектов одинаково.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item file = new DropDownItem("Файл-> ");
            Item create = new DropDownItem("Создать-> ");

            Item open = new DropDownItem("Открыть-> ");
            Item exit = new ClickableItem("Выход-> ");

            file.Add(create);
            file.Add(open);
            file.Add(exit);

            Item project = new ClickableItem("Проект...");
            Item repo = new ClickableItem("Репозиторий...");

            create.Add(project);
            create.Add(repo);

            Item solution = new ClickableItem("Решение...");
            Item folder = new ClickableItem("Папка...");

            open.Add(solution);
            open.Add(folder);

            file.Display();
            Console.WriteLine();

            file.Remove(create);

            file.Display();

        }
    }

    abstract class Item
    {
        protected string itemName;
        protected string ownerName;
        public void SetOwner(string o) => ownerName = o;

        public Item(string name) => itemName = name;

        public virtual void Add(Item subItem) { }

        public virtual void Remove(Item item) { }

        public abstract void Display();
    }

    class ClickableItem : Item
    {
        public ClickableItem(string name) : base(name) { }
        public override void Add(Item subItem)
        {
            throw new Exception();
        }
        public override void Remove(Item subItem)
        {
            throw new Exception();
        }

        public override void Display()
        {
            Console.WriteLine(itemName);
        }

    }

    class DropDownItem : Item
    {
        private List<Item> children;

        public DropDownItem(string name) : base(name)
        {
            children = new List<Item>();
        }

        public override void Add(Item subItem)
        {
            subItem.SetOwner(itemName);
            children.Add(subItem);
        }
        public override void Remove(Item subItem) => children.Remove(subItem);
        
        public override void Display()
        {
            foreach (Item item in children)
            {
                if (ownerName != null)
                    Console.WriteLine(ownerName + itemName);
                item.Display();
            }
        }
    }
}
```

## Декоратор - структурный

Предназначен для динамического подключения объекту дополнительного поведения. Декоратор предоставляет гибкую альтернативу приктики создания подклассов с целью расширения функциональности.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProcessor transmitter = new Transmitter("12345");
            transmitter.Process();
            Console.WriteLine();
            
            Shell hammingCoder = new HammingCoder(transmitter);
            hammingCoder.Process();
            Console.WriteLine();

            Shell encoder = new EncryptoCoder(hammingCoder);
            encoder.Process();

        }
    }

    interface IProcessor
    {
        void Process();
    }

    class Transmitter : IProcessor
    {
        private string data;
        public Transmitter(string data) => this.data = data;
        public void Process() => Console.WriteLine("Данные " +data+ " переданы по каналу связи");
    }

    abstract class Shell : IProcessor
    {
        protected IProcessor processor;
        public Shell(IProcessor pr) => processor = pr;
        public virtual void Process() => processor.Process();
    }

    class HammingCoder : Shell
    {
        public HammingCoder(IProcessor pr) : base(pr) { }
        public override void Process()
        {
            Console.WriteLine("Наложен помехоустойчивый код Хамминга->");
            processor.Process();
        }
    }

    class EncryptoCoder : Shell
    {
        public EncryptoCoder(IProcessor pr) : base(pr) { }
        public override void Process()
        {
            Console.WriteLine("Шифрование данных->");
            processor.Process();
        }
    }

}
```

## Фасад - структурный

Позволяет скрыть сложности системы путем сведений всех возможных внешних вызовов к одному объекту, делегирующему их соответствующим объектам системы.

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MarketPlace marketPlace = new MarketPlace();
            marketPlace.ProductReceipt();
            Console.WriteLine(new string('-', 20));

            marketPlace.ProductRelease();
        }
    }

    class ProviderCommunication
    {
        public void Receive() => Console.WriteLine("Получение продукции от производителя ");
        public void Payment() => Console.WriteLine("Оплата поставщику с удержанием комиссии за продажу продукции");

    }

    class Site
    {
        public void Placement() => Console.WriteLine("Размещение на сайте");
        public void Del() => Console.WriteLine("Удаление с сайта");
    }
    class Database
    {
        public void Insert() => Console.WriteLine("Запись в базу данных");
        public void Del() => Console.WriteLine("Удаление из базы данных");
    }

    class MarketPlace
    {
        private ProviderCommunication providerCommunication;
        private Site site;
        private Database database;

        public MarketPlace()
        {
            providerCommunication = new ProviderCommunication();
            site = new Site();
            database = new Database();
        }

        public void ProductReceipt()
        {
            providerCommunication.Receive();
            site.Placement();
            database.Insert();
        }

        public void ProductRelease()
        {
            providerCommunication.Payment();
            site.Del();
            database.Del();
        }

    }

}
```

## Легковес - структурный

Позволяет вместить большее количество объектов в отведенную оперативную память. Экономит память, выделяя и сохраняя общие параметры объектов.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс Легковеса
public interface IShape
{
    void Draw(int x, int y);
}

// Конкретный Легковес (класс Круга)
public class Circle : IShape
{
    private string _color; // Внутреннее состояние (общий цвет)

    public Circle(string color)
    {
        _color = color;
    }

    public void Draw(int x, int y)
    {
        Console.WriteLine($"Drawing Circle: Color = {_color}, X = {x}, Y = {y}");
    }
}

// Фабрика Легковесов
public class ShapeFactory
{
    private Dictionary<string, IShape> _shapes = new Dictionary<string, IShape>();

    public IShape GetShape(string color)
    {
        if (!_shapes.ContainsKey(color))
        {
            _shapes[color] = new Circle(color);
            Console.WriteLine($"Creating a new circle with color: {color}");
        }
        return _shapes[color];
    }
}

// Программа
public class Program
{
    public static void Main()
    {
        ShapeFactory shapeFactory = new ShapeFactory();

        // Создаем и используем легковесные объекты
        IShape redCircle1 = shapeFactory.GetShape("Red");
        redCircle1.Draw(10, 20);

        IShape redCircle2 = shapeFactory.GetShape("Red");
        redCircle2.Draw(30, 40);

        IShape blueCircle = shapeFactory.GetShape("Blue");
        blueCircle.Draw(50, 60);

        IShape redCircle3 = shapeFactory.GetShape("Red");
        redCircle3.Draw(70, 80);
        
        Console.WriteLine("Total unique circles created: " + shapeFactory.ShapesCount);
    }
}
```

## Заместитель

Используется для создания суррогатного объекта, который управляет доступом к другому объекту. Он позволяет контролировать доступ к объекту, например, добавляя к нему кэширование, ленивую инициализацию, контроль доступа или ведение логов.

```csharp
using System;

// Интерфейс для отображения изображения
public interface IImage
{
    void Display();
}

// Реальный объект, представляющий изображение
public class RealImage : IImage
{
    private string _fileName;

    public RealImage(string fileName)
    {
        _fileName = fileName;
        LoadImageFromDisk();
    }

    // Симуляция загрузки изображения с диска
    private void LoadImageFromDisk()
    {
        Console.WriteLine($"Loading image from disk: {_fileName}");
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image: {_fileName}");
    }
}

// Заместитель для изображения
public class ProxyImage : IImage
{
    private RealImage _realImage;
    private string _fileName;

    public ProxyImage(string fileName)
    {
        _fileName = fileName;
    }

    public void Display()
    {
        // Ленивое создание реального объекта
        if (_realImage == null)
        {
            _realImage = new RealImage(_fileName);
        }
        _realImage.Display();
    }
}

// Тестирование программы
public class Program
{
    public static void Main()
    {
        // Создаем изображение через заместителя
        IImage image1 = new ProxyImage("photo1.jpg");
        IImage image2 = new ProxyImage("photo2.jpg");

        // Изображение будет загружено только при вызове Display
        Console.WriteLine("Displaying image 1:");
        image1.Display(); // Загружает и отображает

        Console.WriteLine("\nDisplaying image 1 again:");
        image1.Display(); // Только отображает

        Console.WriteLine("\nDisplaying image 2:");
        image2.Display(); // Загружает и отображает
    }
}
```

## Цепочка обязанностей

Позволяет передавать запрос по цепочке обработчиков, где каждый обработчик решает, будет ли он обрабатывать запрос или передаст его следующему обработчику в цепи. Это позволяет снизить связанность между отправителем запроса и его получателями.

```csharp
using System;

// Абстрактный класс для обработчиков
abstract class SupportHandler
{
    protected SupportHandler _nextHandler;

    // Устанавливаем следующий обработчик в цепочке
    public void SetNextHandler(SupportHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    // Метод для обработки запроса
    public abstract void HandleRequest(int complexity);
}

// Обработчик 1: Оператор
class OperatorHandler : SupportHandler
{
    public override void HandleRequest(int complexity)
    {
        if (complexity <= 1)
        {
            Console.WriteLine("Operator: I can handle this request.");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine("Operator: Passing request to next handler...");
            _nextHandler.HandleRequest(complexity);
        }
    }
}

// Обработчик 2: Менеджер
class ManagerHandler : SupportHandler
{
    public override void HandleRequest(int complexity)
    {
        if (complexity <= 3)
        {
            Console.WriteLine("Manager: I can handle this request.");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine("Manager: Passing request to next handler...");
            _nextHandler.HandleRequest(complexity);
        }
    }
}

// Обработчик 3: Директор
class DirectorHandler : SupportHandler
{
    public override void HandleRequest(int complexity)
    {
        if (complexity <= 5)
        {
            Console.WriteLine("Director: I can handle this request.");
        }
        else
        {
            Console.WriteLine("Director: This request is too complex for us.");
        }
    }
}

// Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем обработчиков
        SupportHandler operatorHandler = new OperatorHandler();
        SupportHandler managerHandler = new ManagerHandler();
        SupportHandler directorHandler = new DirectorHandler();

        // Устанавливаем цепочку обязанностей
        operatorHandler.SetNextHandler(managerHandler);
        managerHandler.SetNextHandler(directorHandler);

        // Тестируем запросы с разной сложностью
        int[] requestComplexities = { 1, 2, 4, 6 };
        
        foreach (int complexity in requestComplexities)
        {
            Console.WriteLine($"\nHandling request with complexity {complexity}:");
            operatorHandler.HandleRequest(complexity);
        }
    }
}
```

## Команда

Используется для инкапсуляции запроса как объекта, что позволяет параметризовать клиентские объекты с различными запросами, очередями запросов или логами запросов. Это также позволяет поддерживать отмену операций.

```csharp
using System;
using System.Collections.Generic;

// 1. Интерфейс команды
interface ICommand
{
    void Execute();
    void Undo();
}

// 2. Класс Receiver — Свет
class Light
{
    public void TurnOn()
    {
        Console.WriteLine("The light is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("The light is OFF");
    }
}

// 3. Конкретные команды для включения и выключения света
class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }

    public void Undo()
    {
        _light.TurnOff();
    }
}

class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }

    public void Undo()
    {
        _light.TurnOn();
    }
}

// 4. Invoker — Пульт управления
class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }

    public void PressUndo()
    {
        _command.Undo();
    }
}

// 5. Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем Receiver (получатель команды)
        Light livingRoomLight = new Light();

        // Создаем команды
        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);

        // Создаем Invoker (вызыватель команды)
        RemoteControl remote = new RemoteControl();

        // Включаем свет
        remote.SetCommand(lightOn);
        remote.PressButton();  // Вывод: The light is ON
        remote.PressUndo();    // Вывод: The light is OFF

        // Выключаем свет
        remote.SetCommand(lightOff);
        remote.PressButton();  // Вывод: The light is OFF
        remote.PressUndo();    // Вывод: The light is ON
    }
}
```

## Итератор

Используется для последовательного доступа к элементам коллекции без раскрытия её внутренней структуры. Он позволяет клиенту перемещаться по элементам агрегированного объекта (например, списка или массива) без необходимости знать, как эти элементы организованы.

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

// 1. Класс Book, представляющий объект коллекции
class Book
{
    public string Title { get; set; }

    public Book(string title)
    {
        Title = title;
    }
}

// 2. Интерфейс итератора
interface IBookIterator
{
    bool HasNext();
    Book Next();
}

// 3. Интерфейс агрегата
interface IBookCollection
{
    IBookIterator CreateIterator();
}

// 4. Реализация коллекции книг
class BookCollection : IBookCollection
{
    private List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public int Count => _books.Count;

    public Book GetBook(int index)
    {
        if (index < _books.Count)
            return _books[index];
        return null;
    }

    // Создание итератора для данной коллекции
    public IBookIterator CreateIterator()
    {
        return new BookIterator(this);
    }
}

// 5. Реализация итератора для коллекции книг
class BookIterator : IBookIterator
{
    private BookCollection _bookCollection;
    private int _currentIndex = 0;

    public BookIterator(BookCollection bookCollection)
    {
        _bookCollection = bookCollection;
    }

    public bool HasNext()
    {
        return _currentIndex < _bookCollection.Count;
    }

    public Book Next()
    {
        return _bookCollection.GetBook(_currentIndex++);
    }
}

// 6. Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем коллекцию книг
        BookCollection books = new BookCollection();
        books.AddBook(new Book("The Great Gatsby"));
        books.AddBook(new Book("1984"));
        books.AddBook(new Book("To Kill a Mockingbird"));
        books.AddBook(new Book("The Catcher in the Rye"));

        // Создаем итератор для коллекции книг
        IBookIterator iterator = books.CreateIterator();

        // Обходим коллекцию с помощью итератора
        Console.WriteLine("Books in the collection:");
        while (iterator.HasNext())
        {
            Book book = iterator.Next();
            Console.WriteLine($"- {book.Title}");
        }
    }
}
```

## Посредник 

Используется для упрощения взаимодействия между объектами, предоставляя централизованную точку общения. Вместо того чтобы объекты напрямую взаимодействовали друг с другом, они используют объект-посредник для обмена данными и командами. Это снижает связность между компонентами и делает систему более гибкой.

```csharp
using System;
using System.Collections.Generic;

// 1. Интерфейс Посредника
interface IChatMediator
{
    void SendMessage(string message, User sender);
    void AddUser(User user);
}

// 2. Класс Конкретного Посредника
class ChatMediator : IChatMediator
{
    private List<User> _users = new List<User>();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in _users)
        {
            // Сообщение отправляется всем, кроме отправителя
            if (user != sender)
            {
                user.ReceiveMessage(message);
            }
        }
    }
}

// 3. Абстрактный класс Пользователя
abstract class User
{
    protected IChatMediator _mediator;
    protected string _name;

    public User(IChatMediator mediator, string name)
    {
        _mediator = mediator;
        _name = name;
    }

    public abstract void SendMessage(string message);
    public abstract void ReceiveMessage(string message);
}

// 4. Конкретный Пользователь
class ChatUser : User
{
    public ChatUser(IChatMediator mediator, string name) : base(mediator, name) { }

    public override void SendMessage(string message)
    {
        Console.WriteLine($"{_name} отправляет сообщение: {message}");
        _mediator.SendMessage(message, this);
    }

    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"{_name} получил сообщение: {message}");
    }
}

// 5. Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем посредника (чат-комнату)
        IChatMediator chatMediator = new ChatMediator();

        // Создаем пользователей
        User user1 = new ChatUser(chatMediator, "Alice");
        User user2 = new ChatUser(chatMediator, "Bob");
        User user3 = new ChatUser(chatMediator, "Charlie");

        // Регистрируем пользователей в чате
        chatMediator.AddUser(user1);
        chatMediator.AddUser(user2);
        chatMediator.AddUser(user3);

        // Отправка сообщений
        user1.SendMessage("Привет всем!");
        user2.SendMessage("Привет, Alice!");
        user3.SendMessage("Привет, ребята!");

        Console.ReadLine();
    }
}
```

## Снимок 

Используется для сохранения и восстановления состояния объекта без нарушения инкапсуляции. Это полезно, когда нужно сохранить состояния объектов, чтобы впоследствии можно было их восстановить, например, для функции "отмена" (undo).

```csharp
using System;
using System.Collections.Generic;

// 1. Класс Снимка (Memento)
class TextMemento
{
    public string Text { get; private set; }

    public TextMemento(string text)
    {
        Text = text;
    }
}

// 2. Класс Создателя (Originator)
class TextEditor
{
    private string _text;

    public void SetText(string text)
    {
        _text = text;
    }

    public string GetText()
    {
        return _text;
    }

    // Создать снимок
    public TextMemento Save()
    {
        return new TextMemento(_text);
    }

    // Восстановить состояние из снимка
    public void Restore(TextMemento memento)
    {
        _text = memento.Text;
    }
}

// 3. Класс Хранителя (Caretaker)
class TextHistory
{
    private Stack<TextMemento> _history = new Stack<TextMemento>();

    public void Save(TextMemento memento)
    {
        _history.Push(memento);
    }

    public TextMemento Undo()
    {
        if (_history.Count > 0)
        {
            return _history.Pop();
        }
        return null;
    }
}

// 4. Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        TextEditor editor = new TextEditor();
        TextHistory history = new TextHistory();

        // Начальное состояние
        editor.SetText("Привет, мир!");
        Console.WriteLine("Текущий текст: " + editor.GetText());

        // Сохраняем состояние
        history.Save(editor.Save());

        // Изменяем текст
        editor.SetText("Измененный текст.");
        Console.WriteLine("Текущий текст: " + editor.GetText());

        // Сохраняем состояние
        history.Save(editor.Save());

        // Еще одно изменение
        editor.SetText("Еще одно изменение.");
        Console.WriteLine("Текущий текст: " + editor.GetText());

        // Отмена последнего изменения
        editor.Restore(history.Undo());
        Console.WriteLine("После отмены: " + editor.GetText());

        // Отмена еще одного изменения
        editor.Restore(history.Undo());
        Console.WriteLine("После второй отмены: " + editor.GetText());

        Console.ReadLine();
    }
}
```

## Наблюдатель

Используется для создания механизма подписки, который позволяет одним объектам отслеживать изменения в других объектах. Это полезно, когда изменения в одном объекте должны автоматически вызывать изменения в других связанных объектах.

```csharp
using System;
using System.Collections.Generic;

// Интерфейс Наблюдателя
interface IObserver
{
    void Update(float temperature, float humidity, float pressure);
}

// Интерфейс Субъекта (тот, на кого подписываются)
interface IWeatherStation
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

// Класс Метеостанции (Субъект)
class WeatherStation : IWeatherStation
{
    private List<IObserver> observers;
    private float temperature;
    private float humidity;
    private float pressure;

    public WeatherStation()
    {
        observers = new List<IObserver>();
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(temperature, humidity, pressure);
        }
    }

    // Метод для обновления данных о погоде
    public void SetMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        NotifyObservers();
    }
}

// Класс Смартфона (Конкретный Наблюдатель)
class Smartphone : IObserver
{
    private string owner;

    public Smartphone(string owner)
    {
        this.owner = owner;
    }

    public void Update(float temperature, float humidity, float pressure)
    {
        Console.WriteLine($"Уведомление для {owner}: Температура = {temperature}°C, Влажность = {humidity}%, Давление = {pressure} мм рт. ст.");
    }
}

// Класс Компьютера (Конкретный Наблюдатель)
class Computer : IObserver
{
    private string location;

    public Computer(string location)
    {
        this.location = location;
    }

    public void Update(float temperature, float humidity, float pressure)
    {
        Console.WriteLine($"Уведомление на компьютере в {location}: Температура = {temperature}°C, Влажность = {humidity}%, Давление = {pressure} мм рт. ст.");
    }
}

// Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем метеостанцию
        WeatherStation station = new WeatherStation();

        // Создаем наблюдателей
        Smartphone phone1 = new Smartphone("Алексей");
        Smartphone phone2 = new Smartphone("Мария");
        Computer computer1 = new Computer("Офис");

        // Регистрируем наблюдателей
        station.RegisterObserver(phone1);
        station.RegisterObserver(phone2);
        station.RegisterObserver(computer1);

        // Обновляем данные о погоде
        station.SetMeasurements(25.5f, 60.0f, 1012.0f);
        Console.WriteLine();

        // Удаляем одного наблюдателя
        station.RemoveObserver(phone2);

        // Обновляем данные о погоде снова
        station.SetMeasurements(18.2f, 75.0f, 1008.0f);
    }
}
```

## Состояние

Используется для управления поведением объекта в зависимости от его состояния. Этот шаблон позволяет объекту изменять свое поведение при изменении внутреннего состояния, словно объект изменяет свой класс.

```csharp
using System;

// Интерфейс состояния
interface ITicketState
{
    void InsertMoney();
    void EjectMoney();
    void DispenseTicket();
}

// Контекст - Автомат по продаже билетов
class TicketMachine
{
    public ITicketState NoTicketState { get; private set; }
    public ITicketState TicketAvailableState { get; private set; }
    public ITicketState TicketSoldState { get; private set; }

    private ITicketState currentState;
    private int ticketCount;

    public TicketMachine(int numberOfTickets)
    {
        NoTicketState = new NoTicketState(this);
        TicketAvailableState = new TicketAvailableState(this);
        TicketSoldState = new TicketSoldState(this);

        ticketCount = numberOfTickets;
        currentState = ticketCount > 0 ? TicketAvailableState : NoTicketState;
    }

    public void SetState(ITicketState state)
    {
        currentState = state;
    }

    public void InsertMoney()
    {
        currentState.InsertMoney();
    }

    public void EjectMoney()
    {
        currentState.EjectMoney();
    }

    public void DispenseTicket()
    {
        currentState.DispenseTicket();
    }

    public void ReleaseTicket()
    {
        if (ticketCount > 0)
        {
            Console.WriteLine("Выдача билета...");
            ticketCount--;
        }
    }

    public int GetTicketCount()
    {
        return ticketCount;
    }
}

// Состояние: Нет билетов
class NoTicketState : ITicketState
{
    private TicketMachine machine;

    public NoTicketState(TicketMachine machine)
    {
        this.machine = machine;
    }

    public void InsertMoney()
    {
        Console.WriteLine("Билетов нет в наличии.");
    }

    public void EjectMoney()
    {
        Console.WriteLine("Деньги не были внесены.");
    }

    public void DispenseTicket()
    {
        Console.WriteLine("Билет не может быть выдан, так как билетов нет.");
    }
}

// Состояние: Билеты доступны
class TicketAvailableState : ITicketState
{
    private TicketMachine machine;

    public TicketAvailableState(TicketMachine machine)
    {
        this.machine = machine;
    }

    public void InsertMoney()
    {
        Console.WriteLine("Деньги приняты.");
        machine.SetState(machine.TicketSoldState);
    }

    public void EjectMoney()
    {
        Console.WriteLine("Деньги возвращены.");
    }

    public void DispenseTicket()
    {
        Console.WriteLine("Сначала нужно внести деньги.");
    }
}

// Состояние: Билет куплен
class TicketSoldState : ITicketState
{
    private TicketMachine machine;

    public TicketSoldState(TicketMachine machine)
    {
        this.machine = machine;
    }

    public void InsertMoney()
    {
        Console.WriteLine("Подождите, пока ваш билет выдаётся.");
    }

    public void EjectMoney()
    {
        Console.WriteLine("Извините, деньги не могут быть возвращены после покупки.");
    }

    public void DispenseTicket()
    {
        machine.ReleaseTicket();
        if (machine.GetTicketCount() > 0)
        {
            machine.SetState(machine.TicketAvailableState);
        }
        else
        {
            Console.WriteLine("Билеты закончились.");
            machine.SetState(machine.NoTicketState);
        }
    }
}

// Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        TicketMachine machine = new TicketMachine(2);

        // Попробуем купить билет
        machine.InsertMoney();
        machine.DispenseTicket();

        // Покупаем ещё один билет
        machine.InsertMoney();
        machine.DispenseTicket();

        // Пытаемся купить билет, когда билетов нет
        machine.InsertMoney();
        machine.DispenseTicket();
    }
}
```

## Стратегия

Позволяет определить семейство алгоритмов, инкапсулировать каждый из них и сделать их взаимозаменяемыми. Это позволяет выбрать алгоритм во время выполнения, не изменяя код, который его использует.

```csharp
using System;

// Интерфейс стратегии
interface IDeliveryStrategy
{
    decimal CalculateShippingCost(decimal orderAmount);
}

// Конкретная стратегия: Курьерская доставка
class CourierDelivery : IDeliveryStrategy
{
    public decimal CalculateShippingCost(decimal orderAmount)
    {
        Console.WriteLine("Используется курьерская доставка");
        return 50 + (orderAmount * 0.05m); // фиксированная стоимость + процент от суммы заказа
    }
}

// Конкретная стратегия: Почтовая служба
class PostalServiceDelivery : IDeliveryStrategy
{
    public decimal CalculateShippingCost(decimal orderAmount)
    {
        Console.WriteLine("Используется почтовая служба");
        return 20 + (orderAmount * 0.02m); // фиксированная стоимость + процент от суммы заказа
    }
}

// Конкретная стратегия: Самовывоз
class PickupDelivery : IDeliveryStrategy
{
    public decimal CalculateShippingCost(decimal orderAmount)
    {
        Console.WriteLine("Используется самовывоз");
        return 0; // самовывоз бесплатный
    }
}

// Контекст: Класс заказа
class Order
{
    private IDeliveryStrategy _deliveryStrategy;

    public Order(IDeliveryStrategy deliveryStrategy)
    {
        _deliveryStrategy = deliveryStrategy;
    }

    public void SetDeliveryStrategy(IDeliveryStrategy deliveryStrategy)
    {
        _deliveryStrategy = deliveryStrategy;
    }

    public void CalculateTotalCost(decimal orderAmount)
    {
        decimal shippingCost = _deliveryStrategy.CalculateShippingCost(orderAmount);
        Console.WriteLine($"Общая стоимость доставки: {shippingCost} рублей\n");
    }
}

// Тестирование программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем заказ и выбираем стратегию доставки
        Order order = new Order(new CourierDelivery());
        order.CalculateTotalCost(1000);

        // Меняем стратегию на почтовую службу
        order.SetDeliveryStrategy(new PostalServiceDelivery());
        order.CalculateTotalCost(1000);

        // Меняем стратегию на самовывоз
        order.SetDeliveryStrategy(new PickupDelivery());
        order.CalculateTotalCost(1000);
    }
}
```