using System;
using System.Text;

class Program
{
    static bool IsPair(char a, char b)
    {
        return (a == '(' && b == ')') || (a == '[' && b == ']') || (a == '{' && b == '}');
    }

    static string Build(int l, int r, int[,] way, string s, string[,] memo)
    {
        if (l > r) return "";
        if (l == r) return "";
        if (memo[l, r] != null)
            return memo[l, r];
        string result;
        if (way[l, r] == -1)
        {
            result = s[l] + Build(l + 1, r - 1, way, s, memo) + s[r];
        }
        else if (way[l, r] == -2)
        {
            result = "";
        }
        else
        {
            int k = way[l, r];
            result = Build(l, k, way, s, memo) + Build(k + 1, r, way, s, memo);
        }
        memo[l, r] = result;
        return result;
    }

    static void Main()
    {
        string s = Console.ReadLine();
        int n = s.Length;
        int[,] dp = new int[n, n];
        int[,] way = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                way[i, j] = -2;

        for (int len = 2; len <= n; len++)
        {
            for (int i = 0; i + len - 1 < n; i++)
            {
                int j = i + len - 1;

                if (IsPair(s[i], s[j]))
                {
                    int candidate = (len == 2 ? 2 : dp[i + 1, j - 1] + 2);
                    if (candidate > dp[i, j])
                    {
                        dp[i, j] = candidate;
                        way[i, j] = -1;
                    }
                }

                for (int k = i; k < j; k++)
                {
                    int left = dp[i, k];
                    int right = dp[k + 1, j];
                    if (left + right > dp[i, j])
                    {
                        dp[i, j] = left + right;
                        way[i, j] = k;
                    }
                }
            }
        }

        string[,] memo = new string[n, n];
        string result = Build(0, n - 1, way, s, memo);
        Console.WriteLine(result);
    }
}