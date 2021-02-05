using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        static readonly char[] Separator = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        Dictionary<string, Dictionary<int, List<int>>> words = new Dictionary<string, Dictionary<int, List<int>>>();
        Dictionary<int, List<string>> ids = new Dictionary<int, List<string>>();

        public void Add(int id, string documentText)
        {
            var wordList = documentText.Split(Separator);
            var currInd = 0;
            for (int i = 0; i < wordList.Count(); i++) 
            {
                if (!words.ContainsKey(wordList[i]))
                    words[wordList[i]] = new Dictionary<int, List<int>>();
                if (!words[wordList[i]].ContainsKey(id))
                    words[wordList[i]][id] = new List<int>();
                words[wordList[i]][id].Add(currInd);
                currInd+= wordList[i].Length + 1;
            }
            if (!ids.ContainsKey(id))
                ids[id] = new List<string>();
            ids[id] = wordList.ToList();
        }

        public List<int> GetIds(string word)
        {
            if (words.ContainsKey(word))
                return words[word].Keys.ToList();
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (words.ContainsKey(word))
                if (words[word].ContainsKey(id))
                    return words[word][id];
            return new List<int>();
        }

        public void Remove(int id)
        {
            if (ids.ContainsKey(id))
            {
                var wordList = ids[id];
                foreach (string word in wordList)
                    words[word].Remove(id);
                ids.Remove(id);
            }
        }
    }
}