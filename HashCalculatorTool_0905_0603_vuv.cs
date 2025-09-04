// 代码生成时间: 2025-09-05 06:03:19
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components;

// 组件类，用于哈希值计算工具
public partial class HashCalculatorTool
{
    [Parameter]
    public string InputText { get; set; } = "";

    [Parameter]
    public EventCallback<string> OnHashCalculated { get; set; }

    private string hashResult = "";

    // 计算哈希值的方法
    private void CalculateHash()
    {
        try
        {
            // 根据输入文本计算哈希值
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(InputText));
                hashResult = BitConverter.ToString(bytes).Replace("-", string.Empty);
            }

            // 触发事件，通知哈希值已计算
            OnHashCalculated.InvokeAsync(hashResult);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error calculating hash: {ex.Message}");
        }
    }

    // 清除哈希结果的方法
    private void ClearHash()
    {
        hashResult = string.Empty;
    }
}

// Razor组件，用于显示界面
@code {
    // 输入框的文本改变事件处理器
    private void OnTextChanged(string value)
    {
        // 当文本改变时，计算哈希值
        if (!string.IsNullOrEmpty(value))
        {
            CalculateHash();
        }
    }

    // 清除按钮的点击事件处理器
    private void OnClearButtonClick()
    {
        // 清除哈希结果
        ClearHash();
    }
}

@InputText
@Button("Calculate", onclick: () => CalculateHash())
@Button("Clear", onclick: () => ClearHash())
@DisplayFor(m => hashResult)