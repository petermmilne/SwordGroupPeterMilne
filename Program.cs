if (args == null || args.Length == 0)
{
    Console.WriteLine("Please provide an input file");
}
else
{
    try
    {
        string caseSensitive = args[1];
        StreamReader sr = new(args[0]);
        Dictionary<string, int> map = [];
        string input  = sr.ReadToEnd().Replace(" ", string.Empty).Replace("\t", string.Empty).ReplaceLineEndings("");
        int totalCharsExSpecial = input.Length;

        if (caseSensitive == null || caseSensitive == "0")
        {
            //  Group by character, order the result descending and case character and occurance to dictionary
            map = input.GroupBy(x => x.ToString()).
                     OrderByDescending(g => g.Count()).
                        ToDictionary(g => g.Key, g => g.Count());
        }
        else if (caseSensitive == "1") // Ignore Case
        {
            //  Group by character, order the result descending and case character and occurance to dictionary
            map = input.GroupBy(x => x.ToString(), StringComparer.InvariantCultureIgnoreCase).
                    OrderByDescending(g => g.Count()).
                        ToDictionary(g => g.Key, g => g.Count());
        }

        Console.WriteLine("Total Characters: " + totalCharsExSpecial);

        //  Make sure we have 10 top results to show, if not then use the count
        int topX = map.Count < 10 ? map.Count : 10;
        var output = map.Take(topX).ToList();

        if (topX == 0)
        {
            Console.WriteLine("No Results");
            return;
        }

        for (int x=0; x < output.Count; x++)
        {
            //  If we are case insensitive we will use all lower case characters
            var key = (caseSensitive != null && caseSensitive == "1") ? 
                    output[x].Key.ToLowerInvariant() : output[x].Key.ToString();

            Console.WriteLine(key + " (" + output[x].Value + ")");
        }
    }
    catch (Exception e) { Console.WriteLine(e.ToString()); }
}
