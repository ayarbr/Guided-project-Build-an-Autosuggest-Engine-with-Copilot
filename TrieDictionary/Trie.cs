public class TrieNode
{
    // Dictionary to store the children nodes
    public Dictionary<char, TrieNode> Children { get; set; }
    // Boolean to indicate if the node marks the end of a word
    public bool IsEndOfWord { get; set; }

    // Character value of the node
    public char _value;

    // Constructor to initialize a TrieNode
    public TrieNode(char value = ' ')
    {
        // Initialize the children dictionary
        Children = new Dictionary<char, TrieNode>();
        // Set the end of word flag to false
        IsEndOfWord = false;
        // Set the character value
        _value = value;
    }

    // Method to check if a node has a child with a specific character
    public bool HasChild(char c)
    {
        // Return true if the children dictionary contains the character key
        return Children.ContainsKey(c);
    }
}

public class Trie
{
    // Root node of the Trie
    private TrieNode root;

    // Constructor to initialize the Trie
    public Trie()
    {
        // Initialize the root node with a space character
        root = new TrieNode();
    }

    // Returns a list of words that have the given prefix
    /// <summary>
    /// Generates a list of suggested words based on a given prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>A list of suggested words.</returns>
    /*public List<string> AutoSuggest(string prefix)
    {
        TrieNode currentNode = root;
        // Traverse the trie until the prefix is reached
        foreach (char c in prefix)
        {
            // If the character is not in the children of the current node, return an empty list
            if (!currentNode.HasChild(c))
            {
                return new List<string>();
            }
            // Move to the next node
            currentNode = currentNode.Children[c];
        }
        // Get all words with the given prefix starting from the current node
                return GetAllWordsWithPrefix(currentNode, prefix);
    }*/

