using System;
using System.Threading;
using System.Threading.Tasks;

namespace task3.Models;

public class Conveyor
{
    private static readonly Random random = new Random();

    public event Action? MaterialsEnded;
    public event Action? ConveyorBroken;

    public bool IsWorking { get; private set; } = true;
    public int DetailsOnBelt { get; private set; }

    private CancellationTokenSource? _cts;

    public void Start()
    {
        _cts = new CancellationTokenSource();
        Task.Run(() => RunConveyor(_cts.Token));
    }

    public void Stop()
    {
        _cts?.Cancel();
    }

    private async Task RunConveyor(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(500);

            if (DetailsOnBelt > 0)
            {
                DetailsOnBelt--;
            }
            else
            {
                MaterialsEnded?.Invoke();
            }

            if (random.NextDouble() < 0.05) // 10% вероятность поломки
            {
                IsWorking = false;
                ConveyorBroken?.Invoke();
                while (!IsWorking)
                {
                    await Task.Delay(500);
                }
            }
        }
    }

    public void LoadDetails(int count)
    {
        DetailsOnBelt += count;
    }

    public void Repair()
    {
        IsWorking = true;
    }
}
