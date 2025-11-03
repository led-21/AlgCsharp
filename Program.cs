using DSAExamples;
using SystemDesignExamples;

Console.WriteLine("DSA Code Interview Examples");
RunDSAExamples();

Console.WriteLine("\nSystem Design Examples");
RunSystemDesignExamples();


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
