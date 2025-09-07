// 代码生成时间: 2025-09-08 06:58:05
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationSystem
{
    // Interface for message notification
    public interface INotification {
        Task SendAsync(string message);
    }

    // Concrete implementation of the notification service
    public class NotificationService : INotification {
        private readonly List<Func<string, Task>> _subscribers;

        public NotificationService() {
            _subscribers = new List<Func<string, Task>>();
        }

        // Method to subscribe to notifications
        public void Subscribe(Func<string, Task> subscriber) {
            // Ensure subscriber is not null
            if (subscriber == null) {
                throw new ArgumentNullException(nameof(subscriber), "Subscriber cannot be null.");
            }

            _subscribers.Add(subscriber);
        }

        // Method to unsubscribe from notifications
        public void Unsubscribe(Func<string, Task> subscriber) {
            if (subscriber == null) {
                throw new ArgumentNullException(nameof(subscriber), "Subscriber cannot be null.");
            }

            _subscribers.Remove(subscriber);
        }

        // Method to send a notification to all subscribers
        public async Task SendAsync(string message) {
            if (message == null) {
                throw new ArgumentNullException(nameof(message), "Message cannot be null.");
            }

            foreach (var subscriber in _subscribers) {
                await subscriber(message); // Call each subscriber with the message
            }
        }
    }
}
