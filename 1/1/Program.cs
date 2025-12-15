using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int k = int.Parse(input[1]);

        int[] coins = new int[n + 1];
        string[] coinsInput = Console.ReadLine().Split();
        for (int i = 2; i <= n - 1; i++)
        {
            coins[i] = int.Parse(coinsInput[i - 2]);
        }

        int[] dp = new int[n + 1];
        int[] parent = new int[n + 1];

        for (int i = 2; i <= n; i++)
        {
            dp[i] = int.MinValue;
        }
        dp[1] = 0;

        for (int i = 1; i < n; i++)
        {
            for (int j = i + 1; j <= i + k && j <= n; j++)
            {
                int newValue = dp[i] + coins[j];
                if (newValue > dp[j])
                {
                    dp[j] = newValue;
                    parent[j] = i;
                }
            }
        }

        List<int> path = new List<int>();
        int current = n;
        while (current != 0)
        {
            path.Add(current);
            current = parent[current];
        }
        path.Reverse();
        Console.WriteLine(dp[n]);
        Console.WriteLine(path.Count - 1); 
        Console.WriteLine(string.Join(" ", path));
    }
}