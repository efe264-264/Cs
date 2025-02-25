using System;
namespace RG1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double result = 0;
            Console.WriteLine("请输入第一个数字");
            double Op1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入第二个数字");
            double Op2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入运算符");
            char Operator = char.Parse(Console.ReadLine());
            switch (Operator)
            {
                case '+':
                    result = Op1 + Op2;
                    break;
                case '-':
                    result = Op1 - Op2;
                    break;
                case '*':
                    result = Op1 * Op2;
                    break;
                case '/':
                    result = Op1 / Op2;
                    break;
                default:
                    Console.WriteLine("error");
                    return;
            }

            Console.WriteLine("\n结果是：");
            Console.WriteLine(result);
        }
    }
}
