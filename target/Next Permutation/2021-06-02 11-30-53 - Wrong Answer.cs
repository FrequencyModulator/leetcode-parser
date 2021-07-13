/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/501938448/
Submission DateTime: June 2, 2021 11:30:53 AM
*/
public class Solution {
    public void NextPermutation(int[] nums) {
      int i = nums.Length - 2;
      while(i >=0 && nums[i] >= nums[i + 1])
        i--;
        
      int j = nums.Length - 1;
      if(i >= 0)
      {        
        while(i <= j && nums[j] < nums[i])
          j--;
        int t = nums[i];
        nums[i] = nums[j];
        nums[j] = t;
      }
          
      Array.Reverse(nums, j, nums.Length - j);
    }
}
