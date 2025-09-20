// 代码生成时间: 2025-09-20 11:31:41
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

// DataAnalysisApp.cs
// This component is a statistical data analyzer using Blazor framework.

namespace BlazorDataAnalysis
{
    public partial class DataAnalysisApp
    {
        [Parameter]
        public List<double> DataPoints { get; set; } = new List<double>();

        private double mean;
        private double median;
        private double mode;
        private double standardDeviation;
        private double variance;

        // This method computes the mean of the data points.
        private void CalculateMean()
        {
            mean = DataPoints.Average();
        }

        // This method computes the median of the data points.
        private void CalculateMedian()
        {
            int size = DataPoints.Count;
            var sortedData = DataPoints.OrderBy(x => x).ToList();
            if (size % 2 == 0)
                median = (sortedData[size / 2 - 1] + sortedData[size / 2]) / 2;
            else
                median = sortedData[size / 2];
        }

        // This method computes the mode of the data points.
        private void CalculateMode()
        {
            var modeCount = DataPoints.GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .FirstOrDefault();
            mode = modeCount != null ? modeCount.Key : 0;
        }

        // This method computes the standard deviation of the data points.
        private void CalculateStandardDeviation()
        {
            double variance = DataPoints.Average(x => Math.Pow(x - mean, 2));
            standardDeviation = Math.Sqrt(variance);
        }

        // This method computes the variance of the data points.
        private void CalculateVariance()
        {
            variance = DataPoints.Average(x => Math.Pow(x - mean, 2));
        }

        // This method updates all statistical measures.
        private void UpdateStatistics()
        {
            try
            {
                CalculateMean();
                CalculateMedian();
                CalculateMode();
                CalculateStandardDeviation();
                CalculateVariance();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating statistics: {ex.Message}");
                // Handle exceptions or display error messages to the user.
            }
        }

        // Method to be called when component parameters change.
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            UpdateStatistics();
        }
    }
}