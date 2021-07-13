/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/500068312/
Submission DateTime: May 29, 2021 6:01:24 PM
*/
public class Solution {
    public bool CheckSubarraySum(int[] nums, int k) 
    {
      // slide window algo.
      int l = 0;
      int r = 0;
      int sum = 0;
      while(r < nums.Length)
      {                
        sum += nums[r];        
        if(sum > k)       
        {
          sum -= nums[l];
          l++;        
        }
        
          
        if(sum == k)
          return true;
        r++;
      }
      return false;
    }
}
