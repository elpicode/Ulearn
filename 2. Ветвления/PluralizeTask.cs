namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            if ((count % 10 == 1) && (count % 100 != 11)) return "рубль";
            if ((count % 10 > 1) && (count % 10 < 5) && (( count % 100 < 12) || (count % 100 > 15))) return "рубля";
            return "рублей";
        }
    }
}