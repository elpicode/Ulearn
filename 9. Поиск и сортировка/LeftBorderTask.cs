using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (phrases.Count == 0 || string.Compare(prefix, phrases[0], StringComparison.OrdinalIgnoreCase) >= 0)
                return -1;
            if (right - left == 1)
                return left;
            var mean = (right + left) / 2;
            if (string.Compare(prefix, phrases[mean], StringComparison.OrdinalIgnoreCase) < 0)
                return GetLeftBorderIndex(phrases, prefix, mean, right);
            return GetLeftBorderIndex(phrases, prefix, left, mean);
        }
    }
}
