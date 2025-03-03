using System;

namespace task1.Models
{
    public class ComplexNumber
    {
        public double Real { get; }
        public double Imaginary { get; }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public override string ToString()
        {
            return $"{Real} {(Imaginary >= 0 ? "+" : "")} {Imaginary}i";
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double real = a.Real * b.Real - a.Imaginary * b.Imaginary;
            double imaginary = a.Real * b.Imaginary + a.Imaginary * b.Real;
            return new ComplexNumber(real, imaginary);
        }

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
            if (denominator == 0) throw new DivideByZeroException("Attempted to divide by zero.");

            double real = (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator;
            double imaginary = (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator;
            return new ComplexNumber(real, imaginary);
        }

        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            return a.Real == b.Real && a.Imaginary == b.Imaginary;
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }
        public override bool Equals(object? obj)
        {
            if (obj is ComplexNumber other)
                return Real == other.Real && Imaginary == other.Imaginary;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

        }
}
