// 代码生成时间: 2025-08-26 23:30:55
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorNotificationSystem
{
    // 消息通知类
    public class NotificationService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string NotificationMethod = "notification.show";

        public NotificationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        // 显示通知消息
        public async Task ShowNotificationAsync(string message)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(NotificationMethod, message);
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error showing notification: {ex.Message}");
            }
        }
    }

    // Blazor 组件
    public class NotificationComponent : ComponentBase
    {
        private readonly NotificationService _notificationService;
        private string _notificationMessage;

        [Inject]
        public NotificationComponent(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // 发送通知消息
        public async Task SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message cannot be null or empty.");
            }

            await _notificationService.ShowNotificationAsync(message);
        }
    }
}
