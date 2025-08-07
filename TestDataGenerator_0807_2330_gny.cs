// 代码生成时间: 2025-08-07 23:30:06
 * It includes error handling and documentation to ensure clarity and maintainability.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestDataGeneratorApp
{
    // Define the TestData class to hold the generated test data.
    public class TestData
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    // Define the TestDataGenerator class to handle the generation of test data.
    public class TestDataGenerator
    {
        private readonly Random _random;

        public TestDataGenerator()
        {
            _random = new Random();
        }

        // Generate a single TestData instance.
        public TestData GenerateTestData()
        {
            try
            {
                var name = GenerateRandomName();
                var age = GenerateRandomAge();
                var email = GenerateRandomEmail(name);

                return new TestData { Name = name, Age = age, Email = email };
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately.
                Console.WriteLine($"An error occurred while generating test data: {ex.Message}");
                throw;
            }
        }

        // Generate a list of TestData instances.
        public List<TestData> GenerateTestDataList(int count)
        {
            try
            {
                var testDataList = new List<TestData>();
                for (int i = 0; i < count; i++)
                {
                    testDataList.Add(GenerateTestData());
                }
                return testDataList;
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately.
                Console.WriteLine($"An error occurred while generating the test data list: {ex.Message}");
                throw;
            }
        }

        // Helper method to generate a random name.
        private string GenerateRandomName()
        {
            var names = new List<string> { "John", "Jane", "Alice", "Bob" };
            return names[_random.Next(names.Count)];
        }

        // Helper method to generate a random age.
        private int GenerateRandomAge()
        {
            return _random.Next(18, 65); // Generate a random age between 18 and 64.
        }

        // Helper method to generate a random email.
        private string GenerateRandomEmail(string name)
        {
            var domains = new List<string> { "example.com", "test.com", "sample.org" };
            return $