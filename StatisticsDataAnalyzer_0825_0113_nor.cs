// 代码生成时间: 2025-08-25 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp
{
    /// <summary>
    /// 统计数据分析器
    /// </summary>
    public class StatisticsDataAnalyzer
    {
        private List<int> _data;

        public StatisticsDataAnalyzer(List<int> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _data = data;
        }

        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <returns>平均值</returns>
        public double CalculateAverage()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("数据列表为空，无法计算平均值。");

            return _data.Average();
        }

        /// <summary>
        /// 计算中位数
        /// </summary>
        /// <returns>中位数</returns>
        public double CalculateMedian()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("数据列表为空，无法计算中位数。");

            var sortedData = _data.OrderBy(x => x).ToList();
            int size = sortedData.Count;
            int middle = size / 2;
            if (size % 2 == 0)
                return (sortedData[middle - 1] + sortedData[middle]) / 2.0;
            else
                return sortedData[middle];
        }

        /// <summary>
        /// 计算最大值
        /// </summary>
        /// <returns>最大值</returns>
        public int CalculateMax()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("数据列表为空，无法计算最大值。");

            return _data.Max();
        }

        /// <summary>
        /// 计算最小值
        /// </summary>
        /// <returns>最小值</returns>
        public int CalculateMin()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("数据列表为空，无法计算最小值。");

            return _data.Min();
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <returns>标准差</returns>
        public double CalculateStandardDeviation()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("数据列表为空，无法计算标准差。");

            double average = _data.Average();
            double variance = _data.Average(x => Math.Pow(x - average, 2));
            return Math.Sqrt(variance);
        }
    }
}
