/*
Status: Accepted
Runtime: 84 ms
Memory: 24 MB
URL: http://leetcode.com/submissions/detail/446420112/
Submission DateTime: January 22, 2021 2:13:58 PM
*/
public class Solution {
    public string ReverseWords(string s) {
      
      // Skip all leading and trailing white-space 
      int b = 0;
      int e = s.Length - 1;
      while(s[b] == ' ') b++;
      while(s[e] == ' ') e--;
      
      // replace multi space with single space
      char[] a = new char[e - b + 1];
      char lc=(char)0;
      int j = 0; // will be a last possition in array we neet to process
      for(int i=b; i < e + 1; i++)
      {
        if(lc == ' ' && lc == s[i]) 
          continue;
        lc = s[i];
        a[j++] = lc;         
      }            
                  
      Array.Reverse(a, 0, j);
            
      int wb = 0;
      int we = 0;      
      while(we < j)
      {
        if(a[we] == ' ')
        {
          if(wb < we)
          {
            Array.Reverse(a, wb, we - wb);
          }
          wb = ++we;
        }
        we++;
      }
      if(wb < we)
      {
        Array.Reverse(a, wb, we - wb);
      }
          
      return new string(a, 0, j);
    }
}
