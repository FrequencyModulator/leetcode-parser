/*
Status: Accepted
Runtime: 92 ms
Memory: 26.3 MB
URL: http://leetcode.com/submissions/detail/520506688/
Submission DateTime: July 10, 2021 7:38:50 PM
*/
/*
   156 
  1234
   390
i   ^  
carry = 0   
*/
   

public class Solution {
    public string AddStrings(string num1, string num2) 
    {
        int l = Math.Max(num1.Length, num2.Length);
        int carry = 0;
        var r = new Stack<char>();
        for(int e = 0; e < l; e++)
        {
          int n1 = 0;
          if(e < num1.Length)
            n1 = num1[num1.Length - e -1] - '0';
          int n2 = 0;
          if(e < num2.Length)
            n2 = num2[num2.Length - e -1] - '0';
          
          int s = (n1 + n2 + carry) % 10;
          carry = (n1 + n2 + carry) / 10;
          
          r.Push((char)('0' +  s));
        }
        if(carry == 1)
          r.Push((char)('0' +  carry));
        
        return String.Concat(r);
    }
}
