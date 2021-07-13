/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/478647770/
Submission DateTime: April 9, 2021 5:17:45 PM
*/
public class Solution {
    public bool WordBreak(string s, IList<string> wordDict) {
      var wordSet = new HashSet<string>(wordDict);
      int b = 0;          
      for (int e = 0; e < s.Length; e++)        
      {
        var sub = s.Substring(b, e - b + 1);
        if(wordSet.Contains(sub))          
        {
          e++;
          b = e;
        }
      }
      return b == s.Length;
    }
}
