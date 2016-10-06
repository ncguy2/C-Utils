using System;
using System.Collections.Generic;
using System.Management.Instrumentation;

namespace Utils.String {
    public class StringDifferences {

        public static int Levenshtein(string a, string b) {
            if (a == null || b == null) return Int32.MaxValue;
            int m = a.Length;
            int n = b.Length;

            int[,] d = new int[m,n];
            for (int i = 0; i <= m; i++)
                d[i,0] = i;
            for (int j = 0; j <= n; j++)
                d[0, j] = j;

            int cost = 0;

            char[] aChars = a.ToCharArray();
            char[] bChars = b.ToCharArray();

            for (int j = 1; j <= n; j++) {
                for (int i = 1; i <= m; i++) {
                    cost = aChars[i - 1] == bChars[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(d[i, j - 1] + 1,              // Insertion
                        Math.Min(d[i - 1, j] + 1,                    // Deletion
                            d[i - 1, j - 1] + cost));                // Substitution
                }
            }

            return d[m, n];
        }

        public static int OptimalStringAlignment(string a, string b) {
            if (a == null || b == null) return Int32.MaxValue;
            var m = a.Length;
            var n = b.Length;

            int[,] d = new int[m, n];
            for (int i = 0; i <= m; i++)
                d[i,0] = i;
            for (int j = 0; j <= n; j++)
                d[0, j] = j;
            int cost = 0;

            char[] aChars = a.ToCharArray();
            char[] bChars = b.ToCharArray();

            for (int j = 1; j <= n; j++) {
                for(int i = 1; i <= m; i++) {
                    cost = aChars[i-1] == bChars[j-1] ? 0 : 1;
                    d[i, j] = Math.Min(d[i, j - 1] + 1,   // Insertion
                        Math.Min(d[i - 1, j] + 1,         // Deletion
                            d[i - 1, j - 1] + cost));     // Substitution

                    if (i > 1 && j > 1 && aChars[i - 1] == bChars[j - 2] && aChars[i - 2] == bChars[j - 1])
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost); // Transposition
                }
            }
            return d[m, n];
        }

        public static int DamerauLevenshtein(string a, string b) {
            if (a == null || b == null) return Int32.MaxValue;
            int m = a.Length;
            int n = b.Length;
            Dictionary<char, int> charDictionary = new Dictionary<char, int>();

            int[,] d = new int[m, n];
            for (int i = 0; i <= m; i++)
                d[i,0] = i;
            for (int j = 0; j <= n; j++)
                d[0, j] = j;

            char[] aChars = a.ToCharArray();
            char[] bChars = b.ToCharArray();

            foreach (char c in aChars)
                charDictionary[c] = 0;
            foreach (char c in bChars)
                charDictionary[c] = 0;

            for (int i = 1; i <= m; i++) {
                var db = 0;
                for (int j = 1; j <= n; j++) {
                    int i1 = charDictionary[bChars[j - 1]];
                    int j1 = db;
                    int cost = 0;

                    if (aChars[i - 1] == bChars[j - 1])
                        db = j;
                    else cost = 1;
                    d[i,j] = Math.Min(d[i, j-1] + 1,    // Insertion
                        Math.Min(d[i-1, j] + 1,         // Deletion
                            d[i-1, j-1] + cost));       // Substitution

                    if (i1 > 0 && j1 > 0)
                        d[i, j] = Math.Min(d[i, j], d[i1 - 1, j1 - 1] + (i - i1 - 1) + (j - j1 - 1) + 1); // Transposition

                }
                charDictionary[aChars[i - 1]] = i;
            }
            return d[m, n];
        }

    }
}

// https://github.com/mailcheck/mailcheck/wiki/String-Distance-Algorithms