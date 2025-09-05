// 代码生成时间: 2025-09-05 23:34:16
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

// 定义用户类
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
}

// 定义用户登录验证系统
public class UserLoginSystem
{
    // 登录用户信息列表
    private static readonly List<User> users = new List<User>
    {
        new User { Username = "admin", Password = "password123" } // 示例用户
    };

    // 异步登录方法
    public async Task<string> LoginAsync(string username, string password)
    {
        foreach (var user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                return "Login successful";
            }
        }
        return "Invalid username or password";
    }
}

// Blazor 组件
public class UserLoginComponent : ComponentBase
{
    [Inject]
    private UserLoginSystem UserLoginSystem { get; set; }

    [Parameter]
    public EventCallback<string> OnLoginSuccess { get; set; }

    private InputText usernameInput;
    private InputText passwordInput;
    private bool isLoading = false;
    private string loginResult;

    // 注入用户登录验证系统
    protected override void OnInitialized()
    {
        UserLoginSystem = new UserLoginSystem();
    }

    // 登录方法
    private async Task HandleLogin()
    {
        isLoading = true;
        loginResult = await UserLoginSystem.LoginAsync(usernameInput.Value, passwordInput.Value);
        isLoading = false;

        if (loginResult == "Login successful")
        {
            OnLoginSuccess.InvokeAsync(loginResult);
        }
        else
        {
            Console.WriteLine(loginResult); // 打印错误信息
        }
    }
}
