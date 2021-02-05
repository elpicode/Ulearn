using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (phrases.Count == 0 || prefix == "")
                return right;
            while ( right - left > 1)
            {
                var mean = left / 2 + right/ 2;
                if (phrases[mean].StartsWith(prefix, StringComparison.OrdinalIgnoreCase) 
                    || string.Compare(phrases[mean], prefix, StringComparison.OrdinalIgnoreCase) < 0 )
                    left = mean;
                else
                    right = mean;
            }
            return right;
        }
    }
}