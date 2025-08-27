// 代码生成时间: 2025-08-27 17:13:36
using Microsoft.AspNetCore.Components;

namespace ResponsiveLayoutApp
{
    /// <summary>
    /// Represents a responsive layout component.
    /// </summary>
    public class ResponsiveLayoutApp : ComponentBase
    {
        /// <summary>
        /// Handles the resize event to adjust the layout.
        /// </summary>
        [Parameter]
        public EventCallback OnResize { get; set; }

        /// <summary>
        /// The current size of the layout.
        /// </summary>
        private string layoutSize = "default";

        protected override void OnInitialized()
        {
            // Initialize layout size based on the current window size.
            UpdateLayoutSize();
        }

        /// <summary>
        /// Updates the layout size based on the current window size.
        /// </summary>
        private void UpdateLayoutSize()
        {
            if (this.Window.GetWidth() >= 1200)
            {
                layoutSize = "large";
            }
            else if (this.Window.GetWidth() >= 992)
            {
                layoutSize = "medium";
            }
            else if (this.Window.GetWidth() >= 768)
            {
                layoutSize = "small";
            }
            else
            {
                layoutSize = "extra-small";
            }
        }

        /// <summary>
        /// Invoked when the window is resized.
        /// </summary>
        protected void OnWindowResize()
        {
            try
            {
                UpdateLayoutSize();
                OnResize.InvokeAsync(null);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the resize event.
                Console.WriteLine($"Error handling window resize: {ex.Message}");
            }
        }

        /// <summary>
        /// Renders the responsive layout component.
        /// </summary>
        /// <returns>A RenderFragment representing the layout.</returns>
        public RenderFragment RenderLayout()
        {
            return builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", $"layout-{layoutSize}");
                builder.CloseElement();
            };
        }

        /// <summary>
        /// Returns the current layout size.
        /// </summary>
        /// <returns>The current layout size as a string.</returns>
        [Parameter]
        public string CurrentLayoutSize
        {
            get => layoutSize;
            set
            {
                layoutSize = value;
                StateHasChanged();
            }
        }
    }
}
