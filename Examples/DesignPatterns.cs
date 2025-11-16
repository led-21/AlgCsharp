using System;
using System.Collections.Generic;

namespace DesignPatternsExamples;

// ==================== CREATIONAL PATTERNS ====================

// 1. Singleton Pattern - Ensures a class has only one instance
public class Singleton
{
    private static Singleton? _instance;
    private static readonly object _lock = new();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            lock (_lock)
            {
                return _instance ??= new Singleton();
            }
        }
    }

    public void DoSomething() => Console.WriteLine("Singleton instance doing something");
}

// 2. Factory Pattern - Creates objects without specifying exact classes
public interface IProduct
{
    void Use();
}

public class ConcreteProductA : IProduct
{
    public void Use() => Console.WriteLine("Using Product A");
}

public class ConcreteProductB : IProduct
{
    public void Use() => Console.WriteLine("Using Product B");
}

public class ProductFactory
{
    public IProduct CreateProduct(string type)
    {
        return type switch
        {
            "A" => new ConcreteProductA(),
            "B" => new ConcreteProductB(),
            _ => throw new ArgumentException("Invalid product type")
        };
    }
}

// 3. Builder Pattern - Constructs complex objects step by step
public class Car
{
    public string Engine { get; set; } = "";
    public string Wheels { get; set; } = "";
    public string Body { get; set; } = "";

    public void Show() => Console.WriteLine($"Car: Engine={Engine}, Wheels={Wheels}, Body={Body}");
}

public interface ICarBuilder
{
    void BuildEngine();
    void BuildWheels();
    void BuildBody();
    Car GetCar();
}

public class SportsCarBuilder : ICarBuilder
{
    private Car _car = new();

    public void BuildEngine() => _car.Engine = "V8";
    public void BuildWheels() => _car.Wheels = "Sport";
    public void BuildBody() => _car.Body = "Coupe";

    public Car GetCar() => _car;
}

public class CarDirector
{
    public Car Construct(ICarBuilder builder)
    {
        builder.BuildEngine();
        builder.BuildWheels();
        builder.BuildBody();
        return builder.GetCar();
    }
}

// ==================== STRUCTURAL PATTERNS ====================

// 4. Adapter Pattern - Allows incompatible interfaces to work together
public interface ITarget
{
    void Request();
}

public class Adaptee
{
    public void SpecificRequest() => Console.WriteLine("Adaptee specific request");
}

public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee) => _adaptee = adaptee;

    public void Request() => _adaptee.SpecificRequest();
}

// 5. Decorator Pattern - Adds behavior to objects dynamically
public interface IComponent
{
    void Operation();
}

public class ConcreteComponent : IComponent
{
    public void Operation() => Console.WriteLine("Concrete component operation");
}

public abstract class Decorator : IComponent
{
    protected IComponent _component;

    public Decorator(IComponent component) => _component = component;

    public virtual void Operation() => _component.Operation();
}

public class ConcreteDecorator : Decorator
{
    public ConcreteDecorator(IComponent component) : base(component) { }

    public override void Operation()
    {
        base.Operation();
        Console.WriteLine("Added behavior by decorator");
    }
}

// 6. Facade Pattern - Provides a simplified interface to a complex subsystem
public class SubsystemA
{
    public void OperationA() => Console.WriteLine("Subsystem A operation");
}

public class SubsystemB
{
    public void OperationB() => Console.WriteLine("Subsystem B operation");
}

public class Facade
{
    private readonly SubsystemA _subsystemA;
    private readonly SubsystemB _subsystemB;

    public Facade()
    {
        _subsystemA = new SubsystemA();
        _subsystemB = new SubsystemB();
    }

    public void Operation()
    {
        _subsystemA.OperationA();
        _subsystemB.OperationB();
    }
}

// ==================== BEHAVIORAL PATTERNS ====================

// 7. Observer Pattern - Defines a one-to-many dependency
public interface IObserver
{
    void Update(string message);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

public class ConcreteSubject : ISubject
{
    private readonly List<IObserver> _observers = new();
    private string _state = "";

    public string State
    {
        get => _state;
        set
        {
            _state = value;
            Notify();
        }
    }

    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_state);
        }
    }
}

public class ConcreteObserver : IObserver
{
    private readonly string _name;

    public ConcreteObserver(string name) => _name = name;

    public void Update(string message) => Console.WriteLine($"{_name} received: {message}");
}

// 8. Strategy Pattern - Defines a family of algorithms
public interface IStrategy
{
    void Execute();
}

public class ConcreteStrategyA : IStrategy
{
    public void Execute() => Console.WriteLine("Strategy A executed");
}

public class ConcreteStrategyB : IStrategy
{
    public void Execute() => Console.WriteLine("Strategy B executed");
}

public class Context
{
    private IStrategy _strategy;

    public Context(IStrategy strategy) => _strategy = strategy;

    public void SetStrategy(IStrategy strategy) => _strategy = strategy;

    public void ExecuteStrategy() => _strategy.Execute();
}

// 9. Command Pattern - Encapsulates a request as an object
public interface ICommand
{
    void Execute();
}

public class Receiver
{
    public void Action() => Console.WriteLine("Receiver action performed");
}

public class ConcreteCommand : ICommand
{
    private readonly Receiver _receiver;

    public ConcreteCommand(Receiver receiver) => _receiver = receiver;

    public void Execute() => _receiver.Action();
}

public class Invoker
{
    private ICommand? _command;

    public void SetCommand(ICommand command) => _command = command;

    public void ExecuteCommand() => _command?.Execute();
}