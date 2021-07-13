/*
Status: Compile Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/431733449/
Submission DateTime: December 17, 2020 2:58:56 PM
*/
public class Solution {
    public bool IsPalindrome(string s) {
      var i = 0;
      var j = s.Length - 1;      
      while(true)
      {
          if(j - i <= 1)
            return true;
          if(!Char.IsLetterOrDigitSample(s[i]))
          {
            i++;
            continue;
          }
          if(!Char.IsLetterOrDigitSample(s[j]))
          {
            j++;
            continue;
          }
          if(char.ToLowerInvariant(s[i]) != char.ToLowerInvariant(s[j]))
            return false;
          i++;
          j++;
      }
    }
}
