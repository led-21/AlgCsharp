using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSACodeInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DSA Code Interview Examples");
            
            // Test examples
            var arrayProblems = new ArrayProblems();
            var linkedListProblems = new LinkedListProblems();
            var treeProblems = new TreeProblems();
            var sortingAlgorithms = new SortingAlgorithms();
            var searchingAlgorithms = new SearchingAlgorithms();
            var dynamicProgramming = new DynamicProgramming();
            var graphProblems = new GraphProblems();
            
            // Example usage
            int[] nums = {2, 7, 11, 15};
            var result = arrayProblems.TwoSum(nums, 9);
            Console.WriteLine($"Two Sum: [{string.Join(", ", result)}]");
        }
    }

    // ==================== ARRAY PROBLEMS ====================
    public class ArrayProblems
    {
        // Two Sum - O(n) time, O(n) space
        public int[] TwoSum(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                if (map.ContainsKey(complement))
                    return new int[] { map[complement], i };
                map[nums[i]] = i;
            }
            return new int[0];
        }

        // Maximum Subarray (Kadane's Algorithm) - O(n) time, O(1) space
        public int MaxSubArray(int[] nums)
        {
            int maxSoFar = nums[0];
            int maxEndingHere = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                maxEndingHere = Math.Max(nums[i], maxEndingHere + nums[i]);
                maxSoFar = Math.Max(maxSoFar, maxEndingHere);
            }
            return maxSoFar;
        }

        // Best Time to Buy and Sell Stock - O(n) time, O(1) space
        public int MaxProfit(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            foreach (int price in prices)
            {
                if (price < minPrice)
                    minPrice = price;
                else if (price - minPrice > maxProfit)
                    maxProfit = price - minPrice;
            }
            return maxProfit;
        }

        // Rotate Array - O(n) time, O(1) space
        public void Rotate(int[] nums, int k)
        {
            k %= nums.Length;
            Reverse(nums, 0, nums.Length - 1);
            Reverse(nums, 0, k - 1);
            Reverse(nums, k, nums.Length - 1);
        }

        private void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                int temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }
        }

        // Contains Duplicate - O(n) time, O(n) space
        public bool ContainsDuplicate(int[] nums)
        {
            return nums.Length != nums.Distinct().Count();
        }

        // Product of Array Except Self - O(n) time, O(1) space
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] result = new int[nums.Length];
            
            // Left products
            result[0] = 1;
            for (int i = 1; i < nums.Length; i++)
                result[i] = result[i - 1] * nums[i - 1];
            
            // Right products
            int rightProduct = 1;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                result[i] *= rightProduct;
                rightProduct *= nums[i];
            }
            
            return result;
        }
    }

    // ==================== LINKED LIST PROBLEMS ====================
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class LinkedListProblems
    {
        // Reverse Linked List - O(n) time, O(1) space
        public ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode current = head;

            while (current != null)
            {
                ListNode next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            return prev;
        }

        // Merge Two Sorted Lists - O(n + m) time, O(1) space
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            var dummy = new ListNode(0);
            var current = dummy;

            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val)
                {
                    current.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    current.next = list2;
                    list2 = list2.next;
                }
                current = current.next;
            }

            current.next = list1 ?? list2;
            return dummy.next;
        }

        // Linked List Cycle Detection - O(n) time, O(1) space
        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null) return false;

            ListNode slow = head;
            ListNode fast = head.next;

            while (slow != fast)
            {
                if (fast == null || fast.next == null) return false;
                slow = slow.next;
                fast = fast.next.next;
            }
            return true;
        }

        // Remove Nth Node From End - O(n) time, O(1) space
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            var dummy = new ListNode(0) { next = head };
            ListNode fast = dummy;
            ListNode slow = dummy;

            // Move fast n+1 steps ahead
            for (int i = 0; i <= n; i++)
                fast = fast.next;

            // Move both pointers until fast reaches end
            while (fast != null)
            {
                fast = fast.next;
                slow = slow.next;
            }

            // Remove the nth node
            slow.next = slow.next.next;
            return dummy.next;
        }
    }

    // ==================== TREE PROBLEMS ====================
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class TreeProblems
    {
        // Maximum Depth of Binary Tree - O(n) time, O(h) space
        public int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        }

        // Same Tree - O(n) time, O(h) space
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) return true;
            if (p == null || q == null) return false;
            return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }

        // Invert Binary Tree - O(n) time, O(h) space
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return null;

            TreeNode temp = root.left;
            root.left = InvertTree(root.right);
            root.right = InvertTree(temp);

            return root;
        }

        // Binary Tree Inorder Traversal - O(n) time, O(h) space
        public IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            InorderHelper(root, result);
            return result;
        }

        private void InorderHelper(TreeNode node, List<int> result)
        {
            if (node == null) return;
            InorderHelper(node.left, result);
            result.Add(node.val);
            InorderHelper(node.right, result);
        }

        // Validate Binary Search Tree - O(n) time, O(h) space
        public bool IsValidBST(TreeNode root)
        {
            return IsValidBSTHelper(root, long.MinValue, long.MaxValue);
        }

        private bool IsValidBSTHelper(TreeNode node, long minVal, long maxVal)
        {
            if (node == null) return true;
            if (node.val <= minVal || node.val >= maxVal) return false;
            return IsValidBSTHelper(node.left, minVal, node.val) && 
                   IsValidBSTHelper(node.right, node.val, maxVal);
        }

        // Lowest Common Ancestor - O(n) time, O(h) space
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || root == p || root == q) return root;

            TreeNode left = LowestCommonAncestor(root.left, p, q);
            TreeNode right = LowestCommonAncestor(root.right, p, q);

            if (left != null && right != null) return root;
            return left ?? right;
        }
    }

    // ==================== SORTING ALGORITHMS ====================
    public class SortingAlgorithms
    {
        // Quick Sort - O(n log n) average, O(n²) worst case
        public void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }

        // Merge Sort - O(n log n) time, O(n) space
        public void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }

        private void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            Array.Copy(arr, left, leftArr, 0, n1);
            Array.Copy(arr, mid + 1, rightArr, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                    arr[k++] = leftArr[i++];
                else
                    arr[k++] = rightArr[j++];
            }

            while (i < n1) arr[k++] = leftArr[i++];
            while (j < n2) arr[k++] = rightArr[j++];
        }

        // Heap Sort - O(n log n) time, O(1) space
        public void HeapSort(int[] arr)
        {
            int n = arr.Length;

            // Build heap
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            // Extract elements
            for (int i = n - 1; i > 0; i--)
            {
                Swap(arr, 0, i);
                Heapify(arr, i, 0);
            }
        }

        private void Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && arr[left] > arr[largest])
                largest = left;

            if (right < n && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, n, largest);
            }
        }

        private void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

    // ==================== SEARCHING ALGORITHMS ====================
    public class SearchingAlgorithms
    {
        // Binary Search - O(log n) time, O(1) space
        public int BinarySearch(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                    return mid;
                else if (nums[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }

        // Search in Rotated Sorted Array - O(log n) time, O(1) space
        public int SearchRotated(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                    return mid;

                if (nums[left] <= nums[mid]) // Left half is sorted
                {
                    if (target >= nums[left] && target < nums[mid])
                        right = mid - 1;
                    else
                        left = mid + 1;
                }
                else // Right half is sorted
                {
                    if (target > nums[mid] && target <= nums[right])
                        left = mid + 1;
                    else
                        right = mid - 1;
                }
            }
            return -1;
        }

        // Find First and Last Position - O(log n) time, O(1) space
        public int[] SearchRange(int[] nums, int target)
        {
            int[] result = {-1, -1};
            result[0] = FindFirst(nums, target);
            result[1] = FindLast(nums, target);
            return result;
        }

        private int FindFirst(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            int result = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                {
                    result = mid;
                    right = mid - 1;
                }
                else if (nums[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return result;
        }

        private int FindLast(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            int result = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                {
                    result = mid;
                    left = mid + 1;
                }
                else if (nums[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return result;
        }
    }

    // ==================== DYNAMIC PROGRAMMING ====================
    public class DynamicProgramming
    {
        // Fibonacci - O(n) time, O(1) space
        public int Fibonacci(int n)
        {
            if (n <= 1) return n;
            
            int prev2 = 0, prev1 = 1;
            for (int i = 2; i <= n; i++)
            {
                int current = prev1 + prev2;
                prev2 = prev1;
                prev1 = current;
            }
            return prev1;
        }

        // Climbing Stairs - O(n) time, O(1) space
        public int ClimbStairs(int n)
        {
            if (n <= 2) return n;
            
            int prev2 = 1, prev1 = 2;
            for (int i = 3; i <= n; i++)
            {
                int current = prev1 + prev2;
                prev2 = prev1;
                prev1 = current;
            }
            return prev1;
        }

        // House Robber - O(n) time, O(1) space
        public int Rob(int[] nums)
        {
            if (nums.Length == 0) return 0;
            if (nums.Length == 1) return nums[0];
            
            int prev2 = nums[0];
            int prev1 = Math.Max(nums[0], nums[1]);
            
            for (int i = 2; i < nums.Length; i++)
            {
                int current = Math.Max(prev1, prev2 + nums[i]);
                prev2 = prev1;
                prev1 = current;
            }
            return prev1;
        }

        // Coin Change - O(amount * coins.length) time, O(amount) space
        public int CoinChange(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            Array.Fill(dp, amount + 1);
            dp[0] = 0;

            for (int i = 1; i <= amount; i++)
            {
                foreach (int coin in coins)
                {
                    if (coin <= i)
                        dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                }
            }
            return dp[amount] > amount ? -1 : dp[amount];
        }

        // Longest Increasing Subsequence - O(n²) time, O(n) space
        public int LengthOfLIS(int[] nums)
        {
            if (nums.Length == 0) return 0;
            
            int[] dp = new int[nums.Length];
            Array.Fill(dp, 1);
            int maxLength = 1;

            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
                maxLength = Math.Max(maxLength, dp[i]);
            }
            return maxLength;
        }
    }

    // ==================== GRAPH PROBLEMS ====================
    public class GraphProblems
    {
        // DFS - O(V + E) time, O(V) space
        public void DFS(Dictionary<int, List<int>> graph, int start, HashSet<int> visited)
        {
            visited.Add(start);
            Console.Write(start + " ");

            if (graph.ContainsKey(start))
            {
                foreach (int neighbor in graph[start])
                {
                    if (!visited.Contains(neighbor))
                        DFS(graph, neighbor, visited);
                }
            }
        }

        // BFS - O(V + E) time, O(V) space
        public void BFS(Dictionary<int, List<int>> graph, int start)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                Console.Write(node + " ");

                if (graph.ContainsKey(node))
                {
                    foreach (int neighbor in graph[node])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }

        // Number of Islands - O(m * n) time, O(m * n) space
        public int NumIslands(char[][] grid)
        {
            if (grid == null || grid.Length == 0) return 0;

            int numIslands = 0;
            int rows = grid.Length;
            int cols = grid[0].Length;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        numIslands++;
                        DFSIsland(grid, i, j);
                    }
                }
            }
            return numIslands;
        }

        private void DFSIsland(char[][] grid, int i, int j)
        {
            if (i < 0 || i >= grid.Length || j < 0 || j >= grid[0].Length || grid[i][j] == '0')
                return;

            grid[i][j] = '0'; // Mark as visited

            // Check all 4 directions
            DFSIsland(grid, i + 1, j);
            DFSIsland(grid, i - 1, j);
            DFSIsland(grid, i, j + 1);
            DFSIsland(grid, i, j - 1);
        }

        // Course Schedule (Cycle Detection) - O(V + E) time, O(V) space
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            var graph = new Dictionary<int, List<int>>();
            var inDegree = new int[numCourses];

            // Build graph
            foreach (var prereq in prerequisites)
            {
                int course = prereq[0];
                int prerequisite = prereq[1];

                if (!graph.ContainsKey(prerequisite))
                    graph[prerequisite] = new List<int>();
                
                graph[prerequisite].Add(course);
                inDegree[course]++;
            }

            // Topological sort using Kahn's algorithm
            var queue = new Queue<int>();
            for (int i = 0; i < numCourses; i++)
            {
                if (inDegree[i] == 0)
                    queue.Enqueue(i);
            }

            int processedCourses = 0;
            while (queue.Count > 0)
            {
                int course = queue.Dequeue();
                processedCourses++;

                if (graph.ContainsKey(course))
                {
                    foreach (int neighbor in graph[course])
                    {
                        inDegree[neighbor]--;
                        if (inDegree[neighbor] == 0)
                            queue.Enqueue(neighbor);
                    }
                }
            }

            return processedCourses == numCourses;
        }
    }

    // ==================== STRING PROBLEMS ====================
    public class StringProblems
    {
        // Valid Palindrome - O(n) time, O(1) space
        public bool IsPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;

            while (left < right)
            {
                while (left < right && !char.IsLetterOrDigit(s[left]))
                    left++;
                while (left < right && !char.IsLetterOrDigit(s[right]))
                    right--;

                if (char.ToLower(s[left]) != char.ToLower(s[right]))
                    return false;

                left++;
                right--;
            }
            return true;
        }

        // Valid Anagram - O(n) time, O(1) space
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;

            int[] count = new int[26];

            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;
                count[t[i] - 'a']--;
            }

            return count.All(c => c == 0);
        }

        // Longest Substring Without Repeating Characters - O(n) time, O(min(m,n)) space
        public int LengthOfLongestSubstring(string s)
        {
            var charSet = new HashSet<char>();
            int left = 0, maxLength = 0;

            for (int right = 0; right < s.Length; right++)
            {
                while (charSet.Contains(s[right]))
                    charSet.Remove(s[left++]);

                charSet.Add(s[right]);
                maxLength = Math.Max(maxLength, right - left + 1);
            }
            return maxLength;
        }

        // Group Anagrams - O(n * k log k) time, O(n * k) space
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var map = new Dictionary<string, List<string>>();

            foreach (string str in strs)
            {
                char[] chars = str.ToCharArray();
                Array.Sort(chars);
                string key = new string(chars);

                if (!map.ContainsKey(key))
                    map[key] = new List<string>();
                
                map[key].Add(str);
            }

            return map.Values.Cast<IList<string>>().ToList();
        }
    }
}