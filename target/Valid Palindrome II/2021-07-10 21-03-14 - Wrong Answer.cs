/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/520527898/
Submission DateTime: July 10, 2021 9:03:14 PM
*/
public class Solution {
    public bool ValidPalindrome(string s) 
    {
      int l = 0; int r = s.Length - 1;  
      while(l < r)
      {
        if(s[l] != s[r])
          return IsPalindrom(s.Substring(l, r - l - 1)) ||  IsPalindrom(s.Substring(l + 1, r - l));
        l++;
        r--;
      }
      return true;
    }
  
    private bool IsPalindrom(string s)
    {
      int l = 0; int r = s.Length - 1;  
      while(l < r)
      {
        if(s[l] != s[r])
          return false;
        l++;
        r--;
      }
      return true;
    }
}
