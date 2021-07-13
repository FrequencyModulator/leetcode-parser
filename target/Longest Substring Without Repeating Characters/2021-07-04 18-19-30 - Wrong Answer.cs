/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/517454001/
Submission DateTime: July 4, 2021 6:19:30 PM
*/
public class Solution {
    public int LengthOfLongestSubstring(string s) 
    {
      int max = 0;
      int subStringBegin = 0;      
      var distincts = new Dictionary<char, int>();
      for(int subStringEnd = 0; subStringEnd < s.Length; subStringEnd++)
      {
        var c = s[subStringEnd];
        if(distincts.ContainsKey(c))
          subStringBegin = distincts[c] + 1;
        distincts[c] = subStringEnd;
        max = Math.Max(max, subStringEnd - subStringBegin + 1);
      }
      
      return max;
    }
}
