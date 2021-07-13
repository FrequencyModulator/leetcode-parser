/*
Status: Accepted
Runtime: 120 ms
Memory: 24.9 MB
URL: http://leetcode.com/submissions/detail/520977513/
Submission DateTime: July 11, 2021 4:51:52 PM
*/
public class Solution {
    public bool IsPalindrome(string s) 
    {
      int l = 0;    
      int r = s.Length - 1;
      while(l < r)
      {
        if(!char.IsLetterOrDigit(s[l])) 
        {
          l++;
          continue;
        }
        if(!char.IsLetterOrDigit(s[r]))
        {
          r--;
          continue;
        }
        
        if(!char.ToLowerInvariant(s[l]).Equals(char.ToLowerInvariant(s[r])))
          return false;        
        l++;
        r--;
      }
      
      return true;
    }
}
