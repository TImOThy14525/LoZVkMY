// 代码生成时间: 2025-09-01 00:12:40
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestDataGeneratorApp
{
    /// <summary>
    /// Represents a test data generator.
    /// </summary>
    public class TestDataGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// Generates a random integer within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>A random integer between minValue and maxValue.</returns>
        public int GenerateRandomInteger(int minValue, int maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentException("The minValue must be less than maxValue.");
            }

            return random.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// Generates a random string of a specified length.
        /// </summary>
        /// <param name="length">The desired length of the string.</param>
        /// <returns>A random string of the specified length.</returns>
        public string GenerateRandomString(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Length must be a positive integer.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        // Additional methods to generate different types of test data can be added here.

        /// <summary>
        /// Main method for testing the TestDataGenerator class.
        /// </summary>
        public static void Main()
        {
            TestDataGenerator generator = new TestDataGenerator();
            try
            {
                int randomInt = generator.GenerateRandomInteger(1, 100);
                Console.WriteLine($"Random Integer: {randomInt}");

                string randomString = generator.GenerateRandomString(10);
                Console.WriteLine($"Random String: {randomString}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}