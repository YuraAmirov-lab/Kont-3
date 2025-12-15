using System;

class Program
{
    static void Main()
    {
        int mod = 1000000000;
        int n = int.Parse(Console.ReadLine());

        long[,] dp = new long[n + 1, 10];

        for (int d = 0; d < 10; d++)
        {
            if (d != 0 && d != 8)
                dp[1, d] = 1;
        }

        for (int len = 2; len <= n; len++)
        {
            for (int d = 0; d < 10; d++)
            {
                long sum = 0;
                switch (d)
                {
                    case 0: sum = dp[len - 1, 4] + dp[len - 1, 6]; break;
                    case 1: sum = dp[len - 1, 6] + dp[len - 1, 8]; break;
                    case 2: sum = dp[len - 1, 7] + dp[len - 1, 9]; break;
                    case 3: sum = dp[len - 1, 4] + dp[len - 1, 8]; break;
                    case 4: sum = dp[len - 1, 3] + dp[len - 1, 9] + dp[len - 1, 0]; break;
                    case 5: sum = 0; break;
                    case 6: sum = dp[len - 1, 1] + dp[len - 1, 7] + dp[len - 1, 0]; break;
                    case 7: sum = dp[len - 1, 2] + dp[len - 1, 6]; break;
                    case 8: sum = dp[len - 1, 1] + dp[len - 1, 3]; break;
                    case 9: sum = dp[len - 1, 2] + dp[len - 1, 4]; break;
                }
                dp[len, d] = sum % mod;
            }
        }

        long answer = 0;
        for (int d = 0; d < 10; d++)
        {
            answer += dp[n, d];
            answer %= mod;
        }

        Console.WriteLine(answer);
    }
}