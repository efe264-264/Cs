namespace assignment2_1prime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = GetInput();
           List<int> myPrime= Prime(number);
            
            Console.WriteLine($"整数 {number} 的素数因子为：{string.Join(" ", myPrime)}");
        }
        // 获取用户输入的合法整数
        static int GetInput()
        {
            int number;
            while (true)
            {
                Console.Write("请输入你的数字：");
                string input = Console.ReadLine();

                // 验证输入是否为有效整数且大于1
                if (int.TryParse(input, out number)/*转换类型成功*/ )
                {
                    if(number > 1)
                    return number;
                    else
                    {
                        Console.WriteLine("输入不大于1，非质数！");
                    }
                }
                Console.WriteLine("输入无效");
            }
        }

      
        static List<int> Prime(int n)
        {
           //用集合存储所有的质数因子
            HashSet<int> myPrime = new HashSet<int>();
            //计算部分
            while (n % 2 == 0)//首先查找2的
            {
                myPrime.Add(2);
                n /= 2;
            }

            
            for (int i = 3; i * i <= n; i += 2)//然后再找所有奇数
            {
                while (n % i == 0)
                {
                    myPrime.Add(i);
                    n /= i;
                }
            }

            
            if (n > 2)//最后剩下的也是
            {
                myPrime.Add(n);
            }

            return myPrime.ToList();
        }
    }
}
