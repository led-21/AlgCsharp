using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSA;

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
            int[] nums = { 2, 7, 11, 15 };
            var result = arrayProblems.TwoSum(nums, 9);
            Console.WriteLine($"Two Sum: [{string.Join(", ", result)}]");
        }
    }
}
    
   