using System;
using System.Collections.Generic;

// Интерфейс для обозревателя

public interface ISensorObserver
{
    void Update(double sensorData);
}

// Абстрактный класс датчика

public abstract class Sensor
{

    public string Name { get; set; }
    public double Value { get; protected set; }
    private List<ISensorObserver> _observers = new List<ISensorObserver>();

    // Конструктор
    public Sensor(string name)
    {
        Name = name;
    }

    // Подписка на уведомления

    public void Attach(ISensorObserver observer)
    {
        _observers.Add(observer);
    }

    // Отписка от уведомлений

    public void Detach(ISensorObserver observer)
    {
        _observers.Remove(observer);
    }

    // Уведомления всех наблюдателей

    protected void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(Value);
        }
    }

    // Абстрактный метод для измерений, который будет реализован в подклассах

    public abstract void Measure();

}

// Класс датчика температуры

public class TemperatureSensor : Sensor
{

    public TemperatureSensor() : base("Temperature Sensor") { }

    public override void Measure()
    {
        Value = new Random().NextDouble() * 100; // Простое случайное значение
        Console.WriteLine($"{Name} измерил температуру: {Value} °С");
        Notify();
    }
}

// Класс датчика давления

public class PressureSensor : Sensor
{
    public PressureSensor() : base("Pressure Sensor") { }

    public override void Measure()
    {
        Value = new Random().NextDouble() * 200; // Простое случайное значение
        Console.WriteLine($"{Name} измерил давление: {Value} Па");
        Notify();
    }
}

// Класс Reciever, который будет получать уведомления от датчиков

public class Reciever : ISensorObserver
{
    private string _recieverName;

    public Reciever(string name)
    {
        _recieverName = name;
    }

    public void Update(double sensorData)
    {
        Console.WriteLine($"{_recieverName} получил новые данные датчика: {sensorData} ");
    }
}

// Фабрика датчиков

public static class SensorFactory
{

    public static Sensor CreateSensor(string sensorType)
    {
        return sensorType switch
        {
            "Temperature" => new TemperatureSensor(),
            "Pressure" => new PressureSensor(),
            _ => throw new ArgumentException("Неизвестный тип датчика")
        };
    }
}

// Тестирование программы

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем датчики через фабричный метод
        Sensor tempSensor = SensorFactory.CreateSensor("Temperature");
        Sensor pressureSensor = SensorFactory.CreateSensor("Pressure");

        // Создаем получателя уведомлений
        Reciever reciever = new Reciever("Reciever 1");

        // Подписываем получателя на уведомления от датчиков

        tempSensor.Attach(reciever);
        pressureSensor.Attach(reciever);

        // Имитируем измерения

        tempSensor.Measure();
        pressureSensor.Measure();
    }
}