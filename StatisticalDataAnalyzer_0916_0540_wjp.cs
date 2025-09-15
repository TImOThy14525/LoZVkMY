// 代码生成时间: 2025-09-16 05:40:16
// StatisticalDataAnalyzer.cs
using System;
using System.Collections.Generic;
using System.Linq;

// 统计数据分析器
public class StatisticalDataAnalyzer<T> where T : IComparable<T>
{
    // 存储数据的列表
    private List<T> data;

    // 构造函数
    public StatisticalDataAnalyzer()
    {
        data = new List<T>();
    }

    // 添加数据
    public void AddData(T value)
    {
        data.Add(value);
    }

    // 获取数据总数
    public int GetDataCount()
    {
        return data.Count;
    }

    // 计算平均值
    public T CalculateAverage()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("No data available to calculate average.");
        }

        return data.Average();
    }

    // 计算中位数
    public T CalculateMedian()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("No data available to calculate median.");
        }

        var sortedData = new List<T>(data);
        sortedData.Sort();

        int middle = sortedData.Count / 2;
        if (sortedData.Count % 2 == 0)
        {
            // Even number of data points, return average of middle two
            return (sortedData[middle - 1] + sortedData[middle]) / 2;
        }
        else
        {
            // Odd number of data points, return middle data point
            return sortedData[middle];
        }
    }

    // 计算最大值
    public T CalculateMax()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("No data available to calculate max.");
        }

        return data.Max();
    }

    // 计算最小值
    public T CalculateMin()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("No data available to calculate min.");
        }

        return data.Min();
    }
}
