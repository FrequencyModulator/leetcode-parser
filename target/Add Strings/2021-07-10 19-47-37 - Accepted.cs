/*
Status: Accepted
Runtime: 84 ms
Memory: 25.8 MB
URL: http://leetcode.com/submissions/detail/520509117/
Submission DateTime: July 10, 2021 7:47:37 PM
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
        int carry = 0;
        var r = new Stack<char>();                 
      
        int p1 = num1.Length -1;
        int p2 = num2.Length -1;
        
        while(p1 >= 0 || p2 >= 0 || carry != 0)
        {
          int n1 = p1 >= 0 ? num1[p1] - '0' : 0;
          int n2 = p2 >= 0 ? num2[p2] - '0' : 0;
          
          int s = (n1 + n2 + carry) % 10;
          carry = (n1 + n2 + carry) / 10;          
          r.Push((char)('0' +  s));
          
          p1--;
          p2--;
          
        }              
                
        return String.Concat(r);
    }
}
