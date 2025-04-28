using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using task3.ViewModels;

namespace task3.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        // 1) Уведомление о самом клике
        var mgr = new WindowNotificationManager(this) { Position = NotificationPosition.TopRight };
        mgr.Show(new Notification("Debug", "Click в код-behind!", NotificationType.Information));

        // 2) Попробуем вручную вызвать команду из VM
        if (DataContext is MainViewModel vm && vm.AddConveyorCommand.CanExecute(null))
            vm.AddConveyorCommand.Execute(null);
    }
}
