using System;
using CleanCalc;
using Xunit;

namespace CleanCalc.Tests;

public class UnitTest1
{
    
        [Fact]
        public void Add_ReturnsCorrectValue()
        {
            double result = Calculator.Add(5, 3);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Subtract_ReturnsCorrectValue()
        {
            double result = Calculator.Subtract(10, 4);
            Assert.Equal(6, result);
        }

        [Fact]
        public void Multiply_ReturnsCorrectValue()
        {
            double result = Calculator.Multiply(4, 5);
            Assert.Equal(20, result);
        }

        [Fact]
        public void Divide_ReturnsCorrectValue()
        {
            double result = Calculator.Divide(20, 4);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => Calculator.Divide(10, 0));
        }

        [Fact]
        public void Power_ReturnsCorrectValue()
        {
            double result = Calculator.Power(2, 3);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Mod_ReturnsCorrectValue()
        {
            double result = Calculator.Mod(10, 3);
            Assert.Equal(1, result);
        }

        [Fact]
        public void Sqrt_ReturnsCorrectValue()
        {
            double result = Calculator.Sqrt(16);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Sqrt_NegativeNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Calculator.Sqrt(-5));
        }
    }


