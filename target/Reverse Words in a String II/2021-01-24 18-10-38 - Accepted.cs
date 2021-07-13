/*
Status: Accepted
Runtime: 384 ms
Memory: 37.4 MB
URL: http://leetcode.com/submissions/detail/447366802/
Submission DateTime: January 24, 2021 6:10:38 PM
*/
public class Solution {
    public void ReverseWords(char[] s) {
        Array.Reverse(s);
        int b = 0;
        for(int e = 0; e < s.Length; e++)
        {
          if(s[e] == ' ' && s[b] != ' ')
          {
            Array.Reverse(s, b, e - b);
            b = e + 1;            
          }                    
        }
        Array.Reverse(s, b, s.Length - b);
    }
}
