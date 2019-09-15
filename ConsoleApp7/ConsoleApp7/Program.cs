using System;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            int teams;

            while(true)
            {
                try
                {
                    Console.Write("Input the number of seeds: ");
                    teams = int.Parse(Console.ReadLine());
                    if (teams <= 0)
                        Console.WriteLine("Must be a positive number!");
                    else
                    {
                        Console.WriteLine();
                        break;
                    }
                }

                catch
                {
                    Console.WriteLine("That's not a valid input!");
                }
            }

            int[] seeds = new int[teams];

            for (int i = 0; i < teams; i++)
                seeds[i] = i + 1; //This just generates seed numbers

            /*for(int i = 0; i < teams; i++)
            {
                Console.Write($"What is the seed/rank of team {i + 1}? ");
                seeds[i] = int.Parse(Console.ReadLine());
            }*/

            Array.Sort(seeds);

            string[] pairs;

            if (seeds.Length == 1)
            {
                pairs = new string[1];
                pairs[0] = $"There is only one seed, and so {seeds[0]} is the winner!";
            }

            else
            {
                int buyCount = 0;
                for (int i = 0; Math.Pow(2, i) < seeds.Length; i++)
                {
                    buyCount = int.Parse(Math.Pow(2, i).ToString());
                }

                int count;
                int buyMax = buyCount;
                int pairLength;
                buyCount = seeds.Length - buyCount;

                if (buyCount <= buyMax / 2)
                {
                    count = buyCount;
                    pairLength = (buyMax / 2) + buyCount;
                }

                else
                {
                    count = buyMax - buyCount;
                    pairLength = buyMax;
                }

                pairs = new string[pairLength];
                for (int i = 0; i < count; i++)
                    pairs[i] = $"{seeds[i]} - BUY";
                for (int i = count; i + ((seeds.Length - count) / 2) < seeds.Length; i++)
                    pairs[i] = $"{seeds[i]},{seeds[i + ((seeds.Length - count) / 2)]}";

                pairs = plant(pairs);

                if (pairLength == buyMax)
                {
                    for (int i = 0; i < pairs.Length; i++)
                    {
                        if (pairs[i].Contains(","))
                            pairs[i] = $"{pairs[i]} - Round 1";
                    }
                }

                else
                {
                    for (int i = 0; i < pairs.Length; i++)
                    {
                        if (pairs[i].Contains("BUY"))
                        {
                            pairs[i + 1] = $"{pairs[i + 1]} - Round 1";
                            i++;
                        }

                        else
                            pairs[i] = $"{pairs[i]} - Round 2";
                    }
                }
            }

            //The code below to the '&' is used to verify that the original algorithim is working properly by assuming the higher seed always moves forward, producing the next round results at a key press. It continues to do this until only two competitors are left.

            bool check = false;
            int round = 1;
            while(pairs.Length > 0)
            {
                Console.WriteLine($"Round {round}:");
                foreach (string j in pairs)
                    Console.WriteLine(j);

                if (!check)
                {
                    for (int j = 0; j < pairs.Length; j++)
                    {
                        if (pairs[j].Contains(" - BUY"))
                        {
                            pairs[j] = pairs[j].Remove(pairs[j].IndexOf(" -"));
                            pairs[j + 1] = pairs[j + 1].Remove(pairs[j + 1].IndexOf(","));
                        }

                        if (pairs[j].Contains(" - Round 1"))                        
                            pairs[j] = pairs[j].Remove(pairs[j].IndexOf(","));
                    }

                    check = true;
                }

                else
                {
                    for (int j = 0; j < pairs.Length; j++)
                    {
                        if (pairs[j].Contains(","))
                            pairs[j] = pairs[j].Remove(pairs[j].IndexOf(","));
                    }
                }

                int intemp = 1;
                for (int j = 0; intemp < pairs.Length; j++)
                    intemp = intemp * 2;

                intemp = intemp / 2;

                string[] temp = new string[intemp];

                int k = 0;

                for(int j = 0; j < temp.Length && k < pairs.Length; j++)
                {
                    if (pairs[k].Contains(","))
                    {
                        temp[j] = pairs[k];
                        k++;
                    }

                    else
                    {
                        temp[j] = $"{pairs[k]},{pairs[k + 1]}";
                        k = k + 2;
                    }
                }

                pairs = temp;

                if (pairs.Length == 0)
                    Console.WriteLine("--FINAL ROUND--");

                else
                    Console.WriteLine($"--END OF ROUND {round}--");

                round++;

                Console.ReadKey();
                Console.WriteLine();
            }
        }

        //&

        public static string[] plant(string[] start)
        {
            string[] temp;
            
            if (start.Length % 2 == 0)
            {
                int halfInt = start.Length / 2;
                string[] a = new string[halfInt];
                string[] b = new string[halfInt];
                for (int i = 0; i < halfInt; i++)
                {
                    a[i] = start[i * 2];
                    b[i] = start[(i * 2) + 1];
                }

                temp = plant(a, b);
            }

            else
            {
                int halfInt = start.Length / 2;
                string[] a = new string[halfInt + 1];
                string[] b = new string[halfInt];
                for (int i = 0; i < halfInt + 1; i++)
                    a[i] = start[i * 2];

                for (int i = 0; i < halfInt; i++)
                    b[i] = start[(i * 2) + 1];

                temp = plant(a, b);
            }

            return temp;
        }

        private static string[] plant(string[] x, string[] y)
        {
            int halfInt1 = x.Length / 2;
            int halfInt2 = y.Length / 2;
            string[] a = x;
            string[] b = y;

            if (x.Length > 1)
            {
                if (x.Length % 2 == 0)
                {
                    string[] a1 = new string[halfInt1];
                    string[] b1 = new string[halfInt1];

                    for (int i = 0; i < halfInt1; i++)
                    {
                        a1[i] = x[i * 2];
                        b1[i] = x[(i * 2) + 1];
                    }

                    a = plant(a1, b1);
                }

                else
                {
                    string[] a1 = new string[halfInt1 + 1];
                    string[] b1 = new string[halfInt1];

                    for (int i = 0; i < halfInt1 + 1; i++)
                        a1[i] = x[i * 2];

                    for (int i = 0; i < halfInt1; i++)
                        b1[i] = x[(i * 2) + 1];

                    a = plant(a1, b1);
                }
            }

            if (y.Length > 1)
            {
                if (y.Length % 2 == 0)
                {
                    string[] a2 = new string[halfInt2];
                    string[] b2 = new string[halfInt2];

                    for (int i = 0; i < halfInt2; i++)
                    {
                        a2[i] = y[i * 2];
                        b2[i] = y[(i * 2) + 1];
                    }

                    b = plant(a2, b2);
                }

                else
                {
                    string[] a2 = new string[halfInt2 + 1];
                    string[] b2 = new string[halfInt2];

                    for (int i = 0; i < halfInt2 + 1; i++)
                        a2[i] = y[i * 2];

                    for (int i = 0; i < halfInt2; i++)
                        b2[i] = y[(i * 2) + 1];

                    b = plant(a2, b2);
                }
            }

            string[] temp = new string[a.Length + b.Length];
            a.CopyTo(temp, 0);
            b.CopyTo(temp, a.Length);

            return temp;
        }
    }
}