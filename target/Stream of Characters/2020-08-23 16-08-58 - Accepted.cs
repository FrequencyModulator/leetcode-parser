/*
Status: Accepted
Runtime: 1052 ms
Memory: 74.3 MB
URL: http://leetcode.com/submissions/detail/385322071/
Submission DateTime: August 23, 2020 4:08:58 PM
*/
public class StreamChecker 
{
  TrieNode trie;
  List<char> buffer = new List<char>();
  public StreamChecker(string[] words) 
  {
    trie = BuildTrie(words);
  }

  public bool Query(char letter) 
  {
    buffer.Add(letter);
    
    var cur = trie;
    for (var i = buffer.Count - 1; i >= 0; i--)
    {
      var index = buffer[i] - 'a';
      if (cur.Children[index] == null)
        return false;
      
      if(cur.Children[index].IsWord)
        return true;
      
      cur = cur.Children[index];
    }
    return false;
  }
  
  private static TrieNode BuildTrie(string[] words)
  {
    var trie = new TrieNode();
    foreach (var w in words)
    {
      var cur = trie;
      for (var i = w.Length - 1; i >= 0; i--)
      {
        var index = w[i] - 'a';
        cur.Children[index] ??= new TrieNode();
        cur = cur.Children[index];
      }
      
      cur.IsWord = true;
    }
    
    return trie;
  }
  
  private class TrieNode
  {
    public TrieNode[] Children = new TrieNode[26];
    public bool IsWord;
  }
}

/**
 * Your StreamChecker object will be instantiated and called as such:
 * StreamChecker obj = new StreamChecker(words);
 * bool param_1 = obj.Query(letter);
 */
