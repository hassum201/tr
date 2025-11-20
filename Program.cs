using System;
using System;

namespace CleanCalc
{
    /// <summary>
    /// Calculator with safe, validated operations.
    /// </summary>
    public static class Calculator
    {
        public static double Add(double a, double b) => a + b;

        public static double Subtract(double a, double b) => a - b;

        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("No se puede dividir entre cero.");

            return a / b;
        }

        public static double Power(double a, double b) => Math.Pow(a, b);

        public static double Mod(double a, double b) => a % b;

        public static double Sqrt(double a)
        {
            if (a < 0)
                throw new ArgumentException("No se puede hacer raíz cuadrada de un número negativo.");

            return Math.Sqrt(a);
        }
    }
}




