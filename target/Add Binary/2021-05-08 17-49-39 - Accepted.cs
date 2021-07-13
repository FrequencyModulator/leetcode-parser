/*
Status: Accepted
Runtime: 80 ms
Memory: 25.7 MB
URL: http://leetcode.com/submissions/detail/490574837/
Submission DateTime: May 8, 2021 5:49:39 PM
*/
public class Solution {
    public string AddBinary(string a, string b) {
        var p1 = a.Length - 1;
        var p2 = b.Length - 1;
        
        var sb = new StringBuilder();
        int carry = 0;
        while(p1 >= 0 || p2 >= 0)
        {
          int n1 = p1 >= 0 ? a[p1] - '0' : 0;
          int n2 = p2 >= 0 ? b[p2] - '0' : 0;          
          int v = (n1 + n2 + carry) % 2;
          carry = (n1 + n2 + carry) / 2;
          sb.Insert(0, v);
          p1--;
          p2--;
        }
        if (carry != 0)
          sb.Insert(0, carry);
        return sb.ToString();
      }    
}
