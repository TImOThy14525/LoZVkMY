// 代码生成时间: 2025-08-21 05:05:04
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorRandomNumberGenerator
{
    /// <summary>
    /// A Blazor component to generate random numbers.
    /// </summary>
    public partial class RandomNumberGenerator
    {
        [Parameter]
        public int MinValue { get; set; } = 0; // Minimum value for the random number

        [Parameter]
        public int MaxValue { get; set; } = 100; // Maximum value for the random number

        private Random _random = new Random(); // Random number generator

        /// <summary>
        /// Generates a random number within the specified range.
        /// </summary>
        /// <returns>A random integer within the range.</returns>
        public int GenerateRandomNumber()
        {
            try
            {
                // Check if the maximum value is greater than the minimum value
                if (MaxValue < MinValue)
# 添加错误处理
                {
                    throw new InvalidOperationException("Maximum value must be greater than or equal to the minimum value.");
                }

                // Return a random number within the specified range
                return _random.Next(MinValue, MaxValue + 1);
# 添加错误处理
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error generating random number: {ex.Message}");
                return -1; // Return an error code
            }
        }
# FIXME: 处理边界情况

        /// <summary>
        /// Resets the random number generator to its initial state.
# 添加错误处理
        /// </summary>
# 增强安全性
        public void ResetRandomNumberGenerator()
        {
# 添加错误处理
            // Reinitialize the random number generator
            _random = new Random();
        }
    }
}