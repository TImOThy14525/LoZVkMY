// 代码生成时间: 2025-09-01 19:47:19
using System;
# NOTE: 重要实现细节
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysisApp
{
    // DataAnalyzerApp provides statistical analysis of datasets
    public class DataAnalyzerApp
    {
        // Calculates the average of a dataset
# 改进用户体验
        public double CalculateAverage(double[] dataset)
        {
            try
            {
                if (dataset == null || dataset.Length == 0)
                {
# FIXME: 处理边界情况
                    throw new ArgumentException("Dataset must not be null or empty.");
# NOTE: 重要实现细节
                }

                double sum = dataset.Sum();
                return sum / dataset.Length;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during calculation
                // and rethrow to be handled by the caller
                throw new InvalidOperationException("Error calculating average: " + ex.Message, ex);
            }
        }

        // Calculates the median of a dataset
        public double CalculateMedian(double[] dataset)
        {
            try
            {
                if (dataset == null || dataset.Length == 0)
                {
                    throw new ArgumentException("Dataset must not be null or empty.");
                }

                var sortedData = dataset.OrderBy(d => d).ToArray();
                int middle = sortedData.Length / 2;
# FIXME: 处理边界情况
                return sortedData.Length % 2 == 0 ?
                   (sortedData[middle - 1] + sortedData[middle]) / 2 :
                   sortedData[middle];
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error calculating median: " + ex.Message, ex);
            }
        }
# 增强安全性

        // Calculates the standard deviation of a dataset
        public double CalculateStandardDeviation(double[] dataset)
        {
            try
            {
# 改进用户体验
                if (dataset == null || dataset.Length == 0)
                {
                    throw new ArgumentException("Dataset must not be null or empty.");
                }
# NOTE: 重要实现细节

                double mean = CalculateAverage(dataset);
                double sumOfSquares = dataset.Sum(d => Math.Pow(d - mean, 2));
                return Math.Sqrt(sumOfSquares / dataset.Length);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error calculating standard deviation: " + ex.Message, ex);
            }
        }
    }
}
