namespace Algorithms.Strings
{    
    public static class StringExtensions
    {
        public static bool Has(this string source, string pattern)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (pattern.Length > source.Length)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (i + pattern.Length > source.Length)
                {
                    return false;
                }

                if (source[i] == pattern[0])
                {
                    if (pattern.Length == 1)
                    {
                        return true;
                    }

                    var isSubString = true;
                    for (var j = 1; j < pattern.Length; j++)
                    {
                        if (!isSubString)
                        {
                            break;
                        }

                        isSubString = source[i + j] == pattern[j];
                    }

                    if (isSubString)
                    {
                        return true;
                    }
                }
            }

            return false;  
        }    
    }  
}
