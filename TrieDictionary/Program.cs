class Program
{
    static void Main()
    {
        // Array of words to be inserted into the Trie
        string[] words = {
            "as", "astronaut", "asteroid", "are", "around",
            "cat", "cars", "cares", "careful", "carefully",
            "for", "follows", "forgot", "from", "front",
            "mellow", "mean", "money", "monday", "monster",
            "place", "plan", "planet", "planets", "plans",
            "the", "their", "they", "there", "towards"};

        // Initialize the Trie with the given words
        Trie dictionary = InitializeTrie(words);
        
        // Uncomment the following lines to test individual features
        SearchWord(dictionary);
        //PrefixAutocomplete(dictionary);
        //DeleteWord(dictionary); // Note: This method is commented out because it’s incomplete in the provided code.
        //GetSpellingSuggestions(dictionary);
    }

    // Method to initialize a Trie with a list of words
    static Trie InitializeTrie(string[] words)
    {
        Trie trie = new Trie();
        foreach (string word in words)
        {
            trie.Insert(word); // Insert each word into the Trie
        }
        return trie; // Return the initialized Trie
    }

    // Method to search for a word in the Trie
    static void SearchWord(Trie dictionary)
    {
        while (true)
        {
            Console.WriteLine("Enter a word to search for, or press Enter to exit.");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                break; // Exit loop if input is empty
            }
            if (dictionary.Search(input))
            {
                Console.WriteLine($"Found \"{input}\" in dictionary");
            }
            else
            {
                Console.WriteLine($"Did not find \"{input}\" in dictionary");
            }
        }
    }

    // Method to autocomplete words based on a prefix
    static void PrefixAutocomplete(Trie dictionary)
    {
        PrintTrie(dictionary); // Print all words in the Trie
        GetPrefixInput(dictionary); // Get user input and provide autocomplete suggestions
    }

    // Method to delete a word from the Trie (Assuming it exists, but it was commented out)
    static void DeleteWord(Trie dictionary)
    {
        PrintTrie(dictionary);
        while (true)
        {
            Console.WriteLine("\nEnter a word to delete, or press Enter to exit.");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                break;
            }
            // The delete functionality was commented out; Implement delete logic here if needed
            Console.WriteLine($"Did not find \"{input}\" in dictionary");
        }
    }

    // Method to get spelling suggestions for a word
    static void GetSpellingSuggestions(Trie dictionary)
    {
        PrintTrie(dictionary);
        Console.WriteLine("\nEnter a word to get spelling suggestions for, or press Enter to exit.");
        string? input = Console.ReadLine();
        if (input != null)
        {
            var similarWords = dictionary.GetSpellingSuggestions(input);
            Console.WriteLine($"Spelling suggestions for \"{input}\":");
            if (similarWords.Count == 0)
            {
                Console.WriteLine("No suggestions found.");
            }
            else
            {
                foreach (var word in similarWords)
                {
                    Console.WriteLine(word);
                }
            }
        }
    }

    // Method to get prefix input from the user and provide autocomplete suggestions
    static void GetPrefixInput(Trie dictionary)
    {
        Console.WriteLine("\nEnter a prefix to search for, then press Tab to " +
                          "cycle through search results. Press Enter to exit.");

        bool running = true;
        string prefix = "";
        StringBuilder sb = new StringBuilder();
        List<string>? words = null;
        int wordsIndex = 0;

        while (running)
        {
            var input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Spacebar)
            {
                Console.Write(' ');
                prefix = "";
                sb.Append(' ');
                continue;
            }
            else if (input.Key == ConsoleKey.Backspace && Console.CursorLeft > 0)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                sb.Remove(sb.Length - 1, 1);
                prefix = sb.ToString().Split(' ').Last();
            }
            else if (input.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                running = false;
                continue;
            }
            else if (input.Key == ConsoleKey.Tab && prefix.Length > 1)
            {
                string previousWord = sb.ToString().Split(' ').Last();

                if (words != null)
                {
                    if (!previousWord.Equals(words[wordsIndex - 1]))
                    {
                        words = dictionary.AutoSuggest(prefix);
                        wordsIndex = 0;
                    }
                }
                else
                {
                    words = dictionary.AutoSuggest(prefix);
                    wordsIndex = 0;
                }

                for (int i = prefix.Length; i < previousWord.Length; i++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    sb.Remove(sb.Length - 1, 1);
                }

                if (words.Count > 0 && wordsIndex < words.Count)
                {
                    string output = words[wordsIndex++];
                    Console.Write(output.Substring(prefix.Length));
                    sb.Append(output.Substring(prefix.Length));
                }
                continue;
            }
            else if (input.Key != ConsoleKey.Tab)
            {
                Console.Write(input.KeyChar);
                prefix += input.KeyChar;
                sb.Append(input.KeyChar);
                words = null;
                wordsIndex = 0;
            }
        }
    }

    // Method to print all words in the Trie
    static void PrintTrie(Trie trie)
    {
        Console.WriteLine("The dictionary contains the following words:");
        List<string> words = trie.GetAllWords(); // Get all words from the Trie
        foreach (string word in words)
        {
            Console.Write($"{word}, ");
        }
        Console.WriteLine();
    }
}
