using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] cost = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            cost[i] = int.Parse(Console.ReadLine());
        }

        const int INF = int.MaxValue / 2;
        int[,] dp = new int[n + 1, n + 2];
        int[,] prev = new int[n + 1, n + 2];
        int[,] choice = new int[n + 1, n + 2];

        for (int i = 0; i <= n; i++)
            for (int j = 0; j <= n + 1; j++)
                dp[i, j] = INF;
        dp[0, 0] = 0;

        for (int i = 1; i <= n; i++)
        {
            int delta = cost[i] > 100 ? 1 : 0;

            for (int j = 0; j <= n; j++)
            {
                if (j - delta >= 0 && dp[i - 1, j - delta] + cost[i] < dp[i, j])
                {
                    dp[i, j] = dp[i - 1, j - delta] + cost[i];
                    prev[i, j] = j - delta;
                    choice[i, j] = 0;
                }

                if (j + 1 <= n && dp[i - 1, j + 1] < dp[i, j])
                {
                    dp[i, j] = dp[i - 1, j + 1];
                    prev[i, j] = j + 1;
                    choice[i, j] = 1;
                }
            }
        }

        int minCost = INF;
        int bestJ = 0;
        for (int j = 0; j <= n; j++)
        {
            if (dp[n, j] <= minCost)
            {
                minCost = dp[n, j];
                bestJ = j;
            }
        }

        List<int> usedDays = new List<int>();
        int currJ = bestJ;
        for (int i = n; i >= 1; i--)
        {
            if (choice[i, currJ] == 1)
            {
                usedDays.Add(i);
            }
            currJ = prev[i, currJ];
        }

        usedDays.Sort();

        Console.WriteLine(minCost);
        Console.WriteLine($"{bestJ} {usedDays.Count}");
        foreach (int day in usedDays)
        {
            Console.WriteLine(day);
        }
    }
}