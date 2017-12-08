namespace Prime
{
    using System;
    using System.Numerics;
    using static System.Console;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                long n=0;
                int a=0;
                do
                {
                    WriteLine("Введите число n > 4");
                    long.TryParse(ReadLine(), out n);
                }
                while (n < 5);

                //WriteLine("Введите основание");
                //int.TryParse(ReadLine(), out int a);

                if (n % 2 == 0)
                {
                    WriteLine($"Число {n} составное");
                }
                else
                {
                    WriteLine(MillerRabinTest(n, a));
                }

                WriteLine("Повторить тест?");
                key = ReadKey();
            }
            while (key.Key == ConsoleKey.Y);
        }

        private static bool MillerRabinTest(long n, int a)
        {
            FindParams(n, out var s, out var r); // 1

            var rand = new Random();            // 2
            a = rand.Next(2, (int)n - 2);

            long y = (long)BigInteger.ModPow(a, r, n); // 3
            if (y != 1 && y != n - 1) // 4
            {
                for (int i = 0; i < s - 1; i++) // 4.1
                {
                    if (y == n - 1)
                    {
                        return true;
                    }

                    y = (long)BigInteger.ModPow(y, 2, n); // 4.2.1
                    if (y == 1)  //4.2.2
                    {
                        return false;
                    }
                }  // 4.2.3

                if (y != n - 1) // 4.3
                {
                    return false;
                }
            }

            return true; // 5
        }

        private static void FindParams(long n, out int s, out long r)
        {
            r = (n - 1) >> 1;
            for (s = 1;; s++)
            {
                if (r % 2 == 0)
                {
                    r >>= 1;
                }
                else
                {
                    break;
                }
            }
        } 
    }
}
