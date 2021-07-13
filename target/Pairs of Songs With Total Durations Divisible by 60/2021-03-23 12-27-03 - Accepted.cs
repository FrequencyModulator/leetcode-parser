/*
Status: Accepted
Runtime: 124 ms
Memory: 32.9 MB
URL: http://leetcode.com/submissions/detail/471493409/
Submission DateTime: March 23, 2021 12:27:03 PM
*/
public class Solution {
    public int NumPairsDivisibleBy60(int[] time) {
      if(time == null || time.Length == 0)
        return 0;
      
      const int total = 60;
      int count = 0;
      int[] reminders = new int[total];
      foreach (var t in time)
      {
        if(t % total == 0)
          count += reminders[0];
        else
          count += reminders[total - t % total];
        
        reminders[t % total]++;          
      }
      
      return count;
    }
}
