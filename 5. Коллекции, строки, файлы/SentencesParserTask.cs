using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentList = new List<List<string>>();
            text = text.ToLower();
            var sents = text.Split(".:;?!()".ToCharArray());
            foreach (var sent in sents)
                AddSentences(sentList, sent);
            return sentList;
        }

        public static void AddCorrectWord(List<string> wordList, StringBuilder build)
        {
            if (build.Length > 0)
            {
                wordList.Add(build.ToString());
                build.Clear();
            }
        }
		
        public static void AddSentences (  List<List<string>> sentList , string sent )
        {
            var wordList = new List<string>();
            var build = new StringBuilder();
            foreach (var ch in sent)
            {
                if (char.IsLetter(ch) || ch == '\'')
                    build.Append(ch);
                else
                    AddCorrectWord(wordList, build);
            }

            AddCorrectWord(wordList, build);
            if (wordList.Count > 0)
                sentList.Add(wordList);
        }
    }
}