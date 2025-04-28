using System;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using task3.Models;

namespace task3.ViewModels;

public partial class ConveyorViewModel : ObservableObject
{
    private readonly Conveyor _conveyor;
    private readonly Loader _loader;
    private readonly Mechanic _mechanic;

    [ObservableProperty] private int _detailsOnBelt;
    [ObservableProperty] private double _loadingProgress;
    [ObservableProperty] private double _repairProgress;
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(StatusIcon))]
    [NotifyPropertyChangedFor(nameof(StatusText))]
    [NotifyPropertyChangedFor(nameof(IsLoading))]
    [NotifyPropertyChangedFor(nameof(IsRepairing))]
    private ConveyorStatus _status;

    public string StatusIcon => Status switch
    {
        ConveyorStatus.Working   => "✔",
        ConveyorStatus.Broken    => "❌",
        ConveyorStatus.Loading   => "🔄",
        ConveyorStatus.Repairing => "🔧",
        _                        => ""
    };

    public string StatusText => Status switch
    {
        ConveyorStatus.Working   => "Работает",
        ConveyorStatus.Broken    => "Сломан",
        ConveyorStatus.Loading   => "Загрузка",
        ConveyorStatus.Repairing => "Ремонт",
        _                        => ""
    };

    public bool IsLoading   => Status == ConveyorStatus.Loading;
    public bool IsRepairing => Status == ConveyorStatus.Repairing;

    public ConveyorViewModel(Conveyor conveyor)
    {
        _conveyor = conveyor;
        _loader    = new Loader(conveyor);
        _mechanic  = new Mechanic();
        Status     = ConveyorStatus.Working;

        // подписываемся на Loader
        _loader.LoadingStarted   += () => Status = ConveyorStatus.Loading;
        _loader.LoadingProgress  += p  => LoadingProgress = p;
        _loader.LoadingCompleted += () => Status = ConveyorStatus.Working;

        // подписываемся на Mechanic
        _mechanic.RepairStarted   += () => Status = ConveyorStatus.Repairing;
        _mechanic.RepairProgress  += p  => RepairProgress = p;
        _mechanic.RepairCompleted += () => Status = ConveyorStatus.Working;

        // события из Conveyor
        _conveyor.MaterialsEnded += () => _loader.StartLoading();
        _conveyor.ConveyorBroken += () =>
        {
            Status = ConveyorStatus.Broken;
            _mechanic.Repair(_conveyor);
        };

        // таймер обновления деталей на ленте
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(200)
        };
        timer.Tick += (_, _) => DetailsOnBelt = _conveyor.DetailsOnBelt;
        timer.Start();
    }
}
