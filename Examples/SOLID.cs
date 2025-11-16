using System;

namespace SOLIDExamples;

// ==================== SINGLE RESPONSIBILITY PRINCIPLE ====================
// A class should have only one reason to change.

// Bad Example (violates SRP)
public class UserManagerBad
{
    public void CreateUser(string username, string email)
    {
        // Create user logic
        Console.WriteLine($"User {username} created");

        // Send email (violates SRP - multiple responsibilities)
        SendEmail(email, "Welcome!");
    }

    private void SendEmail(string email, string message)
    {
        Console.WriteLine($"Email sent to {email}: {message}");
    }
}

// Good Example (follows SRP)
public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
}

public class UserService
{
    public void CreateUser(User user)
    {
        // Only responsible for user creation
        Console.WriteLine($"User {user.Username} created");
    }
}

public class EmailService
{
    public void SendEmail(string email, string message)
    {
        // Only responsible for sending emails
        Console.WriteLine($"Email sent to {email}: {message}");
    }
}

// ==================== OPEN-CLOSED PRINCIPLE ====================
// Software entities should be open for extension but closed for modification.

// Bad Example (violates OCP)
public class AreaCalculatorBad
{
    public double CalculateArea(object shape)
    {
        if (shape is Rectangle rect)
            return rect.Width * rect.Height;
        else if (shape is Circle circle)
            return Math.PI * circle.Radius * circle.Radius;
        // To add Triangle, we need to modify this class (violates OCP)
        return 0;
    }
}

public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
}

public class Circle
{
    public double Radius { get; set; }
}

// Good Example (follows OCP)
public interface IShape
{
    double CalculateArea();
}

public class RectangleOCP : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public double CalculateArea() => Width * Height;
}

public class CircleOCP : IShape
{
    public double Radius { get; set; }

    public double CalculateArea() => Math.PI * Radius * Radius;
}

// Now we can add Triangle without modifying existing code
public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public double CalculateArea() => 0.5 * Base * Height;
}

public class AreaCalculator
{
    public double CalculateArea(IShape shape) => shape.CalculateArea();
}

// ==================== LISKOV SUBSTITUTION PRINCIPLE ====================
// Subtypes must be substitutable for their base types.

// Bad Example (violates LSP)
public class RectangleLSPBad
{
    public virtual double Width { get; set; }
    public virtual double Height { get; set; }

    public double GetArea() => Width * Height;
}

public class SquareBad : RectangleLSPBad
{
    public override double Width
    {
        get => base.Width;
        set
        {
            base.Width = value;
            base.Height = value; // Violates LSP - changes behavior unexpectedly
        }
    }

    public override double Height
    {
        get => base.Height;
        set
        {
            base.Width = value;
            base.Height = value; // Violates LSP
        }
    }
}

// Good Example (follows LSP)
public interface IShapeLSP
{
    double GetArea();
}

public class RectangleLSP : IShapeLSP
{
    public double Width { get; set; }
    public double Height { get; set; }

    public double GetArea() => Width * Height;
}

public class Square : IShapeLSP
{
    public double Side { get; set; }

    public double GetArea() => Side * Side;
}

// ==================== INTERFACE SEGREGATION PRINCIPLE ====================
// Clients should not be forced to depend on interfaces they do not use.

// Bad Example (violates ISP)
public interface IWorkerBad
{
    void Work();
    void Eat();
    void Sleep();
}

public class HumanWorker : IWorkerBad
{
    public void Work() => Console.WriteLine("Human working");
    public void Eat() => Console.WriteLine("Human eating");
    public void Sleep() => Console.WriteLine("Human sleeping");
}

public class RobotWorker : IWorkerBad
{
    public void Work() => Console.WriteLine("Robot working");
    public void Eat() => throw new NotImplementedException(); // Forced to implement unused method
    public void Sleep() => throw new NotImplementedException(); // Forced to implement unused method
}

// Good Example (follows ISP)
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

public class HumanWorkerISP : IWorkable, IEatable, ISleepable
{
    public void Work() => Console.WriteLine("Human working");
    public void Eat() => Console.WriteLine("Human eating");
    public void Sleep() => Console.WriteLine("Human sleeping");
}

public class RobotWorkerISP : IWorkable
{
    public void Work() => Console.WriteLine("Robot working");
    // No need to implement Eat or Sleep
}

// ==================== DEPENDENCY INVERSION PRINCIPLE ====================
// High-level modules should not depend on low-level modules. Both should depend on abstractions.

// Bad Example (violates DIP)
public class EmailServiceDIPBad
{
    public void SendEmail(string message)
    {
        // Direct dependency on concrete implementation
        var smtpClient = new SmtpClient();
        smtpClient.Send(message);
    }
}

public class SmtpClient
{
    public void Send(string message) => Console.WriteLine($"SMTP: {message}");
}

// Good Example (follows DIP)
public interface IMessageService
{
    void SendMessage(string message);
}

public class EmailServiceDIP : IMessageService
{
    public void SendMessage(string message)
    {
        // Depends on abstraction
        var sender = new SmtpClientDIP();
        sender.Send(message);
    }
}

public class SmsService : IMessageService
{
    public void SendMessage(string message)
    {
        var sender = new SmsClient();
        sender.Send(message);
    }
}

public interface IMessageSender
{
    void Send(string message);
}

public class SmtpClientDIP : IMessageSender
{
    public void Send(string message) => Console.WriteLine($"SMTP: {message}");
}

public class SmsClient : IMessageSender
{
    public void Send(string message) => Console.WriteLine($"SMS: {message}");
}

// High-level module depends on abstraction
public class NotificationService
{
    private readonly IMessageService _messageService;

    public NotificationService(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message) => _messageService.SendMessage(message);
}