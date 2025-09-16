// 代码生成时间: 2025-09-16 22:05:19
using Microsoft.AspNetCore.Components;
using System;

namespace RandomNumberGeneratorApp
{
    public class RandomNumberGenerator : ComponentBase
    {
        // 输入参数：最小值和最大值
        private int minValue = 0;
        private int maxValue = 100;

        // 输出参数：生成的随机数
        private int randomNumber;

        // 输入参数：是否显示随机数
        private bool showRandomNumber = false;

        [Parameter]
        public int? MinValue
        {
            get => minValue;
            set
            {
                if (value.HasValue && value.Value < 0)
                {
                    throw new ArgumentException("MinValue cannot be negative.");
                }
                minValue = value.GetValueOrDefault();
            }
        }

        [Parameter]
        public int? MaxValue
        {
            get => maxValue;
            set
            {
                if (value.HasValue && value.Value < 0)
                {
                    throw new ArgumentException("MaxValue cannot be negative.");
                }
                maxValue = value.GetValueOrDefault();
            }
        }

        // 生成随机数
        public void GenerateRandomNumber()
        {
            if (minValue > maxValue)
            {
                throw new InvalidOperationException("MinValue cannot be greater than MaxValue.");
            }

            Random rand = new Random();
            randomNumber = rand.Next(minValue, maxValue + 1);
            showRandomNumber = true;
        }

        // 重置随机数生成器
        public void Reset()
        {
            showRandomNumber = false;
        }
    }
}