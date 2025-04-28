using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace task3.Extensions;

public static class WindowExtensions
{
    public static void OpenNotification(this Window window, string message)
    {
        var manager = new WindowNotificationManager(window)
        {
            Position = NotificationPosition.TopRight
        };

        manager.Show(new Notification("Уведомление", message, NotificationType.Information));
    }
}
