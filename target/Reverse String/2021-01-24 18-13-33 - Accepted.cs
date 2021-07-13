/*
Status: Accepted
Runtime: 344 ms
Memory: 35.6 MB
URL: http://leetcode.com/submissions/detail/447367799/
Submission DateTime: January 24, 2021 6:13:33 PM
*/
public class Solution {
    public void ReverseString(char[] s) {
        int b=0;
        int e = s.Length - 1;
        char t = 'a';
        while(b < e)
        {
          t = s[b];
          s[b++] = s[e];
          s[e--] = t;          
        }
    }
}
