// 代码生成时间: 2025-09-07 01:02:42
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace SortingBlazorApp
{
    public partial class SortingAlgorithmBlazorApp
    {
# 添加错误处理
        [Parameter]
        public List<int> Numbers { get; set; } = Enumerable.Range(1, 100).ToList();

        // Sort using Bubble Sort algorithm
        private void BubbleSort()
        {
            if (Numbers == null)
# 改进用户体验
            {
                Console.WriteLine("Error: Numbers list is null.");
                return;
            }

            int n = Numbers.Count;
            for (int i = 0; i < n - 1; i++)
# 改进用户体验
            {
# NOTE: 重要实现细节
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (Numbers[j] > Numbers[j + 1])
                    {
                        // Swap the elements
                        int temp = Numbers[j];
                        Numbers[j] = Numbers[j + 1];
                        Numbers[j + 1] = temp;
                    }
                }
            }
# 添加错误处理
        }

        // Sort using Quick Sort algorithm
        private void QuickSort()
        {
# FIXME: 处理边界情况
            if (Numbers == null)
            {
                Console.WriteLine("Error: Numbers list is null.");
                return;
            }

            Numbers = Numbers.OrderBy(x => Guid.NewGuid()).ToList(); // Shuffle to avoid worst-case scenario
            QuickSortHelper(Numbers, 0, Numbers.Count - 1);
        }

        private void QuickSortHelper(List<int> arr, int low, int high)
# 改进用户体验
        {
            if (low < high)
# 增强安全性
            {
# TODO: 优化性能
                int pi = Partition(arr, low, high);

                QuickSortHelper(arr, low, pi - 1); // Before pi
                QuickSortHelper(arr, pi + 1, high); // After pi
            }
# 扩展功能模块
        }

        private int Partition(List<int> arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);
# FIXME: 处理边界情况
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
# 添加错误处理
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
# TODO: 优化性能
            int temp = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp;
            return i + 1;
        }
    }
}