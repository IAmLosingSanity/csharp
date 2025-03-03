using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace task1.ViewModels
{
    public partial class ComplexNumberViewModel : ObservableObject
    {
        [ObservableProperty]
        private double real1;

        [ObservableProperty]
        private double imaginary1;

        [ObservableProperty]
        private double real2;

        [ObservableProperty]
        private double imaginary2;

        [ObservableProperty]
        private string result = string.Empty;

        [RelayCommand]
        private void Add()
        {
            var num1 = new Models.ComplexNumber(Real1, Imaginary1);
            var num2 = new Models.ComplexNumber(Real2, Imaginary2);
            Result = (num1 + num2).ToString();
        }

        [RelayCommand]
        private void Subtract()
        {
            var num1 = new Models.ComplexNumber(Real1, Imaginary1);
            var num2 = new Models.ComplexNumber(Real2, Imaginary2);
            Result = (num1 - num2).ToString();
        }

        [RelayCommand]
        private void Multiply()
        {
            var num1 = new Models.ComplexNumber(Real1, Imaginary1);
            var num2 = new Models.ComplexNumber(Real2, Imaginary2);
            Result = (num1 * num2).ToString();
        }

        [RelayCommand]
        private void Divide()
        {
            try
            {
                var num1 = new Models.ComplexNumber(Real1, Imaginary1);
                var num2 = new Models.ComplexNumber(Real2, Imaginary2);
                Result = (num1 / num2).ToString();
            }
            catch (DivideByZeroException)
            {
                Result = "Ошибка: деление на ноль";
            }
        }
    }
}
