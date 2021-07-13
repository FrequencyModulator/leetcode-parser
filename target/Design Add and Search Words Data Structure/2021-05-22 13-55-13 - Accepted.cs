/*
Status: Accepted
Runtime: 224 ms
Memory: 44.6 MB
URL: http://leetcode.com/submissions/detail/496748867/
Submission DateTime: May 22, 2021 1:55:13 PM
*/
public class WordDictionary {
    
    internal class TrieNode
    {
      public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
      public bool Word = false;
    }
    
  
    private TrieNode root = new TrieNode();
    /** Initialize your data structure here. */
    public WordDictionary() {
        
    }
    
    public void AddWord(string word) {
        var node = root;
        foreach(var c in word)
        {
          if(!node.Children.ContainsKey(c))
            node.Children.Add(c, new TrieNode());
          node = node.Children[c]; // next level
        }
        node.Word = true;
    }
  
    public bool Search(string word) => Search(word, root, 0);
    
    private static bool Search(string word, TrieNode node, int b) {        
        for(int i = b; i < word.Length; i++)
        {
          char c = word[i];
          if(c == '.')
          {            
            // check all current level
            foreach(var p in node.Children)
            {
              if(Search(word, p.Value, i + 1))
                return true;
            }
            return false;
            
          }
          else
          {
            if (!node.Children.ContainsKey(c))
              return false;
            node = node.Children[c];
          }
        }
      
        return node.Word;
    }
}

/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary obj = new WordDictionary();
 * obj.AddWord(word);
 * bool param_2 = obj.Search(word);
 */
