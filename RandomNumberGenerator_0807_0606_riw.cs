// 代码生成时间: 2025-08-07 06:06:52
using System;
using Microsoft.AspNetCore.Components;

namespace BlazorApp
{
    /// <summary>
    /// A component to generate random numbers.
    /// </summary>
# 改进用户体验
    public partial class RandomNumberGenerator
# 扩展功能模块
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
# 优化算法效率

        private int lowerBound = 1;
        private int upperBound = 100;
        private int randomNumber;

        /// <summary>
        /// Method to generate a random number within the specified bounds.
        /// </summary>
        private async Task GenerateRandomNumber()
        {
            try
# 改进用户体验
            {
                if (lowerBound > upperBound)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Lower bound cannot be greater than upper bound.");
                    return;
                }

                var number = await JSRuntime.InvokeAsync<int>("generateRandomNumber", lowerBound, upperBound);
                randomNumber = number;
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        /// <summary>
        /// Method invoked when the component is initialized.
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GenerateRandomNumber();
            }
            await base.OnAfterRenderAsync(firstRender);
# 优化算法效率
        }
    }
}
