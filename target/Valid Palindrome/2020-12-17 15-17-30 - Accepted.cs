/*
Status: Accepted
Runtime: 84 ms
Memory: 24.7 MB
URL: http://leetcode.com/submissions/detail/431738300/
Submission DateTime: December 17, 2020 3:17:30 PM
*/
public class Solution {
    public bool IsPalindrome(string s) {
      var i = 0;
      var j = s.Length - 1;            
      while(true)
      {          
          if(j - i < 1)
            return true;
          if(!char.IsLetterOrDigit(s[i]))
          {
            i++;
            continue;
          }
          if(!char.IsLetterOrDigit(s[j]))
          {
            j--;
            continue;
          }
          if(char.ToLowerInvariant(s[i]) != char.ToLowerInvariant(s[j]))
            return false;
          i++;
          j--;
      }
    }    
}
