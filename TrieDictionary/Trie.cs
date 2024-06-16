public class TrieNode
{
    /// <summary>
    /// Represents a node in the Trie data structure.
    /// </summary>
    public class TrieNode
    {
        /// <summary>
        /// Gets or sets the children nodes of the current node.
        /// </summary>
        public Dictionary<char, TrieNode> Children { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current node represents the end of a word.
        /// </summary>
        public bool IsEndOfWord { get; set; }

        /// <summary>
        /// Gets or sets the character value of the current node.
        /// </summary>
        private char _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrieNode"/> class with the specified character value.
        /// </summary>
        /// <param name="value">The character value of the node.</param>
        public TrieNode(char value = ' ')
        {
            Children = new Dictionary<char, TrieNode>();
            IsEndOfWord = false;
            _value = value;
        }

        /// <summary>
        /// Determines whether the current node has a child node with the specified character.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns><c>true</c> if the current node has a child node with the specified character; otherwise, <c>false</c>.</returns>
        public bool HasChild(char c)
        {
            return Children.ContainsKey(c);
        }
    }
}

// Search for a word in the trie
public bool Search(string word) 
{
    TrieNode current = root;

    // For each character in the word
    foreach (char c in word)
    {
        // If the current node does not have the character as a child
        if (!current.HasChild(c))
        {
            // The word does not exist in the trie
            return false;
        }
        // Move to the child node
        current = current.Children[c];
    }
    // Return true if the current node represents the end of a word
    return current.IsEndOfWord;
}

public class Trie
{
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    public bool Insert(string word)
    {
        TrieNode current = root;


        // Fro each character in the word
        foreach (char c in word)
        {
            // If the current node does not have the character as a child
            if (!current.HasChild(c))
            {
                // Add the character as a child
                current.Children[c] = new TrieNode(c);
            }
            // Move to the child node
            current = current.Children[c];
        }
        if (current.IsEndOfWord)
        {
            // Word already exists in the trie
            return false;
        }
        // Mark the end of the word
        current.IsEndOfWord = true;
        return true;
    }
    
    public List<string> AutoSuggest(string prefix)
    {
        TrieNode currentNode = root;
        foreach (char c in prefix)
        {
            if (!currentNode.HasChild(c))
            {
                return new List<string>();
            }
            currentNode = currentNode.Children[c];
        }
        return GetAllWordsWithPrefix(currentNode, prefix);
    }

    private List<string> GetAllWordsWithPrefix(TrieNode root, string prefix)
    {
        List<string> words = new List<string>();
        if (node.IsEndOfWord)
        {
            words.Add(prefix);
        }

        foreach (var child in root.Children)
        {
            words.AddRange(GetAllWordsWithPrefix(child.Value, prefix + child.Key));
        }

        return words;
    }

    public List<string> GetAllWords()
    {
        return GetAllWordsWithPrefix(root, "");
    }

    public void PrintTrieStructure()
    {
        Console.WriteLine("\nroot");
        _printTrieNodes(root);
    }

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

        foreach(var child in children)
        {
            i++;
            bool isLast = i == childCount;
            _printTrieNodes(child.Value, format, isLast);
        }
    }

    public List<string> GetSpellingSuggestions(string word)
    {
        char firstLetter = word[0];
        List<string> suggestions = new();
        List<string> words = GetAllWordsWithPrefix(root.Children[firstLetter], firstLetter.ToString());
        
        foreach (string w in words)
        {
            int distance = LevenshteinDistance(word, w);
            if (distance <= 2)
            {
                suggestions.Add(w);
            }
        }

        return suggestions;
    }

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
                d[i, j] = Math.Min(Math.Min(d[i, j] + 1, d[i, j] + 1), d[i, j] + cost);
            }
        }

        return d[m, n];
    }
}