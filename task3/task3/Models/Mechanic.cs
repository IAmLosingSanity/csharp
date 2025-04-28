// Models/Mechanic.cs

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace task3.Models;

public class Mechanic : IMechanic
{
    private bool _isRepairing;

    public event Action? RepairStarted;
    public event Action<double>? RepairProgress;
    public event Action? RepairCompleted;

    public void Repair(Conveyor conveyor)
    {
        if (_isRepairing) 
            return;

        _isRepairing = true;

        _ = Task.Run(async () =>
        {
            // 1) Задержка перед тем, как начать чинить
            await Task.Delay(1000);               // <- 1 секунда ожидания

            // 2) Событие что ремонт стартовал (Status -> Repairing)
            RepairStarted?.Invoke();

            // 3) Собственно прогресс ремонта
            const int total = 4000;
            int elapsed = 0;
            const int step = 100;
            while (elapsed < total)
            {
                await Task.Delay(step);
                elapsed += step;
                RepairProgress?.Invoke(Math.Min(1.0, (double)elapsed / total));
            }

            // 4) Фактическая «чинка» через рефлексию
            var method = typeof(Conveyor)
                .GetMethod("Repair", BindingFlags.Instance | BindingFlags.Public);
            method?.Invoke(conveyor, null);

            // 5) Финал
            RepairCompleted?.Invoke();
            _isRepairing = false;
        });
    }
}
