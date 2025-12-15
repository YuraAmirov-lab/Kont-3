using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);

        int[,] coins = new int[n, m];
        for (int i = 0; i < n; i++)
        {
            string[] row = Console.ReadLine().Split();
            for (int j = 0; j < m; j++)
            {
                coins[i, j] = int.Parse(row[j]);
            }
        }

        long[,] dp = new long[n, m];
        char[,] direction = new char[n, m];

        dp[0, 0] = coins[0, 0];

        for (int j = 1; j < m; j++)
        {
            dp[0, j] = dp[0, j - 1] + coins[0, j];
            direction[0, j] = 'R';
        }

        for (int i = 1; i < n; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + coins[i, 0];
            direction[i, 0] = 'D';
        }

        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < m; j++)
            {
                long fromTop = dp[i - 1, j];
                long fromLeft = dp[i, j - 1];

                if (fromTop > fromLeft)
                {
                    dp[i, j] = fromTop + coins[i, j];
                    direction[i, j] = 'D';
                }
                else
                {
                    dp[i, j] = fromLeft + coins[i, j];
                    direction[i, j] = 'R';
                }
            }
        }

        List<char> path = new List<char>();
        int rowIdx = n - 1;
        int colIdx = m - 1;

        while (rowIdx > 0 || colIdx > 0)
        {
            if (direction[rowIdx, colIdx] == 'D')
            {
                path.Add('D');
                rowIdx--;
            }
            else
            {
                path.Add('R');
                colIdx--;
            }
        }

        path.Reverse();

        Console.WriteLine(dp[n - 1, m - 1]);
        Console.WriteLine(new string(path.ToArray()));
    }
}