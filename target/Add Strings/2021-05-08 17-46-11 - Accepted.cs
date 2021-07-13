/*
Status: Accepted
Runtime: 100 ms
Memory: 26.4 MB
URL: http://leetcode.com/submissions/detail/490573821/
Submission DateTime: May 8, 2021 5:46:11 PM
*/
public class Solution {
    public string AddStrings(string num1, string num2) {
        var p1 = num1.Length - 1;
        var p2 = num2.Length - 1;
        
        var sb = new StringBuilder();
        int carry = 0;
        while(p1 >= 0 || p2 >= 0)
        {
          int n1 = p1 >= 0 ? num1[p1] - '0' : 0;
          int n2 = p2 >= 0 ? num2[p2] - '0' : 0;
          int v = (n1 + n2 + carry) % 10;
          carry = (n1 + n2 + carry) / 10;
          sb.Insert(0, v);
          p1--;
          p2--;
        }
        if (carry != 0)
          sb.Insert(0, carry);
        return sb.ToString();
    }
}
