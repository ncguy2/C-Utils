using System;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Utils.String {
    public class Levenshtein {

        public static int Distance(string a, string b) {
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
                    cost = (aChars[i - 1] == bChars[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(d[i, j - 1] + 1,              // Insertion
                        Math.Min(d[i - 1, j] + 1,                    // Deletion
                            d[i - 1, j - 1] + cost));                // Substitution
                }
            }

            return d[m, n];
        }

    }
}

// https://github.com/mailcheck/mailcheck/wiki/String-Distance-Algorithms