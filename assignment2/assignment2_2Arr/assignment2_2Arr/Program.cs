namespace assignment2_2Arr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> myArr = GetInput();
            DoOutput(myArr);
        }

        static List<int>  GetInput()
        {
            List<int> myArr = new List<int>();

            int number;
            
                Console.Write("请输入你的数组，用空格分隔数字：");
                string input = Console.ReadLine();
                string[] inputString = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string numberString in inputString )
                {
                    if (int.TryParse(numberString, out number)) 
                     myArr.Add(number); 
                    else 
                     Console.Write("输入错误");
                }
            
            return myArr;
        }
        static void DoOutput(List<int> myArr)
        {
            Console.Write("最大数="); Console.Write(GetArrMax(myArr)); Console.Write('\n');
            Console.Write("最小数="); Console.Write(GetArrMin(myArr)); Console.Write('\n');
            Console.Write("总和="); Console.Write(GetArrSum(myArr)); Console.Write('\n');
            Console.Write("平均数="); Console.Write(GetArrAve(myArr)); Console.Write('\n');
        }

        static int GetArrMax(List<int> myArr)
        {
            int MaxNumber=myArr.Max();
            return MaxNumber;
        }
        static int GetArrMin(List<int> myArr)
        {
            int MinNumber = myArr.Min();
            return MinNumber;
        }
        static double GetArrAve(List<int> myArr)
        {
            double average = myArr.Average();
            return average;
        }
        static int GetArrSum(List<int> myArr)
        {
            int SumNumber = myArr.Sum();
           
            return SumNumber;
        }


    }
}
