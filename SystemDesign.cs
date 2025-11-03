using System.Collections.Concurrent;

namespace SystemDesignExamples;

// Example 1: URL Shortener (like bit.ly)
public class UrlShortener
{
    private readonly Dictionary<string, string> _urlToShort = new();
    private readonly Dictionary<string, string> _shortToUrl = new();
    private readonly Random _random = new();
    private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string ShortenUrl(string originalUrl)
    {
        if (_urlToShort.ContainsKey(originalUrl))
            return _urlToShort[originalUrl];

        string shortCode = GenerateShortCode();
        _urlToShort[originalUrl] = shortCode;
        _shortToUrl[shortCode] = originalUrl;

        return shortCode;
    }

    public string? GetOriginalUrl(string shortCode)
    {
        return _shortToUrl.GetValueOrDefault(shortCode);
    }

    private string GenerateShortCode()
    {
        var result = new char[6];
        for (int i = 0; i < 6; i++)
        {
            result[i] = Characters[_random.Next(Characters.Length)];
        }
        return new string(result);
    }
}

// Example 2: Rate Limiter (Token Bucket Algorithm)
public class RateLimiter
{
    private readonly int _capacity;
    private readonly int _refillRate;
    private int _tokens;
    private DateTime _lastRefill;
    private readonly object _lock = new();

    public RateLimiter(int capacity, int refillRate)
    {
        _capacity = capacity;
        _refillRate = refillRate;
        _tokens = capacity;
        _lastRefill = DateTime.UtcNow;
    }

    public bool TryConsume(int tokensRequested = 1)
    {
        lock (_lock)
        {
            RefillTokens();

            if (_tokens >= tokensRequested)
            {
                _tokens -= tokensRequested;
                return true;
            }

            return false;
        }
    }

    private void RefillTokens()
    {
        var now = DateTime.UtcNow;
        var timePassed = (now - _lastRefill).TotalSeconds;
        var tokensToAdd = (int)(timePassed * _refillRate);

        _tokens = Math.Min(_capacity, _tokens + tokensToAdd);
        _lastRefill = now;
    }
}

// Example 3: LRU Cache
public class LRUCache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cache;
    private readonly LinkedList<CacheItem> _lruList;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cache = new Dictionary<TKey, LinkedListNode<CacheItem>>(capacity);
        _lruList = new LinkedList<CacheItem>();
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            // Move to front (most recently used)
            _lruList.Remove(node);
            _lruList.AddFirst(node);

            value = node.Value.Value;
            return true;
        }

        value = default;
        return false;
    }

    public void Put(TKey key, TValue value)
    {
        if (_cache.TryGetValue(key, out var existingNode))
        {
            // Update existing
            existingNode.Value.Value = value;
            _lruList.Remove(existingNode);
            _lruList.AddFirst(existingNode);
        }
        else
        {
            // Add new
            if (_cache.Count >= _capacity)
            {
                // Remove least recently used
                var lru = _lruList.Last;
                if (lru != null)
                {
                    _cache.Remove(lru.Value.Key);
                    _lruList.RemoveLast();
                }
            }

            var newItem = new CacheItem { Key = key, Value = value };
            var newNode = _lruList.AddFirst(newItem);
            _cache[key] = newNode;
        }
    }

    private class CacheItem
    {
        public required TKey Key { get; set; }
        public required TValue Value { get; set; }
    }
}

// Example 4: Chat System Message Queue
public class ChatMessage
{
    public required string Id { get; set; }
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
    public required string Content { get; set; }
    public DateTime Timestamp { get; set; }
}

public class ChatSystem
{
    private readonly ConcurrentDictionary<string, ConcurrentQueue<ChatMessage>> _userQueues = new();
    private readonly ConcurrentDictionary<string, TaskCompletionSource<bool>> _waitingUsers = new();

    public void SendMessage(ChatMessage message)
    {
        var queue = _userQueues.GetOrAdd(message.ReceiverId, _ => new ConcurrentQueue<ChatMessage>());
        queue.Enqueue(message);

        // Notify waiting receiver
        if (_waitingUsers.TryRemove(message.ReceiverId, out var tcs))
        {
            tcs.SetResult(true);
        }
    }

    public async Task<ChatMessage?> ReceiveMessageAsync(string userId, CancellationToken cancellationToken = default)
    {
        var queue = _userQueues.GetOrAdd(userId, _ => new ConcurrentQueue<ChatMessage>());

        if (queue.TryDequeue(out var message))
        {
            return message;
        }

        // Wait for new message
        var tcs = new TaskCompletionSource<bool>();
        _waitingUsers[userId] = tcs;

        try
        {
            await tcs.Task.WaitAsync(cancellationToken);
            return queue.TryDequeue(out message) ? message : null;
        }
        catch (OperationCanceledException)
        {
            _waitingUsers.TryRemove(userId, out _);
            throw;
        }
    }
}

// Example 5: Load Balancer (Round Robin)
public class LoadBalancer
{
    private readonly List<string> _servers;
    private int _currentIndex;
    private readonly object _lock = new();

    public LoadBalancer(IEnumerable<string> servers)
    {
        _servers = new List<string>(servers);
        _currentIndex = 0;
    }

    public string? GetNextServer()
    {
        lock (_lock)
        {
            if (_servers.Count == 0)
                return null;

            var server = _servers[_currentIndex];
            _currentIndex = (_currentIndex + 1) % _servers.Count;
            return server;
        }
    }

    public void AddServer(string server)
    {
        lock (_lock)
        {
            _servers.Add(server);
        }
    }

    public void RemoveServer(string server)
    {
        lock (_lock)
        {
            _servers.Remove(server);
            if (_currentIndex >= _servers.Count)
                _currentIndex = 0;
        }
    }
}

// Example 6: Distributed Cache with Consistent Hashing
public class ConsistentHashing
{
    private readonly SortedDictionary<uint, string> _ring = new();
    private readonly int _virtualNodes;

    public ConsistentHashing(int virtualNodes = 100)
    {
        _virtualNodes = virtualNodes;
    }

    public void AddNode(string node)
    {
        for (int i = 0; i < _virtualNodes; i++)
        {
            var hash = ComputeHash($"{node}:{i}");
            _ring[hash] = node;
        }
    }

    public void RemoveNode(string node)
    {
        for (int i = 0; i < _virtualNodes; i++)
        {
            var hash = ComputeHash($"{node}:{i}");
            _ring.Remove(hash);
        }
    }

    public string? GetNode(string key)
    {
        if (_ring.Count == 0)
            return null;

        var hash = ComputeHash(key);

        // Find the first node clockwise
        foreach (var kvp in _ring)
        {
            if (kvp.Key >= hash)
                return kvp.Value;
        }

        // Wrap around to the first node
        return _ring.First().Value;
    }

    private uint ComputeHash(string input)
    {
        // Simple hash function (use more robust one in production)
        uint hash = 0;
        foreach (char c in input)
        {
            hash = hash * 31 + c;
        }
        return hash;
    }
}



