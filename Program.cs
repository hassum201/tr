using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;



namespace CleanCalc
{
    public class CalcHistoryEntry
    {
        public double A { get; }
        public double B { get; }
        public string Operator { get; }
        public double Result { get; }

        public CalcHistoryEntry(double a, double b, string op, double result)
        {
            A = a;
            B = b;
            Operator = op;
            Result = result;
        }

        public override string ToString()
        {
            return $"{A}|{B}|{Operator}|{Result}";
        }
    }

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
            if (Math.Abs(b) < 1e-12)

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

    /// <summary>
    /// Main program class with clear menu and history.
    /// </summary>
    internal class Program
    {
        private static readonly List<CalcHistoryEntry> History = new List<CalcHistoryEntry>();

        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                PrintMenu();
                Console.Write("Opción: ");
                string opt = Console.ReadLine()?.Trim();

                switch (opt)
                {
                    case "1": HandleBinaryOperation("+", Calculator.Add); break;
                    case "2": HandleBinaryOperation("-", Calculator.Subtract); break;
                    case "3": HandleBinaryOperation("*", Calculator.Multiply); break;
                    case "4": HandleBinaryOperation("/", Calculator.Divide); break;
                    case "5": HandleBinaryOperation("^", Calculator.Power); break;
                    case "6": HandleBinaryOperation("%", Calculator.Mod); break;
                    case "7": HandleUnaryOperation("sqrt", Calculator.Sqrt); break;
                    case "8": Console.WriteLine("Función LLM eliminada por seguridad."); break;
                    case "9": ShowHistory(); break;
                    case "0": exit = true; break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine();
            }

            SaveHistoryToFile();
            Console.WriteLine("Adiós.");
        }

        private static void PrintMenu()
        {
            Console.WriteLine("=== CLEAN CALCULATOR ===");
            Console.WriteLine("1) Sumar");
            Console.WriteLine("2) Restar");
            Console.WriteLine("3) Multiplicar");
            Console.WriteLine("4) Dividir");
            Console.WriteLine("5) Potencia");
            Console.WriteLine("6) Módulo");
            Console.WriteLine("7) Raíz cuadrada");
            Console.WriteLine("8) LLM (deshabilitado)");
            Console.WriteLine("9) Historial");
            Console.WriteLine("0) Salir");
        }

        private static void HandleBinaryOperation(string opSymbol, Func<double, double, double> operation)
        {
            try
            {
                double a = ReadDouble("a: ");
                double b = ReadDouble("b: ");

                double result = operation(a, b);

                Console.WriteLine($"= {result.ToString(CultureInfo.InvariantCulture)}");

                AddToHistory(a, b, opSymbol, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void HandleUnaryOperation(string opSymbol, Func<double, double> operation)
        {
            try
            {
                double a = ReadDouble("a: ");

                double result = operation(a);

                Console.WriteLine($"= {result.ToString(CultureInfo.InvariantCulture)}");

                AddToHistory(a, 0, opSymbol, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static double ReadDouble(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine()?.Replace(',', '.');

                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                    return value;

                Console.WriteLine("Entrada inválida. Intente nuevamente.");
            }
        }

        private static void AddToHistory(double a, double b, string op, double result)
        {
            var entry = new CalcHistoryEntry(a, b, op, result);
            History.Add(entry);

            File.AppendAllText("history.txt", entry + Environment.NewLine);
        }

        private static void ShowHistory()
        {
            Console.WriteLine("--- Historial ---");

            if (History.Count == 0)
            {
                Console.WriteLine("No hay operaciones.");
                return;
            }

            foreach (var entry in History)
                Console.WriteLine(entry);
        }

        private static void SaveHistoryToFile()
        {
            try
            {
                File.WriteAllLines("leftover.tmp", History.ConvertAll(h => h.ToString()));
            }
            catch
            {
                Console.WriteLine("No se pudo guardar leftover.tmp");
            }
        }
    }
}




