using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var countByPrefix = Math.Min(count, GetCountByPrefix(phrases, prefix));
            var result = new string[countByPrefix];
            for (int i = 0; i < countByPrefix; i++)
                result[i] = phrases[i + left];
            return result;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return right - left;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var list = new List<string>() { };
            IReadOnlyList<string> phrases = list;
            var prefix = "cdf";
            var count = 1;
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var list = new  List<string>() { "a" , "bb" , "c" };
            IReadOnlyList <string> phrases = list;
            var prefix = "";
            var expectedCount = 3;
            var actualCount = AutocompleteTask.GetCountByPrefix( phrases, prefix );
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
