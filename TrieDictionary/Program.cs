using System.Text;

string[] words = {
        "as", "astronaut", "asteroid", "are", "around",
        "cat", "cars", "cares", "careful", "carefully",
        "for", "follows", "forgot", "from", "front",
        "mellow", "mean", "money", "monday", "monster",
        "place", "plan", "planet", "planets", "plans",
        "the", "their", "they", "there", "towards"};

Trie dictionary = InitializeTrie(words);
SearchWord();
// PrefixAutocomplete();
// DeleteWord();
// GetSpellingSuggestions();

// Method to initialize a Trie with a list of words
Trie InitializeTrie(string[] words)
{
    // Create a new Trie instance
    Trie trie = new Trie();

    // Iterate over each word in the input array
    foreach (string word in words)
    {
        // Insert the word into the Trie
        trie.Insert(word);
    }


    // Return the initialized Trie
    return trie;
}

// Method to search for a word in the Trie
void SearchWord()
{
    // Infinite loop to continuously prompt the user for input
    while (true)
    {
        // Prompt the user to enter a word to search for
        Console.WriteLine("Enter a word to search for, or press Enter to exit.");
        // Read the user input
        string? input = Console.ReadLine();
        // If the input is an empty string, break the loop
        if (input == "")
        {
            break;
        }
        
        // Check if the input is not null and the word exists in the Trie
        if (input != null && dictionary.Search(input))
        {
            // Inform the user that the word was found
            Console.WriteLine($"Found \"{input}\" in dictionary");
        }
        
        else
        {
            // Inform the user that the word was not found
            Console.WriteLine($"Did not find \"{input}\" in dictionary");
        }
    }
}

// Method to autocomplete words based on a prefix
void PrefixAutocomplete()
{
    // Print all words in the Trie
    PrintTrie(dictionary);
    // Get the prefix input from the user and provide autocomplete suggestions
    GetPrefixInput();
}

// Method to delete a word from the Trie
void DeleteWord() 
{
    // Print all words in the Trie
    PrintTrie(dictionary);
    // Infinite loop to continuously prompt the user for input
    while(true)
    {
        // Prompt the user to enter a word to delete
        Console.WriteLine("\nEnter a word to delete, or press Enter to exit.");
        // Read the user input
        string? input = Console.ReadLine();
        // If the input is an empty string, break the loop
        if (input == "")
        {
            break;
        }
        
        // Check if the input is not null and the word exists in the Trie
        if (input != null && dictionary.Search(input))
        {
            // Delete the word from the Trie
            dictionary.Delete(input);
            // Inform the user that the word was deleted
            Console.WriteLine($"Deleted \"{input}\" from dictionary\n");
            // Print the updated Trie
            PrintTrie(dictionary);
        }
        
        else
        {
            // Inform the user that the word was not found
            Console.WriteLine($"Did not find \"{input}\" in dictionary");
        }
    }
}

// Method to get spelling suggestions for a word
void GetSpellingSuggestions() 
{
    // Print all words in the Trie
    PrintTrie(dictionary);
    // Prompt the user to enter a word to get spelling suggestions for
    Console.WriteLine("\nEnter a word to get spelling suggestions for, or press Enter to exit.");
    // Read the user input
    string? input = Console.ReadLine();
    // If the input is not null
    if (input != null)
    {
        // Get spelling suggestions from the Trie
        //var similarWords = dictionary.GetSpellingSuggestions(input);
        // Print the spelling suggestions
        Console.WriteLine($"Spelling suggestions for \"{input}\":");
        // If no suggestions were found
       /* if (similarWords.Count == 0)
        {
            Console.WriteLine("No suggestions found.");
        }
        else 
        {
            // Print each suggestion
            foreach (var word in similarWords)
            {
                Console.WriteLine(word);
            }
        }*/
    }
}

#pragma warning disable CS8321
// Method to run all exercises
void RunAllExercises()
{
    // Run the SearchWord exercise
    SearchWord();
    // Run the PrefixAutocomplete exercise
    PrefixAutocomplete();
    // Run the DeleteWord exercise
    DeleteWord();
    // Run the GetSpellingSuggestions exercise
    GetSpellingSuggestions();
}

// Method to get prefix input from the user and provide autocomplete suggestions
void GetPrefixInput()
{
    // Prompt the user to enter a prefix
    Console.WriteLine("\nEnter a prefix to search for, then press Tab to " + 
                      "cycle through search results. Press Enter to exit.");

    // Initialize variables
    bool running = true;
    string prefix = "";
    StringBuilder sb = new StringBuilder();
    List<string>? words = null;
    int wordsIndex = 0;

    // Loop to handle user input
    while(running)
    {
        // Read a key from the user
        var input = Console.ReadKey(true);

        // If the key is Spacebar
        if (input.Key == ConsoleKey.Spacebar)
        {
            Console.Write(' ');
            prefix = "";
            sb.Append(' ');
            continue;
        } 
        // If the key is Backspace and the cursor is not at the start
        else if (input.Key == ConsoleKey.Backspace && Console.CursorLeft > 0)
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

            sb.Remove(sb.Length - 1, 1);
            prefix = sb.ToString().Split(' ').Last();
        }
        // If the key is Enter
        else if (input.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            running = false;
            continue;
        }
        // If the key is Tab and the prefix length is greater than 1
        else if (input.Key == ConsoleKey.Tab && prefix.Length > 1)
        {
            string previousWord = sb.ToString().Split(' ').Last();

            if (words != null) {
                if (!previousWord.Equals(words[wordsIndex - 1]))
                {
                    words = dictionary.AutoSuggest(prefix);
                    wordsIndex = 0;
                }
            } 
            else {
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
        // If the key is not Tab
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
void PrintTrie(Trie trie)
{
    // Inform the user about the contents of the Trie
    Console.WriteLine("The dictionary contains the following words:");
    // Get all words from the Trie
    //List<string> words = trie.GetAllWords();
    // Print each word
    foreach (string word in words)
    {
        Console.Write($"{word}, ");
    }
    Console.WriteLine();
}