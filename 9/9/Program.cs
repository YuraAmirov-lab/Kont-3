using System;

class Program
{
    static int n, m;
    static int[] blocked;
    static long[,] dp;
    static int fullMask;

    static void Rec(int row, int col, int prevMask, int curMask, int nextMask, long val)
    {
        if (col == m)
        {
            if (((prevMask | curMask) & (~blocked[row])) == (~blocked[row] & fullMask))
                dp[row + 1, nextMask] += val;
            return;
        }

        int bit = 1 << col;
        if ((prevMask & bit) != 0)
        {
            Rec(row, col + 1, prevMask, curMask, nextMask, val);
            return;
        }

        if ((blocked[row] & bit) != 0)
        {
            Rec(row, col + 1, prevMask, curMask, nextMask, val);
            return;
        }

        if (col + 1 < m && (prevMask & (1 << (col + 1))) == 0 && (blocked[row] & (1 << (col + 1))) == 0)
        {
            int newCur = curMask | bit | (1 << (col + 1));
            Rec(row, col + 2, prevMask, newCur, nextMask, val);
        }

        if (row + 1 < n && (blocked[row + 1] & bit) == 0)
        {
            int newCur = curMask | bit;
            int newNext = nextMask | bit;
            Rec(row, col + 1, prevMask, newCur, newNext, val);
        }
    }

    static void Main()
    {
        string[] nm = Console.ReadLine().Split();
        n = int.Parse(nm[0]);
        m = int.Parse(nm[1]);

        blocked = new int[n];
        for (int i = 0; i < n; i++)
        {
            string s = Console.ReadLine();
            int mask = 0;
            for (int j = 0; j < m; j++)
                if (s[j] == 'X')
                    mask |= 1 << j;
            blocked[i] = mask;
        }

        fullMask = (1 << m) - 1;
        dp = new long[n + 1, 1 << m];
        dp[0, 0] = 1;

        for (int row = 0; row < n; row++)
        {
            for (int mask = 0; mask <= fullMask; mask++)
            {
                if (dp[row, mask] != 0)
                    Rec(row, 0, mask, 0, 0, dp[row, mask]);
            }
        }

        Console.WriteLine(dp[n, 0]);
    }
}