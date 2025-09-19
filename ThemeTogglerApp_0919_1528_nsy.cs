// 代码生成时间: 2025-09-19 15:28:40
using Microsoft.AspNetCore.Components;

namespace ThemeTogglerApp
{
    public class ThemeTogglerApp : ComponentBase
    {
        // Assuming the Theme enum is defined in a shared place
        [CascadingParameter]
        private Theme ThemeState { get; set; }

        // EventCallback to handle theme change
        private EventCallback<Theme> OnThemeChanged { get; set; }

        // Method to toggle the theme
        private void ToggleTheme()
        {
            // Check if ThemeState is not null to avoid potential null reference errors
            if (ThemeState != null)
            {
                // Toggle the theme by setting it to the opposite value
                ThemeState.IsDark = !ThemeState.IsDark;

                // Invoke the OnThemeChanged event to notify parent components
                OnThemeChanged.InvokeAsync(ThemeState);
            }
            else
            {
                // Log or handle error if ThemeState is null
                Console.WriteLine("Error: ThemeState is not set.");
            }
        }

        // Render the toggle button
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "onclick", EventCallback.Factory.Create(this, ToggleTheme));
            builder.AddContent(2, ThemeState.IsDark ? "Switch to Light Theme" : "Switch to Dark Theme");
            builder.CloseElement();
        }
    }

    // The Theme struct to hold theme state
    public struct Theme
    {
        public bool IsDark { get; set; }
    }
}
