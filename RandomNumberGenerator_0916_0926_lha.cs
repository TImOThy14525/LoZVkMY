// 代码生成时间: 2025-09-16 09:26:41
using System;
using Microsoft.AspNetCore.Components;

namespace RandomNumberGeneratorApp
{
    /// <summary>
    /// A component that generates a random number.
    /// </summary>
    public partial class RandomNumberGenerator
    {
        [Parameter]
        public int? Min { get; set; }

        [Parameter]
        public int? Max { get; set; }

        private int? randomNumber;

        /// <summary>
        /// Generates a random number within the specified range.
        /// </summary>
        private void GenerateRandomNumber()
        {
            if (Min.HasValue && Max.HasValue)
            {
                if (Min.Value > Max.Value)
                {
                    Console.WriteLine("Error: Min value cannot be greater than Max value.");
                    return;
                }

                var random = new Random();
                randomNumber = random.Next(Min.Value, Max.Value + 1);
            }
            else
            {
                Console.WriteLine("Error: Min and Max values must be specified.");
            }
        }

        /// <summary>
        /// Invoked when the component is initialized.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            GenerateRandomNumber();
        }
    }
}