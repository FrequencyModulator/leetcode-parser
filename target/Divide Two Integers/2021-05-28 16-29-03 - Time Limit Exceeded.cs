/*
Status: Time Limit Exceeded
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/499579647/
Submission DateTime: May 28, 2021 4:29:03 PM
*/
public class Solution {
    public int Divide(int dividend, int divisor) {
      
      if(divisor == 0)
        return 1;
      
      if(divisor == 1)
        return dividend;
      
      // special case to avoid overflow
      if(dividend == int.MinValue && divisor == -1)
        return int.MaxValue;
      
      // convert positive to negative because of avoid overflow  when
      // divedend = (2^31 - 1) and divisor = -1 -> (2^31 - 1) - (-1) -> overflow
      // so will be: -(2^31 - 1) - (-1) == -2^32 -> no overflow      
      int negatives = 2;
      if(dividend > 0)
      {
        negatives--;
        dividend = -dividend;
      }
      if(divisor > 0)
      {
        negatives--;
        divisor = -divisor;
      }
      
      int result = 0;
      while(dividend - divisor <= 0) // <= 0 because dividend and divisor are always < 0
      {
        result++;
        dividend -= divisor;
      }
      
      /* If there was originally one negative sign, then
      * the quotient switch to negative. */
      if(negatives == 1)
        return -result;
      
      return result;
      
    }
}
