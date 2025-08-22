// 代码生成时间: 2025-08-23 01:32:00
// ThemeSwitcherApp.cs
// 这个类实现了Blazor应用的主题切换功能。

using Microsoft.AspNetCore.Components;

namespace YourNamespace;

public class ThemeSwitcherApp : ComponentBase
{
    // 定义主题颜色的枚举
    public enum ThemeColors
    {
        Light,
        Dark
    }

    // 当前主题颜色
    [Parameter]
    public ThemeColors CurrentTheme { get; set; } = ThemeColors.Light;

    // 切换主题颜色的方法
    public void ToggleTheme()
    {
        try
        {
            if (CurrentTheme == ThemeColors.Light)
            {
                // 如果当前是浅色主题，则切换到深色主题
                CurrentTheme = ThemeColors.Dark;
            }
            else
            {
                // 否则切换回浅色主题
                CurrentTheme = ThemeColors.Light;
            }

            // 将主题颜色变化保存到LocalStorage中，以保持用户偏好
            SaveThemePreference(CurrentTheme);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error while toggling theme: {ex.Message}");
        }
    }

    // 从LocalStorage加载主题偏好
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 尝试从LocalStorage获取主题偏好，如果存在则应用
        ThemeColors? storedTheme = LoadThemePreference();
        if (storedTheme.HasValue)
        {
            CurrentTheme = storedTheme.Value;
        }
    }

    // 将主题颜色保存到LocalStorage
    private void SaveThemePreference(ThemeColors theme)
    {
        string themeKey = "themePreference";
        string themeValue = theme.ToString().ToLower();

        // 使用浏览器的LocalStorage API来保存主题偏好
        var localStorage = (IJSRuntime)typeof(JSRuntime)
            .GetField("jsRuntime", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
            .GetValue(null);

        localStorage.InvokeVoidAsync("localStorage.setItem", themeKey, themeValue);
    }

    // 从LocalStorage加载主题偏好
    private ThemeColors? LoadThemePreference()
    {
        string themeKey = "themePreference";

        // 使用浏览器的LocalStorage API来加载主题偏好
        var localStorage = (IJSRuntime)typeof(JSRuntime)
            .GetField("jsRuntime", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
            .GetValue(null);

        string? storedTheme = localStorage.Invoke<string>("localStorage.getItem", themeKey);

        if (Enum.TryParse(storedTheme, true, out ThemeColors theme))
        {
            return theme;
        }

        return null;
    }
}
