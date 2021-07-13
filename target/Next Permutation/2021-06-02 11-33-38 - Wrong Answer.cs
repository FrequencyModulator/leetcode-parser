/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/501939731/
Submission DateTime: June 2, 2021 11:33:38 AM
*/
public class Solution {
    public void NextPermutation(int[] nums) {
      int i = nums.Length - 2;
      while(i >=0 && nums[i] >= nums[i + 1])
        i--;
        
      
      if(i >= 0)
      {        
        int j = nums.Length - 1;
        while(i <= j && nums[j] < nums[i])
          j--;
        int t = nums[i];
        nums[i] = nums[j];
        nums[j] = t;
        Array.Reverse(nums, i + 1, nums.Length - i - 1);
      }
      else
        Array.Reverse(nums);
          
      
    }
}
