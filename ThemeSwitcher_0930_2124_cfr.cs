// 代码生成时间: 2025-09-30 21:24:50
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp
{
    // Define the Theme enum to represent different themes available in the application.
    public enum Theme
    {
        Light,
        Dark,
        Custom
    }

    // Declare a ThemeService class that will manage the theme settings.
    public class ThemeService
    {
        private Theme currentTheme = Theme.Light;
        private readonly Dictionary<string, string> themes = new Dictionary<string, string>
        {
            { Theme.Light.ToString(), "light-theme" },
            { Theme.Dark.ToString(), "dark-theme" },
            { Theme.Custom.ToString(), "custom-theme" }
        };

        // Inject the IJSRuntime to interact with JavaScript interop.
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        // Method to toggle the current theme.
        public void ToggleTheme()
        {
            try
            {
                // Determine the next theme based on the current theme.
                Theme nextTheme;
                switch (currentTheme)
                {
                    case Theme.Light:
                        nextTheme = Theme.Dark;
                        break;
                    case Theme.Dark:
                        nextTheme = Theme.Custom;
                        break;
                    case Theme.Custom:
                        nextTheme = Theme.Light;
                        break;
                    default:
                        nextTheme = Theme.Light;
                        break;
                }

                // Update the current theme.
                currentTheme = nextTheme;
                
                // Invoke JavaScript to update the theme class on the document body.
                JSRuntime.InvokeVoidAsync("updateTheme", themes[currentTheme.ToString()]);
            }
            catch (Exception ex)
            {
                // Log or handle the error appropriately.
                Console.WriteLine($"Error switching theme: {ex.Message}");
            }
        }

        // Method to get the current theme.
        public Theme GetCurrentTheme()
        {
            return currentTheme;
        }
    }

    // Define a ThemeSwitcher component that uses the ThemeService to switch themes.
    public class ThemeSwitcher : ComponentBase
    {
        [Inject]
        private ThemeService ThemeService { get; set; }

        // Method to invoke theme toggle functionality.
        protected void OnThemeToggleClick()
        {
            ThemeService.ToggleTheme();
        }
    }
}
