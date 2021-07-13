/*
Status: Accepted
Runtime: 80 ms
Memory: 26 MB
URL: http://leetcode.com/submissions/detail/516894656/
Submission DateTime: July 3, 2021 3:15:31 PM
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
          subStringBegin = Math.Max(subStringBegin, distincts[c] + 1);
        distincts[c] = subStringEnd;
        max = Math.Max(max, subStringEnd - subStringBegin + 1);
      }
      
      return max;
    }
}
