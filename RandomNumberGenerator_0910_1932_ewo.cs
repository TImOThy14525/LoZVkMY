// 代码生成时间: 2025-09-10 19:32:14
using System;
using Microsoft.AspNetCore.Components;

namespace BlazorApp
{
    // Component for generating random numbers
    public partial class RandomNumberGenerator
    {
        [Parameter]
        public int Min { get; set; } = 1; // Minimum value of the random number

        [Parameter]
        public int Max { get; set; } = 100; // Maximum value of the random number

        [Parameter]
        public EventCallback<int> OnRandomNumberGenerated { get; set; } // Event to call when a random number is generated

        private int randomNumber; // Variable to hold the generated random number

        // Method to generate a random number within the range of Min and Max
        private void GenerateRandomNumber()
        {
            try
            {
                if (Min > Max)
                {
                    // If Min is greater than Max, throw an exception
                    throw new ArgumentException("Min must be less than or equal to Max");
                }

                randomNumber = new Random().Next(Min, Max + 1); // Generate a random number between Min and Max
                OnRandomNumberGenerated.InvokeAsync(randomNumber); // Invoke the event with the generated number
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the generation process
                Console.WriteLine($"Error generating random number: {ex.Message}");
            }
        }

        // Method to handle the button click event
        private void OnGenerateButtonClick()
        {
            GenerateRandomNumber(); // Call the GenerateRandomNumber method when the button is clicked
        }
    }
}
