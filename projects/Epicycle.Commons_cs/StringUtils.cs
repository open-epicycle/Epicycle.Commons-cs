using System.Collections.Generic;

namespace Epicycle.Commons
{
    public static class StringUtils
    {
        public static string EnsureMaxLength(this string s, int maxLength)
        {
            if(s.Length < maxLength)
            {
                return s;
            }

            return s.Substring(maxLength - 3) + "...";
        }

        public static IEnumerable<string> SimpleWordWrap(this string s, int maxCharsPerLine)
        {
            var normalizedString = s.Replace('\t', ' ').Replace("\r", "").Replace("\n", " ");

            if (normalizedString.Length < maxCharsPerLine)
            {
                yield return normalizedString;
                yield break;
            }
            
            var pos = 0;
            var lastWordPos = -1;
            var lastWordEnd = -1;
            var curLineStart = 0;
            var shouldWrap = false;

            while (pos < normalizedString.Length)
            {
                var curLineLength = pos - curLineStart;

                if (shouldWrap || (curLineLength >= maxCharsPerLine))
                {
                    var isNextDelimiter = (pos == (normalizedString.Length - 1)) || (normalizedString[pos] == ' ');
                    var lineLength = ((lastWordEnd >= 0) && !isNextDelimiter) ? (lastWordEnd - curLineStart + 1) : maxCharsPerLine;

                    yield return normalizedString.Substring(curLineStart, lineLength);
                    curLineStart += lineLength;
                    shouldWrap = false;
                    lastWordEnd = -1;
                    lastWordPos = -1;
                    pos = curLineStart;
                    curLineLength = 0;
                }

                char curChar = normalizedString[pos];

                if(curChar == '\n')
                {
                    shouldWrap = true;
                }
                else if(curChar == ' ')
                {
                    lastWordEnd = lastWordPos;
                    if(lastWordPos == -1)
                    {
                        curLineStart++;
                    }
                }
                else
                {
                    lastWordPos = pos;
                }

                pos++;
            }

            yield return normalizedString.Substring(curLineStart, normalizedString.Length - curLineStart);
        }
    }
}
