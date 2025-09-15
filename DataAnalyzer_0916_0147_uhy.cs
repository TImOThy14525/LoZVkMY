// 代码生成时间: 2025-09-16 01:47:53
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalysisApp
{
    public class DataAnalyzer
    {
        /// <summary>
        /// 分析并返回数据的平均值
        /// </summary>
        /// <param name="data">输入的数据列表</param>
        /// <returns>数据的平均值</returns>
        public double CalculateAverage(List<double> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("数据列表为空或为null.");
            }

            return data.Average();
        }

        /// <summary>
        /// 分析并返回数据的最大值
        /// </summary>
        /// <param name="data">输入的数据列表</param>
        /// <returns>数据的最大值</returns>
        public double FindMaximum(List<double> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("数据列表为空或为null.");
            }

            return data.Max();
        }

        /// <summary>
        /// 分析并返回数据的最小值
        /// </summary>
        /// <param name="data">输入的数据列表</param>
        /// <returns>数据的最小值</returns>
        public double FindMinimum(List<double> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("数据列表为空或为null.");
            }

            return data.Min();
        }

        /// <summary>
        /// 分析并返回数据的标准差
        /// </summary>
        /// <param name="data">输入的数据列表</param>
        /// <returns>数据的标准差</returns>
        public double CalculateStandardDeviation(List<double> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("数据列表为空或为null.");
            }

            double average = data.Average();
            double sumOfSquares = data.Sum(x => (x - average) * (x - average));
            return Math.Sqrt(sumOfSquares / data.Count);
        }
    }
}
