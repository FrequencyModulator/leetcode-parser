/*
Status: Accepted
Runtime: 372 ms
Memory: 37.6 MB
URL: http://leetcode.com/submissions/detail/447368918/
Submission DateTime: January 24, 2021 6:16:53 PM
*/
public class Solution {
    public void ReverseWords(char[] s) {
        ReverseString(s, 0, s.Length);
        int b = 0;
        for(int e = 0; e < s.Length; e++)
        {
          if(s[e] == ' ' && s[b] != ' ')
          {
            ReverseString(s, b, e - b);
            b = e + 1;            
          }                    
        }
        ReverseString(s, b, s.Length - b);
    }
  
    private static void ReverseString(char[] s, int b, int l) {        
        int e = b + l - 1;
        char t = 'a';
        while(b < e)
        {
          t = s[b];
          s[b++] = s[e];
          s[e--] = t;          
        }
    }
}
