using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var day = GetLabels(1, 31);
            var birthDaysCounts = new double[31];
            foreach (var person in names)
            {
                if (person.BirthDate.Day != 1 && person.Name == name)
                    birthDaysCounts [person.BirthDate.Day - 1] ++;
            }
            return new HistogramData(string.Format("Рождаемость с именем '{0}'", name), day, birthDaysCounts);
        }
		
        public static string[] GetLabels(int up, int count)
        {
            var array = new string [count];
            for (int i = 0; i < count; i++)
                array[i] = (i + up).ToString();
            return array;
        }
    }
}