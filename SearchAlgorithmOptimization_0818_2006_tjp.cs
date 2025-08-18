// 代码生成时间: 2025-08-18 20:06:33
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAlgorithmOptimization
# FIXME: 处理边界情况
{
    /// <summary>
    /// Provides a basic search algorithm optimization implementation.
    /// </summary>
    public class SearchService
    {
        /// <summary>
        /// Searches for an item in a list using a linear search algorithm.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to search through.</param>
        /// <param name="searchTerm">The term to search for in the list.</param>
        /// <returns>The index of the search term if found, otherwise -1.</returns>
        public int LinearSearch<T>(List<T> items, T searchTerm)
# FIXME: 处理边界情况
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The list cannot be null.");
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Equals(searchTerm))
# NOTE: 重要实现细节
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Searches for an item in a list using a binary search algorithm.
        /// </summary>
        /// <typeparam name="T">The type of items in the list, must be IComparable.</typeparam>
        /// <param name="items">The sorted list of items to search through.</param>
        /// <param name="searchTerm">The term to search for in the list.</param>
        /// <returns>The index of the search term if found, otherwise -1.</returns>
        public int BinarySearch<T>(List<T> items, T searchTerm) where T : IComparable<T>
        {
            if (items == null)
            {
# FIXME: 处理边界情况
                throw new ArgumentNullException(nameof(items), "The list cannot be null.");
            }

            int left = 0;
            int right = items.Count - 1;
# FIXME: 处理边界情况

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = items[mid].CompareTo(searchTerm);

                if (comparison == 0)
                {
                    return mid;
                }
                else if (comparison < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }

    public class Program
    {
        static async Task Main(string[] args)
        {
            var searchService = new SearchService();
            var items = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
            int searchTerm = 7;

            // Perform a linear search
            int linearSearchResult = searchService.LinearSearch(items, searchTerm);
            Console.WriteLine($"Linear Search Result: {linearSearchResult}"); // Should print 3

            // Perform a binary search
            int binarySearchResult = searchService.BinarySearch(items, searchTerm);
            Console.WriteLine($"Binary Search Result: {binarySearchResult}"); // Should print 3
        }
    }
}
