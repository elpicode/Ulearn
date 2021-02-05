using System;
namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var heat = new double[30, 12];
            var day = GetLabels(2, 30);
            var mounth = GetLabels(1, 12);
            foreach (var name in names)
            {
                if (name.BirthDate.Day != 1)
                    heat[name.BirthDate.Day - 2, name.BirthDate.Month - 1] ++;
            }
            return new HeatmapData("Карта интенсивностей", heat, day, mounth);
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