/*
Status: Accepted
Runtime: 56 ms
Memory: 15.8 MB
URL: http://leetcode.com/submissions/detail/499213970/
Submission DateTime: May 27, 2021 8:44:56 PM
*/
public class Solution {
    public double MyPow(double x, int n) => n < 0 ? FastPow(1/x, -n) : FastPow(x, n);
  
    private double FastPow(double x, int n)
    {
      if(n == 0)
        return 1;
      var mid = FastPow(x, n / 2);
      if(n % 2 == 0)
        return mid * mid;
      else
        return mid * mid * x;
    }
    
}
