using DSAExamples;
using SystemDesignExamples;
using SOLIDExamples;
using DesignPatternsExamples;

Console.WriteLine("DSA Code Interview Examples");
RunDSAExamples();

Console.WriteLine("\nSystem Design Examples");
RunSystemDesignExamples();

Console.WriteLine("\nSOLID Examples");
RunSOLIDExamples();

Console.WriteLine("\nDesign Patterns Examples");
RunDesignPatternsExamples();


//DSA Examples
void RunDSAExamples()
{
    // Test examples
    var arrayProblems = new ArrayProblems();
    var linkedListProblems = new LinkedListProblems();
    var treeProblems = new TreeProblems();
    var sortingAlgorithms = new SortingAlgorithms();
    var searchingAlgorithms = new SearchingAlgorithms();
    var dynamicProgramming = new DynamicProgramming();
    var graphProblems = new GraphProblems();

    // Example usage
    int[] nums = { 2, 7, 11, 15 };
    var result = arrayProblems.TwoSum(nums, 9);
    Console.WriteLine($"Two Sum: [{string.Join(", ", result)}]");
}

// System Design Examples
void RunSystemDesignExamples()
{
    // URL Shortener Demo
    var urlShortener = new UrlShortener();
    string shortUrl = urlShortener.ShortenUrl("https://www.example.com/very/long/url");
    Console.WriteLine($"Short URL: {shortUrl}");
    Console.WriteLine($"Original: {urlShortener.GetOriginalUrl(shortUrl)}");

    // Rate Limiter Demo
    var rateLimiter = new RateLimiter(capacity: 10, refillRate: 5);
    Console.WriteLine($"Request allowed: {rateLimiter.TryConsume()}");

    // LRU Cache Demo
    var cache = new LRUCache<string, int>(3);
    cache.Put("key1", 1);
    cache.Put("key2", 2);
    cache.TryGet("key1", out int value);
    Console.WriteLine($"Cached value: {value}");

    // Load Balancer Demo
    var loadBalancer = new LoadBalancer(new[] { "server1", "server2", "server3" });
    Console.WriteLine($"Next server: {loadBalancer.GetNextServer()}");

    // Consistent Hashing Demo
    var consistentHash = new ConsistentHashing();
    consistentHash.AddNode("node1");
    consistentHash.AddNode("node2");
    Console.WriteLine($"Key 'user123' maps to: {consistentHash.GetNode("user123")}");
}

// SOLID Examples
void RunSOLIDExamples()
{
    // Single Responsibility Principle
    Console.WriteLine("=== Single Responsibility Principle ===");
    var user = new User { Username = "john", Email = "john@example.com" };
    var userService = new UserService();
    userService.CreateUser(user);
    var emailService = new EmailService();
    emailService.SendEmail(user.Email, "Welcome!");

    // Open-Closed Principle
    Console.WriteLine("\n=== Open-Closed Principle ===");
    var rect = new RectangleOCP { Width = 5, Height = 10 };
    var calc = new AreaCalculator();
    Console.WriteLine($"Rectangle area: {calc.CalculateArea(rect)}");
    var circle = new CircleOCP { Radius = 3 };
    Console.WriteLine($"Circle area: {calc.CalculateArea(circle)}");
    var triangle = new Triangle { Base = 4, Height = 6 };
    Console.WriteLine($"Triangle area: {calc.CalculateArea(triangle)}");

    // Liskov Substitution Principle
    Console.WriteLine("\n=== Liskov Substitution Principle ===");
    IShapeLSP rectLSP = new RectangleLSP { Width = 5, Height = 10 };
    Console.WriteLine($"Rectangle area: {rectLSP.GetArea()}");
    IShapeLSP square = new Square { Side = 5 };
    Console.WriteLine($"Square area: {square.GetArea()}");

    // Interface Segregation Principle
    Console.WriteLine("\n=== Interface Segregation Principle ===");
    IWorkable human = new HumanWorkerISP();
    human.Work();
    IEatable eatable = (IEatable)human;
    eatable.Eat();
    IWorkable robot = new RobotWorkerISP();
    robot.Work();

    // Dependency Inversion Principle
    Console.WriteLine("\n=== Dependency Inversion Principle ===");
    var emailNotification = new NotificationService(new EmailServiceDIP());
    emailNotification.Notify("Hello via Email");
    var smsNotification = new NotificationService(new SmsService());
    smsNotification.Notify("Hello via SMS");
}

// Design Patterns Examples
void RunDesignPatternsExamples()
{
    // Singleton
    Console.WriteLine("=== Singleton Pattern ===");
    var singleton1 = Singleton.Instance;
    var singleton2 = Singleton.Instance;
    Console.WriteLine($"Same instance: {singleton1 == singleton2}");
    singleton1.DoSomething();

    // Factory
    Console.WriteLine("\n=== Factory Pattern ===");
    var factory = new ProductFactory();
    var productA = factory.CreateProduct("A");
    productA.Use();
    var productB = factory.CreateProduct("B");
    productB.Use();

    // Builder
    Console.WriteLine("\n=== Builder Pattern ===");
    var director = new CarDirector();
    var builder = new SportsCarBuilder();
    var car = director.Construct(builder);
    car.Show();

    // Adapter
    Console.WriteLine("\n=== Adapter Pattern ===");
    var adaptee = new Adaptee();
    var adapter = new Adapter(adaptee);
    adapter.Request();

    // Decorator
    Console.WriteLine("\n=== Decorator Pattern ===");
    var component = new ConcreteComponent();
    var decorator = new ConcreteDecorator(component);
    decorator.Operation();

    // Facade
    Console.WriteLine("\n=== Facade Pattern ===");
    var facade = new Facade();
    facade.Operation();

    // Observer
    Console.WriteLine("\n=== Observer Pattern ===");
    var subject = new ConcreteSubject();
    var observer1 = new ConcreteObserver("Observer1");
    var observer2 = new ConcreteObserver("Observer2");
    subject.Attach(observer1);
    subject.Attach(observer2);
    subject.State = "New State";

    // Strategy
    Console.WriteLine("\n=== Strategy Pattern ===");
    var context = new Context(new ConcreteStrategyA());
    context.ExecuteStrategy();
    context.SetStrategy(new ConcreteStrategyB());
    context.ExecuteStrategy();

    // Command
    Console.WriteLine("\n=== Command Pattern ===");
    var receiver = new Receiver();
    var command = new ConcreteCommand(receiver);
    var invoker = new Invoker();
    invoker.SetCommand(command);
    invoker.ExecuteCommand();
}
