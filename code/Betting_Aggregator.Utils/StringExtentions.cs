using System.Text.RegularExpressions;

namespace Betting_Aggregator.Utils
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public static string ToCamelCase(this string pascalCase)
        {
            if (string.IsNullOrWhiteSpace(pascalCase))
                return pascalCase;

            var nestedModel = pascalCase.Split('.');
            int i = 0;
            foreach (var item in nestedModel)
            {
                nestedModel[i++] = GetCamelCase(item);
            }
            return string.Join(".", nestedModel);
        }

        private static string GetCamelCase(string word)
        {
            if (string.IsNullOrEmpty(word)) return string.Empty;
            return char.ToLowerInvariant(word[0]) + word.Substring(1);
        }

        public static string ConvertEmptyStringToNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str.Trim();
        }
    }
}
