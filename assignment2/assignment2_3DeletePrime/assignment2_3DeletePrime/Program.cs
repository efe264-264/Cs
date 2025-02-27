namespace assignment2_3DeletePrime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OutPrime(DeleteP());
        }
        static List<int> DeleteP()
        {
            List<int> Primes = new List<int>();
            bool PrimeFlag=true;
            for(int i = 2; i <= 100; i++)
            {
                for(int j = 2; j <= 100; j++)
                {
                    if (PrimeForX(i, j)&&i!=j)
                    {
                        PrimeFlag = false;
                    }
                }
                if (PrimeFlag)
                { /**/
                    Primes.Add(i);

                }
                else PrimeFlag = true;
            }
            return Primes;
        }
        static bool PrimeForX(int x,int j)
        {
            return (x % j == 0);
        }
        static void OutPrime(List<int> Primes)
        {
            foreach(int num in Primes)
            {
                Console.Write(num); Console.Write(' ');
            }
        }
    }
}
