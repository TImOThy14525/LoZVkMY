// 代码生成时间: 2025-09-18 09:30:49
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJs.Blazor;

namespace InteractiveChartGenerator
{
    /// <summary>
    /// Component for generating interactive charts.
    /// </summary>
    public partial class InteractiveChartGenerator
    {
        private ChartConfig _chartConfig = new ChartConfig
        {
            Options = new ChartOptions
            {
                Title = new ChartTitle
                {
                    Display = true,
                    Text = "Interactive Chart"
                },
                Tooltips = new TooltipOptions
                {
                    Enabled = true,
                    Mode = TooltipMode.Index,
                    Intercept = false,
                }
            }
        };

        private List<ChartPoint> _dataPoints = new List<ChartPoint>();
        private bool _isLoading = false;
        private string _errorMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the InteractiveChartGenerator component.
        /// </summary>
        public InteractiveChartGenerator()
        {
            // Populate initial data points.
            _dataPoints = Enumerable.Range(1, 10)
                .Select(i => new ChartPoint { XValue = i, YValue = Math.Sin(i) })
                .ToList();
        }

        /// <summary>
        /// Handles the click event to generate a new data point.
        /// </summary>
        private async Task GenerateDataPoint()
        {
            if (_isLoading) return;

            _isLoading = true;
            _errorMessage = string.Empty;

            try
            {
                // Simulate data generation.
                var newDataPoint = new ChartPoint { XValue = DateTime.Now.Millisecond, YValue = Math.Cos(DateTime.Now.Millisecond) };
                _dataPoints.Add(newDataPoint);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            finally
            {
                _isLoading = false;
            }
        }

        /// <summary>
        /// Renders the chart component.
        /// </summary>
        private RenderFragment RenderChart()
        {
            return builder =>
            {
                builder.OpenComponent<ChartJsChart>(0);
                builder.AddAttribute(1, "Configuration", _chartConfig);
                builder.AddAttribute(2, "ChildContent", RenderChartJsData.SimpleLinechart(3, _dataPoints));
                builder.CloseComponent();
            };
        }

        /// <summary>
        /// Renders the error message if any.
        /// </summary>
        private RenderFragment RenderErrorMessage()
        {
            return builder =>
            {
                if (!string.IsNullOrEmpty(_errorMessage))
                {
                    builder.OpenElement(0, "div");
                    builder.AddAttribute(1, "class", "alert alert-danger");
                    builder.AddContent(2, _errorMessage);
                    builder.CloseElement();
                }
            };
        }
    }
}
