using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        long[,] dist = new long[n, n];
        for (int i = 0; i < n; i++)
        {
            string[] line = Console.ReadLine().Split();
            for (int j = 0; j < n; j++)
            {
                dist[i, j] = long.Parse(line[j]);
            }
        }

        int totalMasks = 1 << n;
        long[,] dp = new long[totalMasks, n];
        int[,] prev = new int[totalMasks, n];

        const long INF = long.MaxValue / 2;
        for (int mask = 0; mask < totalMasks; mask++)
            for (int i = 0; i < n; i++)
                dp[mask, i] = INF;

        for (int i = 0; i < n; i++)
        {
            dp[1 << i, i] = 0;
            prev[1 << i, i] = -1;
        }

        for (int mask = 0; mask < totalMasks; mask++)
        {
            for (int last = 0; last < n; last++)
            {
                if ((mask & (1 << last)) == 0) continue;
                if (dp[mask, last] == INF) continue;

                for (int next = 0; next < n; next++)
                {
                    if ((mask & (1 << next)) != 0) continue;
                    int newMask = mask | (1 << next);
                    long newCost = dp[mask, last] + dist[last, next];
                    if (newCost < dp[newMask, next])
                    {
                        dp[newMask, next] = newCost;
                        prev[newMask, next] = last;
                    }
                }
            }
        }

        long minCost = INF;
        int bestLast = -1;
        int fullMask = totalMasks - 1;
        for (int last = 0; last < n; last++)
        {
            if (dp[fullMask, last] < minCost)
            {
                minCost = dp[fullMask, last];
                bestLast = last;
            }
        }

        List<int> path = new List<int>();
        int curMask = fullMask;
        int curLast = bestLast;
        while (curMask != 0)
        {
            path.Add(curLast + 1); 
            int prevLast = prev[curMask, curLast];
            if (prevLast == -1) break;
            curMask ^= (1 << curLast);
            curLast = prevLast;
        }
        path.Reverse();

        Console.WriteLine(minCost);
        Console.WriteLine(string.Join(" ", path));
    }
}