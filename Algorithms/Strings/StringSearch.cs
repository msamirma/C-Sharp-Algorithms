namespace Algorithms.Strings
{    
    public static class StringExtensions
    {
        public static bool Has(this string source, string substring)
        {
            if (string.IsNullOrEmpty(substring) || string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (substring.Length > source.Length)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (i + substring.Length > source.Length)
                {
                    return false;
                }

                if (source[i] == substring[0])
                {
                    if (substring.Length == 1)
                    {
                        return true;
                    }

                    var isSubString = true;
                    for (var j = 1; j < substring.Length; j++)
                    {
                        if (!isSubString)
                        {
                            break;
                        }

                        isSubString = source[i + j] == substring[j];
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
