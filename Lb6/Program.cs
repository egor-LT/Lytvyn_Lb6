using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb6
{

    public class Calculator<T>
    {
        public delegate T Operation(T x, T y);

        public Operation Addition { get; } = (x, y) => (dynamic)x + (dynamic)y;
        public Operation Subtraction { get; } = (x, y) => (dynamic)x - (dynamic)y;
        public Operation Multiplication { get; } = (x, y) => (dynamic)x * (dynamic)y;

        public Operation Division
        {
            get
            {
                return (x, y) =>
                {
                    if ((dynamic)y == 0)
                    {
                        throw new ArgumentException("Ділення на нуль неможливе.");
                    }
                    return (dynamic)x / (dynamic)y;
                };
            }
        }

        public T PerformOperation(Operation operation, T x, T y)
        {
            return operation(x, y);
        }
    }

    class Program
    {
        static void Main()
        {
            Calculator<int> intCalculator = new Calculator<int>();

            int intResultAdd = intCalculator.PerformOperation(intCalculator.Addition, 5, 3);
            int intResultSub = intCalculator.PerformOperation(intCalculator.Subtraction, 10, 4);
            int intResultMul = intCalculator.PerformOperation(intCalculator.Multiplication, 7, 2);
            int intResultDiv = intCalculator.PerformOperation(intCalculator.Division, 15, 3);

            Console.WriteLine($"Результат додавання: {intResultAdd}");
            Console.WriteLine($"Результат віднімання: {intResultSub}");
            Console.WriteLine($"Результат множення: {intResultMul}");
            Console.WriteLine($"Результат ділення: {intResultDiv}");
        }
    }
}