    // Method to insert a word into the Trie
    public bool Insert(string word)
    {
        // Start from the root node
        TrieNode current = root;
        // For each character in the word
        foreach (char c in word)
        {
            // If the character is not in the children of the current node
            if (!current.HasChild(c))
            {
                // Add the character to the children of the current node
                // Create a new TrieNode with the character 'c'
                current.Children[c] = new TrieNode(c);
            }
            // Move to the next node
            current = current.Children[c];
        }
        // Mark the last node as the end of a word
        // If the current node is already marked as the end of a word, return false
        if (current.IsEndOfWord)
        {
            return false;
        }
        // Otherwise, mark it as the end of a word and return true
        current.IsEndOfWord = true;
        return true;
    }
}

    namespace TrieDictionary
    {
        public class TrieNode
        {
            // Dictionary to store the children nodes
            public Dictionary<char, TrieNode> Children { get; set; }
            // Boolean to indicate if the node marks the end of a word
            public bool IsEndOfWord { get; set; }
    
            // Character value of the node
            public char _value;
    
            // Constructor to initialize a TrieNode
            public TrieNode(char value = ' ')
            {
                // Initialize the children dictionary
                Children = new Dictionary<char, TrieNode>();
                // Set the end of word flag to false
                IsEndOfWord = false;
                // Set the character value
                _value = value;
            }
    
            // Method to check if a node has a child with a specific character
            public bool HasChild(char c)
            {
                // Return true if the children dictionary contains the character key
                return Children.ContainsKey(c);
            }
        }
    
        public class Trie
        {
            // Root node of the Trie
            private TrieNode root;
    
            // Constructor to initialize the Trie
            public Trie()
            {
                // Initialize the root node with a space character
                root = new TrieNode();
            }
    
            // Returns a list of words that have the given prefix
            /// <summary>
            /// Generates a list of suggested words based on a given prefix.
            /// </summary>
            /// <param name="prefix">The prefix to search for.</param>
            /// <returns>A list of suggested words.</returns>
            public List<string> AutoSuggest(string prefix)
            {
                TrieNode currentNode = root;
                // Traverse the trie until the prefix is reached
                foreach (char c in prefix)
                {
                    // If the character is not in the children of the current node, return an empty list
                    if (!currentNode.HasChild(c))
                    {
                        return new List<string>();
                    }
                    // Move to the next node
                    currentNode = currentNode.Children[c];
                }
                // Get all words with the given prefix starting from the current node
                    return GetAllWordsWithPrefix(currentNode, prefix);
            }
    
            // Method to insert a word into the Trie
            public bool Insert(string word)
            {
                // Start from the root node
                TrieNode current = root;
                // For each character in the word
                foreach (char c in word)
                {
                    // If the character is not in the children of the current node
                    if (!current.HasChild(c))
                    {
                        // Add the character to the children of the current node
                        // Create a new TrieNode with the character 'c'
                        current.Children[c] = new TrieNode(c);
                    }
                    // Move to the next node
                    current = current.Children[c];
                }
                // Mark the last node as the end of a word
                // If the current node is already marked as the end of a word, return false
                if (current.IsEndOfWord)
                {
                    return false;
                }
                // Otherwise, mark it as the end of a word and return true
                current.IsEndOfWord = true;
                return true;
            }
    
            // Returns a list of all words in the trie
            public List<string> GetAllWordsWithPrefix(TrieNode node, string prefix)
            {
                List<string> words = new();
                // If the current node marks the end of a word, add the prefix to the list
                if (node.IsEndOfWord)
                {
                    words.Add(prefix);
                }
                // Recursively get all words with the prefix
                foreach (var child in node.Children)
                {
                    words.AddRange(GetAllWordsWithPrefix(child.Value, prefix + child.Key));
                }
                return words;
            }
    
            // Prints the structure of the trie
            public void PrintTrieStructure()
            {
                Console.WriteLine("\nroot");
                _printTrieNodes(root);
            }
    
            // Recursive helper method to print the trie nodes
            private void _printTrieNodes(TrieNode root, string format = " ", bool isLastChild = true)
            {
                if (root == null)
                    return;
    
                Console.Write($"{format}");
    
                if (isLastChild)
                {
                    Console.Write("└─");
                    format += "  ";
                }
                else
                {
                    Console.Write("├─");
                    format += "│ ";
                }
    
                Console.WriteLine($"{root._value}");
    
                int childCount = root.Children.Count;
                int i = 0;
                var children = root.Children.OrderBy(x => x.Key);
    
                foreach (var child in children)
                {
                    i++;
                    bool isLast = i == childCount;
                    _printTrieNodes(child.Value, format, isLast);
                }
            }
    
            // Returns a list of spelling suggestions for the given word
            public List<string> GetSpellingSuggestions(string word)
            {
                char firstLetter = word[0];
                List<string> suggestions = new();
                // Get all words with the same first letter as the given word
                List<string> words = GetAllWordsWithPrefix(root.Children[firstLetter], firstLetter.ToString());
    
                foreach (string w in words)
                {
                    // Calculate the Levenshtein distance between the given word and each word in the trie
                    int distance = LevenshteinDistance(word, w);
                    // If the distance is less than or equal to 2, add the word to the suggestions list
                    if (distance <= 2)
                    {
                        suggestions.Add(w);
                    }
                }
    
                return suggestions;
            }
    
            // Calculates the Levenshtein distance between two strings
            private int LevenshteinDistance(string s, string t)
            {
                int m = s.Length;
                int n = t.Length;
                int[,] d = new int[m, n];
    
                if (m == 0)
                {
                    return n;
                }
    
                if (n == 0)
                {
                    return m;
                }
    
                for (int i = 0; i <= m; i++)
                {
                    d[i, 0] = i;
                }
    
                for (int j = 0; j <= n; j++)
                {
                    d[0, j] = j;
                }
    
                for (int j = 0; j <= n; j++)
                {
                    for (int i = 0; i <= m; i++)
                    {
                        int cost = (s[i] == t[j]) ? 0 : 1;
                        d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                    }
                }
    
                return d[m, n];
            }
        }
    }