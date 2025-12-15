using System;

class Program
{
    static bool Good(int prev, int curr, int m)
    {
        for (int j = 0; j < m - 1; j++)
        {
            int a = (prev >> j) & 1;
            int b = (prev >> (j + 1)) & 1;
            int c = (curr >> j) & 1;
            int d = (curr >> (j + 1)) & 1;
            if (a == b && b == c && c == d)
                return false;
        }
        return true;
    }

    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);

        if (n < m)
        {
            int temp = n;
            n = m;
            m = temp;
        }

        if (n == 1)
        {
            Console.WriteLine(1L << m);
            return;
        }

        int masks = 1 << m;
        long[,] dp = new long[masks, masks];

        for (int prev = 0; prev < masks; prev++)
        {
            for (int curr = 0; curr < masks; curr++)
            {
                if (Good(prev, curr, m))
                    dp[prev, curr] = 1;
            }
        }

        for (int i = 3; i <= n; i++)
        {
            long[,] next = new long[masks, masks];
            for (int prev = 0; prev < masks; prev++)
            {
                for (int curr = 0; curr < masks; curr++)
                {
                    if (dp[prev, curr] == 0) continue;
                    for (int nxt = 0; nxt < masks; nxt++)
                    {
                        if (Good(curr, nxt, m))
                            next[curr, nxt] += dp[prev, curr];
                    }
                }
            }
            dp = next;
        }

        long answer = 0;
        for (int prev = 0; prev < masks; prev++)
            for (int curr = 0; curr < masks; curr++)
                answer += dp[prev, curr];

        Console.WriteLine(answer);
    }
}