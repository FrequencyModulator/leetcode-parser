/*
Status: Accepted
Runtime: 192 ms
Memory: 28.3 MB
URL: http://leetcode.com/submissions/detail/519953044/
Submission DateTime: July 9, 2021 3:39:35 PM
*/
public class Solution {
    public bool WordBreak(string s, IList<string> wordDict) {
      // sliding window
      var wordSet = new HashSet<string>(wordDict);
      var memo = new Dictionary<int, bool>();
      return WordBreak(s, wordSet, 0, memo);
    }
  
    private bool WordBreak(string s, HashSet<string> wordSet, int start,  Dictionary<int, bool> memo)
    {
      if(start >= s.Length)
        return true;
      
      if(memo.ContainsKey(start))
        return memo[start];
      
      for(int end = start; end < s.Length; end++)
      {
        if(wordSet.Contains(s.Substring(start, end - start + 1)) && WordBreak(s, wordSet, end + 1, memo))
        {
          memo[start] = true;
          return true;
        }
      }
      memo[start] = false;
      return false;
    }
}
