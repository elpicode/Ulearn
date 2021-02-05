using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        const int ZeroValue = 0;
        const int NextShift = 1;
        const int NextTwoShift = 2;
        public static Dictionary<string, Dictionary<string, int>> Sort(List<List<string>> text, int n)
        {
			var ngramFreq = new Dictionary<string, Dictionary<string, int>>();
			foreach (var sent in text)
			{
				for (int i = 0; i < sent.Count - (n-1); ++i)
                {
                    var firstPart = n == 2 ? sent[i] : sent[i] + " " + sent[i + NextShift];
					var secondPart = sent[i + (n - NextShift)];
					if (ngramFreq.ContainsKey(firstPart))
					{
						if (ngramFreq[firstPart].ContainsKey(secondPart))
							ngramFreq[firstPart][secondPart] += 1;
						else
							ngramFreq[firstPart][secondPart] = 1;
					}
					else
						ngramFreq[firstPart] = new Dictionary<string, int> { { secondPart, 1 } };
				}
			}
			return ngramFreq;
        }
        
        public static Dictionary<string, string> DictionariesSort(
			Dictionary<string, Dictionary<string, int>> ngramsFrequencies)
        {
            var result = new Dictionary<string, string>();
            foreach (var firstPart in ngramsFrequencies)
            {
                var maxValue = ZeroValue;
                string secondEl = null;
                foreach (var secondPart in firstPart.Value)
                    if (secondPart.Value > maxValue ||
                       (string.CompareOrdinal(secondPart.Key, secondEl) < 0) && secondPart.Value == maxValue )
                    {
                        maxValue = secondPart.Value;
                        secondEl = secondPart.Key;
                    }
                result[firstPart.Key] = secondEl;
            }
            return result;
        }

        public static Dictionary<string, string> BigramsSortByFrequency(List<List<string>> text)
        {
            var frequency = Sort(text, 2);
            var final = DictionariesSort(frequency);
            return final;
        }
		
        public static Dictionary<string, string> TrigramsSortByFrequency(List<List<string>> text)
        {
            var frequency = Sort(text, 3);
            var final = DictionariesSort(frequency);
            return final;
        }

        public static Dictionary<string, string> BindDictionaries(
			Dictionary<string, string> firstDictionary, Dictionary<string, string> secondDictionary)
        {
            foreach (var element in firstDictionary)
                secondDictionary[element.Key] = element.Value;
            return secondDictionary;
        }
        
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
			var resBigrams = BigramsSortByFrequency(text);
			var resTrigrams = TrigramsSortByFrequency(text);
			var result = BindDictionaries(resTrigrams, resBigrams);
			return result;
        }
    }
}