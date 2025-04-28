using System;
using System.Threading.Tasks;

namespace task3.Models;

public class Loader
{
    private readonly Conveyor _conveyor;
    private bool _isLoading;

    public event Action? LoadingStarted;
    public event Action<double>? LoadingProgress;
    public event Action? LoadingCompleted;

    public Loader(Conveyor conveyor)
    {
        _conveyor = conveyor;
        _conveyor.MaterialsEnded += StartLoading;
    }

    public void StartLoading()
    {
        if (_isLoading) return;
        _isLoading = true;
        LoadingStarted?.Invoke();

        _ = Task.Run(async () =>
        {
            const int total = 3000;
            int elapsed = 0;
            const int step = 100;
            while (elapsed < total)
            {
                await Task.Delay(step);
                elapsed += step;
                LoadingProgress?.Invoke(Math.Min(1.0, (double)elapsed / total));
            }

            _conveyor.LoadDetails(10);
            LoadingCompleted?.Invoke();
            _isLoading = false;
        });
    }
}
