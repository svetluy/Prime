namespace Prime
{
    using System;
    using static System.Numerics.BigInteger;
    using static System.Math;
    using static System.Console;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                long n;
                //int a=0;
                do
                {
                    WriteLine("Введите число n > 4");
                    long.TryParse(ReadLine(), out n);
                }
                while (n < 5);

                WriteLine("Введите основание");
                int.TryParse(ReadLine(), out int a);

                WriteLine("Ferma test");
                if (n % 2 == 0)
                {
                    WriteLine($"Число {n} составное");
                }
                else
                {
                    WriteLine(FermaTest(n,a));
                }

                WriteLine("Miller-Rabin test");
                if (n % 2 == 0)
                {
                    WriteLine($"Число {n} составное");
                }
                else
                {
                    WriteLine(MillerRabinTest(n,a));
                }

                WriteLine("Solovey-Strassen test");
                if (n % 2 == 0)
                {
                    WriteLine($"Число {n} составное");
                }
                else
                {
                    WriteLine(SolovayStrassenTest(n, a));
                }


                //WriteLine("Luka-Lemer test");
                //if (n % 2 == 0)
                //{
                //    WriteLine($"Число {n} составное");
                //}
                //else
                //{
                //    WriteLine(LukaLemerTest(n));
                //}

                WriteLine("Повторить тест?\n");
                key = ReadKey();
            }
            while (key.Key == ConsoleKey.Y);
        }

        private static bool MillerRabinTest(long n, int a)
        {
            FindParams(n, out var s, out var r); // 1

            var rand = new Random();            // 2
            a = rand.Next(2, (int)n - 2);

            long y = (long)ModPow(a, r, n); // 3
            if (y != 1 && y != n - 1) // 4
            {
                for (int i = 0; i < s - 1; i++) // 4.1
                {
                    if (y == n - 1)
                    {
                        return true;
                    }

                    y = (long)ModPow(y, 2, n); // 4.2.1
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

        private static bool FermaTest(long n, int a)
        {
            //var rand = new Random();            // 2
            //a = rand.Next(2, (int)n - 2);

            if ((int)ModPow(a, n - 1, n) == 1)
                return true;
            return false;
        }

        private static bool SolovayStrassenTest(long n, int a)
        {
            if (a <= 0) throw new ArgumentOutOfRangeException(nameof(a));
            var rand = new Random();            // 2
            a = rand.Next(2, (int)n - 2);

            int r = (int)ModPow(a, (n - 1) / 2, n);
            if (r != 1 && r!=n-1)
                return false;

            long s = a / n;
            if (r == (int) (s % n))
            {
                return false;
            }
            return true;
        }

        private static bool LukaLemerTest(long n)
        {
            if (!IsExponentPrime(n,out int s))
                return false;
            long l = 4;
            for (int i = 0; i < s-2; i++)
            {
                l =  ((long)Pow(l,2)-2) % n;
                if (l== 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsExponentPrime(long n, out int s)
        {
            long m = ( n + 1)/2;
            for (s = 1; ; ++s)
            {
                if (m % 2 == 0)
                {
                    m >>= 1;
                }
                else
                {
                    break;
                }
            }
            for (int i = 2; i <= Sqrt(s); i++)
            {
                if (s % i == 0)
                    return false;
            }
            return true;
        }
    }
}
