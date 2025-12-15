using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] input = Console.ReadLine().Split();
        long[] a = new long[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = long.Parse(input[i]);
        }

        int[] dp = new int[n];
        int[] prev = new int[n];

        for (int i = 0; i < n; i++)
        {
            dp[i] = 1;
            prev[i] = -1;
            for (int j = 0; j < i; j++)
            {
                if (a[j] < a[i] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    prev[i] = j;
                }
            }
        }

        int maxLen = 0;
        int lastIndex = 0;
        for (int i = 0; i < n; i++)
        {
            if (dp[i] > maxLen)
            {
                maxLen = dp[i];
                lastIndex = i;
            }
        }

        List<long> result = new List<long>();
        int current = lastIndex;
        while (current != -1)
        {
            result.Add(a[current]);
            current = prev[current];
        }

        result.Reverse();

        Console.WriteLine(maxLen);
        if (maxLen > 0)
        {
            Console.WriteLine(string.Join(" ", result));
        }
    }
}