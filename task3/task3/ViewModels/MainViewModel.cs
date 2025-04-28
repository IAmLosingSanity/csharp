using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using task3.Models;

namespace task3.ViewModels;

public class MainViewModel
{
    public ObservableCollection<ConveyorViewModel> Conveyors { get; } = new();

    public ICommand AddConveyorCommand { get; }

    public MainViewModel()
    {
        AddConveyorCommand = new RelayCommand(OnAddConveyor);
    }

    private void OnAddConveyor()
    {
        var conveyor = new Conveyor();
        var vm = new ConveyorViewModel(conveyor);
        Conveyors.Add(vm);

        conveyor.Start();
    }
}
