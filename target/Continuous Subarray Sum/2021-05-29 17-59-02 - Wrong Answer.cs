/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/500067677/
Submission DateTime: May 29, 2021 5:59:02 PM
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
        sum = nums[l] + nums[r];
        if(sum == k)
          return true;
        if(sum > k)
          l++;        
        r++;
      }
      return false;
    }
}
